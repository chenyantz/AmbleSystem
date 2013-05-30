using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AmbleUpdate
{
    public class UpdateInfo
    {
        public  const string ReleaseConfigFileName = "ReleaseList.xml";
        public static string tempPath = Path.GetTempPath();

        public static ReleaseList remoteReleaseList;
        public static string currentVersion;
        

        public static void Start()
        {
            currentVersion = OperatorFile.GetIniFileString("Version", "CurrentVersion", "", Environment.CurrentDirectory + "\\AmbleAppServer.ini");
            try
            {
                DownloadTool.DownloadFile(tempPath, "http://" + OperatorFile.GetIniFileString("DataBase", "Server", "", Environment.CurrentDirectory + "\\AmbleAppServer.ini") + "/AmbleUpdate/", ReleaseConfigFileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can not connect to the server,Please contact the Admin");
            }
            remoteReleaseList = new ReleaseList(tempPath + ReleaseConfigFileName);

        }







    }
}
