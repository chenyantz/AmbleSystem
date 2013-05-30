using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AmbleClient.Help
{
    public class UpdateInfo
    {
        public  const string UPDATER_EXE_NAME = "AutoUpdate.exe";
        public  const string ReleaseConfigFileName = "ReleaseList.xml";
        public static string tempPath = Path.GetTempPath();

        public static ReleaseList remoteReleaseList;
        public static string currentVersion;
        

        public static bool NeedUpdate()
        {
            currentVersion = AmbleClient.ComClass.OperatorFile.GetIniFileString("Version", "CurrentVersion", "", Environment.CurrentDirectory + "\\AmbleAppServer.ini");
            try
            {
                DownloadTool.DownloadFile(tempPath, "http://" + ServerInfo.GetServerAddress() + "/AmbleUpdate/", ReleaseConfigFileName);
            }
            catch(Exception)
            {
                MessageBox.Show("Can not connect to the server,Please contact the Admin");
                return false;
            }
            remoteReleaseList = new ReleaseList(tempPath + ReleaseConfigFileName);
            string remoteRelease = remoteReleaseList.ReleaseVersion;
            if (Compare(remoteRelease, currentVersion) > 0)
                return true;
            return false;
        }

        public static int Compare(string remoteNewVersion,string version)
        {
            string[] newVersion = remoteNewVersion.Split('.');
            string[] currentVersion = version.Split('.');
            int i = 0;
            foreach (string v in newVersion)
            {
                int myNumber = int.Parse(v);
                int otherNumber = int.Parse(currentVersion[i]);
                if (myNumber != otherNumber)
                    return myNumber - otherNumber;
                i++;
            }
            return 0;
        }





    }
}
