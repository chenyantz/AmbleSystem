using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;
using AmbleClient.Order;
using AmbleClient.RfqGui.RfqManager;


namespace AmbleClient.SO
{

    public partial class SoViewControl : UserControl
    {

        public bool HasItemChange = false;
        List<SoItemsContentAndState> soItemsStateList = new List<SoItemsContentAndState>();
        List<int> mySubs;

        public List<int> rfqList;
        private int soId=int.MinValue;

        int? selectedSoItemId = null;

        List<string> ShipToList = new List<string>();

        bool isNewCreateSo=false;
        
        public SoViewControl()
        {
            InitializeComponent();
            CustomerAutoComplete();
        }

        private void SoViewControl_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void CustomerAutoComplete()
        {
            List<string> customerNames = custVendor.CustVendorManager.CustVenInfoManager.GetAllCustomerVendorNameICanSee(0, UserInfo.UserId);
            tbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(customerNames.ToArray());
            tbCustomer.AutoCompleteCustomSource = autoSource;
        }

        public void FillTheCustomerInfo(custVendor.CustVendorManager.custvendorinfo custInfo)
        {
            AutoCompleteStringCollection contactSource = new AutoCompleteStringCollection();
            if (custInfo.contact1.Length!=0)
            {
                tbContact.Text = custInfo.contact1;
                contactSource.Add(custInfo.contact1);
            }
            if (custInfo.contact2.Length!=0)
            {
                contactSource.Add(custInfo.contact2);
            }
            tbContact.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbContact.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbContact.AutoCompleteCustomSource = contactSource;

            
            tbCustomerAccount.Text = custInfo.cvnumber;
            tbFreightTerm.Text = custInfo.shippingTerm;
            tbPaymentTerm.Text = custInfo.paymentTerm;
            tbBillto.Text = custInfo.billTo;

            List<custVendor.CustVendorManager.custvendorinfoshipto> shipToList = custVendor.CustVendorManager.CustVenInfoManager.GetShipTo(custInfo.cvId);
            List<string> shipToListString = new List<string>();
            foreach (custVendor.CustVendorManager.custvendorinfoshipto shipto in shipToList)
            {
                shipToListString.Add(shipto.shipTo);
            }
           SetShipToList(shipToListString);

        }


        public void SetShipToList(List<string> list)
        {
            if (list.Count == 1)
            {
                this.tbShipTo.Text = list[0];
            }
            else if (list.Count > 1)
            {
                ShipToList.AddRange(list);
            }

        
        }

        public List<SoItemsContentAndState> GetItemsStateList()
        {
            return soItemsStateList;
        }

        private void GetSoItems()
        {
            this.soItemsStateList.Clear();
            foreach (SoItems item in SoMgr.GetSoItemsAccordingToSoId(this.soId))
            {
                this.soItemsStateList.Add(
                new SoItemsContentAndState
                {
                    soitem = item,
                    state = OrderItemsState.Normal
                }
                );
            }
        
        }

        private void ShowDataInDataGridView()
        {
            dataGridView1.Rows.Clear();
            int indexForSelectedItemsId = -1;

          for(int i=0;i<soItemsStateList.Count;i++)
          {

              string strSaleType, strCurrency;
              switch (soItemsStateList[i].soitem.saleType)
                 
              {

                  case 0:
                  
                  strSaleType = "OEM EXCESS";
                      break;
                  case 1:
                      strSaleType = "OWN STOCK";
                      break;
                  case 2:
                      strSaleType = "OTHERS";
                      break;
                  default:
                      strSaleType = "ERROR";
                      break;
              
              }

              strCurrency = Enum.GetName(typeof(AmbleClient.Currency), soItemsStateList[i].soitem.currencyType);



              dataGridView1.Rows.Add(i + 1, strSaleType, soItemsStateList[i].soitem.partNo, soItemsStateList[i].soitem.mfg, soItemsStateList[i].soitem.rohs, soItemsStateList[i].soitem.dc,
                  soItemsStateList[i].soitem.intPartNo, soItemsStateList[i].soitem.shipFrom, soItemsStateList[i].soitem.shipMethod, soItemsStateList[i].soitem.trackingNo, soItemsStateList[i].soitem.qty,
                  soItemsStateList[i].soitem.qtyshipped, strCurrency, soItemsStateList[i].soitem.unitPrice, soItemsStateList[i].soitem.qtyshipped * soItemsStateList[i].soitem.unitPrice, soItemsStateList[i].soitem.dockDate.ToShortDateString(),
                  soItemsStateList[i].soitem.shippedDate.HasValue?soItemsStateList[i].soitem.shippedDate.Value.ToShortDateString():"");

              if (this.selectedSoItemId != null && soItemsStateList[i].soitem.soItemsId == this.selectedSoItemId.Value)
              {
                  indexForSelectedItemsId = i;
              }



          }
          if (indexForSelectedItemsId > -1)
          { 
            //turn red
              foreach (DataGridViewCell dgvc in dataGridView1.Rows[indexForSelectedItemsId].Cells)
              {
                  dgvc.Style.BackColor = Color.Red;
              
              }
          
          
          }

        }


        public int GetAssignedSaleID()
        {
            return mySubs[cbSp.SelectedIndex];
        }


        public void NewSOFill()
        {
            FillTheSalesComboBox();
            cbSp.SelectedIndex = 0;
            this.isNewCreateSo = true;

            foreach (int id in rfqList)
            {
                Rfq rfq = RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(id);
                SoItems soItem = new SoItems();
                soItem.currencyType =(int) AmbleClient.Currency.USD;
                soItem.unitPrice = rfq.targetPrice ?? 0;
                soItem.mfg = rfq.mfg;
                soItem.partNo = rfq.partNo;
                soItem.rohs = rfq.rohs;
                soItem.qty = rfq.qty ?? 0;
                soItem.intPartNo = rfq.custPartNo;
                soItem.dc = rfq.dc;
                soItem.dockDate = rfq.dockdate;
                soItem.rfqId = id;
                
               this.soItemsStateList.Add(
               new SoItemsContentAndState
               {
                   soitem=soItem,
                   state = OrderItemsState.New
               }
               );
              
            }

            ShowDataInDataGridView();

        }





        private void FillTheSalesComboBox()
        {


            mySubs = AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(UserInfo.UserId, UserCombine.GetUserCanBeSales());

            Dictionary<int, string> mySubsIdAndName = AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(mySubs);
            foreach (string name in mySubsIdAndName.Values)
            {
                cbSp.Items.Add(name);
            }
        
        }


        public void FillTheTable(So so,int? selectedItem)
        {

            this.soId = so.soId;

            this.selectedSoItemId = selectedItem;
            this.isNewCreateSo = false;


            if (UserInfo.Job == JobDescription.Purchaser)
            {
                tbCustomer.Text = "";
            }
            else
            {
                tbCustomer.Text = so.customerName;
            }
            if (UserInfo.Job == JobDescription.Purchaser || UserInfo.Job == JobDescription.PurchasersManager)
            {
                tbContact.Text = "";

            }
            else
            {
                tbContact.Text = so.contact;
            }
                tbSalesOrder.Text = Tool.Get6DigitalNumberAccordingToId(so.soId);
            if (so.approverId != null)
            {
                tbApprover.Text = AmbleClient.Admin.AccountMgr.AccountMgr.GetNameById(so.approverId.Value);
            }
            if (so.approveDate != null)
            {
                tbApproveDate.Text = so.approveDate.Value.ToShortDateString();
            }
             dateTimePicker1.Value = so.orderDate;
            tbCustomerPo.Text = so.customerPo;
            tbPaymentTerm.Text = so.paymentTerm;
            tbFreightTerm.Text = so.freightTerm;
            tbCustomerAccount.Text = so.customerAccount;
            tbSpecialInstructions.Text = so.specialInstructions;
            tbBillto.Text = so.billTo;
            tbShipTo.Text = so.shipTo;
            //Fill the sales ID

            FillTheSalesComboBox();
            if (mySubs.Contains(so.salesId))
            {
                 cbSp.SelectedIndex = mySubs.IndexOf(so.salesId);
            }
            else
            {
                cbSp.Items.Clear();
                cbSp.Items.Add(Admin.AccountMgr.AccountMgr.GetNameById(so.salesId));
                cbSp.SelectedIndex = 0;
            
            }


            GetSoItems();

            ShowDataInDataGridView();

        }


        public bool SoSave()
        {
            if (false == CheckValues())
            { return false; }
            So so=GetValues();
            if (!SoMgr.SaveSoMain(so))
            {
                MessageBox.Show("Save Sale Order Error!");
                return false;
            }
            int soId = SoMgr.GetTheInsertId(so.salesId);
            //save an So number just for search
            SoMgr.SetSoNumber(soId);



            foreach(SoItemsContentAndState sics in soItemsStateList)
            {
              sics.soitem.soId=soId;
              AmbleClient.RfqGui.RfqManager.RfqMgr.ChangeRfqState(RfqStatesEnum.HasSO, sics.soitem.rfqId);
            }
             SoMgr.UpdateSoItems(soItemsStateList);
             MessageBox.Show("Save Sale Order Successfully");
             return true;

        }


        public void SoUpdate()
        {
            if (false == CheckValues())
            { return; }
            So so = GetValues();
            if (!SoMgr.UpdateSoMain(so))
            {
                MessageBox.Show("Update Sale Order Error!");
                return;
            }

            MessageBox.Show("Update Sale Order Successfully");
        }

        private bool CheckValues()
        {
            if (ItemsCheck.CheckTextBoxEmpty(tbCustomer) == false)
            {
                MessageBox.Show("Please input the customer name");
                return false;
            }

            if (ItemsCheck.CheckTextBoxEmpty(tbContact) == false)
            {
                MessageBox.Show("Please input the Contact name");
                return false;
            }

            return true;

        }

        private So GetValues()
        {
           
            return new So
            {
             soId=this.soId,
            customerName = tbCustomer.Text.Trim(),
            contact = tbContact.Text.Trim(),
            salesId = mySubs[cbSp.SelectedIndex],
            salesOrderNo = tbSalesOrder.Text.Trim(),
            orderDate = dateTimePicker1.Value.Date,
            customerPo = tbCustomerPo.Text.Trim(),
            paymentTerm = tbPaymentTerm.Text.Trim(),
            freightTerm = tbFreightTerm.Text.Trim(),
            customerAccount = tbCustomerAccount.Text.Trim(),
            specialInstructions = tbSpecialInstructions.Text.Trim(),
            billTo=tbBillto.Text.Trim(),
            shipTo=tbShipTo.Text.Trim(),
           };

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                SoItemView itemView = new SoItemView(false);
                itemView.FillTheTable(soItemsStateList[e.RowIndex].soitem);

                if (DialogResult.Yes == itemView.ShowDialog())
                {
                    if (isNewCreateSo)
                    {
                        int rfqId = soItemsStateList[e.RowIndex].soitem.rfqId;
                        int soItemState = soItemsStateList[e.RowIndex].soitem.soItemState;
                        soItemsStateList[e.RowIndex].soitem = itemView.GetSoItems();
                        soItemsStateList[e.RowIndex].soitem.rfqId = rfqId;
                        soItemsStateList[e.RowIndex].soitem.soItemState = soItemState;
                    }
                    else
                    {
                        GetSoItems();
                    }
                    ShowDataInDataGridView();
                    this.HasItemChange = true;
                }

            }


        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            NewAddItem nai=new NewAddItem(true);

            if (DialogResult.Yes == nai.ShowDialog())
            {

                Rfq rfq = RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(nai.rfqId);
                
                SoItems soItem = new SoItems();
                soItem.currencyType = (int)AmbleClient.Currency.USD;
                soItem.unitPrice = rfq.targetPrice ?? 0;
                soItem.mfg = rfq.mfg;
                soItem.partNo = rfq.partNo;
                soItem.rohs = rfq.rohs;
                soItem.qty = rfq.qty ?? 0;
                soItem.intPartNo = rfq.custPartNo;
                soItem.dc = rfq.dc;
                soItem.dockDate = rfq.dockdate;
                soItem.rfqId = nai.rfqId;


                if (isNewCreateSo)
                {
                    soItemsStateList.Add(new SoItemsContentAndState
                    {
                        soitem = soItem,
                        state = OrderItemsState.New
                    }
                   );
                }
                else
                {
                    SoMgr.SaveSoItems(this.soId, soItem);
                    GetSoItems();
                

                }
                ShowDataInDataGridView();
                this.HasItemChange = true;
            
            }



        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("Delete the selected SO item ?", "Warning", MessageBoxButtons.YesNo))
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                if (isNewCreateSo)
                {
                    soItemsStateList.RemoveAt(rowIndex);
                }
                else
                {
                    SoMgr.DeleteSoItembySoItemId(soItemsStateList[rowIndex].soitem.soItemsId);
                    GetSoItems();
                }
                ShowDataInDataGridView();
                this.HasItemChange = true;
            }



        }

        private void btSplit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            int rowIndex = dataGridView1.SelectedRows[0].Index;
            int qty = soItemsStateList[rowIndex].soitem.qty;
            DateTime dockDate = soItemsStateList[rowIndex].soitem.dockDate;

            ItemSplit itemSplit = new ItemSplit(qty,dockDate);
            if (DialogResult.OK == itemSplit.ShowDialog())
            { 
             //get the first value;
                int firstValue = itemSplit.GetFirstQty();
                soItemsStateList[rowIndex].soitem.qty = firstValue;
                soItemsStateList[rowIndex].soitem.dockDate = itemSplit.GetFirstDateTime();
                if (soItemsStateList[rowIndex].state != OrderItemsState.New)
                {
                    soItemsStateList[rowIndex].state = OrderItemsState.Modified;
                }
            //set the second one

                var soItemContentAndState = new SoItemsContentAndState();
                soItemContentAndState.soitem = (SoItems)soItemsStateList[rowIndex].soitem.Clone();
                soItemContentAndState.soitem.soId = this.soId;
                soItemContentAndState.soitem.qty = qty - firstValue;
                soItemContentAndState.soitem.dockDate = itemSplit.GetSecondDateTime();
                soItemContentAndState.state = OrderItemsState.New;
                soItemsStateList.Insert(rowIndex + 1, soItemContentAndState);

                if (!isNewCreateSo)
                {
                    SoMgr.UpdateSoItems(soItemsStateList);
                    GetSoItems();
                }

                ShowDataInDataGridView();
                this.HasItemChange = true;
            }


        }

        private void tbShipTo_Enter(object sender, EventArgs e)
        {
            if (ShipToList.Count >= 2)
            {
                ShipToSelect shipToSelect = new ShipToSelect(ShipToList);
                if (DialogResult.Yes == shipToSelect.ShowDialog())
                {
                    tbShipTo.Text = shipToSelect.SelectionString;
                }
            }
        }

        private void tbCustomer_Leave(object sender, EventArgs e)
        {
            if (tbCustomer.Text.Trim().Length == 0) return;
            custVendor.CustVendorManager.custvendorinfo cInfo = custVendor.CustVendorManager.CustVenInfoManager.GetUniqueCustVenInfo(0, tbCustomer.Text.Trim(), UserInfo.UserId);
            if (cInfo == null || cInfo.cvnumber == null || cInfo.cvnumber.Trim().Length == 0)
            {
                MessageBox.Show("The SO can not be Created/Update.The necessary Customer Info did not exist in DB. Please add the customer in the Customer Managerment menu Or ask finance for Customer Number.\n\r Note:the Customer Name copied, you can CTR+V.");

                if (tbCustomer.Text.Trim().Length > 0)
                {
                    Clipboard.SetText(tbCustomer.Text.Trim());
                }
                this.tbCustomer.Text = string.Empty;
                this.tbCustomer.Focus();
                return;
            }
            FillTheCustomerInfo(cInfo);

        }
    }
  
    
    public class SoItemsContentAndState
    {
      public  SoItems soitem;
      public   OrderItemsState state;

    }
    
}
