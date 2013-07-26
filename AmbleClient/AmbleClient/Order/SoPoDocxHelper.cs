using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Novacode;
using AmbleClient.Order.SoMgr;
using AmbleClient.Order;
using AmbleClient.Order.PoMgr;
using AmbleClient.SO;
using AmbleClient.Order.PoView;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.Order
{
   public class SoPoDocxHelper
    {


       static DocX document;


       public static void SaveSoDocx(So so, List<SoItemsContentAndState> soitemsList)
       {
           document = DocX.Load(@"SoDocx.dll");
           Formatting format = new Formatting();
           format.FontFamily = new System.Drawing.FontFamily("Arial");
           format.Size = 9;           
           Table soTable = document.Tables[0];

           soTable.Rows[0].Cells[1].Paragraphs[0].InsertText(so.customerName, false, format);
           soTable.Rows[0].Cells[3].Paragraphs[0].InsertText(Tool.Get6DigitalNumberAccordingToId(so.soId), false, format);
           soTable.Rows[1].Cells[3].Paragraphs[0].InsertText((so.orderDate.Year + "-" + so.orderDate.Month + "-" + so.orderDate.Day).ToString(), false, format);
           soTable.Rows[2].Cells[3].Paragraphs[0].InsertText(so.paymentTerm, false, format);
           soTable.Rows[3].Cells[1].Paragraphs[0].InsertText(so.contact, false, format);
           soTable.Rows[3].Cells[3].Paragraphs[0].InsertText(so.freightTerm, false, format);
           soTable.Rows[4].Cells[1].Paragraphs[0].InsertText(AllAccountInfo.GetNameAccordingToId(UserInfo.UserId), false, format);

           custvendorinfo custven=CustVenInfoManager.GetUniqueCustVenInfo(0, so.customerName, UserInfo.UserId);

           if (custven != null)
           {
               soTable.Rows[1].Cells[1].Paragraphs[0].InsertText(custven.phone1, false, format);
               soTable.Rows[2].Cells[1].Paragraphs[0].InsertText(custven.fax, false, format);
              
           }

           Table itemTable = document.Tables[1];
           if(soitemsList.Count > 2)
           {
               Row sampleRow = itemTable.Rows[2];
               for (int i = 0; i < soitemsList.Count - 2; i++)
               {
                   itemTable.InsertRow(sampleRow,2);
               }
           }
           float totalAmount=0;
           for (int i = 0; i < soitemsList.Count;i++ )
           {
               itemTable.Rows[i + 2].Cells[0].Paragraphs[0].InsertText((i + 1).ToString(),false,format);
               itemTable.Rows[i + 2].Cells[1].Paragraphs[0].InsertText(soitemsList[i].soitem.partNo, false, format);
               itemTable.Rows[i + 2].Cells[2].Paragraphs[0].InsertText(soitemsList[i].soitem.mfg,false,format);
               itemTable.Rows[i + 2].Cells[3].Paragraphs[0].InsertText(soitemsList[i].soitem.qty.ToString(),false,format);
              // itemTable.Rows[i+2].Cells[4].InsertParagraph(soitemsList[i].soitem.
               if (soitemsList[i].soitem.currencyType == (int)Currency.USD)
               {
                   itemTable.Rows[i + 2].Cells[5].Paragraphs[0].InsertText(soitemsList[i].soitem.unitPrice.ToString(),false,format);
                   itemTable.Rows[i + 2].Cells[6].Paragraphs[0].InsertText((soitemsList[i].soitem.unitPrice * soitemsList[i].soitem.qty).ToString(),false,format);
                   
               }
               else
               {
                   itemTable.Rows[i + 2].Cells[5].Paragraphs[0].InsertText(soitemsList[i].soitem.unitPrice.ToString() + Enum.GetName(typeof(Currency), soitemsList[i].soitem.currencyType),false,format);
                   itemTable.Rows[i + 2].Cells[6].Paragraphs[0].InsertText((soitemsList[i].soitem.unitPrice * soitemsList[i].soitem.qty).ToString() + Enum.GetName(typeof(Currency), soitemsList[i].soitem.currencyType),false,format);
               }
               totalAmount += soitemsList[i].soitem.unitPrice * soitemsList[i].soitem.qty;
           }
           itemTable.Rows[itemTable.Rows.Count - 1].Cells[4].Paragraphs[0].InsertText(totalAmount.ToString(),false,format);
           WriteToFile();
       
       }

       public static void SavePoDocx(po po, List<PoItemContentAndState> poitemsList)
       {
           document = DocX.Load(@"PoDocx.dll");
           Formatting format = new Formatting();
           format.FontFamily = new System.Drawing.FontFamily("Arial");
           format.Size = 9;
           Table soTable = document.Tables[0];

           soTable.Rows[0].Cells[1].Paragraphs[0].InsertText(po.vendorName, false, format);
           soTable.Rows[0].Cells[3].Paragraphs[0].InsertText(Tool.Get6DigitalNumberAccordingToId(po.poId), false, format);
           soTable.Rows[1].Cells[3].Paragraphs[0].InsertText((po.poDate.Year + "-" + po.poDate.Month + "-" + po.poDate.Day).ToString(), false, format);
           soTable.Rows[2].Cells[3].Paragraphs[0].InsertText(po.paymentTerms, false, format);
           soTable.Rows[3].Cells[1].Paragraphs[0].InsertText(po.contact, false, format);
           //soTable.Rows[4].Cells[3].Paragraphs[0].InsertText(po.freight, false, format);
           soTable.Rows[4].Cells[1].Paragraphs[0].InsertText(AllAccountInfo.GetNameAccordingToId(po.pa), false, format);

           custvendorinfo custven = CustVenInfoManager.GetUniqueCustVenInfo(1,po.vendorName, UserInfo.UserId);

           if (custven != null)
           {
               soTable.Rows[1].Cells[1].Paragraphs[0].InsertText(custven.phone1, false, format);
               soTable.Rows[2].Cells[1].Paragraphs[0].InsertText(custven.fax, false, format);

           }

           Table itemTable = document.Tables[1];
           if (poitemsList.Count > 2)
           {
               Row sampleRow = itemTable.Rows[2];
             
               for (int i = 0; i < poitemsList.Count - 2; i++)
               {
                   itemTable.InsertRow(sampleRow,3);
               }
           }
           float totalAmount = 0;
           for (int i = 0; i < poitemsList.Count; i++)
           {
               itemTable.Rows[i + 2].Cells[0].Paragraphs[0].InsertText((i + 1).ToString(),false,format);
               itemTable.Rows[i + 2].Cells[1].Paragraphs[0].InsertText(poitemsList[i].poItem.partNo, false, format);
               itemTable.Rows[i + 2].Cells[2].Paragraphs[0].InsertText(poitemsList[i].poItem.mfg, false, format);
               itemTable.Rows[i + 2].Cells[3].Paragraphs[0].InsertText(poitemsList[i].poItem.qty.ToString(), false, format);
               //itemTable.Rows[i+2].Cells[4].Paragraphs[0].InsertText(poitemsList[i].poItem.cr
               if (poitemsList[i].poItem.currency == (int)Currency.USD)
               {
                   itemTable.Rows[i + 2].Cells[5].Paragraphs[0].InsertText(poitemsList[i].poItem.unitPrice.ToString(), false, format);
                   itemTable.Rows[i + 2].Cells[6].Paragraphs[0].InsertText((poitemsList[i].poItem.unitPrice * poitemsList[i].poItem.qty).ToString(), false, format);

               }
               else
               {
                   itemTable.Rows[i + 2].Cells[5].Paragraphs[0].InsertText(poitemsList[i].poItem.unitPrice.ToString() + Enum.GetName(typeof(Currency), poitemsList[i].poItem.currency), false, format);
                   itemTable.Rows[i + 2].Cells[6].Paragraphs[0].InsertText((poitemsList[i].poItem.unitPrice * poitemsList[i].poItem.qty).ToString() + Enum.GetName(typeof(Currency), poitemsList[i].poItem.currency), false, format);
               }
               if (poitemsList[i].poItem.unitPrice.HasValue)
               {
                   totalAmount += poitemsList[i].poItem.unitPrice.Value * poitemsList[i].poItem.qty;
               }
            }
           itemTable.Rows[itemTable.Rows.Count - 1].Cells[6].Paragraphs[0].InsertText(totalAmount.ToString(), false, format);
           WriteToFile();
       }



       static void WriteToFile()
       {
           SaveFileDialog sfd = new SaveFileDialog();
           sfd.Filter = "word97 文件(*.docx)|*.docx|所有文件(*.*)|*.*";
               if (DialogResult.OK == sfd.ShowDialog())
               {
                   FileStream file = new FileStream(sfd.FileName, FileMode.Create);
                   document.SaveAs(file);
                   file.Close();
               }
       }



    }
}
