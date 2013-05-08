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
        private int soId;

        private Order.PoMgr.poitems poItems;

        PoItemStateList poItemStateList = new PoItemStateList();

        public PoItemsView(bool isNewAdd,int soId)
        {
            InitializeComponent();
            this.isNewAdd = isNewAdd;
            this.soId = soId;
            if (isNewAdd)
            {
                tscbOp.Text = "Add";
                this.Text = "Add a PO Item";
                poItemsControl1.NewFill(this.soId);

            }
            else
            {
                tscbOp.Text = "Hold";
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
            this.poItems = poItem;

            poItemsControl1.FillTheItems(poItem);
        
        }

        private void tscbPoState_SelectedIndexChanged(object sender, EventArgs e)
        {
               
            if (MessageBox.Show("Change the state to " + (string)tscbPoState.SelectedItem + "?", "warning", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string selectedItemString = (string)tscbPoState.SelectedItem;

            PoItemState poItemState = poItemStateList.GetPoStateAccordingToValue(poItems.poItemState);
            foreach (Operation op in poItemState.GetOperationList())
            {
                if (selectedItemString == op.operationName)
                {
                    op.operationMethod(poItems.poItemsId);

                }

            }

            this.DialogResult = DialogResult.Yes;
        }



    }
}
