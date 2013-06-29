using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AmbleClient.BomOffer
{
    public partial class MatchedBom : Form
    {
        DataClass.DataBase db = new DataClass.DataBase();

        public MatchedBom()
        {
            InitializeComponent();
            if (UserInfo.Job!=JobDescription.Admin&&UserInfo.Job!=JobDescription.Boss)
            {
                tsbSave.Enabled = false;
                tsbRestore.Enabled = false;
                tsbImport.Enabled = false;

                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.AllowUserToDeleteRows = false;
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“matchedBomDataSet.matchbom”中。您可以根据需要移动或删除它。
            this.matchbomTableAdapter.Fill(this.matchedBomDataSet.matchbom);

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.CurrentCell = null;
                this.matchedBomDataSet.AcceptChanges();
                this.matchbomTableAdapter.Update(this.matchedBomDataSet);
            }
            catch 
            {
              
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void tsbImport_Click(object sender, EventArgs e)
        {

             OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;

            if (DialogResult.OK == ofd.ShowDialog())
            {
                DataTable dt = ExcelHelper.ExcelHelper.Import(ofd.FileName);
                if (dt.Rows.Count == 0)
                    return;

                bool hasCustomer=false,hasMpn = false, hasMfg = false, hasQty = false, hasPrice=false,hasCpn=false,hasBuyer = false;
                int customerColumn = -1, mpnColumn = -1, mfgColumn = -1, qtyColumn = -1, priceColumn = -1,cpnColumn = -1, buyerColumn = -1;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Trim().ToUpper() == "CUSTOMER")
                    {
                        hasCustomer = true;
                        customerColumn = dt.Columns.IndexOf(dc);
                    }

                    if (dc.ColumnName.Trim().ToUpper() == "MPN")
                    {
                        hasMpn = true;
                        mpnColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "MFG")
                    {
                        hasMfg = true;
                        mfgColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "QTY")
                    {
                        hasQty = true;
                        qtyColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "PRICE")
                    {
                        hasPrice = true;
                        priceColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "CPN")
                    {
                        hasCpn = true;
                        cpnColumn = dt.Columns.IndexOf(dc);
                    }
                    
                    if (dc.ColumnName.Trim().ToUpper() == "BUYER")
                    {
                        hasBuyer = true;
                        buyerColumn = dt.Columns.IndexOf(dc);
                    }
                }
                if (false == (hasCustomer && hasMpn && hasMfg && hasQty && hasPrice && hasCpn && hasBuyer))
                {
                    MessageBox.Show("Please check the xls File Column.(CUSTOMER,MFG,MPN,QTY,PRICE,CPN,BUYER)");
                    return;
                }

                int i = 1;

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into matchBom(customer,mfg,mpn,qty,price,cpn,buyer,date) values ");

                foreach (DataRow dr in dt.Rows)
                {

                    //Qty omit the ","
                    string qtyString = dr[qtyColumn].ToString();
                    qtyString = qtyString.Replace(",", string.Empty);

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

                    sb.AppendFormat("('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}'),", dr[customerColumn].ToString(), dr[mfgColumn].ToString(), dr[mpnColumn].ToString(),
                        qtyLocal.HasValue? qtyLocal.Value.ToString():"null", priceLocal.HasValue? priceLocal.Value.ToString():"null",dr[cpnColumn].ToString(),dr[buyerColumn].ToString(),DateTime.Now.ToString());

                }
                string sql=sb.ToString();
                sql=sql.Substring(0,sql.Length-1);
                db.ExecDataBySql(sql);



            }
            Stock_Load(this, null);
        }

        private void tsbRestore_Click(object sender, EventArgs e)
        {
            this.matchedBomDataSet.RejectChanges();
            Stock_Load(this, null);


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(tscbFilterBy.Text.Trim()=="MPN" &&!(string.IsNullOrWhiteSpace(tstbFilterString.Text.Trim())))
            {
             bindingSource1.Filter=string.Format("mpn like '%{0}%'",tstbFilterString.Text.Trim());
                       
            }
            if (tscbFilterBy.Text.Trim() == "Customer" && !(string.IsNullOrWhiteSpace(tstbFilterString.Text.Trim())))
            {
                bindingSource1.Filter = string.Format("customer like '%{0}%'", tstbFilterString.Text.Trim());

            }


        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            this.tscbFilterBy.SelectedIndex = -1;
            this.tstbFilterString.Text = "";
            
            this.dataGridView1.CurrentCell = null;
            bindingSource1.Filter = string.Empty;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Please check the input values");
        }
    }
}
