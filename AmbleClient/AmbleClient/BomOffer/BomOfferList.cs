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
    public partial class BomOfferList : Form
    {
         bool isOffer;
         bool listbyCustVen = false;
         int custVenId;

        public BomOfferList(bool isOffer)
        {
            InitializeComponent();
            this.isOffer = isOffer;
            FillTheDataGridColumn();
        }
        public BomOfferList(bool isOffer, int custVenId)
            : this(isOffer)
        {
            this.listbyCustVen = true;
            this.custVenId = custVenId;
        }

        private void FillTheDataGridColumn()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn Id = new System.Windows.Forms.DataGridViewTextBoxColumn();


            Id.Name = "Id";
            Id.Visible = false;



            System.Windows.Forms.DataGridViewTextBoxColumn Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mfg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Mpn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn Cpn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            if (isOffer)
                Company.HeaderText = "Vendor";
            else
                Company.HeaderText = "Customer";
            Company.Name = "Compnay";

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

            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            Id,
            Company,
            Mfg,
            Mpn,
            Qty,
            Price,
            Cpn
         });

        
        }




        private void BomOfferList_Load(object sender, EventArgs e)
        {
            if (isOffer)
                this.Text = "Offers List";
            else
                this.Text = "BOMs List";

            tscbFilterBy.Items.Clear();
            if (listbyCustVen)
            {
                tscbFilterBy.Items.Add("MPN");
            }
            else
            {
                tscbFilterBy.Items.Add("MPN");
                if (isOffer)
                    tscbFilterBy.Items.Add("Vendor Name");
                else
                    tscbFilterBy.Items.Add("Customer Name");
            }

            this.dataGridView1.Rows.Clear();
            using (BomOfferEntities entity = new BomOfferEntities())
            {
                if (listbyCustVen)
                {
                    var bomOfferList = from bomOffer in entity.publicbomoffer
                                       join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                       where custVen.custVendorType == (isOffer ? 1 : 0) && custVen.custVenId==this.custVenId
                                       select new
                                       {
                                          Id = bomOffer.BomCustVendId,
                                           Company = custVen.custVenName,
                                           MFG = bomOffer.mfg,
                                           MPN = bomOffer.mpn,
                                           Qty = bomOffer.qty,
                                           Price = bomOffer.price,
                                           CPN = bomOffer.cpn
                                       };

                    foreach (var bomOffer in bomOfferList)
                    {
                        dataGridView1.Rows.Add(bomOffer.Id,bomOffer.Company, bomOffer.MFG, bomOffer.MPN, bomOffer.Qty, bomOffer.Price, bomOffer.CPN);
                    }

                }

                else
                {
                    var bomOfferList = from bomOffer in entity.publicbomoffer
                                       join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                       where custVen.custVendorType == (isOffer ? 1 : 0)
                                       select new
                                       {
                                           Id=bomOffer.BomCustVendId,
                                           Company = custVen.custVenName,
                                           MFG = bomOffer.mfg,
                                           MPN = bomOffer.mpn,
                                           Qty = bomOffer.qty,
                                           Price = bomOffer.price,
                                           CPN = bomOffer.cpn
                                       };

                    foreach (var bomOffer in bomOfferList)
                    {
                        dataGridView1.Rows.Add(bomOffer.Id,bomOffer.Company, bomOffer.MFG, bomOffer.MPN, bomOffer.Qty, bomOffer.Price, bomOffer.CPN);
                    }
                }

            }

        }

        private void tsbDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Delete the selected item?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

           DataGridViewRow dgvr = dataGridView1.SelectedRows[0];
           int bomOfferId = Convert.ToInt32(dgvr.Cells["Id"].Value);


            using (BomOfferEntities entity = new BomOfferEntities())
            {
                var bomOfferItem = entity.publicbomoffer.First(item => item.BomCustVendId == bomOfferId);
                entity.DeleteObject(bomOfferItem);
                entity.SaveChanges();
            }
            BomOfferList_Load(this,null);



        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            tscbFilterBy.SelectedIndex = -1;
            tstbFilterString.Text = string.Empty;
            
            BomOfferList_Load(this, null);
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            if ((tscbFilterBy.Text.Trim().Length == 0)|| (tstbFilterString.Text.Trim().Length == 0))
                return;

            if (((tscbFilterBy.Text.Trim() == "Vendor Name") || (tscbFilterBy.Text.Trim() == "Customer Name")) && (tstbFilterString.Text.Trim().Length != 0))
            {
                if (listbyCustVen)
                {
                    return;
                }
                using (BomOfferEntities entity = new BomOfferEntities())
                {
                    var bomOfferList = from bomOffer in entity.publicbomoffer
                                       join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                       where custVen.custVendorType == (isOffer ? 1 : 0) &&(custVen.custVenName.Contains(tstbFilterString.Text.Trim()))
                                       select new
                                       {
                                          Id = bomOffer.BomCustVendId,
                                           Company = custVen.custVenName,
                                           MFG = bomOffer.mfg,
                                           MPN = bomOffer.mpn,
                                           Qty = bomOffer.qty,
                                           Price = bomOffer.price,
                                           CPN = bomOffer.cpn
                                       };

                    this.dataGridView1.Rows.Clear();
                    foreach (var bomOffer in bomOfferList)
                    {
                        dataGridView1.Rows.Add(bomOffer.Id,bomOffer.Company, bomOffer.MFG, bomOffer.MPN, bomOffer.Qty, bomOffer.Price, bomOffer.CPN);
                    }

                }
            
            
            }
            if ((tscbFilterBy.Text.Trim() == "MPN") && (tstbFilterString.Text.Trim().Length != 0))
            {
                using (BomOfferEntities entity = new BomOfferEntities())
                {
                    if (listbyCustVen)
                    {
                        var bomOfferList = from bomOffer in entity.publicbomoffer
                                           join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                           where custVen.custVendorType == (isOffer ? 1 : 0) && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim())
                                           && (custVen.custVenId == this.custVenId))
                                           select new
                                           {
                                               Id = bomOffer.BomCustVendId,
                                               Company = custVen.custVenName,
                                               MFG = bomOffer.mfg,
                                               MPN = bomOffer.mpn,
                                               Qty = bomOffer.qty,
                                               Price = bomOffer.price,
                                               CPN = bomOffer.cpn
                                           };

                        this.dataGridView1.Rows.Clear();
                        foreach (var bomOffer in bomOfferList)
                        {
                            dataGridView1.Rows.Add(bomOffer.Id,bomOffer.Company, bomOffer.MFG, bomOffer.MPN, bomOffer.Qty, bomOffer.Price, bomOffer.CPN);
                        }



                    }
                    else
                    {


                        var bomOfferList = from bomOffer in entity.publicbomoffer
                                           join custVen in entity.publiccustven on bomOffer.BomCustVendId equals custVen.custVenId
                                           where custVen.custVendorType == (isOffer ? 1 : 0) && (bomOffer.mpn.Contains(tstbFilterString.Text.Trim()))
                                           select new
                                           {
                                               Id = bomOffer.BomCustVendId,
                                               Company = custVen.custVenName,
                                               MFG = bomOffer.mfg,
                                               MPN = bomOffer.mpn,
                                               Qty = bomOffer.qty,
                                               Price = bomOffer.price,
                                               CPN = bomOffer.cpn
                                           };

                        this.dataGridView1.Rows.Clear();
                        foreach (var bomOffer in bomOfferList)
                        {
                            dataGridView1.Rows.Add(bomOffer.Id,bomOffer.Company, bomOffer.MFG, bomOffer.MPN, bomOffer.Qty, bomOffer.Price, bomOffer.CPN);
                        }

                    }
                }

            }




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
                  AmbleClient.ExcelHelper.ExcelHelper.Export(dt, this.isOffer?"Offer":"BOM", saveFileDialog1.FileName, "WorkSheet1",columnName.ToArray(),columnName.ToArray());
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
                            row[j] = cellValue.ToString();

                       // }
                            j++;
                  
                    }
                    table.Rows.Add(row);
            }
            return table;
     }


     


    
    
    
    }
}
