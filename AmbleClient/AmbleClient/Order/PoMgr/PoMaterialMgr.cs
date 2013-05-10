using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;

namespace AmbleClient.Order.PoMgr
{
    public class UploadFileInfo
    {
     public int fileId;
     public int poItemId;
     public string pathAndFileName;
     public string fileName;
     public string fileType;
     public int size;
     public DateTime uploadDate;
     public byte[] fileContent;
    }
    
    
    public  class PoMaterialMgr
    {
        static DataClass.DataBase db = new DataClass.DataBase(); 
        
        
        public static bool UploadFiles(List<UploadFileInfo> uploadFiles)
        {
            List<MySqlCommand> commands = new List<MySqlCommand>();
            foreach (UploadFileInfo fileInfo in uploadFiles)
            {
                if (fileInfo.size != 0)
                {
                    MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Format("insert into PoMaterials(poItemId,fileName,fileType,size,uploadDate,fileContent)values({0},'{1}','{2}',{3},'{4}',@fileContet)",
                        fileInfo.poItemId, fileInfo.fileName, fileInfo.fileType, fileInfo.size, fileInfo.uploadDate.ToShortDateString());
                    MySqlParameter pars = new MySqlParameter("@fileContent", MySqlDbType.MediumBlob);
                    pars.Value = fileInfo.fileContent;
                    command.Parameters.Add(pars);
                    commands.Add(command);
                }
            }
          return  db.ExecDataBySqls(commands);
        }

        public static byte[] GetFileContentByFileId(int fileId)
        {
            byte[] file = null;
            string sql = "select fileContent from PoMaterials where fileId=" + fileId;
            MySqlDataReader dr = db.GetDataReader(sql);
            if (dr.Read())
            { 
            
              file=(byte[]) dr[0];
            }
            return file;
        
        }

        public static List<UploadFileInfo> GetTheFileInfoAccordingToPoItemId(int poItemId)
        {
            List<UploadFileInfo> fileInfos=new List<UploadFileInfo>();
            string strSql="select fileId,fileName,fileType,size,uploadDate from PoMaterials where poItemId="+poItemId;
            DataTable dt=db.GetDataTable(strSql,"tmp");
            foreach(DataRow dr in dt.Rows)
            {
                fileInfos.Add(
                     new UploadFileInfo
                     {
                         fileId=Convert.ToInt32(dr["fileId"]),
                         poItemId=poItemId,
                         fileName=dr["fileName"].ToString(),
                         fileType=dr["fileType"].ToString(),
                         size=Convert.ToInt32(dr["size"]),
                         uploadDate=Convert.ToDateTime(dr["uploadDate"]),
                         fileContent=null
                    }
                    );
            
            }
            return fileInfos;
        }

        public static void DeleteFileInfoByFileId(int fileId)
        {
            string strSql = "delete from PoMaterials where fileId=" + fileId;
            db.ExecDataBySql(strSql);
        
        
        }

    }
}
