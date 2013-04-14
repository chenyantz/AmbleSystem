using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Security.Cryptography;


namespace PasswordServer
{
   public class DatabaseInfo:MarshalByRefObject
    {
       private string desKey = "AmbleSYS";
       private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

  

       public string GetDbUser()
       {
           string userId=OperatorFile.GetIniFileString("DataBase", "UserID", "", Environment.CurrentDirectory + "\\Amble.ini");

           return EncryptDES(userId, desKey);
       }

       public string GetDbPassword()
       {
           string password = OperatorFile.GetIniFileString("DataBase", "Pwd", "", Environment.CurrentDirectory + "\\Amble.ini");

           return EncryptDES(password, desKey);
       }

       public static string EncryptDES(string encryptString, string encryptKey)
       {
           try
           {
               byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
               byte[] rgbIV = Keys;
               byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
               DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
               MemoryStream mStream = new MemoryStream();//实例化内存流
               //将数据流链接到加密转换的流
               CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
               cStream.Write(inputByteArray, 0, inputByteArray.Length);
               cStream.FlushFinalBlock();
               return Convert.ToBase64String(mStream.ToArray());
           }
           catch
           {
               return encryptString;
           }
       }





    }
}
