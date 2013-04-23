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
                MessageBox.Show("The RFQ saved successfully");
                tsbSave.Enabled = false;
            }
            this.Close();

        }

        /*
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/

        /*
        private void tsbCreateAnother_Click(object sender, EventArgs e)
        {
            rfqItems1.NewRfqFill();
            tsbSave.Enabled = true;
        }*/

        private void tsbPaste_Click(object sender, EventArgs e)
        {

            RfqGui.RfqManager.RfqMgr rfqMgr = new RfqManager.RfqMgr();
           int rfqId=rfqMgr.GetRfqIdOfTheCopiedRecord(UserInfo.UserId);
           Rfq rfq = rfqMgr.GetRfqAccordingToRfqId(rfqId);
           rfqItems1.FillTheTable(rfq);
           rfqItems1.tbRoutingHistory.Clear();
           rfqItems1.tbCost.Clear();
           rfqItems1.cbCloseReason.SelectedIndex = -1;
           

        }
    }
}
