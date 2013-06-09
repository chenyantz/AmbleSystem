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
    public partial class CustVenInfoFinancialControl : UserControl
    {
        private List<custvendorinfoshipto> shipToList = new List<custvendorinfoshipto>();
        private List<string> newList = new List<string>();

        int cvType=-1;
        
        public CustVenInfoFinancialControl()
        {
            InitializeComponent();
            //readonly for sales, sales Manager,buyer, buyerManager
            if (UserInfo.Job != JobDescription.Admin && UserInfo.Job != JobDescription.Boss && UserInfo.Job != JobDescription.FinancialManager && UserInfo.Job != JobDescription.Financial)
            {
                tbCompnayNumber.ReadOnly = true;
                tbPaymentTerm.ReadOnly = true;
                tbShippingTerm.ReadOnly = true;
                tbBillto.ReadOnly = true;
                btAddShip.Enabled = false;
                btDelete.Enabled = false;
            }
 
        }

        public CustVenInfoFinancialControl(int cvType)
        {
            InitializeComponent();
            //readonly for sales, sales Manager,buyer, buyerManager
            if (UserInfo.Job != JobDescription.Admin && UserInfo.Job != JobDescription.Boss && UserInfo.Job != JobDescription.FinancialManager && UserInfo.Job != JobDescription.Financial)
            {
                tbCompnayNumber.ReadOnly = true;
                tbPaymentTerm.ReadOnly = true;
                tbShippingTerm.ReadOnly = true;
                tbBillto.ReadOnly = true;
                btAddShip.Enabled = false;
                btDelete.Enabled = false;
            }
            if (cvType == 0)
                label1.Text = "  Customer Name:";
            if (cvType == 1)
                label1.Text = "    Vendor Name:";


        }

        public void FillTheInfo(custvendorinfo cvInfo)
        {
            tbName.Text = cvInfo.cvname;

            if (string.IsNullOrWhiteSpace(cvInfo.cvnumber))
            {
                string sixNumber=Tool.Get6DigitalNumberAccordingToId(cvInfo.cvId);
                tbCompnayNumber.Text = (cvInfo.cvtype == 0 ? "C" : "V")+ sixNumber;
            }
            else
            {
                tbCompnayNumber.Text = cvInfo.cvnumber;
            }
            
            tbPaymentTerm.Text = cvInfo.paymentTerm;
            tbShippingTerm.Text = cvInfo.shippingTerm;
            tbBillto.Text = cvInfo.billTo;
            //get the shipToList
           shipToList.AddRange(CustVenInfoManager.GetShipTo(cvInfo.cvId));

           if (shipToList.Count == 1)
           {
               ShipTo1.Text = shipToList[0].shipTo;
           }
           else if (shipToList.Count > 1)
           {
               ShipTo1.Text = shipToList[0].shipTo;


               for (int i = 1; i < shipToList.Count; i++)
               {

                   System.Windows.Forms.TextBox ShipTo = new TextBox();

                   ShipTo.Dock = System.Windows.Forms.DockStyle.Fill;
                   ShipTo.Location = new System.Drawing.Point(3, 3);
                   ShipTo.Multiline = true;
                   ShipTo.Name = "ShipTo";
                   ShipTo.Size = new System.Drawing.Size(570, 76);
                   ShipTo.TabIndex = 0;
                   ShipTo.Text = shipToList[i].shipTo;

                   System.Windows.Forms.TabPage tabPage = new TabPage();

                   tabPage.Controls.Add(ShipTo);
                   tabPage.Location = new System.Drawing.Point(4, 25);
                   tabPage.Name = "tabPage1";
                   tabPage.Padding = new System.Windows.Forms.Padding(3);
                   tabPage.Size = new System.Drawing.Size(576, 82);
                   tabPage.TabIndex = i;
                   tabPage.Text = "Ship To "+(i+1).ToString();
                   tabPage.UseVisualStyleBackColor = true;
                   tabControl1.TabPages.Add(tabPage);

                   
               
               }
           
           
           }

       }

        private void btAddShip_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox ShipTo = new TextBox();

            ShipTo.Dock = System.Windows.Forms.DockStyle.Fill;
            ShipTo.Location = new System.Drawing.Point(3, 3);
            ShipTo.Multiline = true;
            ShipTo.Name = "ShipTo";
            ShipTo.Size = new System.Drawing.Size(570, 76);
            ShipTo.TabIndex = 0;
            
            System.Windows.Forms.TabPage tabPage = new TabPage();

            int count = tabControl1.TabPages.Count;
            tabPage.Controls.Add(ShipTo);

            tabPage.Location = new System.Drawing.Point(4, 25);
            tabPage.Name = "tabPage1";
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(576, 82);
            tabPage.TabIndex = count;
            tabPage.Text = "Ship To " + (count+1).ToString();
            tabPage.UseVisualStyleBackColor = true;
            tabControl1.TabPages.Add(tabPage);

            tabControl1.SelectedTab = tabPage;

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);

        }


        public custvendorinfo GetValues()
        {
            custvendorinfo cvFourInfo = new custvendorinfo();
            cvFourInfo.cvnumber = tbCompnayNumber.Text.Trim();
            cvFourInfo.paymentTerm = tbPaymentTerm.Text.Trim();
            cvFourInfo.shippingTerm = tbShippingTerm.Text.Trim();
            cvFourInfo.billTo = tbBillto.Text.Trim();
            return cvFourInfo;
        }


        public List<custvendorinfoshipto> GetPreviousShipTo()
        {
            return shipToList;
        }

        public List<string> getCurrentShipTo()
        {
            foreach (TabPage tabpage in tabControl1.TabPages)
            {
                foreach (Control control in tabpage.Controls)
                {
                    if (control is TextBox)
                    {
                        if (control.Text.Trim().Length != 0)
                        {
                            newList.Add(control.Text.Trim());
                        }
                    }
                }
            
            }
            return newList;
        }



    }
}
