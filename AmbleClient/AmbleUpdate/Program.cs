using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AmbleUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UpdateInfo.Start();
            Application.Run(new UpdateForm(UpdateInfo.tempPath, UpdateInfo.currentVersion, UpdateInfo.remoteReleaseList));
        }
    }
}
