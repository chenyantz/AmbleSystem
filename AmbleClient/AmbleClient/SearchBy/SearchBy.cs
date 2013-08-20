using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.SearchBy
{
    public partial class SearchBy : Form
    {
        bool searchByMpn;
        public string searchString;
        
        public SearchBy(bool mpn)
        {
            InitializeComponent();
            this.searchByMpn = mpn;
            if (this.searchByMpn)
            {
                label1.Text = "         MPN:";
            }
            else
            {
                label1.Text = "Company Name:";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.searchString = textBox1.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
