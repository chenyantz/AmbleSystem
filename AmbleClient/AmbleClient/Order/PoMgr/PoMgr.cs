using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.PoView;

namespace AmbleClient.Order.PoMgr
{
    public class PoCombine
    {
        public int poId;
        public string vendorName;
        public int buyerId;

        public int salesAgentId;
        public DateTime poDate;
        public int poItemsId;
        public string partNo;
        public string mfg;
        public string dc;
        public int qty;
        public float unitPrice;
        public int poItemState;
    
    
    }





   public class PoMgr
    {
      static private PoEntities poEntity=new PoEntities();


       public static List<po> GetPoAccordingToFilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> stateList)
       { 
         //get the sub;
           List<po> poList = new List<po>();
           if (stateList.Count == 0) return poList;

           List<int> userIds = new List<int>();

           if (includedSubs)
           {

               userIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
           }
           else
           {
               userIds.Add(userId);
           }

               if(filterColumn.Trim().Length==0||filterString.Trim().Length==0)
               {
                var poListFromDb= from poItem in poEntity.po 
                                  where (userIds.Contains((int)poItem.pa)) &&(stateList.Contains((int)poItem.poStates))
                                  select poItem;

                poList.AddRange(poListFromDb);

              }

               else  if (filterColumn.Trim() == "vendorName" && filterString.Trim().Length != 0)
               {
                   var poListFromDb = from poItem in poEntity.po
                                      where (userIds.Contains((int)poItem.pa)) && (stateList.Contains((int)poItem.poStates)&&
                                      (poItem.vendorName.Contains(filterString.Trim())))
                                       select poItem;

                   poList.AddRange(poListFromDb);
               
              }
               else if (filterColumn.Trim() == "poNo" && filterString.Trim().Length != 0)
               {
                   var poListFromDb = from poItem in poEntity.po
                                      where (userIds.Contains((int)poItem.pa)) && (stateList.Contains((int)poItem.poStates) &&
                                      (poItem.poNo.Contains(filterString.Trim())))
                                      select poItem;

                   poList.AddRange(poListFromDb);
               
               }
               else if (filterColumn.Trim() == "mpn" && filterString.Trim().Length != 0)
               {
                   var poIds = (from poExactItem in poEntity.poitems
                                where poExactItem.partNo.Contains(filterString.Trim())
                                select poExactItem.poId).Distinct();
                   var poListFromDb = from poItem in poEntity.po
                                      where (userIds.Contains((int)poItem.pa)) && (stateList.Contains((int)poItem.poStates) &&
                                      (poIds.Contains(poItem.poId)))
                                      select poItem;
                   poList.AddRange(poListFromDb);
               }
               else
               {
                   Logger.Info(filterColumn + "," + filterString);
               }

           return poList;
         
       }

       public static List<PoCombine> GetPoCombineAccordingToFilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> stateList)
       {
           List<PoCombine> poCombineList = new List<PoCombine>();
           if (stateList.Count == 0) return poCombineList;

           List<int> userIds = new List<int>();

           if (includedSubs)
           {

               userIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
           }
           else
           {
               userIds.Add(userId);
           }

           if (filterColumn.Trim().Length == 0 || filterString.Trim().Length == 0)
           {
               var poListFromDb = from pos in poEntity.po
                                  join poItems in poEntity.poitems
                                  on pos.poId equals poItems.poId
                                  where (userIds.Contains((int)pos.pa)) && (stateList.Contains((int)poItems.poItemState))
                                  select new PoCombine
                                  {
                                      poId = pos.poId,
                                      vendorName = pos.vendorName,
                                      buyerId = pos.pa,
                                      salesAgentId = poItems.salesAgent,
                                      poDate = pos.poDate,
                                      poItemsId = poItems.poItemsId,
                                      partNo = poItems.partNo,
                                      mfg = poItems.mfg,
                                      dc = poItems.dc,
                                      qty = poItems.qty,
                                      unitPrice = poItems.unitPrice.Value,
                                      poItemState = poItems.poItemState


                                  };

               poCombineList.AddRange(poListFromDb);

           }

           else if (filterColumn.Trim() == "vendorName" && filterString.Trim().Length != 0)
           {
               var poListFromDb = from pos in poEntity.po
                                  join poItems in poEntity.poitems
                                  on pos.poId equals poItems.poId
                                  where (userIds.Contains((int)pos.pa)) && (stateList.Contains((int)pos.poStates) &&
                                  (pos.vendorName.Contains(filterString.Trim())))
                                  select new PoCombine
                                  {
                                      poId = pos.poId,
                                      vendorName = pos.vendorName,
                                      buyerId = pos.pa,
                                      salesAgentId = poItems.salesAgent,
                                      poDate = pos.poDate,
                                      poItemsId = poItems.poItemsId,
                                      partNo = poItems.partNo,
                                      mfg = poItems.mfg,
                                      dc = poItems.dc,
                                      qty = poItems.qty,
                                      unitPrice = poItems.unitPrice.Value,
                                      poItemState = poItems.poItemState


                                  };

               poCombineList.AddRange(poListFromDb);

           }
               
           else if (filterColumn.Trim() == "poNo" && filterString.Trim().Length != 0)
           {
               var poListFromDb = from pos in poEntity.po
                                  join poItems in poEntity.poitems
                                  on pos.poId equals poItems.poId
                                  where (userIds.Contains((int)pos.pa)) && (stateList.Contains((int)pos.poStates) &&
                                   (pos.poNo.Contains(filterString.Trim())))
                                  select new PoCombine
                                  {
                                      poId = pos.poId,
                                      vendorName = pos.vendorName,
                                      buyerId = pos.pa,
                                      salesAgentId = poItems.salesAgent,
                                      poDate = pos.poDate,
                                      poItemsId = poItems.poItemsId,
                                      partNo = poItems.partNo,
                                      mfg = poItems.mfg,
                                      dc = poItems.dc,
                                      qty = poItems.qty,
                                      unitPrice = poItems.unitPrice.Value,
                                      poItemState = poItems.poItemState


                                  };

               poCombineList.AddRange(poListFromDb);

           }
           else if (filterColumn.Trim() == "mpn" && filterString.Trim().Length != 0)
           {
               var poListFromDb = from pos in poEntity.po
                                  join poItems in poEntity.poitems
                                  on pos.poId equals poItems.poId
                                  where (userIds.Contains((int)pos.pa)) && (stateList.Contains((int)pos.poStates) &&
                                   (poItems.partNo.Contains(filterString.Trim())))
                                  select new PoCombine
                                  {
                                      poId = pos.poId,
                                      vendorName = pos.vendorName,
                                      buyerId = pos.pa,
                                      salesAgentId = poItems.salesAgent,
                                      poDate = pos.poDate,
                                      poItemsId = poItems.poItemsId,
                                      partNo = poItems.partNo,
                                      mfg = poItems.mfg,
                                      dc = poItems.dc,
                                      qty = poItems.qty,
                                      unitPrice = poItems.unitPrice.Value,
                                      poItemState = poItems.poItemState


                                  };
               poCombineList.AddRange(poListFromDb);
           }
           else
           {
               Logger.Info(filterColumn + "," + filterString);
           }

           return poCombineList;
       
       }



       public static void SetPoNumber(int poId)
       {
           po poItem = poEntity.po.Where(item => item.poId == poId).First();
           poItem.poNo = Tool.Get6DigitalNumberAccordingToId(poId);
           poEntity.SaveChanges();
       
       }

       public static int GetPoNumberAccordingToSoId(int soId)
       {
           List<int> soItems=SoMgr.SoMgr.GetSoItemsIdsAccordingToSoId(soId);
           var poIdList = (from poItem in poEntity.poitems
                           where soItems.Contains(poItem.soItemId)
                           select poItem.poId).Distinct();
          return poIdList.Count();
       
       
       }


       public static List<po> GetPoAccordingToSoId(int soId)
       {
           //get all the soItemsId
         List<int> soItems=SoMgr.SoMgr.GetSoItemsIdsAccordingToSoId(soId);
           List<po> poList = new List<po>();
           var poIds = (from poItem in poEntity.poitems
                        where soItems.Contains(poItem.soItemId)
                        select poItem.poId).Distinct();
           var poListFromDb = poEntity.po.Where(poMain =>poIds.Contains(poMain.poId));
           poList.AddRange(poListFromDb);
           return poList;
      
       }


       public static po GetPoAccordingToPoId(int poId)
       {

               var poList = from poMain in poEntity.po
                            where poMain.poId == poId
                            select poMain;
               return poList.First();
         
       
       }


       public static List<poitems> GetPoItemsAccordingToPoId(int poId)
       {
           List<poitems> poitems = new List<poitems>();

               var poitemsList = from poItem in poEntity.poitems
                                 where poItem.poId == poId
                                 select poItem;
               foreach(poitems poitem in poitemsList)
               {
                poitems.Add(poitem);
               
               }
               return poitems;
       }



       public static void SavePoMain(po poMain)
       {

               poEntity.po.AddObject(poMain);
               poEntity.SaveChanges();


       }


       public static int GetTheInsertId(int userId)
       {
              var maxId=(from poMain in poEntity.po
                        where (int)poMain.pa==userId
                        select poMain.poId).Max();

              return maxId;
       }



       public static void SavePoItems(int poId, List<poitems> poitemsList)
       {
               foreach (poitems poitem in poitemsList)
               {
                   poEntity.poitems.AddObject(poitem);
               }
               poEntity.SaveChanges();
       
       }


       public static void UpdatePoState(int poId, int state)
       {
           var poList = poEntity.po.Where(item => item.poId == poId);
           foreach (po po in poList)
           {
               po.poStates = (sbyte)state;
           }

           if (state == (int)PoStatesEnum.Approved || state == (int)PoStatesEnum.Rejected || state == (int)PoStatesEnum.Cancel || state == (int)PoStatesEnum.Closed)
           {

               int value;
               if (state == (int)PoStatesEnum.Approved) value = new PoItemApproved().GetStateValue();
               else if (state == (int)PoStatesEnum.Rejected) value = new PoItemRejected().GetStateValue();
               else if (state == (int)PoStatesEnum.Cancel) value = new PoItemCancelled().GetStateValue();
               else value = new PoItemClosed().GetStateValue();

               var poItemList = poEntity.poitems.Where(item => item.poId == poId);
               foreach (poitems poItems in poItemList)
               {
                   poItems.poItemState = (sbyte)value;
               }
          
           }
           poEntity.SaveChanges();

       }


       public static void UpdatePoItemState(int poItemId, int state)
       {
           poitems poItem = poEntity.poitems.First(item => item.poItemsId == poItemId);
           poItem.poItemState = (sbyte)state;
           poEntity.SaveChanges();
       
       }


       public static void DeletePoItembyPoItemId(int poItemId)
       {
            var items=poEntity.poitems.Where(i => (i.poItemsId == poItemId));
           
           if (items.Count()==0)
           {
               return;
           }
           poitems item = items.First();
           poEntity.poitems.DeleteObject(item);
           poEntity.SaveChanges();
       }




       public static void UpdatePo(po poMain)
       {
           po poItem = poEntity.po.Where(item => item.poId == poMain.poId).First();
         //  poItem = poMain;
                 poItem.vendorName = poMain.vendorName;
                poItem.contact = poMain.contact;
                 poItem.pa = poMain.pa;
                poItem.vendorNumber=poMain.vendorName;
                poItem.freight=poMain.freight;
                poItem.shipMethod=poMain.shipMethod;
                poItem.paymentTerms=poMain.paymentTerms;
                poItem.shipToLocation=poMain.shipToLocation;
                poItem.billTo=poMain.billTo;
                poItem.shipTo = poMain.shipTo;
                   
           poEntity.SaveChanges();
       }

       public static void UpDatePoItems(List<PoItemContentAndState> poItemStateList)
       {
           foreach (PoItemContentAndState pics in poItemStateList)
           {
               switch (pics.state)
               { 
                   case OrderItemsState.Normal:
                       break;
                   
                   case OrderItemsState.New:
                       poEntity.poitems.AddObject(pics.poItem);
                       break;

                   case OrderItemsState.Modified:
                       poitems item = poEntity.poitems.Where(pitem =>(pitem.poItemsId == pics.poItem.poItemsId)).First();
                       item = pics.poItem;
                       break;
               }
           }

           poEntity.SaveChanges();
       }





    }
}
