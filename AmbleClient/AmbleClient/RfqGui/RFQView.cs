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
    public partial class RFQView : Form
    {
        int rfqId;
        Rfq rfq;
        public RFQView(int rfqId)
        {
            InitializeComponent();
            this.rfqId = rfqId;
            this.Text = "Info For RFQ:" + rfqId;
        
        }

        private void RFQView_Load(object sender, EventArgs e)
        {
            rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            rfqItems1.FillTheTable(rfq);
            GuiOpAccordingToRfqState((RfqStatesEnum)rfq.rfqStates);
        }

        private void GuiOpAccordingToRfqState(RfqStatesEnum rfqState)
        {
            switch (rfqState)
            { 
                case RfqStatesEnum.New:
                      tsbQuote.Enabled = false;
                      tsbSo.Enabled = false;
                      tsbViewSo.Enabled = false;
                      tsbOfferView.Enabled = false;
                       break;
                case RfqStatesEnum.Routed:
                       tsbRoute.Enabled = false;
                       tsbQuote.Enabled=false;
                       tsbSo.Enabled = false;
                       tsbViewSo.Enabled = false;
                       tsbOfferView.Enabled = false;
                       break;
                case RfqStatesEnum.Offered:
                       tsbRoute.Enabled = false;
                       tsbSo.Enabled = false;
                       tsbViewSo.Enabled = false;
                       break;
                case RfqStatesEnum.Quoted:
                       tsbRoute.Enabled = false;
                       tsbQuote.Enabled = false;
                       tsbViewSo.Enabled = false;
                       tsbSo.Enabled = true;
                       break;
                case RfqStatesEnum.HasSO:
                       tsbRoute.Enabled = false;
                       tsbQuote.Enabled = false;
                       tsbViewSo.Enabled = true;
                       break;
                case RfqStatesEnum.Closed:
                       tsbRoute.Enabled = false;
                       tsbQuote.Enabled = false;
                       tsbSo.Enabled = false;
                       tsbCloseRfq.Enabled = false;
                       break;
            }
             //在有offer或so的情况下，要重复查看，免得被人家删掉

            if (tsbOfferView.Enabled)
            {

                if (AmbleClient.OfferGui.OfferMgr.OfferMgr.HasOfferByRfq(rfqId))
                {
                    tsbOfferView.Enabled = true;
                }
                else
                {
                    tsbOfferView.Enabled = false;
                }
            }
            if (tsbViewSo.Enabled)
            {
                if (Order.SoMgr.SoMgr.GetSoNumberFromRfqId(rfqId) > 0)
                {
                    tsbViewSo.Enabled = true;
                }
                else
                {
                    tsbViewSo.Enabled = false;
                }
            }
          

        }

        private void tsbQuote_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Quote the RFQ?","", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {

                if (RfqMgr.ChangeRfqState(RfqStatesEnum.Quoted, rfqId))
                {
                    RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId, "Quoted the RFQ");
                    GuiOpAccordingToRfqState(RfqStatesEnum.Quoted);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Quote the RFQ Fail");
                }
            }
            Rfq rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            GuiOpAccordingToRfqState((RfqStatesEnum)rfq.rfqStates);


        }

        private void tsbRoute_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Route the RFQ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (RfqMgr.ChangeRfqState(RfqStatesEnum.Routed, rfqId))
                {
                    RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId, "Routed the RFQ");
                    GuiOpAccordingToRfqState(RfqStatesEnum.Routed);
                    SendRfqRouteEmail();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Route the RFQ Fail");
                }

            }
        }


        private void SendRfqRouteEmail()
        {
            List<string> emailTos = new List<string>();
            emailTos.AddRange(Admin.AccountMgr.AccountMgr.GetEmailsAccordingToJob(JobDescription.Admin));
            emailTos.AddRange(Admin.AccountMgr.AccountMgr.GetEmailsAccordingToJob(JobDescription.Boss));
            string address1 = Admin.AccountMgr.AccountMgr.GetEmailAddressById(UserInfo.UserId);
            if (!emailTos.Contains(address1))
            {
                emailTos.Add(address1);
            }
            string address2 = Admin.AccountMgr.AccountMgr.GetEmailAddressById(Admin.AccountMgr.AccountMgr.GetSuperviserId(UserInfo.UserId));
            if (!emailTos.Contains(address2))
            {
                emailTos.Add(address2);
            }

            string subject = string.Format("The RFQ {0} (MPN {1},sales {2} )has been Routed.", Tool.Get6DigitalNumberAccordingToId(rfq.rfqNo), rfq.partNo, AllAccountInfo.GetNameAccordingToId(rfq.salesId));
            StringBuilder body = new StringBuilder();
            body.Append("<table border=\"0\">");
            body.Append(string.Format("<tr><td>RFQ ID</td><td>{0}</td>",Tool.Get6DigitalNumberAccordingToId(rfq.rfqNo)));
            body.Append(string.Format("<tr><td>Customer Name</td><td>{0}</td>", rfq.customerName));
            body.Append(string.Format("<tr><td>Contact</td><td>{0}</td>", rfq.contact));
            body.Append(string.Format("<tr><td>MPN</td><td>{0}</td>", rfq.partNo));
            body.Append(string.Format("<tr><td>MFG</td><td>{0}</td>", rfq.mfg));
            body.Append(string.Format("<tr><td>DC</td><td>{0}</td>", rfq.dc));
            body.Append(string.Format("<tr><td>CPN</td><td>{0}</td>", rfq.custPartNo));
            body.Append(string.Format("<tr><td>Sales Name</td><td>{0}</td>", AllAccountInfo.GetNameAccordingToId(rfq.salesId)));
            body.Append("</table>");

            AmbleClient.MailService.MailService.SendMail(emailTos, subject, body.ToString());

                   
        }





        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            if (rfqItems1.UpdateInfo(rfqId))
            {
                RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId,"Updated the RFQ");
                MessageBox.Show("Update the RFQ successfully");
            }
            else
            {
                MessageBox.Show("Failed to Update the RFQ");
            }
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            RfqMgr.CopyRfq(rfqId, UserInfo.UserId);
            NewRfq newRfq = new NewRfq(true);
            newRfq.ShowDialog();
        }

        private void tsbSo_Click(object sender, EventArgs e)
        {
            //
            SoItemPicker picker = new SoItemPicker(rfq.customerName,rfqId,(RfqStatesEnum)rfq.rfqStates);
            if (DialogResult.OK == picker.ShowDialog())
            {
                List<int> ids = picker.RfqIdsForSo;
                SO.NewSo newSo = new SO.NewSo(ids);
                if (DialogResult.Yes == newSo.ShowDialog())
                {
                    Rfq rfq1 = RfqMgr.GetRfqAccordingToRfqId(rfqId);
                    GuiOpAccordingToRfqState((RfqStatesEnum)rfq1.rfqStates);
                }
            }



           /*
           newSo.FillContact(this.rfqItems1.tbContact.Text);
           */
        }

        private void tsbViewSo_Click(object sender, EventArgs e)
        {
            SO.SoView soView = new SO.SoView(rfqId);
            soView.ShowDialog();
        }

        private void tsbCloseRfq_Click(object sender, EventArgs e)
        {
            if (rfqItems1.cbCloseReason.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Reason for Closing the RFQ");
                rfqItems1.cbCloseReason.Focus();
            }
            else
            {
                if (MessageBox.Show("Set The RFQ Status to Closed?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    rfqItems1.UpdateInfo(rfqId);
                    RfqMgr.ChangeRfqState(RfqStatesEnum.Closed, rfqId);
                    RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId, "Closed the RFQ");
                    GuiOpAccordingToRfqState(RfqStatesEnum.Closed);
                    this.Close();
                }
            }
            
        }


        private void tsbOfferView_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.OfferView offerView = new OfferGui.OfferView(rfqId);
            offerView.ShowDialog();
        }

    }
}
