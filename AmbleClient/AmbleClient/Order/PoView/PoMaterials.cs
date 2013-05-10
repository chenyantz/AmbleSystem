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
using System.Runtime.InteropServices;
using Microsoft.Win32;
 using System.Security;


namespace AmbleClient.Order.PoView
{
    public partial class PoMaterials : Form
    {
        private int poItemId;
        private int selectedIndex=-1;


        private List<PoMgr.UploadFileInfo> fileInfos = new List<UploadFileInfo>();
        private ImageList largeIconImageList = new ImageList();
        private ImageList smallIconImageList = new ImageList();
        private Dictionary<string, string> extDescrtiption = new Dictionary<string, string>();



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
                FillTheListView();
            }

        }

        private void tsbDownload_Click(object sender, EventArgs e)
        {

            byte[] fileContent = PoMgr.PoMaterialMgr.GetFileContentByFileId(fileInfos[selectedIndex].fileId);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.FileName = fileInfos[selectedIndex].fileName;
            if(DialogResult.OK==sfd.ShowDialog())
            {
                string fileName = sfd.FileName;
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fs.Write(fileContent, 0, fileContent.Length);
                fs.Close();
            }



        }

        private void FillTheListView()
        {
            fileInfos.Clear();
            listView1.Items.Clear();
            largeIconImageList.Images.Clear();
            smallIconImageList.Images.Clear();
            fileInfos.AddRange(PoMgr.PoMaterialMgr.GetTheFileInfoAccordingToPoItemId(this.poItemId));
            foreach (UploadFileInfo ufi in fileInfos)
            {
                //get ext;
                Icon largeIcon, smallIcon;string description;
                IconInfo.GetExtsIconAndDescription(ufi.fileType,out largeIcon,out smallIcon,out description);
                if((largeIcon!=null)&&(!largeIconImageList.Images.Keys.Contains(ufi.fileType)))
                {
                 largeIconImageList.Images.Add(ufi.fileType,largeIcon);
                
                }
                 if((smallIcon!=null)&&(!smallIconImageList.Images.Keys.Contains(ufi.fileType)))
                {
                 smallIconImageList.Images.Add(ufi.fileType,smallIcon);
                
                }
                if(!extDescrtiption.Keys.Contains(ufi.fileType))
                {
                 extDescrtiption.Add(ufi.fileType,description);
                }

           
            }
            listView1.LargeImageList = this.largeIconImageList;
            listView1.SmallImageList = this.smallIconImageList;


            listView1.BeginUpdate();
            foreach (UploadFileInfo ufi in fileInfos)
            {
                ListViewItem lvi = new ListViewItem(ufi.fileName, ufi.fileType);
                lvi.SubItems.Add((ufi.size/1024).ToString()+"K");
                lvi.SubItems.Add(ufi.uploadDate.ToShortDateString());
                lvi.SubItems.Add(extDescrtiption[ufi.fileType]);
                listView1.Items.Add(lvi);
            }
            listView1.EndUpdate();

            EnableDisableButtons();

        }


        private void EnableDisableButtons()
        {
          if(this.selectedIndex==-1)
          {
           this.tsbDelete.Enabled=false;
           this.tsbDownload.Enabled=false;
          }
          else
          {
           this.tsbDelete.Enabled=true;
              this.tsbDownload.Enabled=true;
          }
        
        }





        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Delete file: " + fileInfos[selectedIndex].fileName + " ?", "", MessageBoxButtons.YesNo))
            {
                PoMgr.PoMaterialMgr.DeleteFileInfoByFileId(fileInfos[selectedIndex].fileId);
                FillTheListView();
            }
        }

        private void PoMaterials_Load(object sender, EventArgs e)
        {
            FillTheListView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                this.selectedIndex = listView1.SelectedIndices[0];
            }
            EnableDisableButtons();

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
          string str1=Path.GetTempFileName();
          byte[] fileContent = PoMgr.PoMaterialMgr.GetFileContentByFileId(fileInfos[selectedIndex].fileId);
          string fileName = str1 + fileInfos[selectedIndex].fileType;
          FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
          fs.Write(fileContent, 0, fileContent.Length);
          fs.Close();
          System.Diagnostics.Process.Start(fileName);
        }





    }

   public class IconInfo
{
       [DllImportAttribute("shell32.dll", EntryPoint = "ExtractIconExW", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
       public static extern uint ExtractIconExW([System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(UnmanagedType.LPWStr)] string lpszFile, int nIconIndex, ref IntPtr phiconLarge, ref IntPtr phiconSmall, uint nIcons);

    public static void GetExtsIconAndDescription(string ext, out Icon largeIcon, out Icon smallIcon, out string description)
    {

        GetDefaultIcon(out largeIcon, out smallIcon);   //得到缺省图标 
        description = "";                               //缺省类型描述  
        RegistryKey extsubkey = Registry.ClassesRoot.OpenSubKey(ext);   //从注册表中读取扩展名相应的子键  
        if (extsubkey == null) return;

        string extdefaultvalue = extsubkey.GetValue(null) as string;     //取出扩展名对应的文件类型名称  
        if (extdefaultvalue == null) return;

        if (extdefaultvalue.Equals("exefile", StringComparison.OrdinalIgnoreCase))  //扩展名类型是可执行文件  
        {
            RegistryKey exefilesubkey = Registry.ClassesRoot.OpenSubKey(extdefaultvalue);  //从注册表中读取文件类型名称的相应子键  
            if (exefilesubkey != null)
            {
                string exefiledescription = exefilesubkey.GetValue(null) as string;   //得到exefile描述字符串  
                if (exefiledescription != null) description = exefiledescription;
            }
            System.IntPtr exefilePhiconLarge = new IntPtr();
            System.IntPtr exefilePhiconSmall = new IntPtr();
            ExtractIconExW(Path.Combine(Environment.SystemDirectory, "shell32.dll"), 2, ref exefilePhiconLarge, ref exefilePhiconSmall, 1);
            if (exefilePhiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(exefilePhiconLarge);
            if (exefilePhiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(exefilePhiconSmall);
            return;
        }

        RegistryKey typesubkey = Registry.ClassesRoot.OpenSubKey(extdefaultvalue);  //从注册表中读取文件类型名称的相应子键  
        if (typesubkey == null) return;

        string typedescription = typesubkey.GetValue(null) as string;   //得到类型描述字符串  
        if (typedescription != null) description = typedescription;

        RegistryKey defaulticonsubkey = typesubkey.OpenSubKey("DefaultIcon");  //取默认图标子键  
        if (defaulticonsubkey == null) return;

        //得到图标来源字符串  
        string defaulticon = defaulticonsubkey.GetValue(null) as string; //取出默认图标来源字符串  
        if (defaulticon == null) return;
        string[] iconstringArray = defaulticon.Split(',');
        int nIconIndex = 0; //声明并初始化图标索引  
        if (iconstringArray.Length > 1)
            if (!int.TryParse(iconstringArray[1], out nIconIndex))
                nIconIndex = 0;     //int.TryParse转换失败，返回0  

        //得到图标  
        System.IntPtr phiconLarge = new IntPtr();
        System.IntPtr phiconSmall = new IntPtr();
        ExtractIconExW(iconstringArray[0].Trim('"'), nIconIndex, ref phiconLarge, ref phiconSmall, 1);
        if (phiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(phiconLarge);
        if (phiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(phiconSmall);
    }

    /// <summary>  
    /// 获取缺省图标  
    /// </summary>  
    /// <param name="largeIcon"></param>  
    /// <param name="smallIcon"></param>  
    private static void GetDefaultIcon(out Icon largeIcon, out Icon smallIcon)
    {
        largeIcon = smallIcon = null;
        System.IntPtr phiconLarge = new IntPtr();
        System.IntPtr phiconSmall = new IntPtr();
        ExtractIconExW(Path.Combine(Environment.SystemDirectory, "shell32.dll"), 0, ref phiconLarge, ref phiconSmall, 1);
        if (phiconLarge.ToInt32() > 0) largeIcon = Icon.FromHandle(phiconLarge);
        if (phiconSmall.ToInt32() > 0) smallIcon = Icon.FromHandle(phiconSmall);
    }  

}
}
