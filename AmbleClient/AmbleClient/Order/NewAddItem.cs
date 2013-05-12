using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.Order
{
    public partial class NewAddItem : Form
    {
        bool isRfq;
       public int rfqId;
       public  List<int> soItemsList;

        int rfqSoId;

        public NewAddItem(bool isRfq)
        {
            InitializeComponent();
            if (isRfq)
            {
                this.Text = "Add a RFQ for SO Item";
                this.label1.Text = "Please input a RFQ#";
            }
            else
            {
               this.Text="Add an SO for PO Item";
                this.label1.Text="Please input an SO#";
            }
            this.isRfq=isRfq;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckText()
        {
           return int.TryParse(textBox1.Text.Trim(),out rfqSoId);
        }



        private void btOK_Click(object sender, EventArgs e)
        {

            if (!CheckText()) return;

            if (isRfq)
            {
                this.rfqId = rfqSoId;
                //check the rfq state
                AmbleClient.RfqGui.RfqManager.Rfq rfq=AmbleClient.RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(this.rfqId);
                if (rfq == null)
                {
                    MessageBox.Show("The RFQ:" + Tool.Get6DigitalNumberAccordingToId(this.rfqId) + " does not exist!");
                    return;
                }

                if (rfq.rfqStates !=(int) AmbleClient.RfqGui.RfqManager.RfqStatesEnum.Quoted)
                {
                    MessageBox.Show("The RFQ State of RFQ:" + Tool.Get6DigitalNumberAccordingToId(this.rfqId) + " is not QUOTED!");
                    return;
                
                }
            }
            else
            {
                int? soState = AmbleClient.Order.SoMgr.SoMgr.GetSoStateAccordingToSoId(rfqSoId);
                if (soState == null)
                {
                    MessageBox.Show("The SO:" + Tool.Get6DigitalNumberAccordingToId(rfqSoId) + " does not exist!");
                    return;
                }
               if(soState!=(int)Order.SoMgr.SoStatesEnum.Approved)
                {
                   MessageBox.Show("The SO State of SO:" + Tool.Get6DigitalNumberAccordingToId(rfqSoId) + " is not APPROVED!");
                    return;
                }
                this.soItemsList = SoMgr.SoMgr.GetSoItemsIdsAccordingToSoId(rfqSoId);
            
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
      }
    }
}
