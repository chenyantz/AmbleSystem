using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.AmbleStock
{
    public partial class StockOp : Form
    {
        bool isNewStockItem;
        int? stockId=null;

        DataClass.DataBase db = new DataClass.DataBase();

         public StockOp(bool isNewStockItem)
        {
            InitializeComponent();
            this.isNewStockItem = isNewStockItem;
            if (isNewStockItem)
            {
                this.Text = "Add a Stock Item";
            }
            else
            {
                this.Text = "Modify a Stock Item";
            }
        }

        private void StockOp_Load(object sender, EventArgs e)
        {

        }

        public void FillTheTable(AmbleClient.AmbleStock.stockDataSet.amblestockRow row)
        {
            this.stockId=row.stockId;
            tbMpn.Text = row.mpn;
            tbMfg.Text = row.mfg;
            tbDc.Text = row.dc;
            try
            {
                tbQty.Text = row.qty.ToString();
            }
            catch
            {
                tbQty.Text = "";
            }
            try
            {
                tbResale.Text = row.resale.ToString();
            }
            catch
            {
                tbResale.Text = "";
            }
            try
            {
                tbCost.Text = row.cost.ToString();
            }
            catch
            {
                tbCost.Text = "";
            }
            tbPacking.Text = row.packing;
            tbContact.Text = row.contact;
            cbStatus.Text = row.statu;
            tbNotes.Text = row.notes;
            tbDate.Text = row.stockDate;
        
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            tbDate.Text = dateTimePicker1.Value.ToString();

        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!ItemsCheck.CheckTextBoxEmpty(tbMpn))
            {
                MessageBox.Show("Please input the MPN");
                return;
            }

            if (ItemsCheck.CheckTextBoxEmpty(tbQty)&&!ItemsCheck.CheckIntNumber(tbQty))
            {
                MessageBox.Show("The QTY should be an integer value");
                tbQty.Focus();
                return;
            }

                if (ItemsCheck.CheckTextBoxEmpty(tbResale)&&!ItemsCheck.CheckFloatNumber(tbResale))
                {
                    MessageBox.Show("The Resale should be a float number");
                    tbResale.Focus();
                    return;

                }
                if (ItemsCheck.CheckTextBoxEmpty(tbCost) && !ItemsCheck.CheckFloatNumber(tbCost))
                {
                    MessageBox.Show("The Cost should be a float number");
                    tbCost.Focus();
                    return;

                }
                string sql;
                if (isNewStockItem)
                {
                    sql = string.Format("insert into ambleStock(mpn,mfg,dc,qty,resale,cost,packing,contact,statu,notes,stockDate) values ('{0}','{1}','{2}',{3},{4},{5},'{6}','{7}','{8}','{9}','{10}')",
                        tbMpn.Text.Trim(), tbMfg.Text.Trim(), tbDc.Text.Trim(),
                        string.IsNullOrWhiteSpace(tbQty.Text.Trim()) ? "null" : tbQty.Text.Trim(),
                        string.IsNullOrWhiteSpace(tbResale.Text.Trim()) ? "null" : tbResale.Text.Trim(),
                        string.IsNullOrWhiteSpace(tbCost.Text.Trim()) ? "null" : tbCost.Text.Trim(),
                        tbPacking.Text.Trim(),
                        tbContact.Text.Trim(),
                        cbStatus.Text.Trim(),
                        tbNotes.Text.Trim(),
                        tbDate.Text.Trim()
                        );
                }
                else
                {
                    sql = string.Format("update amblestock set mpn='{0}',mfg='{1}',dc='{2}',qty={3},resale={4},cost={5},packing='{6}',contact='{7}',statu='{8}',notes='{9}',stockDate='{10}' where stockId={11}",
                           tbMpn.Text.Trim(), tbMfg.Text.Trim(), tbDc.Text.Trim(),
                           string.IsNullOrWhiteSpace(tbQty.Text.Trim()) ? "null" : tbQty.Text.Trim(),
                           string.IsNullOrWhiteSpace(tbResale.Text.Trim()) ? "null" : tbResale.Text.Trim(),
                           string.IsNullOrWhiteSpace(tbCost.Text.Trim()) ? "null" : tbCost.Text.Trim(),
                           tbPacking.Text.Trim(),
                           tbContact.Text.Trim(),
                           cbStatus.Text.Trim(),
                           tbNotes.Text.Trim(),
                           tbDate.Text.Trim(),
                           this.stockId
                           );


                }

                db.ExecDataBySql(sql);
                this.DialogResult = DialogResult.OK;
                this.Close();

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
