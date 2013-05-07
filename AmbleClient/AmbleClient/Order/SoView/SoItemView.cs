using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;
using AmbleClient.Order;

namespace AmbleClient.SO
{
    public partial class SoItemView : Form
    {

        private int rfqId;
        SoItems soItem;
        private SoItemOrderStateList soItemOrderStateList = new SoItemOrderStateList();

        

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
                tsbOp.Text="Hold";
           
            }
        }


        private void SetComboxItem()
        {
            SoItemState sis = soItemOrderStateList.GetSoStateAccordingToValue(soItem.soItemState);

            List<Operation> opList = sis.GetOperationList();
            foreach (Operation op in opList)
            {
                if (op.jobs.Contains(UserInfo.Job))
                {
                    tscbSoItemState.Items.Add(op.operationName);
                }
            
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
            this.soItem = item;
            this.soItemsControl1.FillItems(item);
            SetComboxItem();
        }

        private void tscbSoItemState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (soItemsControl1.CheckValues() == false)
            {
                return;
            }


            if (MessageBox.Show("Change the state to " + (string)tscbSoItemState.SelectedItem + " and hold all the changes?", "warning", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string selectedItemString = (string)tscbSoItemState.SelectedItem;

            SoItemState soItemState = soItemOrderStateList.GetSoStateAccordingToValue(soItem.soItemState);
            foreach (Operation op in soItemState.GetOperationList())
            {
                if (selectedItemString == op.operationName)
                {
                    op.operationMethod(soItem.soItemsId);

                }

            }
            this.DialogResult = DialogResult.Yes;
            this.Close();



        }

    }
}
