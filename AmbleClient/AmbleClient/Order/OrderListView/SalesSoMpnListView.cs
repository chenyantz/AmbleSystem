using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.SoMgr;

namespace AmbleClient.Order
{
  public  class SalesSoMpnListView:SoMpnListView
    {
        protected override void ViewStart()
        {
            this.Text = "SO Item List for Sales";
            tscbList.Items.Add("All SO Item");
            tscbList.Items.Add("My SO Item");

            //Add columns for datagridView1
            System.Windows.Forms.DataGridViewTextBoxColumn No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn SoItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn SoNo6digitals = new System.Windows.Forms.DataGridViewTextBoxColumn();
             System.Windows.Forms.DataGridViewTextBoxColumn PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mfg = new System.Windows.Forms.DataGridViewTextBoxColumn();
           System.Windows.Forms.DataGridViewTextBoxColumn  Dc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CustomerPo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
             System.Windows.Forms.DataGridViewTextBoxColumn SalePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn SoItemState = new System.Windows.Forms.DataGridViewTextBoxColumn();

            No.Name = "No";
            No.Visible = false;

            SoItemId.Name = "SoItemId";
            SoItemId.Visible = false;

            // RfqNo6digitals
            // 
            SoNo6digitals.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            SoNo6digitals.HeaderText = "So#";
            SoNo6digitals.Name = "SoNo6digitals";
            SoNo6digitals.ReadOnly = true;
            SoNo6digitals.Width = 61;
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
            Customer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Customer.HeaderText = "Customer";
            Customer.Name = "Customer";
            Customer.ReadOnly = true;
            Customer.Width = 76;

            CustomerPo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            CustomerPo.HeaderText = "Customer PO #";
            CustomerPo.Name = "CustomerPo";
            CustomerPo.ReadOnly = true;
            CustomerPo.Width = 76;




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
            SalePerson.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            SalePerson.HeaderText = "S/P";
            SalePerson.Name = "SalePerson";
            SalePerson.ReadOnly = true;
            SalePerson.Width = 51;
            // 
            // RfqStates
            // 
            SoItemState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            SoItemState.HeaderText = "States";
            SoItemState.Name = "SoItemStates";
            SoItemState.ReadOnly = true;
            SoItemState.Width = 87;
            // 

            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            No,
            SoItemId,
            SoNo6digitals,
            PartNo,
            Mfg,
            Dc,
            Qty,
            UnitPrice,
            Customer,
            CustomerPo,
            Date,
            SalePerson,
            SoItemState});


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

            soCombineList= SoMgr.SoMgr.SalesGetSoCombineAccordingTofilter(UserInfo.UserId, includeSubs, filterColumn, filterString, intStateList);

            int i = 0;
            foreach (SoCombine soc in soCombineList)
            {
                dataGridView1.Rows.Add(i++,soc.soItemsId, Tool.Get6DigitalNumberAccordingToId(soc.soId), soc.partNo, soc.mfg, soc.dc ,soc.qty,soc.unitPrice,soc.customerName,soc.customerPo,soc.orderDate, idNameDict[soc.salesId], soItemStateList.GetSoStateStringAccordingToValue(soc.soItemState));
            }

        }




    }
}
