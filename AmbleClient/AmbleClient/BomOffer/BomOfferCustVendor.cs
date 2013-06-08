using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.BomOffer
{
    public partial class BomOfferCustVendor : Form
    {
        bool isOffer; //false为BOM，true为offer
        
        public BomOfferCustVendor()
        {
            InitializeComponent();
        }

        public BomOfferCustVendor(bool isOffer):this()
        {
            this.isOffer = isOffer;

            if (isOffer)
            {
                this.Text = "Vendors For Excess";
                this.toolStripButton1.Text = "New Vendor";
                this.tsbImportFromExcel.Text = "Import Offer";
                this.tsbNewBomOff.Text = "New Offer";
                this.tsbDelete.Text = "Delete";

                this.tscbSearchBy.Items.Add("Vendor Name");

            }
            else
            {
                this.Text = "Customer For BOMs";
                this.toolStripButton1.Text = "New Customer";
                this.tsbImportFromExcel.Text = "Import BOM";
                this.tsbNewBomOff.Text = "New BOM";
                this.tsbDelete.Text="Delete";
                this.tscbSearchBy.Items.Add("Customer Name");
            }

            FillTheDataGridViewColumn();


        }


        private void FillTheDataGridViewColumn()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Tel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn User= new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn EnterDay = new System.Windows.Forms.DataGridViewTextBoxColumn();

            Id.Name = "Id";
            Id.Visible = false;

            Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            if (isOffer)
                Company.HeaderText = "Vendor";
            else
                Company.HeaderText = "Customer";
            Company.Name = "Compnay";

            Contact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Contact.HeaderText = "Contact";
            Contact.Name = "Contact";

            Tel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Tel.HeaderText = "Tel";
            Tel.Name = "Tel";

            Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Email.HeaderText = "Email";
            Email.Name = "Email";

            User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            if (isOffer)
                User.HeaderText = "Sales";
            else
                User.HeaderText = "PA";
            User.Name = "User";

            EnterDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            EnterDay.HeaderText = "Enter Day";
            EnterDay.Name = "EnterDay";

            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            Id,
            Company,
            Contact,
            Tel,
            Email,
            User,
            EnterDay
         });

            
        }


        private void FillTheDataGrid()
        {
            this.dataGridView1.Rows.Clear();
            using (BomOfferEntities entity = new BomOfferEntities())
            {
                if (tscbSearchBy.SelectedIndex < 0 || tstbFilterString.Text.Trim().Length == 0)
                {

                    var bomOfferList = from bomOffer in entity.publiccustven
                                       join myaccount in entity.account on bomOffer.userID equals myaccount.id
                                       where bomOffer.custVendorType == (isOffer ? 1 : 0)
                                       select new
                                       {
                                           Id = bomOffer.custVenId,
                                           Name = bomOffer.custVenName,
                                           Contact = bomOffer.contact,
                                           Tel = bomOffer.tel,
                                           Email = bomOffer.email,
                                           User = myaccount.accountName,
                                           EnterDay = bomOffer.enterDay
                                       };
                    foreach (var bomOffer in bomOfferList)
                    {
                        dataGridView1.Rows.Add(bomOffer.Id, bomOffer.Name, bomOffer.Contact, bomOffer.Tel, bomOffer.Email, bomOffer.User, bomOffer.EnterDay);

                    }
                }
                else
                {

                    var bomOfferList = from bomOffer in entity.publiccustven
                                       join myaccount in entity.account on bomOffer.userID equals myaccount.id
                                       where bomOffer.custVendorType == (isOffer ? 1 : 0) &&bomOffer.custVenName.Contains(tstbFilterString.Text.Trim())

                                       select new
                                       {
                                           Id = bomOffer.custVenId,
                                           Name = bomOffer.custVenName,
                                           Contact = bomOffer.contact,
                                           Tel = bomOffer.tel,
                                           Email = bomOffer.email,
                                           User = myaccount.accountName,
                                           EnterDay = bomOffer.enterDay
                                       };
                    foreach (var bomOffer in bomOfferList)
                    {
                        dataGridView1.Rows.Add(bomOffer.Id, bomOffer.Name, bomOffer.Contact, bomOffer.Tel, bomOffer.Email, bomOffer.User, bomOffer.EnterDay);

                    }
                
                }



            }
        
        }



        private void BomOfferCustVendor_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            FillTheDataGrid();
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
          {
              BomOfferNewCustVen bomOfferNewCustVen = new BomOfferNewCustVen(isOffer);
              if (bomOfferNewCustVen.ShowDialog() == DialogResult.OK)
              {
                  BomOfferCustVendor_Load(this, null);
              
              }



          }

        private void tsbNewBomOff_Click(object sender, EventArgs e)
        {
           if(dataGridView1.SelectedRows.Count==0) return;


           DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
           int custVenId = Convert.ToInt32(dgvr.Cells["Id"].Value);
           NewBomOffer(custVenId);

        }

        private void NewBomOffer(int custVenId)
        {
            BomOfferNew bomOfferNew = new BomOfferNew(isOffer, custVenId);
            bomOfferNew.ShowDialog();

        
        }

        private void tscbDisplayBomOffer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;


            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            int custVenId = Convert.ToInt32(dgvr.Cells["Id"].Value);
            BomOffer.BomOfferList bomOfferList = new BomOfferList(isOffer, custVenId);
            bomOfferList.ShowDialog();

        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Delete the selected customer/vendor will also delete its related BOM/Offer", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            int custVenIdSelected = Convert.ToInt32(dgvr.Cells["Id"].Value);

            using (BomOfferEntities entity = new BomOfferEntities())
            {
                var bomOfferList = entity.publicbomoffer.Where(bomOffer => bomOffer.BomCustVendId == custVenIdSelected);
                foreach (var bomOffer in bomOfferList)
                {
                    entity.DeleteObject(bomOffer);
                }

                entity.DeleteObject(entity.publiccustven.Where(cv => cv.custVenId == custVenIdSelected).First());

                entity.SaveChanges();
            
            }

            BomOfferCustVendor_Load(this, null);



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0)
                return;

            DataGridViewRow dgvr = dataGridView1.Rows[rowIndex];
            int custVenId = Convert.ToInt32(dgvr.Cells["Id"].Value);
            BomOffer.BomOfferList bomOfferList = new BomOfferList(isOffer, custVenId);
            bomOfferList.ShowDialog();
        }

        private void tsbExportFromExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
            int custVenId = Convert.ToInt32(dgvr.Cells["Id"].Value);
            
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                DataTable dt = ExcelHelper.ExcelHelper.Import(ofd.FileName);
                if (dt.Rows.Count == 0)
                    return;
                
                bool hasCpn=false,hasMpn=false,hasMfg=false,hasQty=false,hasPrice=false;
                int cpnColumn = -1, mpnColumn = -1, mfgColumn = -1, qtyColumn = -1, priceColumn = -1;
                foreach (DataColumn dc in dt.Columns)
                { 
                    if (dc.ColumnName.ToUpper() == "MPN")
                    {
                        hasMpn = true;
                        mpnColumn=dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.ToUpper() == "CPN")
                    {
                        hasCpn = true;
                        cpnColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.ToUpper() == "MFG")
                    {
                        hasMfg = true;
                        mfgColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.ToUpper() == "QTY")
                    {
                        hasQty = true;
                        qtyColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.ToUpper() == "PRICE")
                    {
                        hasPrice = true;
                        priceColumn = dt.Columns.IndexOf(dc);
                    }
                }
                if (false == (hasPrice && hasMpn && hasCpn && hasMfg && hasQty))
                {
                    MessageBox.Show("Please check the xls File Column.(CPN,MPN,MFG,QTY,PRICE)");
                    return;
                }

              //  List<publicbomoffer> publicbomOfferList = new List<publicbomoffer>();
                 using (BomOfferEntities entity = new BomOfferEntities())
                {   int i=1;
                    foreach (DataRow dr in dt.Rows)
                    {

                        //Qty omit the ","

                        string qtyString = dr[qtyColumn].ToString();
                        qtyString = qtyString.Replace(",",string.Empty);
                        
                        if (!string.IsNullOrWhiteSpace(qtyString) && (!ItemsCheck.CheckIntNumber(qtyString)))
                        {
                            
                            MessageBox.Show("The Qty value is not correct in row " + i.ToString());
                            return;
                        }
                        if (!string.IsNullOrWhiteSpace(dr[priceColumn].ToString()) && (!ItemsCheck.CheckFloatNumber(dr[priceColumn])))
                        {
                           

                            MessageBox.Show("The Price value is not correct in row " + i.ToString());
                            return;
                        }

                        int? qtyLocal;
                        float? priceLocal;

                        if (string.IsNullOrWhiteSpace(qtyString))
                        {
                            qtyLocal = null;
                        }
                        else
                        {
                            qtyLocal = Convert.ToInt32(qtyString);
                        }

                        if (string.IsNullOrWhiteSpace(dr[priceColumn].ToString()))
                        {
                            priceLocal = null;
                        }
                        else
                        {
                            priceLocal = Convert.ToSingle(dr[priceColumn]);
                        }


                        entity.publicbomoffer.AddObject(
                        new publicbomoffer
                         {
                       mfg = dr[mfgColumn].ToString(),
                       mpn = dr[mpnColumn].ToString(),
                       qty =qtyLocal,
                       price = priceLocal,
                       cpn = dr[cpnColumn].ToString(),
                       userID = (short)UserInfo.UserId,
                       BomCustVendId = custVenId,
                       enerDay = DateTime.Now
                         }

                       );
                     i++;
                    }
                    entity.SaveChanges();
                }

                 MessageBox.Show("Import file " + ofd.FileName + " successfully.");
            }


        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            FillTheDataGrid();

        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            tscbSearchBy.SelectedIndex = -1;
            tstbFilterString.Text = string.Empty;
            FillTheDataGrid();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            FillTheDataGrid();
        }
    
    
    
    }








    }

