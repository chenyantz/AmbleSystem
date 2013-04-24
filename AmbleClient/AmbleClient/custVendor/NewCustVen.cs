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
    public partial class NewCustVen : Form
    {
        int cvtype;
        
        public NewCustVen(int cvtype)
        {
            this.cvtype = cvtype;
            InitializeComponent();
        }

        private void NewCustVen_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(1);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (false == custVenInfoControl1.ValidateValues())
                return;
            custvendorinfo cvInfo = custVenInfoControl1.GetValues();
            cvInfo.lastUpdateDate = DateTime.Now;
            cvInfo.lastUpdateName = (short)UserInfo.UserId;
            cvInfo.ownerName = (short)UserInfo.UserId;
            CustVendorManager.CustVenInfoManager.AddCustVend(cvInfo);

            this.Close();
        }




    }
}
