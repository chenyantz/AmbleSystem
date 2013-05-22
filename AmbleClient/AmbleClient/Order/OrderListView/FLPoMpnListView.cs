using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.PoMgr;

namespace AmbleClient.Order
{
   public class FLPoMpnListView:PoMpnListView
    {

        protected override void ViewStart()
        {
            base.ViewStart();
            this.Text = "PO List for Finance && logistics";
            tscbList.Enabled = false;

        }

        protected override void FillTheDataGrid()
        {

            dataGridView1.Rows.Clear();

            poCombineList = Order.PoMgr.PoMgr.GetPoCombineAccordingToFilter(1/*Admin*/, true, filterColumn, filterString, intStateList);

            foreach (PoCombine poItem in poCombineList)
            {
                dataGridView1.Rows.Add(poItem.poId, poItem.poItemsId, Tool.Get6DigitalNumberAccordingToId(poItem.poId), poItem.partNo, poItem.mfg, poItem.dc, poItem.qty, poItem.unitPrice, poItem.vendorName, AllAccountInfo.GetNameAccordingToId(poItem.buyerId),
                    poItem.poDate.ToShortDateString(), AllAccountInfo.GetNameAccordingToId(poItem.salesAgentId), poItemStateList.GetPoStateStringAccordingToValue(poItem.poItemState));
            }
        }



    }
}
