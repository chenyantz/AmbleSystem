using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Deployment.Application;
using System.Diagnostics;


namespace AmbleClient
{
    public partial class MainFrame : Form
    {

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.Text += ("---" + UserInfo.UserName);
            SecurityControl();
        }




        private void SecurityControl()
        {
            bOMCustomerToolStripMenuItem.Enabled = false;
            offerVendorListToolStripMenuItem.Enabled = false;
            lToolStripMenuItem.Enabled = false;


            switch (UserInfo.Job)
            { 
                case JobDescription.Admin:
             bOMCustomerToolStripMenuItem.Enabled = true;
            offerVendorListToolStripMenuItem.Enabled = true;
            lToolStripMenuItem.Enabled = true;
            break;
                case JobDescription.Boss:
                    //grey admin
                    adminToolStripMenuItem.Enabled = false;
                                 bOMCustomerToolStripMenuItem.Enabled = true;
            offerVendorListToolStripMenuItem.Enabled = true;
            lToolStripMenuItem.Enabled = true;
                    break;
                case JobDescription.Financial:
                    pOListViewToolStripMenuItem1.Enabled = false;
                    salesToolStripMenuItem.Enabled = false;
                    buyersToolStripMenuItem.Enabled = false;
                  //  warehousesToolStripMenuItem.Enabled = false;
                    sOListViewToolStripMenuItem1.Enabled = false;
                    pOListViewToolStripMenuItem2.Enabled = false;
                  adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                   
                    break;
                case JobDescription.FinancialManager:
                    salesToolStripMenuItem.Enabled = false;
                    buyersToolStripMenuItem.Enabled = false;
                                        sOListViewToolStripMenuItem1.Enabled = false;
                    pOListViewToolStripMenuItem2.Enabled = false;
                    adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    break;
                case JobDescription.Logistics:
                    pOListViewToolStripMenuItem2.Enabled = false;
                    salesToolStripMenuItem.Enabled = false;
                    buyersToolStripMenuItem.Enabled = false;
                    financesToolStripMenuItem.Enabled = false;
                    adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    break;
                case JobDescription.LogisticsManager:
                    salesToolStripMenuItem.Enabled = false;
                    buyersToolStripMenuItem.Enabled = false;
                    financesToolStripMenuItem.Enabled = false;
                    adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    break;
                case JobDescription.Purchaser:
                case JobDescription.PurchasersManager:
                    salesToolStripMenuItem.Enabled = false;
                    financesToolStripMenuItem.Enabled = false;
                                        sOListViewToolStripMenuItem1.Enabled = false;
                    pOListViewToolStripMenuItem2.Enabled = false;
                    adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    break;
                case JobDescription.Sales:
                case JobDescription.SalesManager:
                    buyersToolStripMenuItem.Enabled = false;
                    financesToolStripMenuItem.Enabled = false;
                                        sOListViewToolStripMenuItem1.Enabled = false;
                    pOListViewToolStripMenuItem2.Enabled = false;
                    adminToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    break;
            
            }
        
        
        
        }





        private void viewEditAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.Admin.AccountMainFrame accountOpFrame = new Admin.AccountMainFrame();
            accountOpFrame.MdiParent = this;
            accountOpFrame.Show();

        }

        private void MainFrame_Leave(object sender, EventArgs e)
        {

        }

        private void MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void customerManagermentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            custVendor.customerVendorMainFrame mainFrame = new custVendor.customerVendorMainFrame(0);
            mainFrame.MdiParent = this;
            mainFrame.Show();*/
            custVendor.CustVenderListView custVenderListView = new custVendor.SalesBuyerCustomerListView(0);
            custVenderListView.MdiParent = this;
            custVenderListView.Show();

        }

        private void vendorManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*custVendor.customerVendorMainFrame mainFrame = new custVendor.customerVendorMainFrame(1);
            mainFrame.MdiParent = this;
            mainFrame.Show();*/
            custVendor.CustVenderListView custVenderListView = new custVendor.SalesBuyerCustomerListView(1);
            custVenderListView.MdiParent = this;
            custVenderListView.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.PasswordChange passwdcgFrame = new Settings.PasswordChange();
            passwdcgFrame.MdiParent = this;
            passwdcgFrame.Show();
           

        }

        private void assignCustomerVendorNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Finances.FinancesView financeView = new Finances.FinancesView();
            financeView.MdiParent = this;
            financeView.Show();*/
            custVendor.CustVenderListView custVendorListView = new custVendor.FinanceCustVendorListView();
            custVendorListView.MdiParent = this;
            custVendorListView.Show();


        }

        private void rFQViewNewSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RfqGui.SalesRfqListView rfqView = new RfqGui.SalesRfqListView();
            rfqView.MdiParent = this;
            rfqView.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (UserInfo.Job == JobDescription.Purchaser)
            {
                RfqGui.BuyerRfqListView rfqView = new RfqGui.BuyerRfqListView();
                rfqView.MdiParent = this;
                rfqView.Show();
            }
            else
            {
                RfqGui.BuyerManagerRfqListView rfqView = new RfqGui.BuyerManagerRfqListView();
                rfqView.MdiParent = this;
                rfqView.Show();
            }


        }

        private void testsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.Help.About about = new Help.About();
            about.ShowDialog();
        }

        private void sOViewToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void pOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bOMCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.BomOffer.BomOfferCustVendor bomCustomer = new BomOffer.BomOfferCustVendor(AmbleClient.BomOffer.BomOfferTypeEnum.BOM);
            bomCustomer.MdiParent = this;
            bomCustomer.Show();


        }

        private void bOMListViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BomOffer.BomOfferList bomOfferList = new BomOffer.BomOfferList(AmbleClient.BomOffer.BomOfferTypeEnum.BOM);
            bomOfferList.MdiParent=this;
            bomOfferList.Show();

        }

        private void offerVendorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.BomOffer.BomOfferCustVendor bomCustomer = new BomOffer.BomOfferCustVendor(AmbleClient.BomOffer.BomOfferTypeEnum.Excess);
            bomCustomer.MdiParent = this;
            bomCustomer.Show();
        }

        private void offerListViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BomOffer.BomOfferList bomOfferList = new BomOffer.BomOfferList(AmbleClient.BomOffer.BomOfferTypeEnum.Excess);
            bomOfferList.MdiParent = this;
            bomOfferList.Show();
        }

        private void pOListViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void offerListViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.OfferListView offerListView = new OfferGui.OfferListView();
            offerListView.MdiParent = this;
            offerListView.Show();



        }

        private void sOListViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pOListViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void sOListViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void pOListViewToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void offerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.OfferGui.OfferListView offerListView = new OfferGui.OfferListView(true);
            offerListView.MdiParent = this;
            offerListView.Show();


        }

        private void byCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order.SalesSoListView salesSoListView = new Order.SalesSoListView();
            salesSoListView.MdiParent = this;
            salesSoListView.Show();
        }

        private void byMPNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order.SalesSoMpnListView ssMpnListView = new Order.SalesSoMpnListView();
            ssMpnListView.MdiParent = this;
            ssMpnListView.Show();
        }

        private void byMPNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Order.BuyerSoMpnListView bsMpnListView = new Order.BuyerSoMpnListView();
            bsMpnListView.MdiParent = this;
            bsMpnListView.Show();
        }

        private void byMPNToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Order.FLSoMpnListView fsMpnListView = new Order.FLSoMpnListView();
            fsMpnListView.MdiParent = this;
            fsMpnListView.Show();
        }

        private void byMPNToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Order.FLSoMpnListView fsMpnListView = new Order.FLSoMpnListView();
            fsMpnListView.MdiParent = this;
            fsMpnListView.Show();
        }

        private void byMPNToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Order.PoMpnListView poListView = new Order.PoMpnListView();
            poListView.MdiParent = this;
            poListView.Show();
        }

        private void byMPNToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Order.FLPoMpnListView poListView = new Order.FLPoMpnListView();
            poListView.MdiParent = this;
            poListView.Show();

        }

        private void byVendorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Order.FLPoListView poListView = new Order.FLPoListView();
            poListView.MdiParent = this;
            poListView.Show();

        }

        private void byCustomerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Order.FLSoListView soListView = new Order.FLSoListView();
            soListView.MdiParent = this;
            soListView.Show();
        }

        private void byCustomerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AmbleClient.Order.FLSoListView flSoListView = new Order.FLSoListView();
            flSoListView.MdiParent = this;
            flSoListView.Show();
        }

        private void byVendorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AmbleClient.Order.FLPoListView flPoListView = new Order.FLPoListView();
            flPoListView.MdiParent = this;
            flPoListView.Show();
        }

        private void byMPNToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Order.FLPoMpnListView poListView = new Order.FLPoMpnListView();
            poListView.MdiParent = this;
            poListView.Show();
        }

        private void byCustomerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Order.BuyerSoListView buySoListView = new Order.BuyerSoListView();
            buySoListView.MdiParent = this;
            buySoListView.Show();
        }

        private void byVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order.PoListView poListView = new Order.PoListView();
            poListView.MdiParent = this;
            poListView.Show();
        }

        private void autoUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AmbleClient.Help.UpdateInfo.NeedUpdate() == true)
            {
                if (DialogResult.Yes == MessageBox.Show("Exit the current Program to upgrade?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Process.Start("AmbleUpdate.exe");
                    Application.Exit();
                    return;
                
                }
            }
            else
            {
                MessageBox.Show("New Version is not available");
            
            }




        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleClient.BomOffer.BomOfferCustVendor bomCustomer = new BomOffer.BomOfferCustVendor(AmbleClient.BomOffer.BomOfferTypeEnum.LTOffer);
            bomCustomer.MdiParent = this;
            bomCustomer.Show();
        }

        private void lTOfferListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BomOffer.BomOfferList bomOfferList = new BomOffer.BomOfferList(AmbleClient.BomOffer.BomOfferTypeEnum.LTOffer);
            bomOfferList.MdiParent = this;
            bomOfferList.Show();
        }

        private void ambleStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmbleStock.AmbleStock stock = new AmbleStock.AmbleStock();
            stock.MdiParent = this;
            stock.Show();
        }

        private void matchedBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BomOffer.MatchedBom mactchBom = new BomOffer.MatchedBom();
            mactchBom.MdiParent = this;
            mactchBom.Show();
        }

        private void byMPNToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string searchString;
            AmbleClient.SearchBy.SearchBy search = new SearchBy.SearchBy(true);
            if (DialogResult.OK == search.ShowDialog())
            {
                searchString = search.searchString;
            }
            else
            {
                return;
            }

            switch (UserInfo.Job)
            { 
                case JobDescription.Admin:
                case JobDescription.Boss:
                    AmbleClient.RfqGui.SalesRfqListView rfqListView = new RfqGui.SalesRfqListView(true, searchString);
                    rfqListView.Show();
                    rfqListView.MdiParent = this;
                    AmbleClient.Order.OrderListView offerListView = new AmbleClient.OfferGui.OfferListView(false);
                    offerListView.SetExternalSearch(true, searchString);
                    offerListView.Show();
                    offerListView.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView = new Order.SalesSoMpnListView();
                    soMpnListView.SetExternalSearch(true, searchString);
                    soMpnListView.Show();
                    soMpnListView.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView = new Order.PoMpnListView();
                    poMpnListView.SetExternalSearch(true, searchString);
                    poMpnListView.Show();
                    poMpnListView.MdiParent = this;
                    break;
                case JobDescription.Sales:
                case JobDescription.SalesManager:
                    AmbleClient.RfqGui.SalesRfqListView rfqListView1 = new RfqGui.SalesRfqListView(true, searchString);
                    rfqListView1.Show();
                    rfqListView1.MdiParent = this;
                    AmbleClient.Order.OrderListView offerListView1 = new AmbleClient.OfferGui.OfferListView(true);
                    offerListView1.SetExternalSearch(true, searchString);
                    offerListView1.Show();
                    offerListView1.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView1 = new Order.SalesSoMpnListView();
                    soMpnListView1.SetExternalSearch(true, searchString);
                    soMpnListView1.Show();
                    soMpnListView1.MdiParent = this;
                    break;
                case JobDescription.PurchasersManager:
                    AmbleClient.RfqGui.BuyerManagerRfqListView rfqListView2 = new RfqGui.BuyerManagerRfqListView(true, searchString);
                    rfqListView2.Show();
                    rfqListView2.MdiParent = this;
                    AmbleClient.Order.OrderListView offerListView2 = new AmbleClient.OfferGui.OfferListView(true);
                    offerListView2.SetExternalSearch(true, searchString);
                    offerListView2.Show();
                    offerListView2.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView2 = new Order.BuyerSoMpnListView();
                    soMpnListView2.SetExternalSearch(true, searchString);
                    soMpnListView2.Show();
                    soMpnListView2.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView2 = new Order.PoMpnListView();
                    poMpnListView2.SetExternalSearch(true, searchString);
                    poMpnListView2.Show();
                    poMpnListView2.MdiParent = this;
                    break;
                case JobDescription.Purchaser:
                    AmbleClient.RfqGui.BuyerRfqListView rfqListView3 = new RfqGui.BuyerRfqListView(true, searchString);
                    rfqListView3.Show();
                    rfqListView3.MdiParent = this;
                    AmbleClient.Order.OrderListView offerListView3 = new AmbleClient.OfferGui.OfferListView(true);
                    offerListView3.SetExternalSearch(true, searchString);
                    offerListView3.Show();
                    offerListView3.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView3 = new Order.BuyerSoMpnListView();
                    soMpnListView3.SetExternalSearch(true, searchString);
                    soMpnListView3.Show();
                    soMpnListView3.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView3 = new Order.PoMpnListView();
                    poMpnListView3.SetExternalSearch(true, searchString);
                    poMpnListView3.Show();
                    poMpnListView3.MdiParent = this;
                    break;
                case JobDescription.Logistics:
                case JobDescription.Financial:
                case JobDescription.FinancialManager:
                case JobDescription.LogisticsManager:
                    AmbleClient.Order.OrderListView soMpnListView4 = new Order.FLSoMpnListView();
                    soMpnListView4.SetExternalSearch(true, searchString);
                    soMpnListView4.Show();
                    soMpnListView4.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView4 = new Order.FLPoMpnListView();
                    poMpnListView4.SetExternalSearch(true, searchString);
                    poMpnListView4.Show();
                    poMpnListView4.MdiParent = this;
                    break;
            }
            this.LayoutMdi(MdiLayout.TileHorizontal);

        }

        private void byCompanyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchString;
            AmbleClient.SearchBy.SearchBy search = new SearchBy.SearchBy(false);
            if (DialogResult.OK == search.ShowDialog())
            {
                searchString = search.searchString;
            }
            else
            {
                return;
            }

            switch (UserInfo.Job)
            {
                case JobDescription.Admin:
                case JobDescription.Boss:
                    AmbleClient.custVendor.CustVenderListView custListView = new custVendor.SalesBuyerCustomerListView(0);
                    custListView.SetExternalSearch(searchString);
                    custListView.Show();
                    custListView.MdiParent = this;
                    AmbleClient.custVendor.CustVenderListView venderListView = new custVendor.SalesBuyerCustomerListView(1);
                    venderListView.SetExternalSearch(searchString);
                    venderListView.Show();
                    venderListView.MdiParent = this;


                    AmbleClient.RfqGui.SalesRfqListView rfqListView = new RfqGui.SalesRfqListView(false, searchString);
                    rfqListView.Show();
                    rfqListView.MdiParent = this;
                    AmbleClient.Order.OrderListView offerListView = new AmbleClient.OfferGui.OfferListView(false);
                    offerListView.SetExternalSearch(false, searchString);
                    offerListView.Show();
                    offerListView.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView = new Order.SalesSoMpnListView();
                    soMpnListView.SetExternalSearch(false, searchString);
                    soMpnListView.Show();
                    soMpnListView.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView = new Order.PoMpnListView();
                    poMpnListView.SetExternalSearch(false, searchString);
                    poMpnListView.Show();
                    poMpnListView.MdiParent = this;
                    break;
                case JobDescription.Sales:
                case JobDescription.SalesManager:
                    AmbleClient.custVendor.CustVenderListView custListView2 = new custVendor.SalesBuyerCustomerListView(0);
                    custListView2.SetExternalSearch(searchString);
                    custListView2.Show();
                    custListView2.MdiParent = this;
                    AmbleClient.RfqGui.SalesRfqListView rfqListView1 = new RfqGui.SalesRfqListView(false, searchString);
                    rfqListView1.Show();
                    rfqListView1.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView1 = new Order.SalesSoMpnListView();
                    soMpnListView1.SetExternalSearch(false, searchString);
                    soMpnListView1.Show();
                    soMpnListView1.MdiParent = this;
                    break;
                case JobDescription.PurchasersManager:

                    AmbleClient.custVendor.CustVenderListView venderListView3 = new custVendor.SalesBuyerCustomerListView(1);
                    venderListView3.SetExternalSearch(searchString);
                    venderListView3.Show();
                    venderListView3.MdiParent = this;

                    AmbleClient.RfqGui.BuyerManagerRfqListView rfqListView2 = new RfqGui.BuyerManagerRfqListView(false, searchString);
                    rfqListView2.Show();
                    rfqListView2.MdiParent = this;

                    AmbleClient.Order.OrderListView offerListView2 = new AmbleClient.OfferGui.OfferListView(true);
                    offerListView2.SetExternalSearch(false, searchString);
                    offerListView2.Show();
                    offerListView2.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView2 = new Order.BuyerSoMpnListView();
                    soMpnListView2.SetExternalSearch(false, searchString);
                    soMpnListView2.Show();
                    soMpnListView2.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView2 = new Order.PoMpnListView();
                    poMpnListView2.SetExternalSearch(false, searchString);
                    poMpnListView2.Show();
                    poMpnListView2.MdiParent = this;
                    break;
                case JobDescription.Purchaser:

                     AmbleClient.custVendor.CustVenderListView venderListView4 = new custVendor.SalesBuyerCustomerListView(1);
                    venderListView4.SetExternalSearch(searchString);
                    venderListView4.Show();
                    venderListView4.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView3 = new Order.PoMpnListView();
                    poMpnListView3.SetExternalSearch(true, searchString);
                    poMpnListView3.Show();
                    poMpnListView3.MdiParent = this;
                    break;
                case JobDescription.Logistics:
                case JobDescription.Financial:
                case JobDescription.FinancialManager:
                case JobDescription.LogisticsManager:

                    AmbleClient.custVendor.CustVenderListView companyListView = new custVendor.FinanceCustVendorListView();;
                    companyListView.SetExternalSearch(searchString);
                    companyListView.Show();
                    companyListView.MdiParent = this;
                    AmbleClient.Order.OrderListView soMpnListView4 = new Order.FLSoMpnListView();
                    soMpnListView4.SetExternalSearch(false, searchString);
                    soMpnListView4.Show();
                    soMpnListView4.MdiParent = this;
                    AmbleClient.Order.OrderListView poMpnListView4 = new Order.FLPoMpnListView();
                    poMpnListView4.SetExternalSearch(false, searchString);
                    poMpnListView4.Show();
                    poMpnListView4.MdiParent = this;
                    break;
            }
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}
