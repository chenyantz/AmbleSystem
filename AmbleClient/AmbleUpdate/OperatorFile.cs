using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace AmbleUpdate
{
    public class OperatorFile
    {
        [DllImport("kernel32")] //引入“shell32.dll”API文件
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")] 
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public OperatorFile()
        {
  
        }

        /// <summary>
        /// 从INI文件中读取指定节点的内容
        /// </summary>
        /// <param name="section">INI节点</param>
        /// <param name="key">节点下的项</param>
        /// <param name="def">没有找到内容时返回的默认值</param>
        /// <param name="filePath">要读取的INI文件</param>
        /// <returns>读取的节点内容</returns>
        public static string GetIniFileString(string section,string key,string def,string filePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, temp, 1024, filePath);
            return temp.ToString();
        }

        public static void WriteIniFileString(string section, string key, string val, string filePath)
        { 
        WritePrivateProfileString( section,key, val,filePath);
        }


    }
}
