using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace AmbleClient.Help
{
    public class ReleaseList : XmlDocument
    {
        private readonly string fileName;
        private string applicationStart;

        private string appName;
        private IList<ReleaseFile> files;
        private string minVersion;
        private string releaseDate;
        private string releaseUrl;
        private string releaseVersion;
        private string shortcutIcon;
        private string updateDes;

        public ReleaseList()
        {
            LoadXml(
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<AutoUpdater>
  <AppName></AppName>
  <ReleaseURL></ReleaseURL>
  <ReleaseDate></ReleaseDate>
  <ReleaseVersion></ReleaseVersion>
  <MinVersion></MinVersion>
  <UpdateDes></UpdateDes>
  <ApplicationStart></ApplicationStart>
  <ShortcutIcon></ShortcutIcon>
  <Releases>
  </Releases>
</AutoUpdater>
			");
        }

        public ReleaseList(string filePath)
        {
            fileName = filePath;
            Load(filePath);
            appName = GetNodeValue("/AutoUpdater/AppName");
            releaseDate = GetNodeValue("/AutoUpdater/ReleaseDate");
            releaseUrl = GetNodeValue("/AutoUpdater/ReleaseURL");
            releaseVersion = GetNodeValue("/AutoUpdater/ReleaseVersion");
            minVersion = GetNodeValue("/AutoUpdater/MinVersion");
            updateDes = GetNodeValue("/AutoUpdater/UpdateDes");
            applicationStart = GetNodeValue("/AutoUpdater/ApplicationStart");
            shortcutIcon = GetNodeValue("/AutoUpdater/ShortcutIcon");
            XmlNodeList fileNodes = GetNodeList("/AutoUpdater/Releases");
            files = new List<ReleaseFile>();
            foreach (XmlNode node in fileNodes)
            {
                files.Add(new ReleaseFile(node.Attributes[0].Value, node.Attributes[1].Value,
                                          Convert.ToInt32(node.Attributes[2].Value)));
            }
        }

        /// <summary>
        /// 应用程序名
        /// </summary>
        public string AppName
        {
            set
            {
                appName = value;
                SetNodeValue("AutoUpdater/AppName", value);
            }
            get { return appName; }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// 发布url
        /// </summary>
        public string ReleaseUrl
        {
            get { return releaseUrl; }
            set
            {
                releaseUrl = value;
                SetNodeValue("AutoUpdater/ReleaseURL", value);
            }
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public string ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                releaseDate = value;
                SetNodeValue("AutoUpdater/ReleaseDate", value);
            }
        }

        //版本号
        public string ReleaseVersion
        {
            get { return releaseVersion; }
            set
            {
                releaseVersion = value;
                SetNodeValue("AutoUpdater/ReleaseVersion", value);
            }
        }

        //最小版本号
        public string MinVersion
        {
            get { return minVersion; }
            set
            {
                minVersion = value;
                SetNodeValue("AutoUpdater/MinVersion", value);
            }
        }

        //升级内容
        public string UpdateDescription
        {
            get { return updateDes; }
            set
            {
                updateDes = value;
                SetNodeValue("AutoUpdater/UpdateDes", value);
            }
        }

        //应用程序图标
        public string ShortcutIcon
        {
            get { return shortcutIcon; }
            set
            {
                shortcutIcon = value;
                SetNodeValue("AutoUpdater/ShortcutIcon", value);
            }
        }

        //启动程序
        public string ApplicationStart
        {
            get { return applicationStart; }
            set
            {
                applicationStart = value;
                SetNodeValue("AutoUpdater/ApplicationStart", value);
            }
        }

        //升级文件集合
        public IList<ReleaseFile> Files
        {
            get { return files; }
            set
            {
                files = value;
                RefreshFileNodes();
            }
        }

        //版本号比较
        public int Compare(string version)
        {
            string[] myVersion = releaseVersion.Split('.');
            string[] otherVersion = version.Split('.');
            int i = 0;
            foreach (string v in myVersion)
            {
                int myNumber = int.Parse(v);
                int otherNumber = int.Parse(otherVersion[i]);
                if (myNumber != otherNumber)
                    return myNumber - otherNumber;
                i++;
            }
            return 0;
        }

        //版本号北京
        public int Compare(ReleaseList otherList)
        {
            if (otherList == null)
                throw new ArgumentNullException("otherList");
            int diff = Compare(otherList.ReleaseVersion);
            if (diff != 0)
                return diff;
            return (releaseDate == otherList.ReleaseDate)
                    ? 0
                    : (DateTime.Parse(releaseDate) > DateTime.Parse(otherList.ReleaseDate) ? 1 : -1);
        }

        /// <summary>
        /// 版本号比较，并输出总文件大小
        /// </summary>
        /// <param name="otherList"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public ReleaseFile[] GetDifferences(ReleaseList otherList, out int fileSize)
        {
            fileSize = 0;
            if (otherList == null || Compare(otherList) == 0)
                return null;
            var ht = new Hashtable();
            foreach (ReleaseFile file in files)
            {
                ht.Add(file.FileName, file.ReleaseDate);
            }
            var diffrences = new List<ReleaseFile>();
            foreach (ReleaseFile file in otherList.files)
            {
                //如果本地的XML文件中不包括服务器上要升级的文件或者服务器的文件的发布日期大于本地XML文件的发布日期，开始升级
                if (!ht.ContainsKey(file.FileName) ||
                    DateTime.Parse(file.ReleaseDate) > DateTime.Parse(ht[file.FileName].ToString()))
                {
                    diffrences.Add(file);
                    fileSize += file.FileSize;
                }
            }
            return diffrences.ToArray();
        }

        /// <summary>
        /// 给定一个节点的xPath表达式并返回一个节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XmlNode FindNode(string xPath)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            return xmlNode;
        }

        /// <summary>
        /// 给定一个节点的xPath表达式返回其值
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string GetNodeValue(string xPath)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            return xmlNode.InnerText;
        }

        public void SetNodeValue(string xPath, string value)
        {
            XmlNode xmlNode = SelectSingleNode(xPath);
            xmlNode.InnerXml = value;
        }

        /// <summary>
        /// 给定一个节点的表达式返回此节点下的孩子节点列表
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string xPath)
        {
            XmlNodeList nodeList = SelectSingleNode(xPath).ChildNodes;
            return nodeList;
        }

        public void RefreshFileNodes()
        {
            if (files == null) return;
            XmlNode node = SelectSingleNode("AutoUpdater/Releases");
            node.RemoveAll();
            foreach (ReleaseFile file in files)
            {
                XmlElement el = CreateElement("File");
                XmlAttribute attrName = CreateAttribute("name");
                attrName.Value = file.FileName;
                XmlAttribute attrDate = CreateAttribute("date");
                attrDate.Value = file.ReleaseDate;
                XmlAttribute attrSize = CreateAttribute("size");
                attrSize.Value = file.FileSize.ToString();
                el.Attributes.Append(attrName);
                el.Attributes.Append(attrDate);
                el.Attributes.Append(attrSize);
                node.AppendChild(el);
            }
        }
    }

    /// <summary>
    /// 发布的文件信息
    /// </summary>
    public class ReleaseFile
    {
        public ReleaseFile()
        {
        }

        /// <summary>
        /// 文
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="releaseDate">发布日期</param>
        /// <param name="fileSize">大小</param>
        public ReleaseFile(string fileName, string releaseDate, int fileSize)
        {
            this.FileName = fileName;
            this.ReleaseDate = releaseDate;
            this.FileSize = fileSize;
        }

        public string FileName { get; set; }

        public string ReleaseDate { get; set; }

        public int FileSize { get; set; }
    }

}
