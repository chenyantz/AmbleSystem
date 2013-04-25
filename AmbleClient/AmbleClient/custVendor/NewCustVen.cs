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
            if (cvtype == 0)
            {
                this.Text = "Add a Customer";
            }
            if (cvtype == 1)
            {
                this.Text = "Add a Vendor";
            }

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (false == custVenInfoControl1.ValidateValues())
                return;
            custvendorinfo cvInfo = custVenInfoControl1.GetValues();
            //to check the name if already exist

            if (CustVendorManager.CustVenInfoManager.IsCvNameExist(cvInfo.cvtype, cvInfo.cvname, UserInfo.UserId))
            {
                MessageBox.Show(cvInfo.cvname + " alreasy Exist.");
                return;
            
            }

            cvInfo.lastUpdateDate = DateTime.Now;
            cvInfo.lastUpdateName = (short)UserInfo.UserId;
            cvInfo.ownerName = (short)UserInfo.UserId;
            CustVendorManager.CustVenInfoManager.AddCustVend(cvInfo);
            this.DialogResult = DialogResult.Yes;

            this.Close();
        }




    }
}
