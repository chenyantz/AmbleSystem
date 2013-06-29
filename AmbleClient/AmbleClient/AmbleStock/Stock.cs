using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AmbleClient.AmbleStock
{
    public partial class MatchedBom : Form
    {
        DataClass.DataBase db = new DataClass.DataBase();

        public MatchedBom()
        {
            InitializeComponent();
            if (!UserCombine.GetuserCanBeLogistics().Contains((int)UserInfo.Job))
            {
                tsbImport.Enabled = false;
                tsbAdd.Enabled = false;
                tsbDelete.Enabled = false;

            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            this.amblestockTableAdapter.Fill(this.stockDataSet1.amblestock);
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

                bool hasDc = false, hasMpn = false, hasMfg = false, hasQty = false, hasCost = false, hasResale = false, hasStatus = false, hasNotes = false, hasStockDate = false, hasPacking = false, hasContact = false;
                int dcColumn = -1, mpnColumn = -1, mfgColumn = -1, qtyColumn = -1, costColumn = -1, resaleColumn = -1, statusColumn = -1, notesColumn = -1, stockDateColumn = -1, packingColumn = -1, contactColumn = -1;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.Trim().ToUpper() == "MPN")
                    {
                        hasMpn = true;
                        mpnColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "DC" || dc.ColumnName.Trim().ToUpper() == "D/C")
                    {
                        hasDc = true;
                        dcColumn = dt.Columns.IndexOf(dc);
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
                    if (dc.ColumnName.Trim().ToUpper() == "COST")
                    {
                        hasCost = true;
                        costColumn = dt.Columns.IndexOf(dc);
                    }

                    if (dc.ColumnName.Trim().ToUpper() == "RESALE")
                    {
                        hasResale = true;
                        resaleColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "STATUS")
                    {
                        hasStatus = true;
                        statusColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "NOTES")
                    {
                        hasNotes = true;
                        notesColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "DATE")
                    {
                        hasStockDate = true;
                        stockDateColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "PACKING")
                    {
                        hasPacking = true;
                        packingColumn = dt.Columns.IndexOf(dc);
                    }
                    if (dc.ColumnName.Trim().ToUpper() == "CONTACT")
                    {
                        hasContact = true;
                        contactColumn = dt.Columns.IndexOf(dc);
                    }
                }
                if (false == (hasCost && hasMpn && hasDc && hasMfg && hasQty && hasCost && hasResale && hasStatus && hasNotes && hasStockDate && hasPacking && hasContact))
                {
                    MessageBox.Show("Please check the xls File Column.(DC,MPN,MFG,QTY,COST,RESALE,STATUS,NOTES,DATE,PACKING,CONTACT)");
                    return;
                }

                int i = 1;

                StringBuilder sb = new StringBuilder();
                sb.Append("insert into ambleStock(mpn,mfg,dc,qty,resale,cost,packing,contact,statu,notes,stockDate) values ");

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
                    if (!string.IsNullOrWhiteSpace(dr[resaleColumn].ToString()) && (!ItemsCheck.CheckFloatNumber(dr[resaleColumn])))
                    {


                        MessageBox.Show("The Resale value is not correct in row " + i.ToString());
                        return;
                    }
                    if (!string.IsNullOrWhiteSpace(dr[costColumn].ToString()) && (!ItemsCheck.CheckFloatNumber(dr[costColumn])))
                    {


                        MessageBox.Show("The Cost value is not correct in row " + i.ToString());
                        return;
                    }


                    int? qtyLocal;
                    float? resaleLocal, costLocal;

                    if (string.IsNullOrWhiteSpace(qtyString))
                    {
                        qtyLocal = null;
                    }
                    else
                    {
                        qtyLocal = Convert.ToInt32(qtyString);
                    }

                    if (string.IsNullOrWhiteSpace(dr[resaleColumn].ToString()))
                    {
                        resaleLocal = null;
                    }
                    else
                    {
                        resaleLocal = Convert.ToSingle(dr[resaleColumn]);
                    }

                    if (string.IsNullOrWhiteSpace(dr[costColumn].ToString()))
                    {
                        costLocal = null;
                    }
                    else
                    {
                        costLocal = Convert.ToSingle(dr[costColumn]);
                    }


                    sb.AppendFormat("('{0}','{1}','{2}',{3},{4},{5},'{6}','{7}','{8}','{9}','{10}'),", dr[mpnColumn].ToString(), dr[mfgColumn].ToString(), dr[dcColumn].ToString(),
                        qtyLocal.HasValue ? qtyLocal.Value.ToString() : "null", resaleLocal.HasValue ? resaleLocal.Value.ToString() : "null", costLocal.HasValue ? costLocal.Value.ToString() : "null", dr[packingColumn].ToString(), dr[contactColumn].ToString(), dr[statusColumn].ToString(), dr[notesColumn].ToString(), dr[stockDateColumn].ToString());

                }
                string sql = sb.ToString();
                sql = sql.Substring(0, sql.Length - 1);
                db.ExecDataBySql(sql);



            }
            Stock_Load(this, null);
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tscbFilterBy.Text.Trim() == "MPN" && !(string.IsNullOrWhiteSpace(tstbFilterString.Text.Trim())))
            {
                bindingSource1.Filter = string.Format("mpn like '%{0}%'", tstbFilterString.Text.Trim());

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

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Delete the selected items?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            DataGridViewSelectedRowCollection dgvrrc = dataGridView1.SelectedRows;
            List<int> deletedIds = new List<int>();

            foreach (DataGridViewRow dgvr in dgvrrc)
            {
                deletedIds.Add(Convert.ToInt32(dgvr.Cells["stockIdDataGridViewTextBoxColumn"].Value));
            }
            List<string> strSqls = new List<string>();

            foreach (int id in deletedIds)
            {
                strSqls.Add("delete from ambleStock where stockId=" + id);
            }
            db.ExecDataBySqls(strSqls);
            Stock_Load(this, null);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            StockOp op = new StockOp(true);
            if (DialogResult.OK == op.ShowDialog())
            {
                Stock_Load(this, null);
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.RowIndex > dataGridView1.RowCount - 1)
                    return;

                //get the real index.
                int stockId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                AmbleStock.stockDataSet.amblestockRow selectedRow = null;

                foreach (AmbleStock.stockDataSet.amblestockRow row in this.stockDataSet1.amblestock.Rows)
                {
                    if (row.stockId == stockId)
                    {
                        selectedRow = row;
                        break;
                    }

                }
                if (selectedRow != null)
                {
                    StockOp op = new StockOp(false);
                    op.FillTheTable(selectedRow);
                    if (DialogResult.OK == op.ShowDialog())
                    {
                        Stock_Load(this, null);

                    }
                }



            }
        }

    }
}