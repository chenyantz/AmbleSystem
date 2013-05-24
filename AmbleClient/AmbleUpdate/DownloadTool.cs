using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace AmbleUpdate
{
   public  class DownloadTool
    {
        public static void DownloadFile(string localFolder, string remoteFolder, string fileName)
        {

            string url = remoteFolder+ fileName;
            string path = localFolder + fileName;
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var wc = new WebClient();
            wc.DownloadFile(url, path);
        }


        public static string FormatFileSizeDescription(int bytes)
        {
            if (bytes > 1024 * 1024)
                return string.Format("{0}M", Math.Round((double)bytes / (1024 * 1024), 2, MidpointRounding.AwayFromZero));
            if (bytes > 1024)
                return string.Format("{0}K", Math.Round((double)bytes / 1024, 2, MidpointRounding.AwayFromZero));
            return string.Format("{0}B", bytes);
        }

        public static void DownloadFile(string localFolder, string remoteFolder, string fileName, ProgressBar bar)
        {
            string url = remoteFolder + "/" + fileName;
            string path = localFolder+ fileName;
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            if (res.ContentLength == 0)
                return;

            long fileLength = res.ContentLength;
            string totalSize = FormatFileSizeDescription(bar.Maximum);
            using (Stream srm = res.GetResponseStream())
            {
                var srmReader = new StreamReader(srm);
                var bufferbyte = new byte[fileLength];
                int allByte = bufferbyte.Length;
                int startByte = 0;
                while (fileLength > 0)
                {
                    int downByte = srm.Read(bufferbyte, startByte, allByte);
                    if (downByte == 0)
                    {
                        break;
                    }
                    ;
                    startByte += downByte;
                    allByte -= downByte;
                }

                var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                srm.Close();
                srmReader.Close();
                fs.Close();
            }
        }
    }

   internal class AppTool
   {
       public static void Start(string appName)
       {
           Process.Start(appName, "ok");
       }

       internal static void DeleteTempFolder(string folder)
       {
           try
           {
               Directory.Delete(folder, true);
           }
           catch
           {
           }
       }
   }
}
