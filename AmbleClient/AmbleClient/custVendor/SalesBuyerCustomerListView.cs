using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.custVendor
{
    public class SalesBuyerCustomerListView:CustVenderListView
    {
        private int cvtype;/* 0为customer, 1为vendor*/


        public SalesBuyerCustomerListView(int cvtype)
        {
            this.cvtype = cvtype;

            if (cvtype == 0)
            {
                tscbAllOrMine.Items.Add("All Customer");
                tscbAllOrMine.Items.Add("My Customer");
                this.Text = "Customer List";

            }
            if (cvtype == 1)
            {
                tscbAllOrMine.Items.Add("All Vendor");
                tscbAllOrMine.Items.Add("My Vendor");
                this.Text = "Vendor List";
            }
            tscbAllOrMine.SelectedIndex = 0;
            tscbAllOrMine.SelectedIndexChanged+=tscbAllOrMine_SelectedIndexChanged;

        }


        protected override void SetFilterByDict()
        {
            if (cvtype == 0)
            {
                filterByDict.Add("Customer Name", "cvname");
                filterByDict.Add("Customer Number", "cvnumber");
            }
            if (cvtype == 1)
            {
                filterByDict.Add("Vendor Name", "cvname");
                filterByDict.Add("Vendor Number", "cvnumber");

            }

        }

        protected override void SetTheDataGridViewColumn()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Contact1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Phone1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn CellPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Fax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Email1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn OwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();


            No.Name = "No";
            No.Visible = false;

            CompanyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            if (cvtype == 0)
            {
                CompanyName.HeaderText = "Customer Name";
            }
            if (cvtype == 1)
            {
                CompanyName.HeaderText = "Vendor Name";
            }
            CompanyName.Name = "Customer";
            
            Contact1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Contact1.HeaderText = "Contact 1";
            Contact1.Name = "Contact 1";

            Phone1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Phone1.HeaderText = "Phone 1";
            Phone1.Name = "Phone 1";

            CellPhone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            CellPhone.HeaderText = "CellPhone 1";
            CellPhone.Name = "CellPhone 1";

            Fax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Fax.HeaderText = "Fax";
            Fax.Name = "Fax";

            Email1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Email1.HeaderText = "Email 1";
            Email1.Name = "Email 1";

            OwnerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            OwnerName.HeaderText = "Owner Name";
            OwnerName.Name = "Owner Name";

            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            No,
            CompanyName,
            Contact1,
            Phone1,
            CellPhone,
            Fax,
            Email1,
            OwnerName

         });

        }

        protected override void FillTheDataGrid()
        {

            custVenInfoList.Clear();
            if(tscbAllOrMine.SelectedIndex==0)
            {
                custVenInfoList.AddRange(CustVendorManager.CustVenInfoManager.GetCVIcanSee(cvtype, UserInfo.UserId,filterColumn,tstbFilterString.Text.Trim()));

            }
            else if (tscbAllOrMine.SelectedIndex == 1)
            {
                custVenInfoList.AddRange( CustVenInfoManager.GetMyCustomerOrVendors(cvtype, UserInfo.UserId, filterColumn, tstbFilterString.Text.Trim()));
            }
            else
            {
                
            }
            int i = 0;
            foreach (custvendorinfo cvInfo in custVenInfoList)
            { 

            dataGridView1.Rows.Add(i, cvInfo.cvname, cvInfo.contact1, cvInfo.phone1, cvInfo.cellphone, cvInfo.fax, cvInfo.email1,
                AllAccountInfo.GetNameAccordingToId(cvInfo.ownerName));
            i++;
            
            }




        }

        protected override void NewCustVen()
        {
            NewCustVen newCustVen = new NewCustVen(cvtype);
            if (DialogResult.Yes == newCustVen.ShowDialog())
            {
                this.dataGridView1.Rows.Clear();
                FillTheDataGrid();
            }
            RestoreSelectedRow();

        }

        protected override void DeleteCustVen()
        {
            if (dataGridView1.Rows.Count == 0)
                return;


            if (DialogResult.Yes == MessageBox.Show("Delete the Item?", "", MessageBoxButtons.YesNo))
            {

                int cvid = custVenInfoList[selectedRow].cvId;
                CustVendorManager.CustVenInfoManager.DeleteCV(cvid);
                this.dataGridView1.Rows.Clear();
                FillTheDataGrid();

                RestoreSelectedRow();
            }
        }
        

    }
}
