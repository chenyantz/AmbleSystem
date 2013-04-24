using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.PoMgr;

namespace AmbleClient.Order.PoView
{
    public partial class PoItemsView : Form
    {
        bool isNewAdd;

        public PoItemsView(bool isNewAdd)
        {
            InitializeComponent();
            this.isNewAdd = isNewAdd;
            if (isNewAdd)
            {
                tscbOp.Text = "Add";
                this.Text = "Add a PO Item";

            }
            else
            {
                tscbOp.Text = "Update";
                this.Text = "PO Item View";
            }

        }


        private void tscbOp_Click(object sender, EventArgs e)
        {
            if (!poItemsControl1.CheckValues())
            {
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        public poitems GetPoItem()
        {
            return this.poItemsControl1.GetPoItem();
        }

        public void FillTheTable(poitems poItem)
        {
            poItemsControl1.FillTheItems(poItem);
        
        }



    }
}
