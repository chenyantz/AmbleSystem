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
    public partial class MatchedBomNew : Form
    {
        DataClass.DataBase db = new DataClass.DataBase();


        public MatchedBomNew()
        {
            InitializeComponent();
            this.Text = "Add a New Matched BOM";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ItemsCheck.CheckTextBoxEmpty(tbCustomer))
            {
                MessageBox.Show("Please input the Customer Name");
                return;
            
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbMfg))
            {
                MessageBox.Show("Please input the MFG");
                return;

            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbMpn))
            {
                MessageBox.Show("Please input the MPN");
                return;
            }

            if (!ItemsCheck.CheckIntNumber(tbQty))
            {
                MessageBox.Show("The QTY should be an integer value");
                tbQty.Focus();
                return;
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbPrice))
            {
                MessageBox.Show("Please input the Price");
                return;

            }
            else
            {
                if (!ItemsCheck.CheckFloatNumber(tbPrice))
                {
                    MessageBox.Show("The Price should be a float number");
                    tbPrice.Focus();
                    return;

                }
            }

            string sql = string.Format("insert into matchBom(customer,mfg,mpn,qty,price,cpn,buyer,date) values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}')", tbCustomer.Text.Trim(),
                tbMfg.Text.Trim(), tbMpn.Text.Trim(), tbQty.Text.Trim(), tbPrice.Text.Trim(), tbCpn.Text.Trim(), tbBuyer.Text.Trim(), DateTime.Now.ToString());
            db.ExecDataBySql(sql);
            
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
