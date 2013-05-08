using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AmbleClient.SO;

namespace AmbleClient.Order.SoMgr
{
   public class SoMgr
    {
       static DataClass.DataBase db = new DataClass.DataBase();


       public static List<So> SalesGetSoAccordingTofilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> states)
       {
           

           List<So> soList=new List<So>();
           if (states.Count == 0) return soList;
           List<int> salesIds = new List<int>();

           if (includedSubs)
           {
              
               salesIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId,UserCombine.GetUserCanBeSales()));
           }
           else
           {
               salesIds.Add(userId);
           }

           StringBuilder sb=new StringBuilder();
           sb.Append("select soId from So where ( salesId="+salesIds[0]);
           for(int i=1;i<salesIds.Count;i++)
           {
               sb.Append(" or salesId=" + salesIds[i]);
           }
           sb.Append(" ) ");

           if (filterColumn.Trim() == "partNo" && (!string.IsNullOrWhiteSpace(filterString)))
           {
              List<int> idsList=GetSoIdByMPN(filterString.Trim());
              if (idsList.Count >= 1)
              {
                  sb.Append(" and (soId=" + idsList[0]);

                  for (int i = 1; i < idsList.Count; i++)
                  {
                      sb.Append(" or soId=" + idsList[i]);
                  }
                  sb.Append(" ) ");

              }
              else
              {
                  return soList;
              }
               
          }
           else
           {
               //append the filter
               if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
               {
                   sb.Append(string.Format(" and {0} like '%{1}%' ", filterColumn, filterString));
               }
           }
           sb.Append(" and (soStates="+states[0]);
           for(int i=1;i<states.Count;i++)
           {
            sb.Append(" or soStates="+states[i]);
           
           }
           sb.Append(" )");

           DataTable dt=db.GetDataTable(sb.ToString(),"soId");

           foreach(DataRow dr in dt.Rows)
           {
           soList.Add(GetSoAccordingToSoId(Convert.ToInt32(dr["soId"])));
           }
           return soList;

       }

       public static List<SoCombine> SalesGetSoCombineAccordingTofilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> states)
       {


           List<SoCombine> soCombineList = new List<SoCombine>();
           if (states.Count == 0) return soCombineList;
           List<int> salesIds = new List<int>();

           if (includedSubs)
           {

               salesIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeSales()));
           }
           else
           {
               salesIds.Add(userId);
           }

           StringBuilder sb = new StringBuilder();
           sb.Append("select s.soId,customerName,salesId,orderDate,customerPo,soItemsId,partNo,mfg,dc,qty,unitPrice,soItemState from So s,SoItems si where(s.soId=si.soId) and  ( salesId=" + salesIds[0]);
           for (int i = 1; i < salesIds.Count; i++)
           {
               sb.Append(" or salesId=" + salesIds[i]);
           }
           sb.Append(" ) ");
               //append the filter
          if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
           {
                   sb.Append(string.Format(" and {0} like '%{1}%' ", filterColumn, filterString));
            }
     
           sb.Append(" and (soItemState=" + states[0]);
           for (int i = 1; i < states.Count; i++)
           {
               sb.Append(" or soItemState=" + states[i]);

           }
           sb.Append(" )");

           DataTable dt = db.GetDataTable(sb.ToString(), "soId");

           foreach (DataRow dr in dt.Rows)
           {
               soCombineList.Add(
                   new SoCombine { 
                    soId=Convert.ToInt32(dr["soId"]),
                    customerName=dr["customerName"].ToString(),
                    salesId=Convert.ToInt32(dr["salesId"]),
                    orderDate=Convert.ToDateTime(dr["orderDate"]),
                    customerPo=dr["customerPo"].ToString(),
                    soItemsId=Convert.ToInt32(dr["soItemsId"]),
                    partNo=dr["partNo"].ToString(),
                    mfg=dr["mfg"].ToString(),
                    dc=dr["dc"].ToString(),
                    qty=Convert.ToInt32(dr["qty"]),
                    unitPrice=Convert.ToSingle(dr["unitPrice"]),
                    soItemState=Convert.ToInt32(dr["soItemState"])
                   }
                   );
           }
           return soCombineList;

       }

       public static List<So> BuyerGetSoAccordingToFilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> states)

       {
           

           List<So> soList = new List<So>();
           
           if (states.Count == 0) return soList;

           List<int> buyersIds = new List<int>();

           if (includedSubs)
           {

               buyersIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
           }
           else
           {
               buyersIds.Add(userId);
           }

           StringBuilder sb = new StringBuilder();
           sb.Append(string .Format("select s.soId from So s,SoItems si,rfq r where(si.rfqId=r.rfqNo and s.soId=si.soId) and ( (r.firstPA={0} or r.secondPa={0})",buyersIds[0]));
           for (int i = 1; i < buyersIds.Count; i++)
           {
               sb.Append(string.Format(" or (firstPA={0} or secondPA={0}) ",buyersIds[i]));
           }
           sb.Append(" ) ");

           if (filterColumn.Trim() == "partNo" && (!string.IsNullOrWhiteSpace(filterString)))
           {
               List<int> idsList = GetSoIdByMPN(filterString.Trim());
               if (idsList.Count >= 1)
               {
                   sb.Append(" and (s.soId=" + idsList[0]);

                   for (int i = 1; i < idsList.Count; i++)
                   {
                       sb.Append(" or s.soId=" + idsList[i]);
                   }
                   sb.Append(" ) ");

               }
               else { return soList; }
           }
           else
           {
               //append the filter
               if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
               {
                   sb.Append(string.Format(" and s.{0} like '%{1}%' ", filterColumn, filterString));
               }
           }
           sb.Append(" and (soStates=" + states[0]);
           for (int i = 1; i < states.Count; i++)
           {
               sb.Append(" or soStates=" + states[i]);

           }
           sb.Append(" )");

           DataTable dt = db.GetDataTable(sb.ToString(), "soId");

           foreach (DataRow dr in dt.Rows)
           {
               soList.Add(GetSoAccordingToSoId(Convert.ToInt32(dr["soId"])));
           }
           return soList; 
       }

       public static List<SoCombine> BuyerGetSoCombineAccordingToFilter(int userId, bool includedSubs, string filterColumn, string filterString, List<int> states)
       {


           List<SoCombine> soCombineList = new List<SoCombine>();

           if (states.Count == 0) return soCombineList;

           List<int> buyersIds = new List<int>();

           if (includedSubs)
           {

               buyersIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
           }
           else
           {
               buyersIds.Add(userId);
           }

           StringBuilder sb = new StringBuilder();
           sb.Append(string.Format("select s.soId,s.customerName,s.salesId,orderDate,customerPo,soItemsId,si.partNo,si.mfg,si.dc,si.qty,si.unitPrice,soItemState from So s,SoItems si,Rfq r where(s.soId=si.soId and si.rfqId=r.rfqNo) and ( (r.firstPA={0} or r.secondPa={0})", buyersIds[0]));
           for (int i = 1; i < buyersIds.Count; i++)
           {
               sb.Append(string.Format(" or (firstPA={0} or secondPA={0}) ", buyersIds[i]));
           }
           sb.Append(" ) ");

               //append the filter
               if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
               {
                   sb.Append(string.Format(" and s.{0} like '%{1}%' ", filterColumn, filterString));
               }

               sb.Append(" and (soItemState=" + states[0]);
               for (int i = 1; i < states.Count; i++)
               {
                   sb.Append(" or soItemState=" + states[i]);

               }
               sb.Append(" )");

               DataTable dt = db.GetDataTable(sb.ToString(), "soId");

               foreach (DataRow dr in dt.Rows)
               {
                   soCombineList.Add(
                       new SoCombine
                       {
                           soId = Convert.ToInt32(dr["soId"]),
                           customerName = dr["customerName"].ToString(),
                           salesId = Convert.ToInt32(dr["salesId"]),
                           orderDate = Convert.ToDateTime(dr["orderDate"]),
                           customerPo = dr["customerPo"].ToString(),
                           soItemsId = Convert.ToInt32(dr["soItemsId"]),
                           partNo = dr["partNo"].ToString(),
                           mfg = dr["mfg"].ToString(),
                           dc = dr["dc"].ToString(),
                           qty = Convert.ToInt32(dr["qty"]),
                           unitPrice = Convert.ToSingle(dr["unitPrice"]),
                           soItemState = Convert.ToInt32(dr["soItemState"])
                       }
                       );
               }
               return soCombineList;
       }


       public static DataTable BuyerGetSoItemsWithSameVendor(int userId, int soItemId)
       {
           List<int> buyersIds = new List<int>();
          buyersIds.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
           //get vendorName
          StringBuilder sb = new StringBuilder();
          sb.Append(string.Format("select vendorName from soItems si, offer o where(si.rfqId=o.rfqNo) and si.soItemsId={0} and ( o.buyerId={1}", soItemId, buyersIds[0]));

          for (int i = 1; i < buyersIds.Count; i++)
          {
              sb.Append(string.Format(" or o.buyerId={0} ", buyersIds[i]));
          }
          sb.Append(")");

          DataTable dt = db.GetDataTable(sb.ToString(),"temp");
          if (dt.Rows.Count < 1)
              return null;
          List<string> vendorNameList = new List<string>();
          foreach (DataRow dr in dt.Rows)
          {
              vendorNameList.Add(dr["vendorName"].ToString());
          }

          sb.Clear();

          sb.Append(string.Format("select soItemsId,o.mpn,o.mfg,si.dc,o.vendorName,si.qty from soItem si,offer o where(si.rfqId=o.rfqNo) and si.soItemState={0} and (o.buyerId={1}", new SoItemApprove().GetStateValue(), buyersIds[0]));
          for (int i = 1; i < buyersIds.Count; i++)
          {
              sb.Append(string.Format(" or o.buyerId={0} ", buyersIds[i]));
          }
          sb.Append(string .Format(") and (vendorName='{0}'",vendorNameList[0]));
          for (int i = 1; i < vendorNameList.Count; i++)
          { 
             sb.Append(string.Format("or vendorName='{0}'",vendorNameList[i]));
          }
          sb.Append(" )");
         return db.GetDataTable(sb.ToString(), "temp");

       }



       public static int GetSoNumberFromRfqId(int rfqId)
       {
           string strSql = "select count(soId) from SoItems where rfqId=" + rfqId;
           return Convert.ToInt32(db.GetSingleObject(strSql));

       
       }



       public static List<So> GetSoAccordingToRfqId(int rfqId)
       {
           List<So> soList = new List<So>();
         string strSql="select soId from So where rfqId="+rfqId;

         DataTable dt = db.GetDataTable(strSql,"soId");
         foreach (DataRow dr in dt.Rows)
         {
             soList.Add(GetSoAccordingToSoId(Convert.ToInt32(dr["soId"])));
         
         }

         return soList;
       }

       public static So GetSoAccordingToSoId(int soId)
       {
           string strSql = "select * from So where soId=" + soId;

           DataTable dt = db.GetDataTable(strSql,"So");
           if (dt.Rows.Count != 1)
           {
               return null;
           }
           DataRow dr = dt.Rows[0];

           int? tmpApproverId=null;
           if(dr["approverId"]!=DBNull.Value)
           {
            tmpApproverId=Convert.ToInt32(dr["approverId"]);
           }
           DateTime? tmpApproveDate=null;
           if(dr["approveDate"]!=DBNull.Value)
           {
            tmpApproveDate=Convert.ToDateTime(dr["approveDate"]);
           }

           return new So
           {
               soId = Convert.ToInt32(dr["soId"]),
               customerName = dr["customerName"].ToString(),
               contact = dr["contact"].ToString(),
               salesId = Convert.ToInt32(dr["salesId"]),
               approverId = tmpApproverId,
               approveDate = tmpApproveDate,
               salesOrderNo = dr["salesOrderNo"].ToString(),
               orderDate = Convert.ToDateTime(dr["orderDate"]),
               customerPo = dr["customerPo"].ToString(),
               paymentTerm = dr["paymentTerm"].ToString(),
               freightTerm = dr["freightTerm"].ToString(),
               customerAccount = dr["customerAccount"].ToString(),
               specialInstructions = dr["specialInstructions"].ToString(),
               billTo = dr["billTo"].ToString(),
               shipTo = dr["shipTo"].ToString(),
               soStates = Convert.ToInt32(dr["soStates"])
           };

       }

       public static List<int> GetSoItemsIdsAccordingToSoId(int soId)
       {
           List<int> soItemsIdsList = new List<int>();
                      string strSql = "select soItemsId from SoItems where soId="+soId;
           DataTable dt = db.GetDataTable(strSql, "soitems");
           foreach (DataRow dr in dt.Rows)
           {
               soItemsIdsList.Add(Convert.ToInt32(dr[0]));
           }
       return soItemsIdsList;
       }


       public static SoItems GetSoItemInfoAccordingToSoItemId(int soItemId)
       { 
           string strSql = "select * from SoItems where soItemsId="+soItemId;
           DataRow dr = db.GetDataTable(strSql, "soitems").Rows[0];

               int? qtyShippedLocal; DateTime? shippedDateLocal;
               if (dr["qtyShipped"] == DBNull.Value)
               {
                   qtyShippedLocal = null;
               }
               else
               {
                   qtyShippedLocal = Convert.ToInt32(dr["qtyShipped"]);
               }
               if (dr["shippedDate"] == DBNull.Value)
               {
                   shippedDateLocal = null;
               }
               else
               { 
                 shippedDateLocal=Convert.ToDateTime(dr["shippedDate"]);
               }

               
              return    new SoItems
                   {
                       soItemsId = Convert.ToInt32(dr["soItemsId"]),
                       soId = Convert.ToInt32(dr["soId"]),
                       saleType = Convert.ToInt32(dr["saleType"]),
                       partNo = dr["partNo"].ToString(),
                       mfg = dr["mfg"].ToString(),
                       rohs = Convert.ToInt32(dr["rohs"]),
                       dc = dr["dc"].ToString(),
                       intPartNo = dr["intPartNo"].ToString(),
                       shipFrom = dr["shipFrom"].ToString(),
                       shipMethod=dr["shipMethod"].ToString(),
                       trackingNo = dr["trackingNo"].ToString(),
                       qty = Convert.ToInt32(dr["qty"]),
                       qtyshipped = qtyShippedLocal,
                       currencyType = Convert.ToInt32(dr["currency"]),
                       unitPrice = Convert.ToSingle(dr["unitPrice"]),
                      
                       dockDate = Convert.ToDateTime(dr["dockDate"]),
                       shippedDate =shippedDateLocal,
                       shippingInstruction = dr["shippingInstruction"].ToString(),
                       packingInstruction = dr["packingInstruction"].ToString(),
                       soItemState=Convert.ToInt32(dr["soItemState"])
                    };
       
       }



       public static List<SoItems> GetSoItemsAccordingToSoId(int soId)
       {
           List<SoItems> soItemsList = new List<SoItems>();

           string strSql = "select * from SoItems where soId="+soId;
           DataTable dt = db.GetDataTable(strSql, "soitems");
           foreach (DataRow dr in dt.Rows)
           {
               int? qtyShippedLocal; DateTime? shippedDateLocal;
               if (dr["qtyShipped"] == DBNull.Value)
               {
                   qtyShippedLocal = null;
               }
               else
               {
                   qtyShippedLocal = Convert.ToInt32(dr["qtyShipped"]);
               }
               if (dr["shippedDate"] == DBNull.Value)
               {
                   shippedDateLocal = null;
               }
               else
               { 
                 shippedDateLocal=Convert.ToDateTime(dr["shippedDate"]);
               }

               
               soItemsList.Add(
                   new SoItems
                   {
                       soItemsId = Convert.ToInt32(dr["soItemsId"]),
                       soId = Convert.ToInt32(dr["soId"]),
                       saleType = Convert.ToInt32(dr["saleType"]),
                       partNo = dr["partNo"].ToString(),
                       mfg = dr["mfg"].ToString(),
                       rohs = Convert.ToInt32(dr["rohs"]),
                       dc = dr["dc"].ToString(),
                       intPartNo = dr["intPartNo"].ToString(),
                       shipFrom = dr["shipFrom"].ToString(),
                       shipMethod=dr["shipMethod"].ToString(),
                       trackingNo = dr["trackingNo"].ToString(),
                       qty = Convert.ToInt32(dr["qty"]),
                       qtyshipped = qtyShippedLocal,
                       currencyType = Convert.ToInt32(dr["currency"]),
                       unitPrice = Convert.ToSingle(dr["unitPrice"]),
                      
                       dockDate = Convert.ToDateTime(dr["dockDate"]),
                       shippedDate =shippedDateLocal,
                       shippingInstruction = dr["shippingInstruction"].ToString(),
                       packingInstruction = dr["packingInstruction"].ToString(),
                       soItemState=Convert.ToInt32(dr["soItemState"])

                   });
          }
           return soItemsList; 
    
       }

       
       public static bool SaveSoMain(So so)
       {
           string strSql = "insert into So(customerName,contact,salesId,salesOrderNo,orderDate,customerPo,paymentTerm,freightTerm,customerAccount,specialInstructions,billTo,shipTo,soStates) " +
               string.Format(" values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',0)",so.customerName, so.contact, so.salesId, so.salesOrderNo, so.orderDate.ToShortDateString(), so.customerPo,
               so.paymentTerm, so.freightTerm, so.customerAccount, so.specialInstructions, so.billTo, so.shipTo);

           if (db.ExecDataBySql(strSql) == 1)
               return true;
           
           return false;
       }


       public static int GetTheInsertId(int salesId)
       {
           string strSql = "select max(soId) from So where salesId=" + salesId;
           return Convert.ToInt32(db.GetSingleObject(strSql));
       }

       
       /*
       public static bool SaveSoItems(int soId, List<SoItems> soitems)
       {
           List<string> strSqls = new List<string>();
           foreach (SoItems soItem in soitems)
           {

               string strsql = "insert into SoItems(soId,rfqId,saleType,partNo,mfg,rohs,dc,intPartNo,shipFrom,shipMethod,trackingNo,qty,qtyShipped,currency,unitPrice,dockDate,shippedDate,shippingInstruction,packingInstruction) " +
                   string.Format(" values({0},{1},{2},'{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}',{11},{12},{13},{14},'{15}','{16}','{17}','{18}')", soId, soItem.rfqId,soItem.saleType, soItem.partNo, soItem.mfg, soItem.rohs, soItem.dc,
                   soItem.intPartNo, soItem.shipFrom,soItem.shipMethod,soItem.trackingNo, soItem.qty, soItem.qtyshipped, soItem.currencyType, soItem.unitPrice, soItem.dockDate.ToShortDateString(),soItem.shippedDate.HasValue?soItem.shippedDate.Value.ToShortDateString():"null",
                   soItem.shippingInstruction, soItem.packingInstruction);
               strSqls.Add(strsql);
           
           }

           return db.ExecDataBySqls(strSqls);

       }*/

       public static int GetSoStateAccordingToSoId(int soId)
       {
           string strSql = " select soStates from so where soId=" + soId.ToString();
           return Convert.ToInt32(db.GetSingleObject(strSql));
      
       }

       public static void UpdateSoState(int soId, int userid, SoStatesEnum state)
       {
           List<string> strSqls = new List<string>();
           if (state ==SoStatesEnum.Approved)
           {
               strSqls.Add(string.Format("update so set soStates={0},approverId={1},approveDate='{2}' where soId={3}", (int)state, userid, DateTime.Now.ToShortDateString(), soId));
               strSqls.Add(string.Format("update soItems set soItemState={0} where soId={1}",new SoItemApprove().GetStateValue(),soId));

           }
           else if (state == SoStatesEnum.Rejected || state == SoStatesEnum.Cancel)
           {
               strSqls.Add(string.Format("update so set soStates={0} where soId={1}", (int)state, soId));
               strSqls.Add(string.Format("update soItems set soItemState={0} where soId={1}",state==SoStatesEnum.Rejected? new SoItemRejected().GetStateValue():new SoItemCancelled().GetStateValue(),soId));
           }
           else
           { 
              strSqls.Add(string.Format("update so set soStates={0} where soId={1}", (int)state, soId));
           
           }
          db.ExecDataBySqls(strSqls);
       }


       public static void UpdateSoItemState(int soItemId, int state)
       {
           string strSql = string.Format("update soItems set soItemState={0} where soItemsId={1}", state, soItemId);
           db.ExecDataBySql(strSql);
       
       }


      /*
       
       public static bool UpdateSoState(int soId,int userid, int state)
       {
           string strSql;

           if (state == new SoItemApprove().GetStateValue())
           {
               strSql = string.Format("update so set soStates={0},approverId={1},approveDate='{2}' where soId={3}", state, userid, DateTime.Now.ToShortDateString(), soId);
           }
           else if (state == new SoItemWaitingForShip().GetStateValue())
           {
               if (GetSoStateAccordingToSoId(soId) == new SoItemApprove().GetStateValue())
               {
                   strSql = string.Format("update so set soStates={0} where soId={1}", state, soId);

               }
               else
               {
                   return false;
               }
            
           }
           else
           {
               strSql = string.Format("update so set soStates={0} where soId={1}", state, soId);
           }
           
           if (db.ExecDataBySql(strSql) == 1)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       */

       public static bool UpdateSoMain(So so)
       {
           string strSql = string.Format("update So set customerName='{0}',contact='{1}',salesId={2},customerPo='{3}',paymentTerm='{4}',freightTerm='{5}',customerAccount='{6}',specialInstructions='{7}',billTo='{8}',shipTo='{9}' where soId={10}",
        so.customerName, so.contact, so.salesId, so.customerPo,so.paymentTerm, so.freightTerm, so.customerAccount, so.specialInstructions, so.billTo, so.shipTo,so.soId);

           if (db.ExecDataBySql(strSql) == 1)
               return true;

           return false;
       }
       public static void SetSoNumber(int soId)
       { 
           //This is only used for search by so number, because so id is digital value, can not be search by string
           string strsql=string.Format("update So set salesOrderNo='{0}' where soId={1}",Tool.Get6DigitalNumberAccordingToId(soId),soId);
           db.ExecDataBySql(strsql);
       
       }
       


       public static List<int> GetSoIdByMPN(string mpn)
       { 
        List<int> ids=new List<int>();

        string strSql = string.Format("select distinct soId from SoItems where partNo like '%{0}%'", mpn);
             DataTable dt=db.GetDataTable(strSql,"idTable");
           foreach(DataRow dr in dt.Rows)
           {
            ids.Add(Convert.ToInt32(dr["soId"]));
           }
           return ids;
       
       }

       


       public static string GetUpDateSoItemString(SoItems soItem)
       {

          return string.Format("update SoItems set saleType={0},partNo='{1}',mfg='{2}',rohs={3},dc='{4}',intPartNo='{5}',shipFrom='{6}',shipMethod='{7}',trackingNo='{8}',qty={9},qtyShipped={10},currency={11},unitPrice={12},dockDate='{13}',shippedDate={14},shippingInstruction='{15}',packingInstruction='{16}' where soItemsId={17} ",
       soItem.saleType, soItem.partNo, soItem.mfg, soItem.rohs, soItem.dc, soItem.intPartNo, soItem.shipFrom, soItem.shipMethod, soItem.trackingNo, soItem.qty, soItem.qtyshipped.HasValue ? soItem.qtyshipped.Value.ToString() : "null", soItem.currencyType, soItem.unitPrice, soItem.dockDate.ToShortDateString(), soItem.shippedDate.HasValue ? ("'" + soItem.shippedDate.Value.ToShortDateString() + "'") : "null",
    soItem.shippingInstruction, soItem.packingInstruction,soItem.soItemsId);

       }

       public static string GetSaveNewSoItemString(SoItems soItem)
       {
           string strsql = "insert into SoItems(soId,rfqId,saleType,partNo,mfg,rohs,dc,intPartNo,shipFrom,shipMethod,trackingNo,qty,qtyShipped,currency,unitPrice,dockDate,shippedDate,shippingInstruction,packingInstruction,soItemState) " +
        string.Format(" values({0},{1},{2},'{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}',{11},{12},{13},{14},'{15}',{16},'{17}','{18}',{19})", soItem.soId,soItem.rfqId, soItem.saleType, soItem.partNo, soItem.mfg, soItem.rohs, soItem.dc,
        soItem.intPartNo, soItem.shipFrom, soItem.shipMethod, soItem.trackingNo, soItem.qty, soItem.qtyshipped.HasValue?soItem.qtyshipped.Value.ToString():"null", soItem.currencyType, soItem.unitPrice, soItem.dockDate.ToShortDateString(), soItem.shippedDate.HasValue ?("'"+soItem.shippedDate.Value.ToShortDateString()+"'") : "null",
        soItem.shippingInstruction, soItem.packingInstruction,0);
           return strsql;
       }

       public static void DeleteSoItembySoItemId(int soItemsId)
       {
           string strSql= string.Format("delete from SoItems where soItemsId={0}",soItemsId);
           db.ExecDataBySql(strSql);
       }

       public static int GetRfqIdAccordingToSoId(int soId)
       {
           string strSql = "select rfqId from So where soId=" + soId;
           return Convert.ToInt32(db.GetSingleObject(strSql));
       }


       public static void UpdateSoItems(List<SoItemsContentAndState> soItemStateList)
       {
           List<string> strSqls = new List<string>();
           
           foreach (SoItemsContentAndState sics in soItemStateList)
           {
               switch (sics.state)
               {
                   case OrderItemsState.Normal:
                       break;

                   case OrderItemsState.New:
                       strSqls.Add(GetSaveNewSoItemString(sics.soitem));
                       break;

                   case OrderItemsState.Modified:
                      strSqls.Add(GetUpDateSoItemString(sics.soitem));
                       break;

               }
           }
           if (strSqls.Count == 0)
           {
               return;
           }
           
           db.ExecDataBySqls(strSqls);

       }
    }
}
