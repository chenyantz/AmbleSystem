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

        private Order.PoMgr.poitems poItems;

        private bool isNewCreatePo;


        PoItemStateList poItemStateList = new PoItemStateList();

        public PoItemsView(bool isNewCreatePo)
        {
            InitializeComponent(); 
            this.Text = "PO Item View";
                if (isNewCreatePo)
                {
                    this.tscbPoState.Enabled = false;
                
                }

                this.isNewCreatePo = isNewCreatePo;

        }

        private void tscbOp_Click(object sender, EventArgs e)
        {
            if (!poItemsControl1.CheckValues())
            {
                return;
            }

            if (!isNewCreatePo)
            {
                poitems poItemForUpdate = poItemsControl1.GetPoItem();
                poItemForUpdate.poId = poItems.poId;
                poItemForUpdate.poItemsId = poItems.poItemsId;
                poItemForUpdate.poItemState = poItems.poItemState;
                poItemForUpdate.soItemId = poItems.soItemId;
                PoMgr.PoMgr.UpdatePoItem(poItemForUpdate);
            
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
            SetComboxItem();
        
        }

        private void SetComboxItem()
        {
            PoItemState pis = poItemStateList.GetPoStateAccordingToValue(poItems.poItemState);

            List<Operation> opList = pis.GetOperationList();
            foreach (Operation op in opList)
            {
                if (op.jobs.Contains(UserInfo.Job))
                {
                   tscbPoState.Items.Add(op.operationName);
                }

            }


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
            this.Close();
        }

    }
}
