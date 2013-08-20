using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace AmbleClient.BomOffer
{
    public partial class BomOfferList : Form
    {
        // bool isOffer;

        BomOfferTypeEnum bomOfferType;
        bool listbyCustVen = false;
        int custVenId;
        bool listAll = true;
        Dictionary<int, string> idToName = AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllIds());
 
        public BomOfferList(BomOfferTypeEnum bomOfferType)
        {
            InitializeComponent();
            this.bomOfferType = bomOfferType;
            FillTheDataGridColumn();
        }
        public BomOfferList(BomOfferTypeEnum bomOfferType, int custVenId)
            : this(bomOfferType)
        {
            this.listbyCustVen = true;
            this.custVenId = custVenId;
        }

        private void FillTheDataGridColumn()
        {
            #region unused
            /*
            System.Windows.Forms.DataGridViewTextBoxColumn Id = new System.Windows.Forms.DataGridViewTextBoxColumn();


            Id.Name = "Id";
            Id.Visible = false;

            System.Windows.Forms.DataGridViewTextBoxColumn Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mfg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mpn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Cpn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn EnterDay = new System.Windows.Forms.DataGridViewTextBoxColumn();

            Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            if (this.bomOfferType==BomOfferTypeEnum.Excess)
                Company.HeaderText = "Vendor";
            else if(this.bomOfferType==BomOfferTypeEnum.BOM)
                Company.HeaderText = "Customer";
            else 
                Company.HeaderText="Company";
            Company.Name = "Company";

            Mfg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Mfg.HeaderText = "MFG";
            Mfg.Name = "Mfg";

            Mpn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Mpn.HeaderText = "MPN";
            Mpn.Name = "Mpn";
            
            Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Qty.HeaderText = "QTY";
            Qty.Name = "Qty";


            Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Price.HeaderText = "Price";
            Price.Name = "Price";

            Cpn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            Cpn.HeaderText = "CPN";
            Cpn.Name = "Cpn";

            User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            User.HeaderText = "Owner";
            User.Name = "User";

            EnterDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            EnterDay.HeaderText = "Date";
            EnterDay.Name = "EnterDay";

            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            Id,
            Company,
            Mfg,
            Mpn,
            Qty,
            Price,
            Cpn,
            User,
            EnterDay
         });

        */
            #endregion
        }

        private void BomOfferList_Load(object sender, EventArgs e)
        {
            if (this.bomOfferType==BomOfferTypeEnum.Excess)
                this.Text = "Excess List";
            else if(this.bomOfferType==BomOfferTypeEnum.BOM)
                this.Text = "BOMs List";
            else
                this.Text="L/T Offer List";

            if (UserInfo.Job != JobDescription.Admin && UserInfo.Job != JobDescription.Boss)
            {
                tsbToExcel.Enabled = false;
                tsbDeleteItem.Enabled = false;
            }
            tscbFilterBy.Items.Clear();
            if (listbyCustVen)
            {
                tscbFilterBy.Items.Add("MPN");
            }
            else
            {
                tscbFilterBy.Items.Add("MPN");
                if (this.bomOfferType == BomOfferTypeEnum.Excess)
                    tscbFilterBy.Items.Add("Vendor Name");
                else if (this.bomOfferType == BomOfferTypeEnum.BOM)
                    tscbFilterBy.Items.Add("Customer Name");
                else
                    tscbFilterBy.Items.Add("Company Name");
            }
            tscbFilterBy.Items.Add("Date");
           

        }

        private void tsbDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Delete the selected item?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            DataGridViewSelectedRowCollection dgvrrc = dataGridView1.SelectedRows;
            int bomOfferId;

            using (BomOfferEntities entity = new BomOfferEntities())
            {
                foreach (DataGridViewRow dgvr in dgvrrc)
                {
                    bomOfferId = Convert.ToInt32(dgvr.Cells["Id"].Value);
                    var bomOfferItem = entity.publicbomoffer.First(item => item.bomOfferId == bomOfferId);
                    entity.publicbomoffer.DeleteObject(bomOfferItem);
                }
                entity.SaveChanges();
            }
            if (listAll)
            {
                tsbListAll_Click(this, null);
            }
            else
            {
                tsbSearch_Click(this, null);
            }



        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            tscbFilterBy.SelectedIndex = -1;
            tstbFilterString.Text = string.Empty;

            dataGridView1.DataSource = null;
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            listAll = false;
            if ((tscbFilterBy.Text.Trim().Length == 0)|| (tstbFilterString.Text.Trim().Length == 0))
                return;

            if (((tscbFilterBy.Text.Trim() == "Vendor Name") || (tscbFilterBy.Text.Trim() == "Customer Name")||(tscbFilterBy.Text.Trim()=="Company Name"))
                && (tstbFilterString.Text.Trim().Length != 0))
            {
                if (listbyCustVen)
                {
                    return;
                }
                using (BomOfferEntities entity = new BomOfferEntities())
                {
                    this.dataGridView1.DataSource = null;
                    if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                    {
                        var bomOfferList = from bomOffer in entity.publicbomoffer
                                           join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                           join a in entity.account on bomOffer.userID equals a.id
                                           where custVen.custVendorType == (int)this.bomOfferType && (custVen.custVenName.Contains(tstbFilterString.Text.Trim()))
                                           orderby bomOffer.bomOfferId descending
                                           select new
                                           {
                                               Id = bomOffer.bomOfferId,
                                               Company = custVen.custVenName,
                                               MFG = bomOffer.mfg,
                                               MPN = bomOffer.mpn,
                                               Qty = bomOffer.qty,
                                               Price = bomOffer.price,
                                               CPN = bomOffer.cpn,
                                               Owner = a.accountName,
                                               Enterday = bomOffer.enerDay
                                           };
                        this.dataGridView1.DataSource = bomOfferList;
                        this.dataGridView1.Columns[0].Visible = false;
                    }
                    else
                    {
                        var bomOfferList = from bomOffer in entity.publicbomoffer
                                           join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                           join a in entity.account on bomOffer.userID equals a.id
                                           where custVen.custVendorType == (int)this.bomOfferType && (custVen.custVenName.Contains(tstbFilterString.Text.Trim()))
                                           orderby bomOffer.bomOfferId descending
                                           select new
                                           {
                                               Id = bomOffer.bomOfferId,
                                               Company = "OEM",
                                               MFG = bomOffer.mfg,
                                               MPN = bomOffer.mpn,
                                               Qty = bomOffer.qty,
                                               Price = bomOffer.price,
                                               CPN = "OEM",
                                               Owner = a.accountName,
                                               Enterday = bomOffer.enerDay
                                           };
                        this.dataGridView1.DataSource = bomOfferList;
                        this.dataGridView1.Columns[0].Visible = false;

                    }
                }
            
            
            }
            else if ((tscbFilterBy.Text.Trim() == "MPN") && (tstbFilterString.Text.Trim().Length != 0))
            {
                using (BomOfferEntities entity = new BomOfferEntities())
                {
                    if (listbyCustVen)
                    {


                        this.dataGridView1.DataSource = null;
                        if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim())
                                               && (custVen.custVenId == this.custVenId))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = custVen.custVenName,
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = bomOffer.cpn,
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }
                        else
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim())
                                               && (custVen.custVenId == this.custVenId))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = "OEM",
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = "OEM",
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        
                        }


                    }
                    else
                    {

                        this.dataGridView1.DataSource = null;
                        if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim()))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = custVen.custVenName,
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = bomOffer.cpn,
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }
                        else
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim()))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = "OEM",
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = "OEM",
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }
                    }
                }

            }

            else if ((tscbFilterBy.Text.Trim() == "Date") && (tstbFilterString.Text.Trim().Length != 0))
            {
                string[] date = AmbleClient.RfqGui.RfqManager.RfqMgr.GetStartDateAndEndDate(tstbFilterString.Text.Trim());
                if (string.IsNullOrWhiteSpace(date[0]) || string.IsNullOrWhiteSpace(date[1]))
                {
                    MessageBox.Show("Format error in the Filter String");
                    return;
                }
                DateTime startDate,endDate;

                try{
                   startDate=DateTime.Parse(date[0]);
                   endDate=DateTime.Parse(date[1]);  
                }
                catch
                {
                    MessageBox.Show("Format error in the Filter String");
                    return;
                }



                using (BomOfferEntities entity = new BomOfferEntities())
                {
                    if (listbyCustVen)
                    {
                        this.dataGridView1.DataSource = null;
                        if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where (custVen.custVendorType == (int)this.bomOfferType && ((bomOffer.enerDay.HasValue) && (bomOffer.enerDay.Value > startDate) && (bomOffer.enerDay.Value < endDate))
                                               && (custVen.custVenId == this.custVenId))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = custVen.custVenName,
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = bomOffer.cpn,
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }
                        else
                        {
                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where (custVen.custVendorType == (int)this.bomOfferType && ((bomOffer.enerDay.HasValue) && (bomOffer.enerDay.Value > startDate) && (bomOffer.enerDay.Value < endDate))
                                               && (custVen.custVenId == this.custVenId))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = "OEM",
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = "OEM",
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }


                    }
                    else
                    {

                        this.dataGridView1.DataSource = null;
                        if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                        {

                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && ((bomOffer.enerDay.HasValue) && (bomOffer.enerDay.Value > startDate) && (bomOffer.enerDay.Value < endDate))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = custVen.custVenName,
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = bomOffer.cpn,
                                                   Owner = a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            this.dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        }
                        else
                        {

                            var bomOfferList = from bomOffer in entity.publicbomoffer
                                               join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                               join a in entity.account on bomOffer.userID equals a.id
                                               where custVen.custVendorType == (int)this.bomOfferType && ((bomOffer.enerDay.HasValue) && (bomOffer.enerDay.Value > startDate) && (bomOffer.enerDay.Value < endDate))
                                               orderby bomOffer.bomOfferId descending
                                               select new
                                               {
                                                   Id = bomOffer.bomOfferId,
                                                   Company = "OEM",
                                                   MFG = bomOffer.mfg,
                                                   MPN = bomOffer.mpn,
                                                   Qty = bomOffer.qty,
                                                   Price = bomOffer.price,
                                                   CPN = "OEM",
                                                   Owner =a.accountName,
                                                   Enterday = bomOffer.enerDay
                                               };
                            this.dataGridView1.DataSource = bomOfferList;
                            this.dataGridView1.Columns[0].Visible = false;
                        
                        }
                    }
                }





            }

            else
            { }



        }

        private void tsbToExcel_Click(object sender, EventArgs e)
        {

            DataTable dt;
            //generate datatable according to DataGridView
            try
            {
              dt=GridView2DataTable(dataGridView1);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                MessageBox.Show("Met some errors while generating the Excel format file ");
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
              saveFileDialog1.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            //以文件“*.xls”导出

              List<string> columnName = new List<string>();
              foreach (DataColumn column in dt.Columns)
              {
                  columnName.Add(column.ColumnName);
              
              }



              if (saveFileDialog1.ShowDialog() == DialogResult.OK)
              {
                  AmbleClient.ExcelHelper.ExcelHelper.Export(dt,Enum.GetName(typeof(BomOfferTypeEnum),this.bomOfferType)/*this.isOffer?"Offer":"BOM"*/, saveFileDialog1.FileName, "WorkSheet1",columnName.ToArray(),columnName.ToArray());
              }


        }
    
         private  DataTable GridView2DataTable(DataGridView gv)
        {
            DataTable table = new DataTable();
            if (gv.Rows.Count==0 && gv.Columns.Count == 0)
            {
                return table;
            }
            int columnCount = gv.Columns.Count;
            for (int i = 1; i < columnCount; i++)//omit the id column
            {
                string text = gv.Columns[i].HeaderText;
                table.Columns.Add(text);
              
            }
            foreach (DataGridViewRow r in gv.Rows)
            {
                    DataRow row = table.NewRow();
                    int j = 0;
                    for (int i = 1; i < columnCount; i++) //i=1 means omit the id column
                    {
                        object cellValue=r.Cells[i].Value;
                       /*
                        Type t = r.Cells[i].ValueType;

                        if (t==typeof(int))
                        {
                            row[j] = (int)cellValue ;
                        }
                        else if (t==typeof(short))
                        {
                            row[j] = (short)cellValue;
                        }
                        else if (t == typeof(float))
                        {
                            row[j] = (float)cellValue;
                        }
                        else
                        {*/
                        if (cellValue == null)
                        {
                            row[j] = "";
                        }
                        else
                        {
                            row[j] = cellValue.ToString();
                        }
                       // }
                            j++;
                  
                    }
                    table.Rows.Add(row);
            }
            return table;
     }

         private void tscbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (tscbFilterBy.SelectedItem!=null&&tscbFilterBy.SelectedItem.ToString() == "Date")
             {
                 basicGui.TimePicker tp = new basicGui.TimePicker();
                 tp.ShowDialog();
                 tstbFilterString.Text = tp.FromTo;
             }

         }

         private void tsbListAll_Click(object sender, EventArgs e)
         {
             listAll = true;
             dataGridView1.DataSource = null;
             using (BomOfferEntities entity = new BomOfferEntities())
             {
                 if (listbyCustVen)
                 {
                     

                     if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                     {
                         var bomOfferList = from bomOffer in entity.publicbomoffer
                                            join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                            join a in entity.account on bomOffer.userID equals a.id
                                            where custVen.custVendorType == (int)this.bomOfferType && custVen.custVenId == this.custVenId
                                            orderby bomOffer.bomOfferId descending
                                            select new
                                            {
                                                Id = bomOffer.bomOfferId,
                                                Company = custVen.custVenName,
                                                MFG = bomOffer.mfg,
                                                MPN = bomOffer.mpn,
                                                Qty = bomOffer.qty,
                                                Price = bomOffer.price,
                                                Owner = a.accountName,
                                                CPN = bomOffer.cpn,
                                                Enterday = bomOffer.enerDay
                                            };
                         dataGridView1.DataSource = bomOfferList;
                         this.dataGridView1.Columns[0].Visible = false;

                     }
                     else
                     {
                         var bomOfferList = from bomOffer in entity.publicbomoffer
                                            join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                            join a in entity.account on bomOffer.userID equals a.id
                                            where custVen.custVendorType == (int)this.bomOfferType && custVen.custVenId == this.custVenId
                                            orderby bomOffer.bomOfferId descending
                                            select new
                                            {
                                                Id = bomOffer.bomOfferId,
                                                Company = "OEM",
                                                MFG = bomOffer.mfg,
                                                MPN = bomOffer.mpn,
                                                Qty = bomOffer.qty,
                                                Price = bomOffer.price,
                                                Owner = a.accountName,
                                                CPN = "OEM",
                                                Enterday = bomOffer.enerDay
                                            };
                         dataGridView1.DataSource = bomOfferList;
                         this.dataGridView1.Columns[0].Visible = false;
                     }


                 }

                 else
                 {
                    if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
                     {

                         var bomOfferList = from bomOffer in entity.publicbomoffer
                                            join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                            join a in entity.account on bomOffer.userID equals a.id
                                            where custVen.custVendorType == (int)this.bomOfferType
                                            orderby bomOffer.bomOfferId descending
                                            select new
                                            {
                                                Id = bomOffer.bomOfferId,
                                                Company = custVen.custVenName,
                                                MFG = bomOffer.mfg,
                                                MPN = bomOffer.mpn,
                                                Qty = bomOffer.qty,
                                                Price = bomOffer.price,
                                                CPN = bomOffer.cpn,
                                                Ower=a.accountName,
                                                Enterday = bomOffer.enerDay
                                            };

                         dataGridView1.DataSource = bomOfferList;
                         this.dataGridView1.Columns[0].Visible = false;
                        
                       
                     }
                     else
                     {
                         var bomOfferList = from bomOffer in entity.publicbomoffer
                                            join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                            join a in entity.account on bomOffer.userID equals a.id
                                            where custVen.custVendorType == (int)this.bomOfferType
                                            orderby bomOffer.bomOfferId descending
                                            select new
                                            {
                                                Id = bomOffer.bomOfferId,
                                                Company = "OEM",
                                                MFG = bomOffer.mfg,
                                                MPN = bomOffer.mpn,
                                                Qty = bomOffer.qty,
                                                Price = bomOffer.price,
                                                CPN = "OEM",
                                                Owner = a.accountName,
                                                Enterday = bomOffer.enerDay
                                            };
                         

                         //AllAccountInfo.GetNameAccordingToId
                         dataGridView1.DataSource = bomOfferList;
                         this.dataGridView1.Columns[0].Visible = false;
                     }

                 }

             }
         }

         private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
         {

         }


   
    }
}
