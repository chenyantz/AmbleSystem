using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.PoMgr;
using AmbleClient;

namespace AmbleClient.Order.PoView
{

    public partial class PoViewControl : UserControl
    {
      public bool HasItemChange=false;
      private  List<int> mysubs;
      private  Dictionary<int, string> buyerIdsAndNames;
      private int poId = int.MinValue;

      public List<int> soItemsIdList;

      int? selectedPoItemId = null;

      private bool isNewCreatePo = false;

     List<PoItemContentAndState> poItemsStateList = new List<PoItemContentAndState>();

     List<string> ShipToList = new List<string>();

        public PoViewControl()
        {
            InitializeComponent();
            FillThePACombo();
            VendorAutoComplete();

        }
        private void SoViewControl_Load(object sender, EventArgs e)
        {

        }

        private void VendorAutoComplete()
        {
            List<string> vendorNames = custVendor.CustVendorManager.CustVenInfoManager.GetAllCustomerVendorNameICanSee(1, UserInfo.UserId);
            this.tbVendor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbVendor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
            autoSource.AddRange(vendorNames.ToArray());
            tbVendor.AutoCompleteCustomSource = autoSource;
        }


        public void FillTheVendorInfo(custVendor.CustVendorManager.custvendorinfo custInfo)
        {
            this.tbVendorNumber.Text = custInfo.cvnumber;
            this.tbPaymentTerms.Text = custInfo.paymentTerm;
            this.tbBillTo.Text = custInfo.billTo;

            if (custInfo.contact1 != null && custInfo.contact1.Trim().Length > 0)
            {
                this.tbContact.Text = custInfo.contact1;

                if (custInfo.contact2 != null && custInfo.contact2.Trim().Length > 0)//两者都有值
                {
                    this.tbContact.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    this.tbContact.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
                    autoSource.Add(custInfo.contact1);
                    autoSource.Add(custInfo.contact2);
                    tbContact.AutoCompleteCustomSource = autoSource;
                }
            }
            else if (custInfo.contact2 != null && custInfo.contact2.Trim().Length > 0)
            {
                this.tbContact.Text = custInfo.contact2;

            }
            else
            { 
            
            }

                

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


        public void NewPoFill()
        {
            this.isNewCreatePo = true;
            cbPa.SelectedIndex = 0;

            foreach(int soItemsId in soItemsIdList)
            {
                Order.SoMgr.SoItems item = SoMgr.SoMgr.GetSoItemInfoAccordingToSoItemId(soItemsId);
                Order.SoMgr.So so = SoMgr.SoMgr.GetSoAccordingToSoId(item.soId);
                float miniPrice = float.MaxValue;
                List<OfferGui.OfferMgr.Offer> offerList = OfferGui.OfferMgr.OfferMgr.GetOffersByRfqId(item.rfqId);
        
                    foreach (OfferGui.OfferMgr.Offer o in offerList)
                    {
                        if (o.price < miniPrice)
                            miniPrice = o.price;

                    }
                
                poitems poItem = new poitems();
                poItem.partNo = item.partNo;
                poItem.mfg = item.mfg;
                poItem.dc = item.dc;
                poItem.qty = item.qty;
                poItem.dockDate = item.dockDate;
                poItem.unitPrice = miniPrice;
                poItem.receiveDate = null;
                poItem.currency = (sbyte)((int)AmbleClient.Currency.USD);
                poItem.soItemId = item.soItemsId;
                poItem.salesAgent = (sbyte)so.salesId;
                poItem.poItemState = (sbyte)new PoItemNew().GetStateValue();

                this.poItemsStateList.Add(
                    new PoItemContentAndState
                    {
                        poItem = poItem,
                        state = OrderItemsState.New
                    }
                    );
            }
            FillTheDataGridPoItems();
        
        }




        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }


        public List<PoItemContentAndState> GetPoItemsStateList()
        {

            return poItemsStateList;
        }




        private void PoViewControl_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }


        public List<PoItemContentAndState> GetPoItemContentAndState()
        {
            return poItemsStateList;
        }


        private void FillThePACombo()
        {


            mysubs = AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(UserInfo.UserId, UserCombine.GetUserCanBeBuyers());

            buyerIdsAndNames = AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(mysubs);

            foreach (string buyerName in buyerIdsAndNames.Values)
            {
                cbPa.Items.Add(buyerName);
            
            }
            cbPa.SelectedIndex = 0; //0为myself

        }


        public void PoSave()
        {
            if (false == CheckValues())
            {
                return;
            }
            po poMain = GetValues();
            poMain.poDate = DateTime.Now;
            PoMgr.PoMgr.SavePoMain(poMain);

            int poId = PoMgr.PoMgr.GetTheInsertId((int)poMain.pa);

            foreach (PoItemContentAndState pics in poItemsStateList)
            {
                pics.poItem.poId = poId;
            }
            PoMgr.PoMgr.UpDatePoItems(poItemsStateList);

            MessageBox.Show("Save Purchase Order Successfully");

        }


        public void PoUpdate()
        {
            if (false == CheckValues())
            {
                return;
            }
            po poMain = GetValues();
            PoMgr.PoMgr.UpdatePo(poMain);
           MessageBox.Show("Update Purchase Order Successfully");
        }

        public bool CheckValues()
        {
            if (ItemsCheck.CheckTextBoxEmpty(tbVendor) == false)
            {
                MessageBox.Show("Please input the Vendor name");
                return false;
            }
            if (ItemsCheck.CheckTextBoxEmpty(tbContact) == false)
            {
                MessageBox.Show("Please input the Contact name");
                return false;
            }
            return true;
        }

  


        public void FillTheTable(po poMain,int? selectedItem)
        {
            this.poId = poMain.poId;

            this.selectedPoItemId = selectedItem;

            if (UserInfo.Job == JobDescription.Sales || UserInfo.Job == JobDescription.SalesManager)
            {
                tbVendor.Text = "";
                tbContact.Text = "";
            }
            else
            {
                tbVendor.Text = poMain.vendorName;
                tbContact.Text = poMain.contact;
            }
            if (buyerIdsAndNames.Keys.Contains(poMain.pa))
            {
                cbPa.SelectedItem = buyerIdsAndNames[poMain.pa];
            }
            else
            {
                cbPa.Items.Clear();
                cbPa.Items.Add(AmbleClient.Admin.AccountMgr.AccountMgr.GetNameById(poMain.pa));
                cbPa.SelectedIndex=0;
            }
            tbVendorNumber.Text = poMain.vendorNumber;
            tbPoDate.Text = poMain.poDate.ToShortDateString();
            tbPoNo.Text = Tool.Get6DigitalNumberAccordingToId(poMain.poId);
            cbFreight.Text = poMain.freight;
            tbShipMethod.Text = poMain.shipMethod;
            tbPaymentTerms.Text = poMain.paymentTerms;
            tbShipToLocation.Text = poMain.shipToLocation;
            tbBillTo.Text = poMain.billTo;
            tbShipTo.Text = poMain.shipTo;
         

            GetPoItemsList();
            FillTheDataGridPoItems();

            if (mysubs.Contains(poMain.pa))
            {
                btAdd.Enabled = true;
                btDelete.Enabled = true;
                btSplit.Enabled = true;
            }
            else
            {
                btAdd.Enabled = false;
                btDelete.Enabled = false;
                btSplit.Enabled = false;
            }

            if (poMain.poStates != (sbyte)PoStatesEnum.New)
            {
                btAdd.Enabled = false;
            }
            else
            {
                btAdd.Enabled = true;
            }


        }


        public void GetPoItemsList()
        {
            poItemsStateList.Clear();

            foreach (poitems itemDb in PoMgr.PoMgr.GetPoItemsAccordingToPoId(this.poId))
            {
                poItemsStateList.Add(new PoItemContentAndState
                {
                    poItem = itemDb,
                    state = OrderItemsState.Normal

                }
                );

            }
        
        }


        public void FillTheDataGridPoItems()
        {
            dataGridView1.Rows.Clear();
            int indexForSelectedItemsId = -1;
            int i = 0;
            foreach (PoItemContentAndState cSitem in poItemsStateList)
            {
                dataGridView1.Rows.Add(i+1, cSitem.poItem.partNo, cSitem.poItem.mfg, cSitem.poItem.dc, cSitem.poItem.vendorIntPartNo, cSitem.poItem.coo, cSitem.poItem.qty,
                                     cSitem.poItem.qtyRecd, cSitem.poItem.qtyCorrected, cSitem.poItem.qtyAccept, cSitem.poItem.qtyRejected, cSitem.poItem.qtyRTV, cSitem.poItem.qcPending,
                                      Enum.GetName(typeof(AmbleClient.Currency), cSitem.poItem.currency), cSitem.poItem.unitPrice, cSitem.poItem.qty * cSitem.poItem.unitPrice, cSitem.poItem.dockDate.ToShortDateString(), cSitem.poItem.receiveDate.HasValue?cSitem.poItem.receiveDate.Value.ToShortDateString():"",cSitem.poItem.stepCode,AllAccountInfo.GetNameAccordingToId(cSitem.poItem.salesAgent));
              
                if (this.selectedPoItemId != null && poItemsStateList[i].poItem.poItemsId == this.selectedPoItemId.Value)
                {
                    indexForSelectedItemsId = i;
                }
                i++;
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
        
        public po GetValues()
        {
            return new po
            {
                poId=this.poId,
                vendorName = tbVendor.Text.Trim(),
                contact = tbContact.Text.Trim(),
                pa = (short)mysubs[cbPa.SelectedIndex],
                vendorNumber=tbVendorNumber.Text.Trim(),
                //poDate=DateTime.Now, //update时不可写入
                //poNo=tbPoNo.Text.Trim(),
                freight=cbFreight.Text.Trim(),
                shipMethod=tbShipMethod.Text.Trim(),
                paymentTerms=tbPaymentTerms.Text.Trim(),
                shipToLocation=tbShipToLocation.Text.Trim(),
                billTo=tbBillTo.Text.Trim(),
                shipTo=tbShipTo.Text.Trim()
            };
        }

       private void btAdd_Click(object sender, EventArgs e)
       {
            NewAddItem nai=new NewAddItem(false);

            if (DialogResult.Yes == nai.ShowDialog())
            {
                SO.SoItemPicker sip = new SO.SoItemPicker(nai.soItemsList);
                if (DialogResult.OK == sip.ShowDialog())
                {
                    List<int> soItemList = new List<int>();
                    soItemList.AddRange(sip.SoItemsIdsForPo);
                    List<poitems> poItems = new List<poitems>();

                    foreach (int soItemsId in soItemList)
                    {
                        Order.SoMgr.SoItems item = SoMgr.SoMgr.GetSoItemInfoAccordingToSoItemId(soItemsId);
                        Order.SoMgr.So so = SoMgr.SoMgr.GetSoAccordingToSoId(item.soId);
                        float miniPrice = float.MaxValue;
                        List<OfferGui.OfferMgr.Offer> offerList = OfferGui.OfferMgr.OfferMgr.GetOffersByRfqId(item.rfqId);

                        foreach (OfferGui.OfferMgr.Offer o in offerList)
                        {
                            if (o.price < miniPrice)
                                miniPrice = o.price;

                        }

                        poitems poItem = new poitems();
                        poItem.partNo = item.partNo;
                        poItem.mfg = item.mfg;
                        poItem.dc = item.dc;
                        poItem.qty = item.qty;
                        poItem.dockDate = item.dockDate;
                        poItem.unitPrice = miniPrice;
                        poItem.receiveDate = null;
                        poItem.currency = (sbyte)((int)AmbleClient.Currency.USD);
                        poItem.soItemId = item.soItemsId;
                        poItem.salesAgent = (sbyte)so.salesId;


                        if (isNewCreatePo)
                        {
                            this.poItemsStateList.Add(
                                new PoItemContentAndState
                                {
                                    poItem = poItem,
                                    state = OrderItemsState.New
                                }
                                );
                        }
                        else
                        {
                            poItems.Add(poItem);
                        
                        }
                    }

                    if (!isNewCreatePo)
                    {
                        PoMgr.PoMgr.SavePoItems(this.poId, poItems);
                        GetPoItemsList();
                    }
                    FillTheDataGridPoItems();
                    this.HasItemChange = true;
                }

            }

       }

       private void btDelete_Click(object sender, EventArgs e)
       {
           if (dataGridView1.SelectedRows.Count == 0)
           {
              return;
           }
          if( DialogResult.Yes==MessageBox.Show("Delete the selected PO item ?","Warning",MessageBoxButtons.YesNo))
          {
              int rowIndex=dataGridView1.SelectedRows[0].Index;

              if (!isNewCreatePo)
              {
                  PoMgr.PoMgr.DeletePoItembyPoItemId(poItemsStateList[rowIndex].poItem.poItemsId);
                  GetPoItemsList();
              }
              else
              {
                  poItemsStateList.Remove(poItemsStateList[rowIndex]);
              }
              FillTheDataGridPoItems();
              HasItemChange = true;
              
          }


       }

       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {

       }

       private void btSplit_Click(object sender, EventArgs e)
       {
           if (dataGridView1.SelectedRows.Count == 0)
           {
               return;
           }
           int rowIndex = dataGridView1.SelectedRows[0].Index;
           int qty = poItemsStateList[rowIndex].poItem.qty;
           DateTime dockDate=poItemsStateList[rowIndex].poItem.dockDate;
           ItemSplit itemSplit = new ItemSplit(qty,dockDate);
           if (DialogResult.OK == itemSplit.ShowDialog())
           {
               //get the first value;
               int firstValue = itemSplit.GetFirstQty();
               poItemsStateList[rowIndex].poItem.qty = firstValue;
               poItemsStateList[rowIndex].poItem.dockDate = itemSplit.GetFirstDateTime();

               if (poItemsStateList[rowIndex].state != OrderItemsState.New)
               {
                   poItemsStateList[rowIndex].state = OrderItemsState.Modified;
               }
               //set the second one

               var poItemContentAndState = new PoItemContentAndState();
               //can not be cloned,just assign the value;
               poItemContentAndState.poItem = new poitems();
               poItemContentAndState.poItem.currency = poItemsStateList[rowIndex].poItem.currency;
               poItemContentAndState.poItem.dc = poItemsStateList[rowIndex].poItem.dc;
               poItemContentAndState.poItem.dockDate = itemSplit.GetSecondDateTime();
               poItemContentAndState.poItem.mfg = poItemsStateList[rowIndex].poItem.mfg;
               poItemContentAndState.poItem.noteToVendor = poItemsStateList[rowIndex].poItem.noteToVendor;
               poItemContentAndState.poItem.coo = poItemsStateList[rowIndex].poItem.coo;
               poItemContentAndState.poItem.partNo = poItemsStateList[rowIndex].poItem.partNo;
               poItemContentAndState.poItem.qcPending = poItemsStateList[rowIndex].poItem.qcPending;
               poItemContentAndState.poItem.qtyAccept=poItemsStateList[rowIndex].poItem.qtyAccept;
               poItemContentAndState.poItem.qtyCorrected = poItemsStateList[rowIndex].poItem.qtyCorrected;
               poItemContentAndState.poItem.qtyRecd = poItemsStateList[rowIndex].poItem.qtyRecd;
               poItemContentAndState.poItem.qtyRejected = poItemsStateList[rowIndex].poItem.qtyRejected;
               poItemContentAndState.poItem.qtyRTV = poItemsStateList[rowIndex].poItem.qtyRTV;
               poItemContentAndState.poItem.receiveDate = poItemsStateList[rowIndex].poItem.receiveDate;
               poItemContentAndState.poItem.salesAgent = poItemsStateList[rowIndex].poItem.salesAgent;
               poItemContentAndState.poItem.stepCode = poItemsStateList[rowIndex].poItem.stepCode;
               poItemContentAndState.poItem.unitPrice = poItemsStateList[rowIndex].poItem.unitPrice;
               poItemContentAndState.poItem.vendorIntPartNo = poItemsStateList[rowIndex].poItem.vendorIntPartNo;
                             
               poItemContentAndState.poItem.poId = this.poId;
               poItemContentAndState.poItem.poItemState = poItemsStateList[rowIndex].poItem.poItemState;
               poItemContentAndState.poItem.qty = qty - firstValue;

               if ((poItemsStateList[rowIndex].poItem.poItemState!= new PoItemNew().GetStateValue())
    && (poItemsStateList[rowIndex].poItem.poItemState != new PoItemRejected().GetStateValue())
    && (poItemsStateList[rowIndex].poItem.poItemState != new PoItemCancelled().GetStateValue())
    )
               {
                   poItemContentAndState.poItem.poItemState = (sbyte)new PoItemApproved().GetStateValue();
               }

               poItemContentAndState.state = OrderItemsState.New;

               poItemsStateList.Insert(rowIndex + 1, poItemContentAndState);

               if (!isNewCreatePo)
               {
                   PoMgr.PoMgr.UpDatePoItems(poItemsStateList);
                   GetPoItemsList();

               }
               FillTheDataGridPoItems();
               this.HasItemChange = true;
           }
       }

       private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
       {
           if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
           {

               PoItemsView itemView = new PoItemsView(isNewCreatePo);
               itemView.FillTheTable(poItemsStateList[e.RowIndex].poItem);

               if (DialogResult.Yes == itemView.ShowDialog())
               {
                   if (isNewCreatePo)
                   {
                       sbyte poItemState = poItemsStateList[e.RowIndex].poItem.poItemState;
                       int soItemId = poItemsStateList[e.RowIndex].poItem.soItemId;
                       poItemsStateList[e.RowIndex].poItem = itemView.GetPoItem();
                       poItemsStateList[e.RowIndex].poItem.poItemState = poItemState;
                       poItemsStateList[e.RowIndex].poItem.soItemId = soItemId;

                   }
                   else
                   {
                    GetPoItemsList();
                   }
                   FillTheDataGridPoItems();
                   HasItemChange = true;
               }
           }

       }

       private void tbVendor_Leave(object sender, EventArgs e)
       {
           if (tbVendor.Text.Trim().Length == 0) return;

           custVendor.CustVendorManager.custvendorinfo cInfo = custVendor.CustVendorManager.CustVenInfoManager.GetUniqueCustVenInfo(1, this.tbVendor.Text.Trim(), UserInfo.UserId);
           if (cInfo == null || cInfo.cvnumber == null || cInfo.cvnumber.Trim().Length == 0)
           {
               MessageBox.Show("The PO can not be Created/Update.The necessary Vendor Info did not exist in DB. Please add the vendor in the Vendor Managerment menu Or ask finance for Vendor Number.\n\r Note:the Vendor Name copied, you can CTR+V.");

               if (tbVendor.Text.Trim().Length > 0)
               {
                   Clipboard.SetText(tbVendor.Text.Trim());
               }
               this.tbVendor.Text = string.Empty;
               this.tbVendor.Focus();
               return;
           }
           FillTheVendorInfo(cInfo);
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
    }

    public class PoItemContentAndState
    {
        public poitems poItem;
        public OrderItemsState state;

    }

}
 