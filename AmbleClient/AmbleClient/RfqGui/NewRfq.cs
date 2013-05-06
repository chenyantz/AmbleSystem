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
    public partial class NewRfq : Form
    {
        bool copied = false;

        int RFQSaved = 0;
               
        public NewRfq()
        {
            InitializeComponent();
        }

        public NewRfq(bool copied)
        {
            InitializeComponent();
            this.copied = true;
        }


        private void NewRfq_Load(object sender, EventArgs e)
        {

               rfqItems1.NewRfqFill();
            if(this.copied)
            {
                tsbPaste_Click(this, null);
            }
        }



        private void tsbSave_Click(object sender, EventArgs e)
        {

            if (rfqItems1.SaveInfo())
            {
                MessageBox.Show("The RFQ "+RFQSaved+" saved successfully");
                rfqItems1.tbCustomer.ReadOnly = true;
                rfqItems1.ClearInfoForNewRfqWithSameCustomer();
                RFQSaved++;
                tsbSave.Text = "Save " + (RFQSaved+1);
            }
           

        }



        private void tsbPaste_Click(object sender, EventArgs e)
        {
           int rfqId=RfqMgr.GetRfqIdOfTheCopiedRecord(UserInfo.UserId);
           Rfq rfq = RfqMgr.GetRfqAccordingToRfqId(rfqId);
           rfqItems1.FillTheTable(rfq);
           rfqItems1.tbRoutingHistory.Clear();
           rfqItems1.tbCost.Clear();
           rfqItems1.cbCloseReason.SelectedIndex = -1;
           

        }
    }
}
