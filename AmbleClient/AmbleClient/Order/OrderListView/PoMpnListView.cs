using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.PoMgr;
using System.Windows.Forms;

namespace AmbleClient.Order
{
   public class PoMpnListView:OrderListView
    {
        protected List<PoCombine> poCombineList;

       protected  PoItemStateList poItemStateList = new PoItemStateList();

        protected override void ViewStart()
        {
            this.Text = "PO List";
            tscbList.Items.Add("All PO");
            tscbList.Items.Add("My PO");

                        //Add columns for datagridView1
            System.Windows.Forms.DataGridViewTextBoxColumn PoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PoItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PoNo6digitals = new System.Windows.Forms.DataGridViewTextBoxColumn();
             System.Windows.Forms.DataGridViewTextBoxColumn PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mfg = new System.Windows.Forms.DataGridViewTextBoxColumn();
           System.Windows.Forms.DataGridViewTextBoxColumn  Dc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Buyer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
             System.Windows.Forms.DataGridViewTextBoxColumn SaleAgent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PoItemState = new System.Windows.Forms.DataGridViewTextBoxColumn();

            PoId.Name = "PoId";
            PoId.Visible = false;

            PoItemId.Name = "PoItemId";
            PoItemId.Visible = false;

            // RfqNo6digitals
            // 
            PoNo6digitals.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            PoNo6digitals.HeaderText = "So#";
            PoNo6digitals.Name = "SoNo6digitals";
            PoNo6digitals.ReadOnly = true;
            PoNo6digitals.Width = 61;
            // 
            // PartNo
            // 
            PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            PartNo.HeaderText = "MPN";
            PartNo.Name = "PartNo";
            PartNo.ReadOnly = true;
            PartNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            PartNo.Width = 56;
            // 
            // Mfg
            // 
            Mfg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Mfg.HeaderText = "MFG";
            Mfg.Name = "Mfg";
            Mfg.ReadOnly = true;
            Mfg.Width = 55;
            // 
            // Dc
            // 
            Dc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Dc.HeaderText = "D/C";
            Dc.Name = "Dc";
            Dc.ReadOnly = true;
            Dc.Width = 52;
            // 
            // Qty
            // 
            Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Qty.HeaderText = "Qty";
            Qty.Name = "Qty";
            Qty.ReadOnly = true;
            Qty.Width = 48;
            // 
            // Resale
            // 
            UnitPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            UnitPrice.HeaderText = "Unit Price";
            UnitPrice.Name = "UnitPrice";
            UnitPrice.ReadOnly = true;
            UnitPrice.Width = 65;
            // 
            // 
            // Customer
            // 
            Vendor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Vendor.HeaderText = "Vendor";
            Vendor.Name = "Vendor";
            Vendor.ReadOnly = true;
            Vendor.Width = 76;

            Buyer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Buyer.HeaderText = "Purchaser";
            Buyer.Name = "Purchaser";
            Buyer.ReadOnly = true;
            Buyer.Width = 76;




            // 
            // Date
            // 
            Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Date.HeaderText = "Date";
            Date.Name = "Date";
            Date.ReadOnly = true;
            Date.Width = 55;
            // 
            // SalePerson
            // 
            SaleAgent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            SaleAgent.HeaderText = "Sale Agent";
            SaleAgent.Name = "SaleAgent";
            SaleAgent.ReadOnly = true;
            SaleAgent.Width = 51;
            // 
            // RfqStates
            // 
            PoItemState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            PoItemState.HeaderText = "States";
            PoItemState.Name = "PoItemStates";
            PoItemState.ReadOnly = true;
            PoItemState.Width = 87;
            // 

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            PoId,
            PoItemId,
            PoNo6digitals,
            PartNo,
            Mfg,
            Dc,
            Qty,
            UnitPrice,
            Vendor,
            Buyer,
            Date,
            SaleAgent,
            PoItemState});


        }

        protected override void FillTheFilterColumnDict()
        {
            filterColumnDict.Add("Vendor Name", "vendorName");
            filterColumnDict.Add("PO Number", "poNo");
            filterColumnDict.Add("MPN", "mpn");

        }


        protected override void GetTheStateList()
        {
            foreach (PoItemState poState in poItemStateList.GetWholeSoStateList())
            {
                intStateList.Add(poState.GetStateValue());
            }
            intStateList.Remove(new PoItemRejected().GetStateValue());
            intStateList.Remove(new PoItemCancelled().GetStateValue());
            intStateList.Remove(new PoItemClosed().GetStateValue());
        }




        protected override void FillTheStateCombox()
        {
            //fill the state List
            foreach (PoItemState poItemState in poItemStateList.GetWholeSoStateList())
            {
                tscbListState.Items.Add(poItemState.GetStateString());
            }

        }

        protected override void StateChanged(object sender, EventArgs e)
        {
            intStateList.Clear();
            intStateList.Add(tscbListState.SelectedIndex);
            FillTheDataGrid();

        }


        protected override void FillTheDataGrid()
        {
            if (tscbList.SelectedIndex < 0) return;

            bool includeSubs = false;
            dataGridView1.Rows.Clear();

            if (tscbList.SelectedIndex == 0)
            {
                includeSubs = true;
            }

            poCombineList = Order.PoMgr.PoMgr.GetPoCombineAccordingToFilter(UserInfo.UserId, includeSubs, filterColumn, filterString, intStateList);

      

            foreach (PoCombine poItem in poCombineList)
            {
                dataGridView1.Rows.Add(poItem.poId,poItem.poItemsId,Tool.Get6DigitalNumberAccordingToId(poItem.poId),poItem.partNo,poItem.mfg,poItem.dc,poItem.qty,poItem.unitPrice,poItem.vendorName,idNameDict[(int)poItem.buyerId],
                    poItem.poDate.ToShortDateString(),idNameDict[(int)poItem.salesAgentId],poItemStateList.GetPoStateStringAccordingToValue(poItem.poItemState));
            }

        }

        protected override void OpenOrderDetails(int rowIndex)
        {
            if (rowIndex >= poCombineList.Count)
                return;
            int poId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["PoId"].Value);
            int poItemId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["PoItemId"].Value);
            AmbleClient.Order.PoView.PoView poView = new AmbleClient.Order.PoView.PoView(poId,poItemId);
            if (DialogResult.Yes == poView.ShowDialog())
            {
                FillTheDataGrid();
            }
        }



    }
}
