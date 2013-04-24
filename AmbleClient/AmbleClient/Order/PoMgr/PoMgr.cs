using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.PoView;

namespace AmbleClient.Order.PoMgr
{
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


       public static int GetPoNumberAccordingToSoId(int soId)
       {
           return poEntity.po.Where(poMain => poMain.soId == soId).Count();
       }

       public static void SetPoNumber(int poId)
       {
           po poItem = poEntity.po.Where(item => item.poId == poId).First();
           poItem.poNo = Tool.Get6DigitalNumberAccordingToId(poId);
           poEntity.SaveChanges();
       
       }

       
       public static List<po> GetPoAccordingToSoId(int soId)
       {
           List<po> poList = new List<po>();

               var poListFromDb = poEntity.po.Where(poMain => poMain.soId == soId);
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
               po poMain = (poEntity.po.First(item => item.poId == poId));
               poMain.poStates = (sbyte)state;
               poEntity.SaveChanges();
       }


       public static int GetSoIdAccordingToPoId(int poId)
       {

           var soId = from poItem in poEntity.po
                      where poItem.poId == poId
                      select poItem.soId;

             return (int)soId.First();

       }

       public static void DeletePoItembyPoItemId(int poItemId)
       {
            var items=poEntity.poitems.Where(i => i.PoItemsId == poItemId);
           
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
                       poitems item = poEntity.poitems.Where(pitem =>(pitem.PoItemsId == pics.poItem.PoItemsId)).First();
                       item = pics.poItem;
                       break;
               }
           }

           poEntity.SaveChanges();
       }





    }
}
