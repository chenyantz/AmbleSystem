using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.custVendor
{
    
    
    
    public partial class CustVenView : Form
    {
        custvendorinfo info;
        
        public CustVenView()
        {
            InitializeComponent();
        }

        public CustVenView(custvendorinfo info)
        {
            this.info = info;
            InitializeComponent();
        }

        private void CustVenView_Load(object sender, EventArgs e)
        {

            custVenInfoControl1.FillTheTextboxs(info);
            custVenInfoFinancialControl1.FillTheInfo(info);

        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {




        }






    }
}
