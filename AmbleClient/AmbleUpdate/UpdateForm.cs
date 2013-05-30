using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AmbleUpdate
{
    public partial class UpdateForm : Form
    {
        string tempPath;
        string localReleaseVersion;
			ReleaseList remoteRelease;
        ReleaseFile[] diff;

        bool downloaded;

       const string RetryText = " Retry ";
        const string FinishText = "Complete ";

        public UpdateForm()
        {
            InitializeComponent();
        }

        public UpdateForm(
			string tempPath,
			string localRelease,
			ReleaseList remoteRelease
			)
		{
			InitializeComponent();
			this.tempPath = tempPath;
			this.localReleaseVersion = localRelease;
			this.remoteRelease = remoteRelease;
            this.diff = remoteRelease.Files.ToArray();
			
		}

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            label1.Text = string.Format("Current Version：{0}  Latest Version：{1}  Release Date：{2}", localReleaseVersion, remoteRelease.ReleaseVersion,
                remoteRelease.ReleaseDate);
            //升级内容
            textBox1.Text = remoteRelease.UpdateDescription;

        }

        private void Upgrade()
        {
           DoUpgrade();
        }
        private void DoUpgrade()
        {
            downloaded = false;
            progressBar1.Value = 0;
            foreach (ReleaseFile file in diff)
            {
                try
                { 
                    DownloadTool.DownloadFile(tempPath,
                        "http://" + OperatorFile.GetIniFileString("DataBase", "Server", "", Environment.CurrentDirectory + "\\AmbleAppServer.ini") + "/AmbleUpdate/" + remoteRelease.ReleaseVersion, file.FileName, progressBar1);
                }
                catch (Exception ex)
                {
                    AppTool.DeleteTempFolder(tempPath);
                    MessageBox.Show(file.FileName + "Download Fail， Please try later");
                    return;
                }
            }
            try
            {
                foreach (ReleaseFile file in diff)
                {
                    string dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory + file.FileName);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    File.Copy(tempPath + file.FileName, AppDomain.CurrentDomain.BaseDirectory + file.FileName, true);
                }
            }
            catch (Exception ex)
            {
                AppTool.DeleteTempFolder(tempPath);
                MessageBox.Show(ex.Message, "Upgrate Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            downloaded = true;
            OperatorFile.WriteIniFileString("Version", "CurrentVersion",remoteRelease.ReleaseVersion, Environment.CurrentDirectory + "\\AmbleAppServer.ini");
            MessageBox.Show("Upgrade Successful.");
            Application.Exit();
           
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppTool.DeleteTempFolder(tempPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Upgrade();
        }
    }
}
