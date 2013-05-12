using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.SoMgr;
using System.Windows.Forms;

namespace AmbleClient.Order
{
   public class SoMpnListView : OrderListView
   {

       protected List<SoCombine> soCombineList;


       protected SoItemOrderStateList soItemStateList = new SoItemOrderStateList();
       
       protected override void FillTheStateCombox()
       {
           //fill the state List
            foreach (SoItemState soItemState in soItemStateList.GetWholeSoStateList())
           {
               tscbListState.Items.Add(soItemState.GetStateString());
           }

       }

       protected override void GetTheStateList()
       {
          
            foreach (SoItemState soState in soItemStateList.GetWholeSoStateList())
            {
                intStateList.Add(soState.GetStateValue());
            }
            intStateList.Remove(new SoItemRejected().GetStateValue());
            intStateList.Remove(new SoItemCancelled().GetStateValue());
            intStateList.Remove(new SoItemClose().GetStateValue());

       }


       protected override void FillTheFilterColumnDict()
       {
           filterColumnDict.Add("Customer", "customerName");
           filterColumnDict.Add("SO number", "salesOrderNo");
           filterColumnDict.Add("Customer PO No", "customerPo");
           filterColumnDict.Add("MPN", "partNo");
       }

       protected override void StateChanged(object sender, EventArgs e)
       {
           intStateList.Clear();
           intStateList.Add(tscbListState.SelectedIndex);
           FillTheDataGrid();
       }



       protected override void OpenOrderDetails(int rowIndex)
       {
           if (rowIndex >= soCombineList.Count)
               return;
           int realRowIndex = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["No"].Value);
           int soItemId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["SoItemId"].Value);
           SO.SoView soView = new SO.SoView(soCombineList[realRowIndex].soId,soItemId);
           if (DialogResult.Yes == soView.ShowDialog())
           {
               FillTheDataGrid();
           }

       }
       



    }
}
