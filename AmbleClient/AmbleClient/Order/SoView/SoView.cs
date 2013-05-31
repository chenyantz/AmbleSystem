using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;
using AmbleClient.Order;


namespace AmbleClient.SO
{

    public partial class SoView : Form
    {

        private SoItemOrderStateList soStateList = new SoItemOrderStateList();

        private List<So> soList;
        List<SoViewControl> soViewControlList = new List<SoViewControl>();

        int? selectedSoItemId = null;


        public SoView(int rfqId)
        {
            InitializeComponent();
            soList = SoMgr.GetSoAccordingToRfqId(rfqId);
            this.Text = "List All SO info for RFQ:" + rfqId;
        }

        public SoView(So so)
        {

            InitializeComponent();
            soList = new List<So>();
            soList.Add(so);
            this.Text = "Info for SO:" + so.soId;
        }

        public SoView(int soId, int soItemId)
        {
            InitializeComponent();
            soList = new List<So>();
            soList.Add(SoMgr.GetSoAccordingToSoId(soId));
            this.Text = "Info for SO:" + soId;
            this.selectedSoItemId = soItemId;
        }


        private void GenerateGui()
        {


            So so = soList[tabControl1.SelectedIndex];

            //for view Po
            if (Order.PoMgr.PoMgr.GetPoNumberAccordingToSoId(soList[tabControl1.SelectedIndex].soId) <= 0)
            {
                tsbViewPo.Enabled = false;
            }
            else
            {
                tsbViewPo.Enabled = true;
            }

            //for approve and rejected.
            if (so.soStates == (int)SoStatesEnum.New)
            {
                tsbApprove.Enabled = true;
                tsbReject.Enabled = true;

            }
            else
            {
                tsbApprove.Enabled = false;
                tsbReject.Enabled = false;
            }
            //for cancel

            if (so.soStates == (int)SoStatesEnum.Approved)
            {
                tsbCancel.Enabled = true;
            }
            else
            {
                tsbCancel.Enabled = false;
            }

            if (UserInfo.Job == JobDescription.Admin || UserInfo.Job == JobDescription.Boss)
            {
                tsbForceClose.Enabled = true;
            }
            else
            {
                tsbForceClose.Enabled = false;
            }

            if (so.soStates != (int)SoStatesEnum.New && UserCombine.GetUserCanBeSalesManager().Contains((int)UserInfo.Job))
            {
                tsbUpdate.Enabled = true;
            }
            else if (so.soStates == (int)SoStatesEnum.New && UserCombine.GetUserCanBeSales().Contains((int)UserInfo.Job))
            {
                tsbUpdate.Enabled = true;
            }
            else
            {
                tsbUpdate.Enabled = false;
            }

            if (!UserCombine.GetUserCanBeSalesManager().Contains((int)UserInfo.Job))
            {
                tsbApprove.Enabled = false;
                tsbReject.Enabled = false;
                tsbForceClose.Enabled = false;
                tsbCancel.Enabled = false;
            }



        }

        private void SoView_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < soList.Count; i++)
            {
                SoViewControl soViewControlItem = new SoViewControl();
                soViewControlItem.Dock = System.Windows.Forms.DockStyle.Fill;
                soViewControlItem.Location = new System.Drawing.Point(3, 3);
                soViewControlItem.Name = "buyerOfferIems" + i;
                soViewControlItem.Size = new System.Drawing.Size(906, 456);
                soViewControlItem.TabIndex = 0;
                soViewControlItem.FillTheTable(soList[i], selectedSoItemId);
                soViewControlList.Add(soViewControlItem);



            }

            for (int i = 0; i < soViewControlList.Count; i++)
            {
                System.Windows.Forms.TabPage tabPage = new TabPage();

                tabPage.Controls.Add(soViewControlList[i]);
                tabPage.Location = new System.Drawing.Point(4, 22);
                tabPage.Name = "tabPage" + i;
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.Size = new System.Drawing.Size(941, 46297);
                tabPage.TabIndex = i;
                tabPage.Text = "SO " + i;
                tabPage.UseVisualStyleBackColor = true;
                this.tabControl1.Controls.Add(tabPage);
            }
            tabControl1_SelectedIndexChanged(this, null);

        }


        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateGui();
        }

        private void tsbUpdate_Click(object sender, EventArgs e)
        {

            soViewControlList[tabControl1.SelectedIndex].SoUpdate();
            this.DialogResult = DialogResult.Yes;

        }


        private void tsbViewPo_Click(object sender, EventArgs e)
        {
            Order.PoView.PoView poview = new Order.PoView.PoView(soList[tabControl1.SelectedIndex].soId);
            poview.ShowDialog();
        }

        private void tsbToExcel_Click(object sender, EventArgs e)
        {
            List<List<SoItemsContentAndState>> soItemsListList = new List<List<SoItemsContentAndState>>();

            for (int i = 0; i < soViewControlList.Count; i++)
            {
                soItemsListList.Add(soViewControlList[i].GetItemsStateList());
            }
            if (UserInfo.Job == JobDescription.PurchasersManager || UserInfo.Job == JobDescription.Purchaser)
            {
                foreach (So so in soList)
                {
                    so.contact = "";
                    if (UserInfo.Job == JobDescription.Purchaser)
                    {
                        so.customerName = "";
                    }
                }
            }
            SoPoExcelHelper.SaveSOExcel(soList, soItemsListList);


        }

        private void tsbApprove_Click(object sender, EventArgs e)
        {
           if(DialogResult.Yes==MessageBox.Show("Approve SO?","",MessageBoxButtons.YesNo))
           {

            So so = soList[tabControl1.SelectedIndex];
            SoMgr.WholeUpdateSoState(so.soId, UserInfo.UserId, SoStatesEnum.Approved);
            this.DialogResult = DialogResult.Yes;
            this.Close();
           }
        }

        private void tsbReject_Click(object sender, EventArgs e)
        {
          if(DialogResult.Yes==MessageBox.Show("Reject SO?","",MessageBoxButtons.YesNo))
           {
            So so = soList[tabControl1.SelectedIndex];
            SoMgr.WholeUpdateSoState(so.soId, UserInfo.UserId, SoStatesEnum.Rejected);
            this.DialogResult = DialogResult.Yes;
            this.Close();
          }
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
         if(DialogResult.Yes==MessageBox.Show("Cancel SO?","",MessageBoxButtons.YesNo))
         {
            So so = soList[tabControl1.SelectedIndex];
            SoMgr.WholeUpdateSoState(so.soId, UserInfo.UserId, SoStatesEnum.Cancel);
            this.DialogResult = DialogResult.Yes;
            this.Close();
         }
        }

        private void tsbForceClose_Click(object sender, EventArgs e)
        {

         if(DialogResult.Yes==MessageBox.Show("Close SO and its all Items?","",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2))
         {
            So so = soList[tabControl1.SelectedIndex];
            SoMgr.WholeUpdateSoState(so.soId, UserInfo.UserId, SoStatesEnum.Closed);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        }
        private void SoView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

                foreach (SoViewControl control in soViewControlList)
                {
                    if (control.HasItemChange == true)
                    {
                        this.DialogResult = DialogResult.Yes;
                        return;
                    }

                }


            }
        }
    }
}

