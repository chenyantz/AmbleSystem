using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI;
using AmbleClient.Order.SoMgr;
using AmbleClient.Order;
using AmbleClient.Order.PoMgr;
using AmbleClient.SO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.Windows.Forms;
using System.IO;
using AmbleClient.Order.PoView;

namespace AmbleClient.Order
{
   public class SoPoExcelHelper
    {

        static HSSFWorkbook hssfworkbook;






       public static void SaveSOExcel(List<So> soList, List<List<SoItemsContentAndState>> soitemsListList)
       {
           if (soList.Count() != soitemsListList.Count())
           {
               MessageBox.Show("Internal Error. Please send the log file to the Author");
               Logger.Error(soList.Count() + "," + soitemsListList.Count());
               return;
           }

           FileStream file;
           try
           {
               file = new FileStream(@"SoTemplate.dll", FileMode.Open, FileAccess.Read);
           }
           catch (Exception )
           {
               MessageBox.Show("Please check the SoTemplate.dll.");
               return;
           }
           hssfworkbook = new HSSFWorkbook(file);
           WriteDsiInfo();

         
           for (int i = 0; i < soList.Count(); i++)
           {
              ISheet sheet = hssfworkbook.CloneSheet(0);
              FillTheSoSheet(sheet,soList[i], soitemsListList[i]);
              hssfworkbook.SetSheetName(3 + i, "SO" + i.ToString());
           }
           hssfworkbook.RemoveSheetAt(0);
           hssfworkbook.RemoveSheetAt(0);
           hssfworkbook.RemoveSheetAt(0);
           WriteToFile();


           
       }


       static void FillTheSoSheet(ISheet sheet,So so, List<SoItemsContentAndState> soitemList)
       {
           AmbleClient.Admin.AccountMgr.AccountMgr accMgr=new Admin.AccountMgr.AccountMgr();
           sheet.GetRow(0).CreateCell(2).SetCellValue(so.customerName);
           sheet.GetRow(0).CreateCell(6).SetCellValue(so.contact);
           sheet.GetRow(0).CreateCell(9).SetCellValue(accMgr.GetNameById(so.salesId));
         
           if (so.approverId != null)
           {
               sheet.GetRow(0).CreateCell(14).SetCellValue(accMgr.GetNameById(so.approverId.Value) + "," + so.approveDate.Value.ToShortDateString());
           }
           sheet.GetRow(1).CreateCell(2).SetCellValue(so.salesOrderNo);
           sheet.GetRow(1).CreateCell(6).SetCellValue(so.orderDate.ToShortDateString());

           sheet.GetRow(1).CreateCell(9).SetCellValue(so.customerPo);
           sheet.GetRow(1).CreateCell(14).SetCellValue(so.paymentTerm);

           sheet.GetRow(2).CreateCell(2).SetCellValue(so.freightTerm);
           sheet.GetRow(2).CreateCell(6).SetCellValue(so.customerAccount);
           sheet.GetRow(2).CreateCell(9).SetCellValue(so.specialInstructions);

           IRow row = sheet.CreateRow(4);
           row.CreateCell(0).SetCellValue(so.billTo);
           row.CreateCell(8).SetCellValue(so.shipTo);
     
           int itemRowIndex = 9;
           foreach (SoItemsContentAndState scs in soitemList)
           {
               IRow itemRow = sheet.CreateRow(itemRowIndex);
               itemRow.CreateCell(0).SetCellValue(soitemList.IndexOf(scs)+1);
               string strSaleType;
               switch (scs.soitem.saleType)
               {

                   case 0:

                       strSaleType = "OEM EXCESS";
                       break;
                   case 1:
                       strSaleType = "OWN STOCK";
                       break;
                   case 2:
                       strSaleType = "OTHERS";
                       break;
                   default:
                       strSaleType = "ERROR";
                       break;

               }

               itemRow.CreateCell(1).SetCellValue(strSaleType);
               itemRow.CreateCell(2).SetCellValue(scs.soitem.partNo);
               itemRow.CreateCell(3).SetCellValue(scs.soitem.mfg);
               itemRow.CreateCell(4).SetCellValue(scs.soitem.rohs == 1 ? "Y" : "N");
               itemRow.CreateCell(5).SetCellValue(scs.soitem.dc);
               itemRow.CreateCell(6).SetCellValue(scs.soitem.intPartNo);
               itemRow.CreateCell(7).SetCellValue(scs.soitem.shipFrom);
               itemRow.CreateCell(8).SetCellValue(scs.soitem.shipMethod);
               itemRow.CreateCell(9).SetCellValue(scs.soitem.trackingNo);
               itemRow.CreateCell(10).SetCellValue(scs.soitem.qty);
               itemRow.CreateCell(11).SetCellValue(scs.soitem.qtyshipped);
               itemRow.CreateCell(12).SetCellValue(Enum.GetName(typeof(AmbleClient.Currency), scs.soitem.currencyType));
               itemRow.CreateCell(13).SetCellValue(scs.soitem.unitPrice);
               itemRow.CreateCell(14).SetCellValue(scs.soitem.unitPrice * scs.soitem.qty);
               itemRow.CreateCell(15).SetCellValue(scs.soitem.dockDate.ToShortDateString());

               if (scs.soitem.shippedDate != null)
               {
                   itemRow.CreateCell(16).SetCellValue(scs.soitem.shippedDate.Value.ToShortDateString());
               }
               itemRowIndex++;
               
               IRow infoRow=sheet.CreateRow(itemRowIndex);
               infoRow.CreateCell(1).SetCellValue("Shipping Instructions >>" + scs.soitem.shippingInstruction);
               sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(itemRowIndex,itemRowIndex,1,16));
              
              
               itemRowIndex++;
               infoRow=sheet.CreateRow(itemRowIndex);
               infoRow.CreateCell(1).SetCellValue("Packing Instructions >>" + scs.soitem.packingInstruction);
               sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(itemRowIndex,itemRowIndex,1,16));
               itemRowIndex++;
           }

       }



       public static void SavePoExcel(List<po> poList, List<List<PoItemContentAndState>> poitemsListList)
       {
           if (poList.Count() != poitemsListList.Count())
           {
               MessageBox.Show("Internal Error. Please send the log file to the Author");
               Logger.Error(poList.Count() + "," + poitemsListList.Count());
               return;
           }

           FileStream file;
           try
           {
               file = new FileStream(@"PoTemplate.dll", FileMode.Open, FileAccess.Read);
           }
           catch (Exception)
           {
               MessageBox.Show("Please check the PoTemplate.dll.");
               return;
           }
           hssfworkbook = new HSSFWorkbook(file);
           WriteDsiInfo();


           for (int i = 0; i < poList.Count(); i++)
           {
               ISheet sheet = hssfworkbook.CloneSheet(0);
               FillThePoSheet(sheet, poList[i], poitemsListList[i]);
               hssfworkbook.SetSheetName(3 + i, "PO" + i.ToString());
           }
           hssfworkbook.RemoveSheetAt(0);
           hssfworkbook.RemoveSheetAt(0);
           hssfworkbook.RemoveSheetAt(0);
           WriteToFile();

      
       }

       static void FillThePoSheet(ISheet sheet,po po, List<PoItemContentAndState> poitemList)
       {
           AmbleClient.Admin.AccountMgr.AccountMgr accMgr = new Admin.AccountMgr.AccountMgr();

           sheet.GetRow(0).CreateCell(2).SetCellValue(po.vendorName);
           sheet.GetRow(0).CreateCell(9).SetCellValue(po.contact);
           if (po.pa != null)
           {
               sheet.GetRow(0).CreateCell(14).SetCellValue(accMgr.GetNameById((int)po.pa.Value));
           }
           sheet.GetRow(1).CreateCell(2).SetCellValue(po.vendorNumber);

           if (po.poDate != null)
           {
               sheet.GetRow(1).CreateCell(5).SetCellValue(po.poDate.Value.ToShortDateString());
           }
           sheet.GetRow(1).CreateCell(9).SetCellValue(po.poNo);
           sheet.GetRow(1).CreateCell(14).SetCellValue(po.paymentTerms);

           sheet.GetRow(2).CreateCell(2).SetCellValue(po.shipMethod);
           sheet.GetRow(2).CreateCell(9).SetCellValue(po.freight);
           sheet.GetRow(2).CreateCell(14).SetCellValue(po.shipToLocation);

           IRow row = sheet.CreateRow(4);
           row.CreateCell(0).SetCellValue(po.billTo);
           row.CreateCell(10).SetCellValue(po.shipTo);

           int itemRowIndex = 9; int totalQty = 0; float totalTotal = 0;int totalQtyRecd = 0;
           foreach (PoItemContentAndState pcs in poitemList)
           {
               IRow itemRow = sheet.CreateRow(itemRowIndex);
               itemRow.CreateCell(0).SetCellValue(poitemList.IndexOf(pcs)+1);
               itemRow.CreateCell(1).SetCellValue(pcs.poItem.partNo);
               itemRow.CreateCell(2).SetCellValue(pcs.poItem.mfg);
               itemRow.CreateCell(3).SetCellValue(pcs.poItem.dc);
               itemRow.CreateCell(4).SetCellValue(pcs.poItem.vendorIntPartNo);
               itemRow.CreateCell(5).SetCellValue(pcs.poItem.org);
               itemRow.CreateCell(6).SetCellValue(pcs.poItem.qty.Value);
               totalQty += pcs.poItem.qty.Value;

               if (pcs.poItem.qtyRecd != null)
               {
                   itemRow.CreateCell(7).SetCellValue(pcs.poItem.qtyRecd.Value);
                   totalQtyRecd += pcs.poItem.qtyRecd.Value;
               }
               if(pcs.poItem.qtyCorrected!=null)
               itemRow.CreateCell(8).SetCellValue(pcs.poItem.qtyCorrected.Value);

               if(pcs.poItem.qtyAccept!=null)
               itemRow.CreateCell(9).SetCellValue(pcs.poItem.qtyAccept.Value);

               if(pcs.poItem.qtyRejected!=null)
               itemRow.CreateCell(10).SetCellValue(pcs.poItem.qtyRejected.Value);

               if(pcs.poItem.qtyRTV!=null)
                itemRow.CreateCell(11).SetCellValue(pcs.poItem.qtyRTV.Value);

               if(pcs.poItem.qcPending!=null)
               itemRow.CreateCell(12).SetCellValue(pcs.poItem.qcPending.Value);

               itemRow.CreateCell(13).SetCellValue(Enum.GetName(typeof(AmbleClient.Currency), pcs.poItem.currency));
               itemRow.CreateCell(14).SetCellValue(pcs.poItem.unitPrice.Value);
               itemRow.CreateCell(15).SetCellValue(pcs.poItem.qty.Value*pcs.poItem.unitPrice.Value);
               totalTotal += pcs.poItem.qty.Value * pcs.poItem.unitPrice.Value;



               if(pcs.poItem.dueDate!=null)
               itemRow.CreateCell(16).SetCellValue(pcs.poItem.dueDate.Value.ToShortDateString());

               if(pcs.poItem.receiveDate!=null)
               itemRow.CreateCell(17).SetCellValue(pcs.poItem.receiveDate.Value.ToShortDateString());

               itemRow.CreateCell(18).SetCellValue(pcs.poItem.stepCode);
               itemRow.CreateCell(19).SetCellValue("Unknown");

               itemRowIndex++;

               IRow infoRow = sheet.CreateRow(itemRowIndex);
               infoRow.CreateCell(1).SetCellValue("Note To Vendor >>" + pcs.poItem.noteToVendor);
               sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(itemRowIndex, itemRowIndex, 1, 16));

               itemRowIndex++;

           }
           //add total bar
            IRow totalRow=sheet.CreateRow(itemRowIndex);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(itemRowIndex,itemRowIndex,0,4));
                totalRow.CreateCell(5).SetCellValue("Total");
                totalRow.CreateCell(6).SetCellValue(totalQty);
                totalRow.CreateCell(7).SetCellValue(totalQtyRecd);
                totalRow.CreateCell(15).SetCellValue(totalTotal);

       }


       static void WriteDsiInfo()
       {
           DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
           dsi.Company = "Amble";
           hssfworkbook.DocumentSummaryInformation = dsi;

           //create a entry of SummaryInformation
           SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
           si.Subject = "Amble Order Info";
           hssfworkbook.SummaryInformation = si;
       }





       static void WriteToFile()
       {
           SaveFileDialog sfd = new SaveFileDialog();
           sfd.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
           if (DialogResult.OK == sfd.ShowDialog())
           {
               FileStream file = new FileStream(sfd.FileName, FileMode.Create);
               hssfworkbook.Write(file);
               file.Close();
           }
 

       
       }






    }
}
