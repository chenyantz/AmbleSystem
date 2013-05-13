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

        SoItems soItem;
        private SoItemOrderStateList soItemOrderStateList = new SoItemOrderStateList();

        private bool isNewCreateSo;        

        public SoItemView(bool isNewCreateSo)
        {
            InitializeComponent();
            this.Text = "So Item View";
             this.isNewCreateSo = isNewCreateSo;

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
            if (!isNewCreateSo)
            {
                SoItems soItemForUpdate = soItemsControl1.GetSoItem();
                soItemForUpdate.soId = soItem.soId;
                soItemForUpdate.soItemsId = soItem.soItemsId;
                soItemForUpdate.rfqId = soItem.rfqId;
                soItemForUpdate.soItemState = soItem.soItemState;
                SoMgr.UpdateSoItems(soItemForUpdate);
            
            }
            
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        public void FillTheTable(SoItems item)
        {
            this.soItem = item;
            this.soItemsControl1.FillItems(item);
            SetComboxItem();

            if (!isNewCreateSo)
            {
                if (soItemOrderStateList.GetSoStateAccordingToValue(item.soItemState).WhoCanUpdate().Contains(UserInfo.Job))
                {
                    this.tsbOp.Enabled = true;
                }
                else
                {
                    this.tsbOp.Enabled = false;
                }
            
            }



        }

        private void tscbSoItemState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (soItemsControl1.CheckValues() == false)
            {
                return;
            }


            if (MessageBox.Show("Change the state to " + (string)tscbSoItemState.SelectedItem + " ?", "warning", MessageBoxButtons.YesNo) == DialogResult.No)
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

        private void tsbGeneratePo_Click(object sender, EventArgs e)
        {
            SoItemPicker soItemPicker = new SoItemPicker(soItem.soItemsId);
            if (DialogResult.OK == soItemPicker.ShowDialog())
            {
                List<int> soItemList = soItemPicker.SoItemsIdsForPo;
                AmbleClient.Order.PoView.NewPo newPo = new Order.PoView.NewPo(soItemList);
                newPo.ShowDialog();
            }


        }

    }
}
