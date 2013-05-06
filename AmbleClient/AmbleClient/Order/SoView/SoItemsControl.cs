using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;

namespace AmbleClient.SO
{
    public partial class SoItemsControl : UserControl
    {
        public SoItemsControl()
        {
            InitializeComponent();

            this.cbCurrency.Items.AddRange(
                Enum.GetNames(typeof(Currency))
                );
            if (!UserCombine.GetUserCanBeFinance().Contains((int)UserInfo.Job) && !UserCombine.GetuserCanBeLogistics().Contains((int)UserInfo.Job))
            {
                tbTrackingNo.ReadOnly = true;
                tbQtyShipped.ReadOnly = true;
            }
        }

        /*
        public void NewCreateItems(int rfqId)
        {
            this.cbCurrency.SelectedIndex = 0;
            this.cbSaleType.SelectedIndex = 0;
        //fill the necessary Info

         RfqGui.RfqManager.Rfq rfq=RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(rfqId);
         this.tbPartNo.Text = rfq.partNo;
         this.tbDc.Text = rfq.dc;
         this.tbMfg.Text = rfq.mfg;
         this.cbRohs.Checked = rfq.rohs == 1 ? true : false;

        }*/

        public void FillItems(SoItems soItem)
        {
            cbSaleType.SelectedIndex = soItem.saleType;
            tbPartNo.Text = soItem.partNo;
            tbMfg.Text = soItem.mfg;
            cbRohs.Checked =( soItem.rohs == 1 ? true : false);
            tbDc.Text = soItem.dc;
            tbIntPartNo.Text = soItem.intPartNo;
            tbShipFrom.Text = soItem.shipFrom;
            tbShipMethod.Text = soItem.shipMethod;
            tbTrackingNo.Text = soItem.trackingNo;
            tbQty.Text = soItem.qty.ToString();
            tbQtyShipped.Text=soItem.qtyshipped.ToString();
            cbCurrency.SelectedIndex = soItem.currencyType;
            tbUnitPrice.Text = soItem.unitPrice.ToString();
            dateTimePicker1.Value = soItem.dockDate;

            if (soItem.shippedDate == null)
            {
                dateTimePicker2.Checked = false;
            }
            else
            {
                dateTimePicker2.Value = soItem.shippedDate.Value;
            }
            tbShipInst.Text = soItem.shippingInstruction;
            tbPackingInst.Text = soItem.packingInstruction;
        
        }


        public bool CheckValues()
        {
            if (ItemsCheck.CheckTextBoxEmpty(tbPartNo) == false)
            {
                MessageBox.Show("Please input the Part number.");
                return false;
            
            }
            if (ItemsCheck.CheckTextBoxEmpty(tbMfg) == false)
            {
                MessageBox.Show("Please input the MFG.");
                return false;
            }
            if (ItemsCheck.CheckTextBoxEmpty(tbDc) == false)
            {
                MessageBox.Show("Please input the D/C.");
                return false;
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbQty))
            {
                MessageBox.Show("Please input the Qty value.");
                tbQty.Focus();
                return false;
            }


            if (!ItemsCheck.CheckIntNumber(tbQty))
            {
                MessageBox.Show("The Qty should be an integer value.");
                tbQty.Focus();
                return false;
            }


            if(ItemsCheck.CheckTextBoxEmpty(tbQtyShipped)&&!ItemsCheck.CheckIntNumber(tbQtyShipped))
            {
               MessageBox.Show("The Qty Shipped should be an integer value.");
                tbQtyShipped.Focus();
                return false;
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbUnitPrice))
            {
                MessageBox.Show("Please input the Unit Price.");
                tbUnitPrice.Focus();
                return false;
            
            }


            if (!ItemsCheck.CheckFloatNumber(tbUnitPrice))
            {
                MessageBox.Show("The Unit Price should be a float value.");
                tbUnitPrice.Focus();
                return false;
            }

            return true;
        
        
        }





        public SoItems GetSoItem()
        {
            DateTime? datetime; int? qtyS;
            if (dateTimePicker2.Checked)
            {
                datetime = dateTimePicker2.Value.Date;
            }
            else
            {
                datetime = null;
            }
            if (tbQtyShipped.Text.Trim().Length == 0)
            {
                qtyS = null;
            }
            else
            { 
               qtyS=Convert.ToInt32(tbQtyShipped.Text.Trim());
            }



            return new SoItems
            {
             saleType=cbSaleType.SelectedIndex,
             partNo=tbPartNo.Text.Trim().ToUpper(),
             mfg=tbMfg.Text.Trim().ToUpper(),
             rohs=cbRohs.Checked?1:0,
             dc=tbDc.Text.Trim(),
             intPartNo=tbIntPartNo.Text.Trim().ToUpper(),
             shipFrom=tbShipFrom.Text.Trim(),
             shipMethod=tbShipMethod.Text.Trim(),
             trackingNo=tbTrackingNo.Text.Trim(),
             qty=Convert.ToInt32(tbQty.Text.Trim()), //will check first
             qtyshipped=qtyS,
             currencyType=cbCurrency.SelectedIndex,
             unitPrice=Convert.ToSingle(tbUnitPrice.Text.Trim()),
             dockDate=dateTimePicker1.Value.Date,
             shippedDate=datetime,
             shippingInstruction=tbShipInst.Text.Trim(),
             packingInstruction=tbPackingInst.Text.Trim()
            };
               
        
        }

        private void SoItemsControl_Load(object sender, EventArgs e)
        {

        }

        private void tbUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(tbQtyShipped.Text.Trim())) && ItemsCheck.CheckIntNumber(tbQtyShipped))
            {
                if (ItemsCheck.CheckFloatNumber(tbUnitPrice))
                {
                    tbTotal.Text = (Convert.ToInt32(tbQtyShipped.Text.Trim()) * Convert.ToSingle(tbUnitPrice.Text.Trim())).ToString();
                }
                else
                {
                    tbTotal.Text = "";
                }
            }

                

        }

        private void tbQtyShipped_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(tbUnitPrice.Text.Trim())) && ItemsCheck.CheckFloatNumber(tbUnitPrice))
            {
                if (ItemsCheck.CheckIntNumber(tbQtyShipped))
                {
                    tbTotal.Text = (Convert.ToInt32(tbQtyShipped.Text.Trim()) * Convert.ToSingle(tbUnitPrice.Text.Trim())).ToString();
                }
                else
                {
                    tbTotal.Text = "";
                }
            }



        }

        private void tbQty_TextChanged(object sender, EventArgs e)
        {

        }
        

    }
}
