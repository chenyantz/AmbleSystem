using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.RfqGui.RfqManager;

namespace AmbleClient.RfqGui
{
    public partial class BuyerManagerRfqView : Form
    {
        int rfqId;
        Rfq rfq;
        public BuyerManagerRfqView(int rfqId)
        {
            this.rfqId = rfqId;
            InitializeComponent();
            this.Text = "Info for RFQ:" + rfqId;
        }

        private void BuyerManagerRfqView_Load(object sender, EventArgs e)
        {
            rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            buyerManagerRfqItems1.FillTheTable(rfq);
            SetMenuStateAccordingToRfqState((RfqStatesEnum)rfq.rfqStates);
        }

        private void SetMenuStateAccordingToRfqState(RfqStatesEnum state)
        {
            switch (state)
            {
                case RfqStatesEnum.New:
                    tsbAssign.Enabled = false;
                    tsbEnterOffer.Enabled = false;
                    tsbViewOffers.Enabled = false;
                    break;
                case RfqStatesEnum.Routed:
                    tsbViewOffers.Enabled = false;
                    break;
                  default:
                    break;
            }
            //在offered情况下，再查看一下，免得被删掉，还有像closed的情况下，无法判断offer的情况
            if(AmbleClient.OfferGui.OfferMgr.OfferMgr.HasOfferByRfq(rfqId))
            {
             tsbViewOffers.Enabled=true;
            }
            else
            {
             tsbViewOffers.Enabled=false;
            }
            Rfq rfq1 = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            if ((!rfq1.firstPA.HasValue) && (!rfq1.secondPA.HasValue))
            {
                tsbEnterOffer.Enabled = false;
            }
            else
            {
                tsbEnterOffer.Enabled = true;
            }
       



        }

        
        private void tsbAssign_Click(object sender, EventArgs e)
        {
            int? primaryPA, altPA;
            if (buyerManagerRfqItems1.cbPrimaryPA.SelectedIndex == -1)
            {
                primaryPA = null;

            }
            else
            {
                primaryPA = buyerManagerRfqItems1.MySubs[buyerManagerRfqItems1.cbPrimaryPA.SelectedIndex];
            }

            if (buyerManagerRfqItems1.cbAltPA.SelectedIndex == -1)
            {
                altPA = null;

            }
            else
            {
                altPA = buyerManagerRfqItems1.MySubs[buyerManagerRfqItems1.cbAltPA.SelectedIndex];
            }
            if (primaryPA == null && altPA == null)
            {
                MessageBox.Show("Please choose the primary P/A or Alt P/A in the form");
                return;
            }

            if (RfqMgr.AssignPAForRfq(rfqId, primaryPA, altPA))
            {
                MessageBox.Show("Assign the RFQ to Buyer(s) successfully");
                SendRfqAssignEmail(primaryPA, altPA);
            }
            else
            {
                MessageBox.Show("Fail to assign the RFQ");
            }

            this.Close();

        }

        private void SendRfqAssignEmail(int? buyer1, int? buyer2)
        {
            List<string> emailTos = new List<string>();
            List<string> ccTos = new List<string>();
            if (buyer1 != null)
            {
                emailTos.Add(Admin.AccountMgr.AccountMgr.GetEmailAddressById(buyer1.Value));
            }
            if (buyer2 != null)
            {
                string email2 = Admin.AccountMgr.AccountMgr.GetEmailAddressById(buyer2.Value);
                if (!emailTos.Contains(email2))
                {
                    emailTos.Add(email2);
                }

            }
            string email3 = Admin.AccountMgr.AccountMgr.GetEmailAddressById(UserInfo.UserId);
            {
                if(!emailTos.Contains(email3))
                {
                ccTos.Add(email3);
                }
            }

            string subject = string.Format("The RFQ {0} (MPN：{1})has been Assigned to you.", Tool.Get6DigitalNumberAccordingToId(rfq.rfqNo),rfq.partNo);
            StringBuilder body = new StringBuilder();
            body.Append("<table border=\"0\">");
            body.Append(string.Format("<tr><td>RFQ ID</td><td>{0}</td>", Tool.Get6DigitalNumberAccordingToId(rfq.rfqNo)));
            body.Append(string.Format("<tr><td>MPN</td><td>{0}</td>", rfq.partNo));
            body.Append(string.Format("<tr><td>MFG</td><td>{0}</td>", rfq.mfg));
            body.Append(string.Format("<tr><td>DC</td><td>{0}</td>", rfq.dc));
            body.Append("</table>");

            AmbleClient.MailService.MailService.SendMail(emailTos,ccTos, subject, body.ToString());


        }


        private void tsbOffer_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.NewOffer newOffer = new OfferGui.NewOffer(rfqId);
            newOffer.NewOfferAutoFill(this.buyerManagerRfqItems1.tbPartNo.Text, this.buyerManagerRfqItems1.tbMfg.Text);
            newOffer.ShowDialog();
            Rfq rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            SetMenuStateAccordingToRfqState((RfqStatesEnum)rfq.rfqStates);

        }

        private void tsbViewOffers_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.OfferView offerList = new OfferGui.OfferView(rfqId);
            offerList.ShowDialog();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
