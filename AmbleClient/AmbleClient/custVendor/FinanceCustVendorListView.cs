﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.custVendor
{
    public class FinanceCustVendorListView : CustVenderListView
    {

        public FinanceCustVendorListView()
        {

            tscbAllOrMine.Items.Add("All Customer/Vendor");
            tscbAllOrMine.SelectedIndex = 0;
            tsbNew.Enabled = false;
            tsbDelete.Enabled = false;
            this.Text = "Customer/Vendor List";

        
        }



        protected override void SetFilterByDict()
        {
            filterByDict.Add("Company Name", "cvname");
            filterByDict.Add("Company Number", "cvnumber");
        }

        protected override void SetTheDataGridViewColumn()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CompanyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn OwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn PaymentTerm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ShippingTerm = new System.Windows.Forms.DataGridViewTextBoxColumn();

            No.Name = "No";
            No.Visible = false;

            CompanyType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            CompanyType.HeaderText = "Type";
            CompanyType.Name = "Company Type";

            CompanyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            CompanyName.HeaderText = "Company Name";
            CompanyName.Name = "Compnay Name";

            Country.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Country.HeaderText = "Country";
            Country.Name = "Country";

            OwnerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            OwnerName.HeaderText = "Owner Name";
            OwnerName.Name = "Owner Name";

            PaymentTerm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            PaymentTerm.HeaderText = "Payment Term";
            PaymentTerm.Name = "Payment Term";

            ShippingTerm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            ShippingTerm.HeaderText = "Shipping Term";
            ShippingTerm.Name = "Shipping Term";

            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            No,
            CompanyType,
            CompanyName,
            Country,
            OwnerName,
            PaymentTerm,
            ShippingTerm
         });

       
        }

        protected override void FillTheDataGrid()
        {
            custVenInfoList.Clear();
            custVenInfoList.AddRange(CustVendorManager.CustVenInfoManager.GetAllCustomerAndVendors(filterColumn,tstbFilterString.Text.Trim()));
            int i = 0;
            foreach(custvendorinfo info in custVenInfoList)
            {
                dataGridView1.Rows.Add(i, info.cvtype == 0 ? "C" : "V", info.cvname, info.country, AllAccountInfo.GetNameAccordingToId(info.ownerName), info.paymentTerm, info.shippingTerm);
                i++;
            }
        
        }
    }

}
