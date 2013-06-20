using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using MySql.Data.MySqlClient;




namespace AmbleClient.Admin.AccountMgr
{
    public class AccountMgr
    {
       static DataClass.DataBase db = new DataClass.DataBase();
       static MySqlDataReader sdr=null;


        public static bool ChangePasswd(int id, string passwd)
        {
            string strSql = string.Format("update account set accountPassword='{0}' where id={1}", passwd, id);
             if (db.ExecDataBySql(strSql) > 0)
               return true;
          
           return false;
        
        }


       //when check name and password, get the user's information
       public static PropertyClass CheckNameAndPasswd(string name,string password)
       {
           PropertyClass accountClass=null;


       string strSql="select * from account where accountName='"+name.Trim()+"' and accountPassword='"+password.Trim()+"'";
        try{
           sdr=db.GetDataReader(strSql);
            sdr.Read();
            if(sdr.HasRows)
            {
            accountClass=new PropertyClass();
            accountClass.UserId=sdr.GetInt32("id");
            accountClass.AccountName=name.Trim();
            accountClass.AccountPassword=password.Trim();
            accountClass.Email=sdr["email"].ToString();
            accountClass.Job=sdr.GetInt32("job");;
            accountClass.Superviser=sdr.GetInt32("superviser");
            }
        }
           catch(Exception ex)
        {
            Logger.Error(ex.Message);
            Logger.Error(ex.StackTrace);

          }
        finally
        {
            sdr.Close();
        }
          return accountClass; 
      
       }

       public static bool IsNameExist(string name)
       {
       string strSql="select * from account where accountName='"+name.Trim()+"'";
           try{
               sdr=db.GetDataReader(strSql);
               sdr.Read();
               if(sdr.HasRows)
               {
                 sdr.Close();
                return true;
               }
               else
               {
               sdr.Close();
               return false;
               }
            
           }
           catch(Exception e)
           {
               Logger.Error(e.Message);
               Logger.Error(e.StackTrace);
           }
           finally
           {
           sdr.Close();
           }
           return true;
       }
       
  
        public static DataSet ReturnDataSet()
        {

           return db.GetDataSet("select * from account", "test");
        
      
        }
       public static DataTable ReturnWholeAccountTable()
       {
           return db.GetDataTable("select * from account", "AccountTable");
       
       }

       public static bool AddAnAccount(string accountName,string password,string email,int job,int superviser)
       {
           string strSql = "INSERT INTO account(accountName,accountPassword,email,job,superviser)VALUES('" + accountName + "','" + password+ "','" +
               email + "'," + job + "," + superviser + ")";
           if (db.ExecDataBySql(strSql) > 0)
               return true;
                    
           return false;
       }


       public static bool ModifyAnAccount(int id,string accountName,string password,string email,int job,int superviser)
       {
           string strSql = "UPDATE account SET accountName='" + accountName + "',accountPassword='" + password + "',email='" + email + "',job="
               + job + ",superviser=" + superviser + "   WHERE id="+id;
           if (db.ExecDataBySql(strSql) > 0)
               return true;
           return false;

       }

       public static List<int> GetAllIds()
       {
           List<int> ids = new List<int>();
           string strSql = "select id from account";
           DataTable dt = db.GetDataTable(strSql, "idTable");
           if (dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   ids.Add(Convert.ToInt32(dr["id"]));
               }
           }
           return ids;
       
       }


       public static List<int> GetAllSubsId(int id,List<int> jobs)
       {
            List<int> allSubsId = new List<int>();
           allSubsId.Add(id);

           for (int startIndex = 0; allSubsId.Count > startIndex; startIndex++)
           {
              string strSql = "select id,job from account where superviser=" +allSubsId[startIndex];
              DataTable dt = db.GetDataTable(strSql,"idTable");
           if (dt.Rows.Count > 0)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   int subId = Convert.ToInt32(dr["id"]);
                   if (!allSubsId.Contains(subId))
                   {
                      if(jobs==null)
                      {
                       allSubsId.Add(subId);
                      }
                      else
                      {    if(jobs.Contains(Convert.ToInt32(dr["job"])))
                             {
                              allSubsId.Add(subId);
                             }
                      
                      }
                   }
               }
           }
 
        }
           return allSubsId;           
       }


       public static Dictionary<int, string> GetIdsAndNames(List<int> ids)
       {
           Dictionary<int,string> idsAndNames=new Dictionary<int,string>();
           string strSql = "select id,accountName from account";
           DataTable dt = db.GetDataTable(strSql, "table");

           foreach (int id in ids)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   if (Convert.ToInt32(dr["id"]) == id)
                   {
                       if (!idsAndNames.Keys.Contains(id))
                       {

                       idsAndNames.Add(id, dr["accountName"].ToString());
                      
                       }
                   }
               
               }
           
           }
           return idsAndNames;
       
       }

       public static string GetNameById(int id)
       {
           string strSql = "select accountName from account where id=" + id;
           return (string)db.GetSingleObject(strSql);
       
       }

       public static int GetSuperviserId(int id)
       {
           string strSql = "select superviser from account where id=" + id;
           int superId=Convert.ToInt32(db.GetSingleObject(strSql));
           if (superId == -1)
           {
               superId = 1;
           }
           return superId;


       }

       public static string GetEmailAddressById(int id)
       {
           string strSql = "select email from account where id=" + id;
           return (string)db.GetSingleObject(strSql);
       
       }

       public static List<string> GetEmailsAccordingToJob(JobDescription job)
       {
           List<string> mails=new List<string>();
           string strsql = "select email from account where job=" + (int)job;
            DataTable dt = db.GetDataTable(strsql,"idTable");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    mails.Add(dr[0].ToString());

                }
            }
         return mails;
       }



    }
}
;

