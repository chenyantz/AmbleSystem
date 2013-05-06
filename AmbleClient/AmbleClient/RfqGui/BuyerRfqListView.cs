using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AmbleClient.RfqGui.RfqManager;

namespace AmbleClient.RfqGui
{
    public class BuyerRfqListView:RFQListView
    {
        public BuyerRfqListView()
        {
            base.Text = "RFQ List for Purchasers";
            base.tsbNewRfq.Enabled = false;
            base.tscbAllOrMine.Items.Add("All RFQs");
            base.tscbAllOrMine.Items.Add("My Related RFQs");
            tscbAllOrMine.SelectedIndexChanged -= tscbAllOrMine_SelectedIndexChanged;
            tscbAllOrMine.SelectedIndex = 0;
            tscbAllOrMine.SelectedIndexChanged += tscbAllOrMine_SelectedIndexChanged;

            base.cbNew.Checked = false;
            base.cbRouted.Checked = true;
            base.cbOffered.Checked = true;
            base.cbQuoted.Checked = false;
            base.cbHasSo.Checked = false;
            base.cbClosed.Checked = false;

            base.cbNew.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.cbRouted.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.cbOffered.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.cbQuoted.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.cbHasSo.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.cbClosed.CheckedChanged += new System.EventHandler(base.rfqStatesSelectedChanged);
            base.rfqStatesSelectedChanged(this, null);//to fill the datagrid

            //in the list ,do not show the customer
            Customer.Visible = false;


        }

        public override int GetPageCount(int itemsPerPage, string filterColumn, string filterString, List<RfqStatesEnum> selections, bool includeSubs)
        {
            return RfqMgr.BuyerGetThePageCountOfDataTable(UserInfo.UserId, itemsPerPage, filterColumn, filterString, selections);

        }
        public override DataTable GetDataTableAccordingToPageNumber(int itemsPerPage, int currentPage, string filterColumn, string filterString, List<RfqStatesEnum> selections, bool includeSubs)
        {
            return RfqMgr.BuyerGetRfqDataTableAccordingToPageNumber(UserInfo.UserId,currentPage, itemsPerPage, filterColumn, filterString, selections);
        }
        public override void CellDoubleClickShow(int rfqId)
        {
            BuyerRfqView rfqView = new BuyerRfqView(rfqId);
            rfqView.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BuyerRfqListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1140, 527);
            this.Name = "BuyerRfqListView";
            this.Text = "RFQ List for Purchaser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


    }
}
