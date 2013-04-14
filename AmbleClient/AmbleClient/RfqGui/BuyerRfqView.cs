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
    public partial class BuyerRfqView : Form
    {
        int rfqId;
        public BuyerRfqView(int rfqId)
        {
            this.rfqId = rfqId;
            InitializeComponent();
            this.Text = "Info for RFQ:" + rfqId;
        }

        private void BuyerRfqView_Load(object sender, EventArgs e)
        {
            Rfq rfq = new AmbleClient.RfqGui.RfqManager.RfqMgr().GetRfqAccordingToRfqId(rfqId);
            buyerRfqItems1.FillTheTable(rfq);
            SetMenuStateAccordingToRfqState((RfqStatesEnum)rfq.rfqStates);
        }

        private void SetMenuStateAccordingToRfqState(RfqStatesEnum state)
        {
             switch (state)
            {
               case RfqStatesEnum.Routed:
                    tsbViewOffer.Enabled = false;
                    break;
                case RfqStatesEnum.Offered:
                    tsbViewOffer.Enabled=true;
                    break;
                default:
                    break;
        
        }
        }


        private void tsbOffer_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.NewOffer newOffer = new OfferGui.NewOffer(rfqId);
            newOffer.ShowDialog();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbViewOffer_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.OfferView offerList = new OfferGui.OfferView(rfqId);
            offerList.ShowDialog();
        }




    }
}
