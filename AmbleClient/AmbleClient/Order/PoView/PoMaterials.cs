using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AmbleClient.Order.PoMgr;

namespace AmbleClient.Order.PoView
{
    public partial class PoMaterials : Form
    {
        private int poItemId;
        private int selectedFileId;

        public PoMaterials(int poItemId)
        {
            InitializeComponent();
            this.poItemId = poItemId;
        }

        private void tsbUpload_Click(object sender, EventArgs e)
        {
            List<PoMgr.UploadFileInfo> uploadFileInfos=new List<PoMgr.UploadFileInfo>();
            string[] fileNames;
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.RestoreDirectory = false;
            ofd.Multiselect = true;
            if (DialogResult.OK == ofd.ShowDialog())
            {
               fileNames = ofd.FileNames;

                foreach (string fileName in fileNames)
                {
                    uploadFileInfos.Add(
                        new PoMgr.UploadFileInfo { 
                         poItemId=this.poItemId,
                         pathAndFileName=fileName,
                         fileName=Path.GetFileName(fileName),
                         fileType=Path.GetExtension(fileName),
                         uploadDate=DateTime.Now
                        }
                    );
                }

                foreach (PoMgr.UploadFileInfo upLoadfileInfo in uploadFileInfos)
                {
                    FileInfo fi = new FileInfo(upLoadfileInfo.pathAndFileName);
                    FileStream fs=fi.OpenRead();
                    if (fi.Length / 1024 / 1024 >= 16)
                    {
                        upLoadfileInfo.size = 0;
                        upLoadfileInfo.fileContent= null;
                        MessageBox.Show("The size of " + upLoadfileInfo.fileName + " >=16M. This file will not be uploaded");
                        continue;
                    }

                    upLoadfileInfo.size =Convert.ToInt32(fi.Length);
                    byte[] bytes=new byte[upLoadfileInfo.size];
                    fs.Read(bytes,0,upLoadfileInfo.size);
                    upLoadfileInfo.fileContent = bytes;
                
                }
                if (PoMgr.PoMaterialMgr.UploadFiles(uploadFileInfos))
                {
                    MessageBox.Show("Upload files successfully");

                }
                else
                {
                    MessageBox.Show("Met some errors while uploading the files");
                }

            }

        }

        private void tsbDownload_Click(object sender, EventArgs e)
        {
            byte[] fileContent = PoMgr.PoMaterialMgr.GetFileContentByFileId(this.selectedFileId);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            if(DialogResult.OK==sfd.ShowDialog())
            {
                string fileName=sfd.FileName;
                StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
                sw.Write(fileContent);
            
            }



        }

        private void FillTheListView()
        {
            List<PoMgr.UploadFileInfo> fileInfos = PoMgr.PoMaterialMgr.GetTheFileInfoAccordingToPoItemId(this.poItemId);
            foreach (UploadFileInfo ufi in fileInfos)
            {
                listView1.Items.Add(ufi.fileName);
            }
        }




        private void tsbDelete_Click(object sender, EventArgs e)
        {
            PoMgr.PoMaterialMgr.DeleteFileInfoByFileId(this.selectedFileId);
            FillTheListView();
        }

        private void PoMaterials_Load(object sender, EventArgs e)
        {
            FillTheListView();
        }





    }
}
