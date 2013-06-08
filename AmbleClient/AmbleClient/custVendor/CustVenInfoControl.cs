using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.custVendor
{
    public partial class CustVenInfoControl : UserControl
    {

       private int cvtype;/*0 customer, 1 vendor, 2 all*/

        public CustVenInfoControl()
        {
            InitializeComponent();
        }

        public CustVenInfoControl(int cvtype)
        {
            this.cvtype = cvtype;

            InitializeComponent();
            if (cvtype == 0)
            {
                label1.Text = " Customer Name:*";
                tbProductLine.Enabled = false;
            }
            if (cvtype == 1)
                label1.Text = "   Vendor Name:*";



        }

        
        public bool ValidateValues()
        {
            if (!ItemsCheck.CheckTextBoxEmpty(tbName))
            {
                if (cvtype == 0)
                    MessageBox.Show("Please input the Customer Name");
                if (cvtype == 1)
                    MessageBox.Show("Please input the Vendor Name");
                return false;
            }
            if (ItemsCheck.CheckTextBoxEmpty(tbRating))
            {
                if (!ItemsCheck.CheckIntNumber(tbRating))
                {
                    MessageBox.Show("The Rating Should be an Integer Value (0-100).");
                    tbRating.Focus();
                    return false;
                }
                if (Convert.ToInt32(tbRating.Text.Trim()) > 100 || Convert.ToInt32(tbRating.Text.Trim()) < 0)
                {
                    MessageBox.Show("The Rating should be 0-100");
                    tbRating.Focus();
                       return false;
                }
            }
            if(ItemsCheck.CheckTextBoxEmpty(tbAmount)&&(!ItemsCheck.CheckIntNumber(tbAmount)))
            {
               MessageBox.Show("The Amount should be an Integer Value.");
                tbAmount.Focus();
                return false;
            
            }

            return true;

        }
        public  void FillTheTextboxs(custvendorinfo cvinfo)
        {
            //Bind the info of Datarow to control
            tbName.Text = cvinfo.cvname;
            tbCountry.Text = cvinfo.country;
            tbRating.Text = cvinfo.rate.ToString();
            tbContact1.Text = cvinfo.contact1;
            tbContact2.Text = cvinfo.contact2;
            tbPhone1.Text = cvinfo.phone1;
            tbPhone2.Text = cvinfo.phone2;
            tbCell.Text = cvinfo.cellphone;
            tbFax.Text = cvinfo.fax;
            tbEmail1.Text = cvinfo.email1;
            tbEmail2.Text = cvinfo.email2;
            tbLastUpdateName.Text = cvinfo.lastUpdateName.HasValue?AmbleClient.Admin.AccountMgr.AccountMgr.GetNameById(cvinfo.lastUpdateName.Value):"";
            tbLastUpdateDate.Text = cvinfo.lastUpdateDate.HasValue?cvinfo.lastUpdateDate.Value.ToShortDateString():"";
            tbAmount.Text = cvinfo.amount.ToString();
            tbNotes.Text = cvinfo.notes;
            comboBox2.SelectedIndex = cvinfo.blacklisted.HasValue? cvinfo.blacklisted.Value : -1;
            tbProductLine.Text = cvinfo.productLine;
        }

        public custvendorinfo  GetValues()
        {
            custvendorinfo cvInfo = new custvendorinfo();
            cvInfo.cvtype = (sbyte)this.cvtype;
            cvInfo.cvname = tbName.Text.Trim();
            cvInfo.country = tbCountry.Text.Trim();
            if (tbRating.Text.Trim().Length == 0)
            {
                cvInfo.rate = null;
            }
            else
            {
                cvInfo.rate = Convert.ToSByte(tbRating.Text.Trim());
            }
            cvInfo.contact1 = tbContact1.Text.Trim();
            cvInfo.contact2 = tbContact2.Text.Trim();
            cvInfo.phone1 = tbPhone1.Text.Trim();
            cvInfo.phone2 = tbPhone2.Text.Trim();
            cvInfo.cellphone = tbCell.Text.Trim();
            cvInfo.fax = tbFax.Text.Trim();
            cvInfo.email1 = tbEmail1.Text.Trim();
            cvInfo.email2 = tbEmail2.Text.Trim();
            if (tbAmount.Text.Trim().Length == 0)
            {
                cvInfo.amount = null;
            }
            else
            {
                cvInfo.amount = Convert.ToInt32(tbAmount.Text.Trim());
            }
            cvInfo.notes = tbNotes.Text.Trim();
            cvInfo.blacklisted = (sbyte)comboBox2.SelectedIndex;
            cvInfo.productLine = tbProductLine.Text.Trim();

            return cvInfo;
        
        }






    }
}
