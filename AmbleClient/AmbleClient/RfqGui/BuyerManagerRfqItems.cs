using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.RfqGui.RfqManager;

namespace AmbleClient.RfqGui
{
   public class BuyerManagerRfqItems:RfqItems
    {
        private List<int> mySubs;

        public List<int> MySubs
        {
            get 
            {
                return mySubs;
            }
        
        }

       public BuyerManagerRfqItems()
       {

       }

       public override void FillTheTable(AmbleClient.RfqGui.RfqManager.Rfq rfq)
       {
           base.FillTheTable(rfq);
           
           
           tbCustomer.Text = rfq.customerName;
          

           this.tbContact.Text = string.Empty; this.tbContact.ReadOnly = true;//can not be seen by sales Manager 
           this.tbPhone.Text = string.Empty; this.tbPhone.ReadOnly = true; //can not be seen by sales Manager.
           this.tbFax.Text = string.Empty; this.tbFax.ReadOnly = true;


           //Fill the sales
           List<int> sales = new List<int>();
           sales.Add(rfq.salesId);

           cbSales.Items.Add(AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(sales)[rfq.salesId]);
           cbSales.SelectedIndex = 0;
           // cbSales.Text = (GlobalRemotingClient.GetAccountMgr().GetIdsAndNames(sales))[rfq.salesId];
           
           //Fill the PA
           mySubs = AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(UserInfo.UserId, UserCombine.GetUserCanBeBuyers());
           Dictionary<int, string> mySubsIdAndName = AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(mySubs);

           //确认里面的buyer是不是我的下属，如果不是，不能更改。
           if (rfq.firstPA.HasValue == false || mySubs.Contains(rfq.firstPA.Value))
           {
               foreach (string name in mySubsIdAndName.Values)
               {
                   cbPrimaryPA.Items.Add(name);
               }
               if (rfq.firstPA.HasValue == false)
               {
                   cbPrimaryPA.SelectedIndex = -1;
               }
               else
               {
                   cbPrimaryPA.SelectedIndex = mySubs.IndexOf(rfq.firstPA.Value);
               }

           }
           else
           {
               List<int> buyer = new List<int>();
               buyer.Add(rfq.firstPA.Value);
               cbPrimaryPA.Items.Add(AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(buyer)[rfq.firstPA.Value]);
               cbPrimaryPA.SelectedIndex = 0;
           }
           if (rfq.secondPA.HasValue == false || mySubs.Contains(rfq.secondPA.Value))
           {
               foreach (string name in mySubsIdAndName.Values)
               {
                   cbAltPA.Items.Add(name);
               }

               if (rfq.secondPA.HasValue == false)
               {
                   cbAltPA.SelectedIndex = -1;
               }
               else
               {
                   cbAltPA.SelectedIndex = mySubs.IndexOf(rfq.secondPA.Value);
               }

           }
           else
           {
               List<int> buyer = new List<int>();
               buyer.Add(rfq.secondPA.Value);
               cbAltPA.Items.Add(AmbleClient.Admin.AccountMgr.AccountMgr.GetIdsAndNames(buyer)[rfq.secondPA.Value]);
              cbAltPA.SelectedIndex = 0;
           }

           //if rfq state is new, promp the buyer Manager to fill the first pa and second pa

           if (rfq.rfqStates == (int)RfqStatesEnum.Routed)
           {
               label23.ForeColor = System.Drawing.Color.Red;
               label24.ForeColor = System.Drawing.Color.Red;
               cbPrimaryPA.Focus();
           }



       }





    }
    



}
