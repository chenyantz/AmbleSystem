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
        public BuyerManagerRfqView(int rfqId)
        {
            this.rfqId = rfqId;
            InitializeComponent();
            this.Text = "Info for RFQ:" + rfqId;
        }

        private void BuyerManagerRfqView_Load(object sender, EventArgs e)
        {
            Rfq rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
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
            Rfq rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
            if ((!rfq.firstPA.HasValue) && (!rfq.secondPA.HasValue))
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
            }
            else
            {
                MessageBox.Show("Fail to assign the RFQ");
            }

            this.Close();

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
