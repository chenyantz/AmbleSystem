using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbleClient.Order
{
   public class FLSoMpnListView:SalesSoMpnListView
    {
        protected override void ViewStart()
        {
            base.ViewStart();
            this.Text = "SO Item List for Finance && logistics";
            tscbList.Enabled = false;


        }

        protected override void FillTheDataGrid()
        {
            dataGridView1.Rows.Clear();
            soCombineList = SoMgr.SoMgr.SalesGetSoCombineAccordingTofilter(1, true, filterColumn, filterString, intStateList);

            int i = 0;
            foreach (SoMgr.SoCombine soc in soCombineList)
            {
                dataGridView1.Rows.Add(i++, soc.soItemsId, Tool.Get6DigitalNumberAccordingToId(soc.soId), soc.partNo, soc.mfg, soc.dc, soc.qty, soc.unitPrice, soc.customerName, soc.customerPo, soc.orderDate.ToShortDateString(), AllAccountInfo.GetNameAccordingToId(soc.salesId), soItemStateList.GetSoStateStringAccordingToValue(soc.soItemState));
            }

        }
    }
}
