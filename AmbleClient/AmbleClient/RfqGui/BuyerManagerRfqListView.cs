using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AmbleClient.RfqGui.RfqManager;

namespace AmbleClient.RfqGui
{
    public class BuyerManagerRfqListView:RFQListView
    {
        public BuyerManagerRfqListView()
        {
            ConstructBase();
        }

        public BuyerManagerRfqListView(bool isMpnSearch, string searchString)
        {
            SetExternalSearch(isMpnSearch, searchString);
            ConstructBase();
        }

        private void ConstructBase()
        {
            base.Text = "RFQ List for Purchaser Manager";

            base.tsbNewRfq.Enabled = false;
            base.tscbAllOrMine.Enabled = true;
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
            base.rfqStatesSelectedChanged(this, null);
        }




      public override int GetPageCount(int itemsPerPage, string filterColumn, string filterString,List<RfqStatesEnum> selections,bool includeSubs)
      {
          if (includeSubs)
          {
              return RfqMgr.BMGetThePageCountOfDataTable(itemsPerPage, filterColumn, filterString, selections);
          }
          else
          {
              return RfqMgr.BuyerGetThePageCountOfDataTable(UserInfo.UserId, itemsPerPage, filterColumn, filterString, selections);
          }
      }
      public override DataTable GetDataTableAccordingToPageNumber(int itemsPerPage, int currentPage, string filterColumn, string filterString, List<RfqStatesEnum> selections, bool includeSubs)
      {
          if (includeSubs)
          {
              return RfqMgr.BMGetRfqDataTableAccordingToPageNumber(currentPage, itemsPerPage, filterColumn, filterString, selections);
          }
          else
          {
              return RfqMgr.BuyerGetRfqDataTableAccordingToPageNumber(UserInfo.UserId, currentPage, itemsPerPage, filterColumn, filterString, selections);
          }

      }
      public override void CellDoubleClickShow(int rfqId)
      {
          BuyerManagerRfqView rfqView = new BuyerManagerRfqView(rfqId);
          rfqView.ShowDialog();
      }

      private void InitializeComponent()
      {
          this.SuspendLayout();
          // 
          // BuyerManagerRfqListView
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.ClientSize = new System.Drawing.Size(1140, 527);
          this.Name = "BuyerManagerRfqListView";
          this.Text = "RFQ List for Purchaser Manager";
          this.ResumeLayout(false);
          this.PerformLayout();

      }



        
    }
}
