﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace AmbleClient.OfferGui.OfferMgr
{
   public class OfferMgr
    {
      
       
       static DataClass.DataBase db = new DataClass.DataBase();


       
       public static bool SaveOffer(Offer offer)
       {
           string strSql = "insert into offer(rfqNo,mpn,mfg,vendorName,contact,phone,fax,email,quantity,price,LT,buyerId,offerDate,offerStates,notes,packing) " +
               string.Format(" values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}',{11},'{12}',{13},'{14}','{15}')", offer.rfqNo, offer.mpn, offer.mfg, offer.vendorName, offer.contact,
               offer.phone, offer.fax, offer.email, offer.quantity, offer.price, offer.LT,offer.buyerId, offer.offerDate.ToShortDateString(), offer.offerStates,offer.notes,offer.packing);

           int row = db.ExecDataBySql(strSql);
           if (row == 1)
               return true;
           else
               return false;
       }


       public static void UpdateOffer(Offer offer)
       {
           string strSql = string.Format("update offer set mpn='{0}',mfg='{1}',vendorName='{2}',contact='{3}',phone='{4}',fax='{5}',email='{6}',quantity={7},price={8},LT='{9}',buyerId={10},notes='{11}',packing='{12}', where offerId={13} ",
          offer.mpn, offer.mfg, offer.vendorName, offer.contact,offer.phone, offer.fax, offer.email, offer.quantity, offer.price, offer.LT,offer.buyerId, offer.notes,offer.packing,offer.offerId);

         db.ExecDataBySql(strSql);

       }



       public static int GetNewSavedOfferId(int buyerId)
       {
           string strSql = "select max(offerId) from offer where buyerId=" + buyerId;
           return Convert.ToInt32(db.GetSingleObject(strSql));
       
       }

       public static List<Offer> SalesGetOfferAccordingToFilter(int userId, bool allOffer, string filterColumn, string filterString, List<int> intStateList)
       {
           List<Offer> offerList = new List<Offer>();
           if (intStateList.Count == 0) return offerList;

           StringBuilder sb = new StringBuilder();
           if (allOffer)
           {
               sb.Append("select * from offer o where ( TRUE )");
           }
           else
           {
               sb.Append(string.Format("select o.* from offer o left join rfq r on o.rfqNo=r.rfqNo where( r.salesId={0} )", UserInfo.UserId));
           }
           //append the filter
           if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
           {
               sb.Append(string.Format(" and o.{0} like '%{1}%' ", filterColumn, filterString));
           }

           sb.Append(" and ( o.offerStates=" + intStateList[0]);
           for (int i = 1; i < intStateList.Count; i++)
           {
               sb.Append(" or o.offerStates=" + intStateList[i]);

           }
           sb.Append(" )");

           DataTable dt = db.GetDataTable(sb.ToString(), "offers");
           foreach (DataRow dr in dt.Rows)
           {
               offerList.Add(GetOfferFromDataRow(dr));

           }
           return offerList;



       }






       public static List<Offer> GetOfferAccordingToFilter(int userId, bool allOffer, string filterColumn, string filterString, List<int> intStateList)
       {
           List<Offer> offerList = new List<Offer>();
           if (intStateList.Count == 0) return offerList;

           StringBuilder sb = new StringBuilder();
           if (allOffer)
           {
               sb.Append("select * from offer where ( TRUE )");
           }
           else
           {
               sb.Append(string.Format("select * from offer where ( buyerId={0} )", UserInfo.UserId));
           }
           //append the filter
           if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
           {
               sb.Append(string.Format(" and {0} like '%{1}%' ", filterColumn, filterString));
           }

           sb.Append(" and ( offerStates=" + intStateList[0]);
           for (int i = 1; i < intStateList.Count; i++)
           {
               sb.Append(" or offerStates=" + intStateList[i]);

           }
           sb.Append(" )");

           DataTable dt = db.GetDataTable(sb.ToString(), "offers");
           foreach (DataRow dr in dt.Rows)
           {
               offerList.Add(GetOfferFromDataRow(dr));

           }
           return offerList;

       }



      /*
       public List<Offer> GetOfferAccordingToFilter(int userId, bool includeSubs,string filterColumn,string filterString, List<int> intStateList)
        {
            List<Offer> offerList = new List<Offer>();
            if (intStateList.Count == 0) return offerList;
            List<int> buyerId = new List<int>();

            if (includeSubs)
            {
                buyerId.AddRange(AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(userId, UserCombine.GetUserCanBeBuyers()));
            }
            else
            {
                buyerId.Add(userId);
            }

           StringBuilder sb=new StringBuilder();
           sb.Append("select * from offer where ( buyerId="+buyerId[0]);
           for(int i=1;i<buyerId.Count;i++)
           {
               sb.Append(" or buyerId=" + buyerId[i]);
           }
           sb.Append(" ) ");

           //append the filter
           if ((!string.IsNullOrWhiteSpace(filterColumn)) && (!string.IsNullOrWhiteSpace(filterString)))
           { 
            sb.Append(string.Format(" and {0} like '%{1}%' ",filterColumn,filterString));
           }

           sb.Append(" and ( offerStates="+intStateList[0]);
           for(int i=1;i<intStateList.Count;i++)
           {
            sb.Append(" or offerStates="+intStateList[i]);
           
           }
           sb.Append(" )");

           DataTable dt=db.GetDataTable(sb.ToString(),"offers");
           foreach (DataRow dr in dt.Rows)
           {
               offerList.Add(GetOfferFromDataRow(dr));
           
           }
           return offerList;
  
      }*/

       public static bool HasOfferByRfq(int rfqId)
       {
           string strSql = "select count(*) from offer where rfqNo=" + rfqId.ToString();
           int offerNumber = Convert.ToInt32(db.GetSingleObject(strSql));
           if (offerNumber > 0)
               return true;
           return false;
       
       }



       public static List<Offer> GetOffersByRfqId(int rfqId)
       {
           List<Offer> offerList = new List<Offer>();

           string strSql = "select * from offer where rfqNo=" + rfqId;

           DataTable dt = db.GetDataTable(strSql, "tempTable");
           foreach (DataRow dr in dt.Rows)
           {
               offerList.Add(GetOfferFromDataRow(dr));
           
           }
           return offerList;

       }

       public static Offer GetOfferByOfferId(int offerId)
       {

           string strSql = "select * from offer where offerId=" + offerId.ToString();

           DataTable dt = db.GetDataTable(strSql, "tempTable");

         return GetOfferFromDataRow(dt.Rows[0]);
       }





       private static Offer GetOfferFromDataRow(DataRow dr)
       {

           return new Offer
           {
               offerId=Convert.ToInt32(dr["offerId"]),
               rfqNo=Convert.ToInt32(dr["rfqNo"]),
               mpn=dr["mpn"].ToString(),
               mfg=dr["mfg"].ToString(),
               vendorName=dr["vendorName"].ToString(),
               contact=dr["contact"].ToString(),
               phone=dr["phone"].ToString(),
               fax=dr["fax"].ToString(),
               email=dr["email"].ToString(),
               packing=dr["packing"].ToString(),
               quantity=Convert.ToInt32(dr["quantity"]),
               price=Convert.ToSingle(dr["price"]),
               LT=dr["LT"].ToString(),
               buyerId=Convert.ToInt32(dr["buyerId"]),
               offerDate=Convert.ToDateTime(dr["offerDate"]),
               offerStates=Convert.ToInt32(dr["offerStates"]),
               notes=dr["notes"].ToString()
           };
       }



       public static bool ChangeOfferState(int offerState, int offerId)
       {
           string strSql = string.Format("update offer set offerStates={0} where offerId={1}", offerState,offerId);
           if (db.ExecDataBySql(strSql) == 1)
           {
               if(offerState==1)
               {
                   FillTheRfqCost(offerId);
                }
               return true;
           
           }

           return false;
       }

       public static void FillTheRfqCost(int offerId)
       {
           string strSql = "select rfqNo,price from offer where offerId=" + offerId;
           DataTable dt = db.GetDataTable(strSql, "offer");
           DataRow dr = dt.Rows[0];
           int rfqNo = Convert.ToInt32(dr["rfqNo"]);
           float price = Convert.ToSingle(dr["price"]);
           //get the cost in the RFQ table
           strSql = "select cost,rfqStates from rfq where rfqNo=" + rfqNo;
           dr = db.GetDataTable(strSql, "tmp").Rows[0];
           int rfqStates = Convert.ToInt32(dr["rfqStates"]);

           if ((dr["cost"] == DBNull.Value) || Convert.ToSingle(dr["cost"]) > price)
           {
               if (rfqStates == (int)AmbleClient.RfqGui.RfqManager.RfqStatesEnum.Routed)
               {
                   strSql = string.Format("update rfq set cost={0},rfqStates={1} where rfqNo={2}", price, (int)AmbleClient.RfqGui.RfqManager.RfqStatesEnum.Offered, rfqNo);

               }
               else
               {
                   strSql = string.Format("update rfq set cost={0} where rfqNo={1}", price, rfqNo);
               
               }
               db.ExecDataBySql(strSql);
           }
       }

       public static void SendOfferRouteEmail(int offerId)
       {
           List<string> emailTos = new List<string>();

           Offer offer = GetOfferByOfferId(offerId);

           AmbleClient.RfqGui.RfqManager.Rfq rfq = AmbleClient.RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(offer.rfqNo);

           int salesId = rfq.salesId;
           emailTos.Add(AmbleClient.Admin.AccountMgr.AccountMgr.GetEmailAddressById(salesId));

           List<string> ccTos = new List<string>();
           string email1 = Admin.AccountMgr.AccountMgr.GetEmailAddressById(UserInfo.UserId);
           if (!emailTos.Contains(email1))
           {
               ccTos.Add(email1);
           }
           string subject = string.Format("Your RFQ {0} (MPN：{1}) has an Offer.", Tool.Get6DigitalNumberAccordingToId(rfq.rfqNo), rfq.partNo);
           StringBuilder body = new StringBuilder();
           body.Append("<table border=\"0\">");
           body.Append(string.Format("<tr><td>Offer ID</td><td>{0}</td>", Tool.Get6DigitalNumberAccordingToId(offer.offerId)));
           body.Append(string.Format("<tr><td>MPN</td><td>{0}</td>", offer.mpn));
           body.Append(string.Format("<tr><td>MFG</td><td>{0}</td>", offer.mfg));
           body.Append(string.Format("<tr><td>QTY</td><td>{0}</td>", offer.quantity));
           body.Append(string.Format("<tr><td>LT</td><td>{0}</td>", offer.LT));
           body.Append(string.Format("<tr><td>Price</td><td>{0}</td>", offer.price));
           body.Append("</table>");

           AmbleClient.MailService.MailService.SendMail(emailTos, ccTos, subject, body.ToString());


       }

    }
}
