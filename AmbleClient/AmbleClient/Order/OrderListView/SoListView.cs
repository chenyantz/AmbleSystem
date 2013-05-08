using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.SoMgr;
using System.Windows.Forms;

namespace AmbleClient.Order
{
    public class SoListView:OrderListView
    {
       protected List<So> soList;


       protected override void FillTheStateCombox()
       {
           //fill the state List
           /*foreach (SoState soState in soStateList.GetWholeSoStateList())
           {
               tscbListState.Items.Add(soState.GetStateString());
           }*/
           foreach(SoStatesEnum state in Enum.GetValues(typeof(SoStatesEnum)))
           {
             tscbListState.Items.Add(Enum.GetName(typeof(SoStatesEnum),state));
           
           }



       }

       protected override void GetTheStateList()
       {
          /*
           foreach (SoState soState in soStateList.GetWholeSoStateList())
           {
               intStateList.Add(soState.GetStateValue());
           }
           intStateList.Remove(new SoRejected().GetStateValue());
           intStateList.Remove(new SoCancelled().GetStateValue());
           intStateList.Remove(new SoClose().GetStateValue());
           */

           intStateList.Add((int)SoStatesEnum.New);
           intStateList.Add((int)SoStatesEnum.Approved);
           intStateList.Add((int)SoStatesEnum.UnderProcess);
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
           if (rowIndex >= soList.Count)
               return;
           int realRowIndex = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["No"].Value);
           SO.SoView soView=new SO.SoView(soList[realRowIndex]);
           if (DialogResult.Yes == soView.ShowDialog())
           {
               FillTheDataGrid();
           }

       }


    }
}
