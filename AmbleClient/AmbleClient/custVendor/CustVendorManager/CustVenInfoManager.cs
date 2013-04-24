using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbleClient.custVendor.CustVendorManager
{
   public class CustVenInfoManager
    {

       static private CustVenInfoEntities custVenEntity = new CustVenInfoEntities();


       public static void AddCustVend(custvendorinfo custVenInfo)
       {
           custVenEntity.custvendorinfo.AddObject(custVenInfo);
           custVenEntity.SaveChanges();
       }

       public static bool IsCvNameExist(int cvtype,string cvName,int userId)
       {
           var cv = from custVen in custVenEntity.custvendorinfo
                    where custVen.cvtype == cvtype && custVen.cvname == cvName && custVen.ownerName == (short)userId
                    select custVen;
           if (cv.Count() == 0)
               return false;
           else
               return true;
       }

       public static List<custvendorinfo> GetMyCustomerOrVendors(int cvtype, int usrId,string filterColumn,string filterString )
       {
           if (filterColumn.Trim().Length == 0 || filterString.Trim().Length == 0)
           {


               var cv = from custVen in custVenEntity.custvendorinfo
                        where custVen.cvtype == cvtype && custVen.ownerName == (short)usrId
                        select custVen;
               return cv.ToList();
           }
           else if (filterColumn == "cvname")
           {
               var cv = from custVen in custVenEntity.custvendorinfo
                        where custVen.cvtype == cvtype && custVen.ownerName == (short)usrId && custVen.cvname.Contains(filterString.Trim())
                        select custVen;
               return cv.ToList();

           }
           else
           {
               return new List<custvendorinfo>();
           }

       
       }

       public static List<custvendorinfo> GetCVIcanSee(int cvtype, int usrid,string filterColumn,string filterString)
       {
           List<int> subIds = AmbleClient.Admin.AccountMgr.AccountMgr.GetAllSubsId(usrid, null);

           if (filterColumn.Trim().Length == 0 || filterString.Trim().Length == 0)
           {
               var cv = from custVen in custVenEntity.custvendorinfo
                        where custVen.cvtype == cvtype && subIds.Contains(custVen.ownerName)
                        select custVen;
               return cv.ToList();
           }
           else if (filterColumn == "cvname")
           {
               var cv = from custVen in custVenEntity.custvendorinfo
                        where custVen.cvtype == cvtype && subIds.Contains(custVen.ownerName) && custVen.cvname.Contains(filterString.Trim())
                        select custVen;
               return cv.ToList();

           }
           else
           {
               return new List<custvendorinfo>();
           }


       }


       public static List<custvendorinfoshipto> GetShipTo(int custVenId)
       {
           var shipTos = custVenEntity.custvendorinfoshipto.Where(shipto => shipto.cvId == custVenId);
           return shipTos.ToList();
       }

       public static void DeleteCV(int cvId)
       {
           var cvinfo = custVenEntity.custvendorinfo.First(cv => cv.cvId == cvId);
           custVenEntity.custvendorinfo.DeleteObject(cvinfo);
           var cvshipInfos = custVenEntity.custvendorinfoshipto.Where(cvShip => cvShip.cvId == cvId);
           if (cvshipInfos != null)
           {
               foreach (custvendorinfoshipto shipInfo in cvshipInfos)
               {
                   custVenEntity.custvendorinfoshipto.DeleteObject(shipInfo);
               }
           }
           custVenEntity.SaveChanges();
       
       }

       public static void ModifyCV(custvendorinfo cvInfo, custvendorinfoshipto cvShipTo)
       { }





    }
}
