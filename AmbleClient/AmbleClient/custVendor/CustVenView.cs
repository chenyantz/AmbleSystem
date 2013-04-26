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
        
        public CustVenView(custvendorinfo info)
        {
            this.info = info;
            InitializeComponent();
            if (info.cvtype == 0)
                this.Text = "Customer Info";
            else
                this.Text = "Vendor Info";


        }

        private void CustVenView_Load(object sender, EventArgs e)
        {

            custVenInfoControl1.FillTheTextboxs(info);
            custVenInfoFinancialControl1.FillTheInfo(info);

        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            //first get values from info control and then from financialControl
            if (custVenInfoControl1.ValidateValues() == false)
                return;
            custvendorinfo cvinfo = custVenInfoControl1.GetValues();
            cvinfo.cvId = info.cvId;
            cvinfo.cvtype = info.cvtype;
            cvinfo.ownerName = info.ownerName;

            custvendorinfo cvinfo2 = custVenInfoFinancialControl1.GetValues();
            cvinfo.paymentTerm = cvinfo2.paymentTerm;
            cvinfo.shippingTerm = cvinfo2.shippingTerm;
            cvinfo.billTo = cvinfo2.billTo;
            cvinfo.cvnumber=cvinfo2.cvnumber;
            CustVendorManager.CustVenInfoManager.UpateCvInfo(cvinfo);

            if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss || UserInfo.Job == JobDescription.FinancialManager || UserInfo.Job == JobDescription.Financial)
            {
                CustVendorManager.CustVenInfoManager.UpdateCvShipInfo(info.cvId, custVenInfoFinancialControl1.getCurrentShipTo());
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

    }
}
