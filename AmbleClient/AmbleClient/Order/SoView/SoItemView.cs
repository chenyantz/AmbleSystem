using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;

namespace AmbleClient.SO
{
    public partial class SoItemView : Form
    {

        private int rfqId;
        

        public SoItemView()
        {
            InitializeComponent();
        }


        public SoItemView(bool newItems)
        {
            InitializeComponent();
            if (newItems)
            {   this.Text = "Add an SO Item";
                tsbOp.Text = "Add";
               //his.soItemsControl1.NewCreateItems(rfqId);
            }
            else
            {
                this.Text = "So Item View";
                tsbOp.Text="Update";
           
            }
        }

        public SoItems GetSoItems()
        {
            return soItemsControl1.GetSoItem();
        }

        private void tsbOp_Click(object sender, EventArgs e)
        {
            if (soItemsControl1.CheckValues() == false)
            {
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        public void FillTheTable(SoItems item)
        {
            this.soItemsControl1.FillItems(item);
        
        }

    }
}
