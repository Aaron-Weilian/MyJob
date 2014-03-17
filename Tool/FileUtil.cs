using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NPOI.HSSF.UserModel;
using System.Drawing;

namespace Tool
{
    public class FileUtil
    {
        public static void exportExcel2007forRos(Tool.Message<ROSHeader> rosM, string newfilePath, string filePath, string FileName, string userType)
        {
           
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new ExcelPackage(file))
                    {
                        int i = 0;
                        int h = 0;

                         try
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["ROS"];

                       
                        foreach (ROSHeader ros in rosM.headers)
                        {
                            worksheet.Cells[1, 2 + i].Value = ros.item_no;
                            worksheet.Cells[2, 2 + i].Value = ros.category;
                            worksheet.Cells[3, 2 + i].Value = ros.buyer;
                            worksheet.Cells[4, 2 + i].Value = rosM.partner.vender_name;
                            worksheet.Cells[5, 2 + i].Value = ros.description;
                            worksheet.Cells[6, 2 + i].Value = ros.allocation_percent;
                            if (userType == "Supplier")
                            {
                                worksheet.Row(6).Hidden = true;
                            }

                            worksheet.Cells[7, 2 + i].Value = ros.lead_time;
                            
                            worksheet.Cells[8, 2 + i].Value = FormatUtil.String2Double(ros.safe_stock);
                            worksheet.Cells[9, 2 + i].Value = FormatUtil.String2Double(ros.open_po_qty);
                            worksheet.Cells[10, 2 + i].Value = FormatUtil.String2Double(ros.stock_qty);
                            worksheet.Cells[11, 2 + i].Value = FormatUtil.String2Double(ros.vmi_stock);
                        
                            worksheet.Cells[12, 2 + i].Value = ros.glod_plan_flag;
                            worksheet.Cells[12, 1].Style.Locked = true;

                            worksheet.Cells[13, 2 + i].Value = "";
                            worksheet.Cells[13, 1].Style.Locked = true;


                            worksheet.Cells[14, 2 + i].Value = "Demand";
                            worksheet.Cells[14, 2 + i].Style.Locked = true;

                            worksheet.Cells[14, 3 + i].Value = "ETA";
                            worksheet.Cells[14, 3 + i].Style.Locked = true;

                            worksheet.Cells[14, 4 + i].Value = "ETD";
                            worksheet.Cells[14, 4 + i].Style.Locked = true;

                            worksheet.Cells[14, 5 + i].Value = "Balance";
                            worksheet.Cells[14, 5 + i].Style.Locked = true;

                            worksheet.Cells[14, 6 + i].Value = "Cum ETA";
                            worksheet.Cells[14, 6 + i].Style.Locked = true;

                            worksheet.Cells[14, 7 + i].Value = "DIO";
                            worksheet.Cells[14, 7 + i].Style.Locked = true;

                            //int cop = 8;


                            worksheet.Cells[14, 8 + i].Value = "PO No";
                            worksheet.Cells[14, 8 + i].Style.Locked = true;


                            for (int m = 1; m < 14; m++)
                            {
                                worksheet.Cells[m, 2 + i, m, 8 + i].Merge = true;
                                if (m != 13)
                                {
                                    worksheet.Cells[m, 2 + i, m, 8 + i].Style.Locked = true;
                                }
                                else
                                {
                                    worksheet.Cells[m, 2 + i, m, 8 + i].Style.Locked = false;
                                }
                            }

                            int rowIndex = 15;
                            double value = 0;
                            double next = 0;
                            foreach (ROSLine line in ros.lines)
                            {

                                worksheet.Cells[rowIndex, 1].Value = line.demand_date;
                                worksheet.Cells[rowIndex, 1].Style.Locked = true;

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 1].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }

                                worksheet.Cells[rowIndex, 2 + i].Value = FormatUtil.String2Double(line.demand_quantity);
                                worksheet.Cells[rowIndex, 2 + i].Style.Locked = true;

                                worksheet.Cells[rowIndex, 3 + i].Value = FormatUtil.String2Double(line.eta_qty);
                                worksheet.Cells[rowIndex, 3 + i].Style.Locked = false;

                                worksheet.Cells[rowIndex, 4 + i].Value = FormatUtil.String2Double(line.etd_qty);
                                worksheet.Cells[rowIndex, 4 + i].Style.Locked = false;

                                //Balance
                                //if (ros.stock_qty == null || "".Equals(ros.stock_qty))
                                //{
                                //    worksheet.Cells[rowIndex, 5 + i].Formula = "=("+ line.demand_quantity +")";
                                //}
                                //else {


                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 2 + i, rowIndex, 4 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 2 + i, rowIndex, 4 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }


                                if (rowIndex == 15)
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Formula = "=(" + worksheet.Cells[10, 2 + i].Address +
                                        " - " + worksheet.Cells[8, 2 + i].Address + " - " + worksheet.Cells[rowIndex, 2 + i].Address + ")";


                                    value = FormatUtil.String2Double(ros.stock_qty) - FormatUtil.String2Double(ros.safe_stock) - FormatUtil.String2Double(line.demand_quantity);
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Formula = "=(" + worksheet.Cells[rowIndex - 1, 5 + i].Address +
                                        " + " + worksheet.Cells[rowIndex - 1, 3 + i].Address + " - " + worksheet.Cells[rowIndex, 2 + i].Address + ")";

                                    next = value - FormatUtil.String2Double(line.eta_qty) - FormatUtil.String2Double(line.demand_quantity);
                                    value = next;
                                }


                                //if (value < 0)
                                //{

                                //    //worksheet.Cells[rowIndex, 5 + i].Style.Font.Color.SetColor(Color.Red);

                                //}
                                //else {

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 5 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }
                                //}
                                //}

                                // worksheet.Cells[rowIndex, 5 + i].Value = line.shortage_qty;
                                worksheet.Cells[rowIndex, 5 + i].Style.Locked = true;


                                //if (userType == "Supplier")
                                //{

                                if (rowIndex > 15)
                                {
                                    worksheet.Cells[rowIndex, 6 + i].Formula = "=(" + worksheet.Cells[rowIndex - 1, 6 + i].Address + "+" +
                                        worksheet.Cells[rowIndex - 1, 3 + i].Address + ")";

                                }

                            //}
                                else
                                {
                                    worksheet.Cells[rowIndex, 6 + i].Value = FormatUtil.String2Double(line.cum_eta) ;
                                }
                                worksheet.Cells[rowIndex, 6 + i].Style.Locked = true;


                                worksheet.Cells[rowIndex, 7 + i].Value = line.dio;
                                worksheet.Cells[rowIndex, 7 + i].Style.Locked = true;

                                if (userType == "Supplier")
                                {
                                    worksheet.Column(7 + i).Hidden = true;
                                    //cop = 7;

                                }

                                worksheet.Cells[rowIndex, 8 + i].Value = line.po_no;
                                worksheet.Cells[rowIndex, 8 + i].Style.Locked = false;

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 6 + i, rowIndex, 8 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 6 + i, rowIndex, 8 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }

                                rowIndex = rowIndex + 1;
                                h = rowIndex;
                            }
                            i = i + 7;

                        }

                        int maxRowNum = worksheet.Dimension.End.Row;
                        int maxColNum = worksheet.Dimension.End.Column;

                        //for (int h = 12; h <= 39; h++)
                        //{
                        //    worksheet.Cells[h, 1, h, maxColNum ].Style.Fill.PatternType = ExcelFillStyle.Solid ;
                        //    worksheet.Cells[h, 1, h, maxColNum ].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

                        //}

                        //设置字体样式
                        worksheet.Cells.Style.Font.Name = "Tahoma";
                        //设置字体大小
                        worksheet.Cells.Style.Font.Size = 10;

                        //设置边框
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        worksheet.Protection.AllowSelectUnlockedCells = true;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        worksheet.Protection.IsProtected = true;

                        //package.Workbook.VbaProject

                        ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];

                        sheetSender.Cells[1, 2].Value = rosM.sender.key;
                        sheetSender.Cells[2, 2].Value = rosM.sender.referenceID;
                        sheetSender.Cells[3, 2].Value = rosM.sender.messageName;
                        sheetSender.Cells[4, 2].Value = rosM.sender.messageAlias;
                        sheetSender.Cells[5, 2].Value = rosM.sender.creationDateTime;
                        sheetSender.Cells[6, 2].Value = rosM.sender.edi_location_code;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        sheetSender.Protection.IsProtected = true;

                        ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];

                        sheetPartner.Cells[1, 2].Value = rosM.partner.vender_name;
                        sheetPartner.Cells[2, 2].Value = rosM.partner.vender_num;
                        sheetPartner.Cells[3, 2].Value = rosM.partner.vender_site;
                        sheetPartner.Cells[4, 2].Value = rosM.partner.vender_site_num;
                        sheetPartner.Cells[5, 2].Value = rosM.partner.duns_num;
                        sheetPartner.Cells[6, 2].Value = rosM.partner.contact_name;
                        sheetPartner.Cells[7, 2].Value = rosM.partner.email;
                        sheetPartner.Cells[8, 2].Value = rosM.partner.phone;
                        sheetPartner.Cells[9, 2].Value = rosM.partner.address;
                        sheetPartner.Cells[10, 2].Value = rosM.partner.street;
                        sheetPartner.Cells[11, 2].Value = rosM.partner.city;
                        sheetPartner.Cells[12, 2].Value = rosM.partner.postal_code;
                        sheetPartner.Cells[13, 2].Value = FileName;
                        worksheet.Protection.SetPassword("BmcBmc!");
                        sheetPartner.Protection.IsProtected = true;

                        //ExcelWorksheet INFO = package.Workbook.Worksheets["INFO"];

                        //INFO.Cells[1, 1].Value = FileName;

                        //INFO.Protection.SetPassword("BmcBmc!");
                        //INFO.Protection.IsProtected = true;

                        package.Workbook.Protection.LockStructure = true;

                        FileInfo newFile = new FileInfo(newfilePath);
                        package.SaveAs(newFile);
                        }

                         catch (Exception e)
                         {
                             Console.WriteLine(i);
                             Console.WriteLine(h);
                             throw (e);

                         }

                    }
                }
           
            //}
            ////finally {
            ////    //if (File.Exists(filePath))
            ////    //    File.Delete(filePath);
            ////}
        }

        public static Byte[] exportExcelforRos(Tool.Message<ROSHeader> rosM,  string filePath, string fileName, string userType)
        {
            try {
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new ExcelPackage(file))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["ROS"];

                        int i = 0;
                        foreach (ROSHeader ros in rosM.headers)
                        {
                            worksheet.Cells[1, 2 + i].Value = ros.item_no;
                            worksheet.Cells[2, 2 + i].Value = ros.category;
                            worksheet.Cells[3, 2 + i].Value = ros.buyer;
                            worksheet.Cells[4, 2 + i].Value = rosM.partner.vender_name;
                            worksheet.Cells[5, 2 + i].Value = ros.description;
                            worksheet.Cells[6, 2 + i].Value = ros.allocation_percent;

                            if (userType == "Supplier")
                            {
                                worksheet.Row(6).Hidden = true;
                                //worksheet.Cells[6, 2 + i].
                            }

                            worksheet.Cells[7, 2 + i].Value = ros.lead_time;
                            if (ros.safe_stock == null || "".Equals(ros.safe_stock))
                            {
                                ros.safe_stock = "0";
                                worksheet.Cells[8, 2 + i].Value = 0;
                            }
                            else
                            {
                                worksheet.Cells[8, 2 + i].Value = ros.safe_stock;
                            }

                            worksheet.Cells[9, 2 + i].Value = ros.open_po_qty;

                            if (ros.stock_qty == null || "".Equals(ros.stock_qty))
                            {
                                ros.stock_qty = "0";
                                worksheet.Cells[10, 2 + i].Value = 0;
                            }
                            else
                            {
                                worksheet.Cells[10, 2 + i].Value = ros.stock_qty;
                            }

                            if (ros.vmi_stock == null || "".Equals(ros.vmi_stock))
                            {
                                ros.vmi_stock = "0";
                                worksheet.Cells[11, 2 + i].Value = 0;
                            }
                            else
                            {
                                worksheet.Cells[11, 2 + i].Value = ros.vmi_stock;
                            }

                            worksheet.Cells[12, 2 + i].Value = ros.glod_plan_flag;
                            worksheet.Cells[12, 1].Style.Locked = true;

                            worksheet.Cells[13, 2 + i].Value = "";
                            worksheet.Cells[13, 1].Style.Locked = true;


                            worksheet.Cells[14, 2 + i].Value = "Demand";
                            worksheet.Cells[14, 2 + i].Style.Locked = true;

                            worksheet.Cells[14, 3 + i].Value = "ETA";
                            worksheet.Cells[14, 3 + i].Style.Locked = true;

                            worksheet.Cells[14, 4 + i].Value = "ETD";
                            worksheet.Cells[14, 4 + i].Style.Locked = true;

                            worksheet.Cells[14, 5 + i].Value = "Balance";
                            worksheet.Cells[14, 5 + i].Style.Locked = true;

                            worksheet.Cells[14, 6 + i].Value = "Cum ETA";
                            worksheet.Cells[14, 6 + i].Style.Locked = true;

                            worksheet.Cells[14, 7 + i].Value = "DIO";
                            worksheet.Cells[14, 7 + i].Style.Locked = true;

                            //int cop = 8;


                            worksheet.Cells[14, 8 + i].Value = "PO No";
                            worksheet.Cells[14, 8 + i].Style.Locked = true;


                            for (int m = 1; m < 14; m++)
                            {
                                worksheet.Cells[m, 2 + i, m, 8 + i].Merge = true;
                                if (m != 13)
                                {
                                    worksheet.Cells[m, 2 + i, m, 8 + i].Style.Locked = true;
                                }
                                else
                                {
                                    worksheet.Cells[m, 2 + i, m, 8 + i].Style.Locked = false;
                                }
                            }

                            int rowIndex = 15;
                            int value = 0;
                            int next = 0;
                            foreach (ROSLine line in ros.lines)
                            {

                                worksheet.Cells[rowIndex, 1].Value = line.demand_date;
                                worksheet.Cells[rowIndex, 1].Style.Locked = true;

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 1].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }

                                if (line.demand_quantity == null || "".Equals(line.demand_quantity))
                                {
                                    line.demand_quantity = "0";
                                    worksheet.Cells[rowIndex, 2 + i].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 2 + i].Value = line.demand_quantity;
                                }

                                worksheet.Cells[rowIndex, 2 + i].Style.Locked = true;

                                if (line.eta_qty == null || "".Equals(line.eta_qty))
                                {
                                    line.eta_qty = "0";
                                    worksheet.Cells[rowIndex, 3 + i].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 3 + i].Value = line.eta_qty;
                                }


                                worksheet.Cells[rowIndex, 3 + i].Style.Locked = false;

                                worksheet.Cells[rowIndex, 4 + i].Value = line.etd_qty;
                                worksheet.Cells[rowIndex, 4 + i].Style.Locked = false;

                                //Balance
                                //if (ros.stock_qty == null || "".Equals(ros.stock_qty))
                                //{
                                //    worksheet.Cells[rowIndex, 5 + i].Formula = "=("+ line.demand_quantity +")";
                                //}
                                //else {


                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 2 + i, rowIndex, 4 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 2 + i, rowIndex, 4 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }


                                if (rowIndex == 15)
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Formula = "=(" + worksheet.Cells[10, 2 + i].Address +
                                        " - " + worksheet.Cells[8, 2 + i].Address + " - " + worksheet.Cells[rowIndex, 2 + i].Address + ")";

                                    value = int.Parse(ros.stock_qty.ToString()) - int.Parse(ros.safe_stock.ToString()) - int.Parse(line.demand_quantity.ToString());
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Formula = "=(" + worksheet.Cells[rowIndex - 1, 5 + i].Address +
                                        " + " + worksheet.Cells[rowIndex - 1, 3 + i].Address + " - " + worksheet.Cells[rowIndex, 2 + i].Address + ")";

                                    next = value - int.Parse(line.eta_qty.ToString()) - int.Parse(line.demand_quantity.ToString());
                                    value = next;
                                }


                                //if (value < 0)
                                //{

                                //    //worksheet.Cells[rowIndex, 5 + i].Style.Font.Color.SetColor(Color.Red);

                                //}
                                //else {

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 5 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 5 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }
                                //}
                                //}

                                // worksheet.Cells[rowIndex, 5 + i].Value = line.shortage_qty;
                                worksheet.Cells[rowIndex, 5 + i].Style.Locked = true;


                                //if (userType == "Supplier")
                                //{

                                if (rowIndex > 15)
                                {
                                    worksheet.Cells[rowIndex, 6 + i].Formula = "=(" + worksheet.Cells[rowIndex - 1, 6 + i].Address + "+" +
                                        worksheet.Cells[rowIndex - 1, 3 + i].Address + ")";
                                }

                            //}
                                else
                                {
                                    worksheet.Cells[rowIndex, 6 + i].Value = line.cum_eta == "" || "".Equals(line.cum_eta) ? 0 : int.Parse(line.cum_eta);
                                }
                                worksheet.Cells[rowIndex, 6 + i].Style.Locked = true;


                                worksheet.Cells[rowIndex, 7 + i].Value = line.dio;
                                worksheet.Cells[rowIndex, 7 + i].Style.Locked = true;

                                if (userType == "Supplier")
                                {
                                    worksheet.Column(7 + i).Hidden = true;
                                    //cop = 7;

                                }

                                worksheet.Cells[rowIndex, 8 + i].Value = line.po_no;
                                worksheet.Cells[rowIndex, 8 + i].Style.Locked = false;

                                if (rowIndex <= 42)
                                {
                                    worksheet.Cells[rowIndex, 6 + i, rowIndex, 8 + i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[rowIndex, 6 + i, rowIndex, 8 + i].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                                }

                                rowIndex = rowIndex + 1;

                            }
                            i = i + 7;
                        }

                        int maxRowNum = worksheet.Dimension.End.Row;
                        int maxColNum = worksheet.Dimension.End.Column;

                        //for (int h = 12; h <= 39; h++)
                        //{
                        //    worksheet.Cells[h, 1, h, maxColNum ].Style.Fill.PatternType = ExcelFillStyle.Solid ;
                        //    worksheet.Cells[h, 1, h, maxColNum ].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);

                        //}

                        //设置字体样式
                        worksheet.Cells.Style.Font.Name = "Tahoma";
                        //设置字体大小
                        worksheet.Cells.Style.Font.Size = 10;

                        //设置边框
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        worksheet.Protection.AllowSelectUnlockedCells = true;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        worksheet.Protection.IsProtected = true;

                        //package.Workbook.VbaProject

                        ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];

                        sheetSender.Cells[1, 2].Value = rosM.sender.key;
                        sheetSender.Cells[2, 2].Value = rosM.sender.referenceID;
                        sheetSender.Cells[3, 2].Value = rosM.sender.messageName;
                        sheetSender.Cells[4, 2].Value = rosM.sender.messageAlias;
                        sheetSender.Cells[5, 2].Value = rosM.sender.creationDateTime;
                        sheetSender.Cells[6, 2].Value = rosM.sender.edi_location_code;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        sheetSender.Protection.IsProtected = true;

                        ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];

                        sheetPartner.Cells[1, 2].Value = rosM.partner.vender_name;
                        sheetPartner.Cells[2, 2].Value = rosM.partner.vender_num;
                        sheetPartner.Cells[3, 2].Value = rosM.partner.vender_site;
                        sheetPartner.Cells[4, 2].Value = rosM.partner.vender_site_num;
                        sheetPartner.Cells[5, 2].Value = rosM.partner.duns_num;
                        sheetPartner.Cells[6, 2].Value = rosM.partner.contact_name;
                        sheetPartner.Cells[7, 2].Value = rosM.partner.email;
                        sheetPartner.Cells[8, 2].Value = rosM.partner.phone;
                        sheetPartner.Cells[9, 2].Value = rosM.partner.address;
                        sheetPartner.Cells[10, 2].Value = rosM.partner.street;
                        sheetPartner.Cells[11, 2].Value = rosM.partner.city;
                        sheetPartner.Cells[12, 2].Value = rosM.partner.postal_code;
                        sheetPartner.Cells[13, 2].Value = fileName;
                        worksheet.Protection.SetPassword("BmcBmc!");
                        sheetPartner.Protection.IsProtected = true;

                        //ExcelWorksheet INFO = package.Workbook.Worksheets["info"];

                        //INFO.Cells[1, 1].Value = fileName;

                        //INFO.Protection.SetPassword("BmcBmc!");
                        //INFO.Protection.IsProtected = true;

                        package.Workbook.Protection.LockStructure = true;

                        //FileInfo newFile = new FileInfo(newfilePath);
                        //package.SaveAs(newFile);
                        //package.Dispose();
                        return package.GetAsByteArray();
                    }
                }
            }
            finally {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        public static void exportExcel2007forPO(Tool.Message<POHeader> pM, string newfilePath, string filePath, string fileName)
        {
            //读取模板
            //try {
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new ExcelPackage(file))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["PO"];

                        //worksheet.Protection.AllowSelectUnlockedCells = false;
                        //worksheet.Protection.AllowSelectLockedCells = false;
                        //worksheet.Protection.AllowEditObject = false;

                        //int i = 0;
                        foreach (Tool.POHeader ph in pM.headers)
                        {
                            worksheet.Cells[2, 3].Value = ph.buyer;
                            worksheet.Cells[3, 3].Value = ph.po_date;
                            worksheet.Cells[4, 3].Value = ph.po_number;
                            worksheet.Cells[5, 3].Value = ph.po_type;
                            worksheet.Cells[6, 3].Value = ph.your_reference;
                            worksheet.Cells[7, 3].Value = ph.desc;
                            worksheet.Cells[8, 3].Value = ph.supplier;
                            worksheet.Cells[9, 3].Value = ph.supplier_site;
                            worksheet.Cells[10, 3].Value = ph.supplier_address;
                            worksheet.Cells[11, 3].Value = ph.delivery_location;
                            worksheet.Cells[12, 3].Value = ph.delivery_address;
                            worksheet.Cells[13, 3].Value = ph.currency;
                            worksheet.Cells[14, 3].Value = ph.terms_of_delivery;
                            worksheet.Cells[15, 3].Value = ph.term_of_payment;

                            for (int m = 2; m < 16; m++)
                            {
                                worksheet.Cells[m, 3, m, 12].Merge = true;
                                worksheet.Cells[m, 3, m, 12].Style.Locked = true;
                                // worksheet.Cells[m, 2 + i, m, 8 + i].Style.Font.Color.SetColor(color.red);

                            }

                            int rowIndex = 18;

                            foreach (POLine line in ph.polines)
                            {
                                worksheet.Row(rowIndex).Height = 15;

                                worksheet.Cells[rowIndex, 1].Value = line.lineNo;
                                worksheet.Cells[rowIndex, 1].Style.Locked = true;

                                worksheet.Cells[rowIndex, 2].Value = line.item_no;
                                worksheet.Cells[rowIndex, 2].Style.Locked = true;

                                worksheet.Cells[rowIndex, 3].Value = line.desc;
                                worksheet.Cells[rowIndex, 3].Style.Locked = true;

                                worksheet.Cells[rowIndex, 4].Value = line.request_qty;
                                worksheet.Cells[rowIndex, 4].Style.Locked = true;

                                worksheet.Cells[rowIndex, 5].Value = line.comfirm_eta_date;
                                worksheet.Cells[rowIndex, 5].Style.Locked = false;

                                worksheet.Cells[rowIndex, 6].Value = line.comfirm_etd_date;
                                worksheet.Cells[rowIndex, 6].Style.Locked = false;

                                worksheet.Cells[rowIndex, 7].Value = line.comfirm_qty;
                                worksheet.Cells[rowIndex, 7].Style.Locked = false;

                                worksheet.Cells[rowIndex, 8].Value = line.request_delivery_date;
                                worksheet.Cells[rowIndex, 8].Style.Locked = true;

                                worksheet.Cells[rowIndex, 9].Value = ph.currency;
                                worksheet.Cells[rowIndex, 9].Style.Locked = true;

                                if (line.unit_price == null || "".Equals(line.unit_price))
                                {
                                    worksheet.Cells[rowIndex, 10].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 10].Value = Double.Parse(line.unit_price);
                                }
                                worksheet.Cells[rowIndex, 10].Style.Locked = true;


                                worksheet.Cells[rowIndex, 11].Value = line.price_unit;
                                worksheet.Cells[rowIndex, 11].Style.Locked = true;

                                if (line.line_item_tatoal_amount == null || "".Equals(line.line_item_tatoal_amount))
                                {
                                    worksheet.Cells[rowIndex, 12].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 12].Value = Double.Parse(line.line_item_tatoal_amount);
                                }
                                worksheet.Cells[rowIndex, 12].Style.Locked = true;

                                worksheet.Cells[rowIndex, 13].Value = "@";
                                worksheet.Cells[rowIndex, 13].Style.Locked = true;

                                //var shape = worksheet.Drawings.AddShape("Add Line" + rowIndex, eShapeStyle.Rect);
                                //shape.SetPosition(0 + rowIndex, 5 + rowIndex, 5 + rowIndex, 5 + rowIndex);
                                //shape.SetSize(400, 200);

                                rowIndex++;

                            }
                            worksheet.Cells[rowIndex, 1].Value = "TOTAL:";
                            worksheet.Cells[rowIndex, 1].Style.Locked = true;
                            worksheet.Cells[rowIndex, 2].Formula = "=SUM(L18:" + worksheet.Cells[rowIndex - 1, 12].Address + ")";
                            worksheet.Cells[rowIndex, 2].Style.Locked = true;

                            worksheet.Cells[rowIndex, 2, rowIndex, 12].Merge = true;
                            worksheet.Cells[rowIndex, 2, rowIndex, 12].Style.Locked = true;
                        }

                        int maxRowNum = worksheet.Dimension.End.Row;
                        int maxColNum = worksheet.Dimension.End.Column;

                        //设置字体样式
                        worksheet.Cells.Style.Font.Name = "Tahoma";
                        //设置字体大小
                        worksheet.Cells.Style.Font.Size = 10;

                        //设置边框
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        worksheet.Protection.AllowSelectUnlockedCells = true;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        worksheet.Protection.IsProtected = true;

                        ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];

                        sheetSender.Cells[1, 2].Value = pM.sender.key;
                        sheetSender.Cells[2, 2].Value = pM.sender.referenceID;
                        sheetSender.Cells[3, 2].Value = pM.sender.messageName;
                        sheetSender.Cells[4, 2].Value = pM.sender.messageAlias;
                        sheetSender.Cells[5, 2].Value = pM.sender.creationDateTime;
                        sheetSender.Cells[6, 2].Value = pM.sender.edi_location_code;

                        sheetSender.Protection.SetPassword("BmcBmc!");
                        sheetSender.Protection.IsProtected = true;

                        ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];

                        sheetPartner.Cells[1, 2].Value = pM.partner.vender_name;
                        sheetPartner.Cells[2, 2].Value = pM.partner.vender_num;
                        sheetPartner.Cells[3, 2].Value = pM.partner.vender_site;
                        sheetPartner.Cells[4, 2].Value = pM.partner.vender_site_num;
                        sheetPartner.Cells[5, 2].Value = pM.partner.duns_num;
                        sheetPartner.Cells[6, 2].Value = pM.partner.contact_name;
                        sheetPartner.Cells[7, 2].Value = pM.partner.email;
                        sheetPartner.Cells[8, 2].Value = pM.partner.phone;
                        sheetPartner.Cells[9, 2].Value = pM.partner.address;
                        sheetPartner.Cells[10, 2].Value = pM.partner.street;
                        sheetPartner.Cells[11, 2].Value = pM.partner.city;
                        sheetPartner.Cells[12, 2].Value = pM.partner.postal_code;
                        sheetPartner.Cells[13, 2].Value = fileName;
                        sheetPartner.Protection.SetPassword("BmcBmc!");
                        sheetPartner.Protection.IsProtected = true;

                        //ExcelWorksheet INFO = package.Workbook.Worksheets["INFO"];

                        //INFO.Cells[1, 1].Value = fileName;

                        //INFO.Protection.SetPassword("BmcBmc!");
                        //INFO.Protection.IsProtected = true;

                        //List<X509Certificate2> ret = new List<X509Certificate2>();
                        //X509Store store = new X509Store(StoreLocation.CurrentUser);
                        //store.Open(OpenFlags.ReadOnly);
                        //package.Workbook.VbaProject.Signature.Certificate = store.Certificates[2];

                        package.Workbook.Protection.LockStructure = true;

                        FileInfo newFile = new FileInfo(newfilePath);
                        package.SaveAs(newFile);
                        //return package.GetAsByteArray();
                    }
                }
            //}
            //finally {
            //    if (File.Exists(filePath))
            //        File.Delete(filePath);
            //}
        }

        public static Byte[] exportExcelforPO(Tool.Message<POHeader> pM,  string filePath, string fileName)
        {
            //读取模板
            try {
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new ExcelPackage(file))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["PO"];
                        //worksheet.Protection.AllowSelectUnlockedCells = false;
                        //worksheet.Protection.AllowSelectLockedCells = false;
                        //worksheet.Protection.AllowEditObject = false;

                        //int i = 0;
                        foreach (Tool.POHeader ph in pM.headers)
                        {
                            worksheet.Cells[2, 3].Value = ph.buyer;
                            worksheet.Cells[3, 3].Value = ph.po_date;
                            worksheet.Cells[4, 3].Value = ph.po_number;
                            worksheet.Cells[5, 3].Value = ph.po_type;
                            worksheet.Cells[6, 3].Value = ph.your_reference;
                            worksheet.Cells[7, 3].Value = ph.desc;
                            worksheet.Cells[8, 3].Value = ph.supplier;
                            worksheet.Cells[9, 3].Value = ph.supplier_site;
                            worksheet.Cells[10, 3].Value = ph.supplier_address;
                            worksheet.Cells[11, 3].Value = ph.delivery_location;
                            worksheet.Cells[12, 3].Value = ph.delivery_address;
                            worksheet.Cells[13, 3].Value = ph.currency;
                            worksheet.Cells[14, 3].Value = ph.terms_of_delivery;
                            worksheet.Cells[15, 3].Value = ph.term_of_payment;

                            for (int m = 2; m < 16; m++)
                            {
                                worksheet.Cells[m, 3, m, 12].Merge = true;
                                worksheet.Cells[m, 3, m, 12].Style.Locked = true;
                                // worksheet.Cells[m, 2 + i, m, 8 + i].Style.Font.Color.SetColor(color.red);
                            }

                            int rowIndex = 18;

                            foreach (POLine line in ph.polines)
                            {
                                worksheet.Row(rowIndex).Height = 15;

                                worksheet.Cells[rowIndex, 1].Value = line.lineNo;
                                worksheet.Cells[rowIndex, 1].Style.Locked = true;

                                worksheet.Cells[rowIndex, 2].Value = line.item_no;
                                worksheet.Cells[rowIndex, 2].Style.Locked = true;

                                worksheet.Cells[rowIndex, 3].Value = line.desc;
                                worksheet.Cells[rowIndex, 3].Style.Locked = true;

                                worksheet.Cells[rowIndex, 4].Value = line.request_qty;
                                worksheet.Cells[rowIndex, 4].Style.Locked = true;

                                worksheet.Cells[rowIndex, 5].Value = line.comfirm_eta_date;
                                worksheet.Cells[rowIndex, 5].Style.Locked = false;

                                worksheet.Cells[rowIndex, 6].Value = line.comfirm_etd_date;
                                worksheet.Cells[rowIndex, 6].Style.Locked = false;

                                worksheet.Cells[rowIndex, 7].Value = line.comfirm_qty;
                                worksheet.Cells[rowIndex, 7].Style.Locked = false;

                                worksheet.Cells[rowIndex, 8].Value = line.request_delivery_date;
                                worksheet.Cells[rowIndex, 8].Style.Locked = true;

                                worksheet.Cells[rowIndex, 9].Value = ph.currency;
                                worksheet.Cells[rowIndex, 9].Style.Locked = true;

                                if (line.unit_price == null || "".Equals(line.unit_price))
                                {
                                    worksheet.Cells[rowIndex, 10].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 10].Value = Double.Parse(line.unit_price);
                                }
                                worksheet.Cells[rowIndex, 10].Style.Locked = true;


                                worksheet.Cells[rowIndex, 11].Value = line.price_unit;
                                worksheet.Cells[rowIndex, 11].Style.Locked = true;

                                if (line.line_item_tatoal_amount == null || "".Equals(line.line_item_tatoal_amount))
                                {
                                    worksheet.Cells[rowIndex, 12].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[rowIndex, 12].Value = Double.Parse(line.line_item_tatoal_amount);
                                }
                                worksheet.Cells[rowIndex, 12].Style.Locked = true;

                                worksheet.Cells[rowIndex, 13].Value = "@";
                                worksheet.Cells[rowIndex, 13].Style.Locked = true;

                                //var shape = worksheet.Drawings.AddShape("Add Line" + rowIndex, eShapeStyle.Rect);
                                //shape.SetPosition(0 + rowIndex, 5 + rowIndex, 5 + rowIndex, 5 + rowIndex);
                                //shape.SetSize(400, 200);

                                rowIndex++;

                            }
                            worksheet.Cells[rowIndex, 1].Value = "TOTAL:";
                            worksheet.Cells[rowIndex, 1].Style.Locked = true;
                            worksheet.Cells[rowIndex, 2].Formula = "=SUM(L18:" + worksheet.Cells[rowIndex - 1, 12].Address + ")";
                            worksheet.Cells[rowIndex, 2].Style.Locked = true;

                            worksheet.Cells[rowIndex, 2, rowIndex, 12].Merge = true;
                            worksheet.Cells[rowIndex, 2, rowIndex, 12].Style.Locked = true;
                        }

                        int maxRowNum = worksheet.Dimension.End.Row;
                        int maxColNum = worksheet.Dimension.End.Column;

                        //设置字体样式
                        worksheet.Cells.Style.Font.Name = "Tahoma";
                        //设置字体大小
                        worksheet.Cells.Style.Font.Size = 10;

                        //设置边框
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[1, 1, maxRowNum, maxColNum - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        worksheet.Protection.AllowSelectUnlockedCells = true;

                        worksheet.Protection.SetPassword("BmcBmc!");
                        worksheet.Protection.IsProtected = true;

                        ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];

                        sheetSender.Cells[1, 2].Value = pM.sender.key;
                        sheetSender.Cells[2, 2].Value = pM.sender.referenceID;
                        sheetSender.Cells[3, 2].Value = pM.sender.messageName;
                        sheetSender.Cells[4, 2].Value = pM.sender.messageAlias;
                        sheetSender.Cells[5, 2].Value = pM.sender.creationDateTime;
                        sheetSender.Cells[6, 2].Value = pM.sender.edi_location_code;

                        sheetSender.Protection.SetPassword("BmcBmc!");
                        sheetSender.Protection.IsProtected = true;

                        ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];

                        sheetPartner.Cells[1, 2].Value = pM.partner.vender_name;
                        sheetPartner.Cells[2, 2].Value = pM.partner.vender_num;
                        sheetPartner.Cells[3, 2].Value = pM.partner.vender_site;
                        sheetPartner.Cells[4, 2].Value = pM.partner.vender_site_num;
                        sheetPartner.Cells[5, 2].Value = pM.partner.duns_num;
                        sheetPartner.Cells[6, 2].Value = pM.partner.contact_name;
                        sheetPartner.Cells[7, 2].Value = pM.partner.email;
                        sheetPartner.Cells[8, 2].Value = pM.partner.phone;
                        sheetPartner.Cells[9, 2].Value = pM.partner.address;
                        sheetPartner.Cells[10, 2].Value = pM.partner.street;
                        sheetPartner.Cells[11, 2].Value = pM.partner.city;
                        sheetPartner.Cells[12, 2].Value = pM.partner.postal_code;
                        sheetPartner.Cells[13, 2].Value = fileName;
                        sheetPartner.Protection.SetPassword("BmcBmc!");
                        sheetPartner.Protection.IsProtected = true;

                        //ExcelWorksheet INFO = package.Workbook.Worksheets["INFO"];

                        //INFO.Cells[1, 1].Value = fileName;

                        //INFO.Protection.SetPassword("BmcBmc!");
                        //INFO.Protection.IsProtected = true;

                        //List<X509Certificate2> ret = new List<X509Certificate2>();
                        //X509Store store = new X509Store(StoreLocation.CurrentUser);
                        //store.Open(OpenFlags.ReadOnly);
                        //package.Workbook.VbaProject.Signature.Certificate = store.Certificates[2];

                        package.Workbook.Protection.LockStructure = true;

                        //FileInfo newFile = new FileInfo(newfilePath);
                        //package.SaveAs(newFile);
                        //package.Dispose();
                        return package.GetAsByteArray();
                    }
                }
            }
            finally {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        public static Tool.Message<ROSHeader> readROSExcel2007(Stream ms,string sheetName)
        {
            //new ExcelPackage(new MemoryStream(bytes));
            //try {
               // using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                    using (var package = new ExcelPackage()) {
                        ms.Position = 0;
                        package.Load(ms);
                        Tool.Message<ROSHeader> rosM = new Tool.Message<ROSHeader>();

                        ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];
                        ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];
                        ExcelWorksheet info = package.Workbook.Worksheets["INFO"];

                        MessageSender sender = new MessageSender();
                        MessagePartner partner = new MessagePartner();
                        List<ROSHeader> list = new List<ROSHeader>();

                        sender.key                  = FormatUtil.StrNull(sheetSender.Cells[1, 2].GetValue<String>());
                        sender.referenceID          = FormatUtil.StrNull(sheetSender.Cells[2, 2].GetValue<String>());
                        sender.messageName          = FormatUtil.StrNull(sheetSender.Cells[3, 2].GetValue<String>());
                        sender.messageAlias         = FormatUtil.StrNull(sheetSender.Cells[4, 2].GetValue<String>());
                        sender.creationDateTime     = FormatUtil.StrNull(sheetSender.Cells[5, 2].GetValue<String>());
                        sender.edi_location_code    = FormatUtil.StrNull(sheetSender.Cells[6, 2].GetValue<String>());

                        partner.vender_name         = FormatUtil.StrNull(sheetPartner.Cells[1, 2].GetValue<String>());
                        partner.vender_num          = FormatUtil.StrNull(sheetPartner.Cells[2, 2].GetValue<String>());
                        partner.vender_site         = FormatUtil.StrNull(sheetPartner.Cells[3, 2].GetValue<String>());
                        partner.vender_site_num     = FormatUtil.StrNull(sheetPartner.Cells[4, 2].GetValue<String>());
                        partner.duns_num            = FormatUtil.StrNull(sheetPartner.Cells[5, 2].GetValue<String>());
                        partner.contact_name        = FormatUtil.StrNull(sheetPartner.Cells[6, 2].GetValue<String>());
                        partner.email               = FormatUtil.StrNull(sheetPartner.Cells[7, 2].GetValue<String>());
                        partner.phone               = FormatUtil.StrNull(sheetPartner.Cells[8, 2].GetValue<String>());
                        partner.address             = FormatUtil.StrNull(sheetPartner.Cells[9, 2].GetValue<String>());
                        partner.street              = FormatUtil.StrNull(sheetPartner.Cells[10, 2].GetValue<String>());
                        partner.city                = FormatUtil.StrNull(sheetPartner.Cells[11, 2].GetValue<String>());
                        partner.postal_code         = FormatUtil.StrNull(sheetPartner.Cells[12, 2].GetValue<String>());
                        rosM.filename               = FormatUtil.StrNull(sheetPartner.Cells[13, 2].GetValue<String>());
                       

                        rosM.sender = sender;
                        rosM.partner = partner;

                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                        int firstrownum = worksheet.Dimension.Start.Row;
                        int maxRowNum = worksheet.Dimension.End.Row;
                        int maxColNum = worksheet.Dimension.End.Column;

                        int col = 7;
                        int num = (maxColNum - 1)/col;

                        for (int i = 0; i < num; i++) {
                            ROSHeader ros = new ROSHeader();
                            ros.item_no              = FormatUtil.StrNull(worksheet.Cells[1, 2 + i*col].GetValue<String>());
                            ros.category             = FormatUtil.StrNull(worksheet.Cells[2, 2 + i*col].GetValue<String>());
                            ros.buyer                = FormatUtil.StrNull(worksheet.Cells[3, 2 + i*col].GetValue<String>());
                            ros.vendor_name          = FormatUtil.StrNull(worksheet.Cells[4, 2 + i*col].GetValue<String>());
                            ros.description          = FormatUtil.StrNull(worksheet.Cells[5, 2 + i*col].GetValue<String>());
                            ros.allocation_percent   = FormatUtil.StrNull(worksheet.Cells[6, 2 + i*col].GetValue<String>());
                            ros.lead_time            = FormatUtil.StrNull(worksheet.Cells[7, 2 + i*col].GetValue<String>());
                            ros.safe_stock           = FormatUtil.StrNull(worksheet.Cells[8, 2 + i*col].GetValue<String>());
                            ros.open_po_qty          = FormatUtil.StrNull(worksheet.Cells[9, 2 + i*col].GetValue<String>());
                            ros.stock_qty            = FormatUtil.StrNull(worksheet.Cells[10, 2 + i*col].GetValue<String>());
                            ros.vmi_stock            = FormatUtil.StrNull(worksheet.Cells[11, 2 + i*col].GetValue<String>());
                            ros.glod_plan_flag       = FormatUtil.StrNull(worksheet.Cells[12, 2 + i*col].GetValue<String>());
                            ros.updateflag           = FormatUtil.StrNull(worksheet.Cells[13, 2 + i*col].GetValue<String>());

                            for (int row = 15; row <= maxRowNum; row++) {
                                ROSLine line = new ROSLine();
                                line.demand_date            = FormatUtil.StrNull(worksheet.Cells[row, 1].GetValue<String>());
                                line.demand_quantity        = FormatUtil.StrNull(worksheet.Cells[row, 2 + i*col].GetValue<String>());
                                line.eta_qty                = FormatUtil.StrNull(worksheet.Cells[row, 3 + i*col].GetValue<String>());
                                line.etd_qty                = FormatUtil.StrNull(worksheet.Cells[row, 4 + i*col].GetValue<String>());
                                line.balance                = FormatUtil.StrNull(worksheet.Cells[row, 5 + i*col].GetValue<String>());
                                line.cum_eta                = FormatUtil.StrNull(worksheet.Cells[row, 6 + i*col].GetValue<String>());
                                line.dio                    = FormatUtil.StrNull(worksheet.Cells[row, 7 + i*col].GetValue<String>());
                                line.po_no                  = FormatUtil.StrNull(worksheet.Cells[row, 8 + i*col].GetValue<String>());

                                ros.lines.Add(line);
                            }
                            list.Add(ros);

                        }
                        rosM.headers = list;
                        //rosM.filename = worksheet.Cells[1,1].Value == null ? null : worksheet.Cells[1, 1].Value.ToString();
                        return rosM;
                    }
                
            //}
            //finally {
            //    //if (File.Exists(filePath))
            //    //    File.Delete(filePath);
            //}
        }

        public static Tool.Message<POHeader> readPOExcel2007(MemoryStream ms, string sheetName)
        {
            //try {
            //    using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
            using (var package = new ExcelPackage())
            {
                package.Load(ms);
                Tool.Message<POHeader> po = new Tool.Message<POHeader>();
                ExcelWorksheet sheetSender = package.Workbook.Worksheets["Sender"];
                ExcelWorksheet sheetPartner = package.Workbook.Worksheets["Partner"];
                ExcelWorksheet info = package.Workbook.Worksheets["INFO"];

                MessageSender sender = new MessageSender();
                MessagePartner partner = new MessagePartner();
                List<POHeader> list = new List<POHeader>();

                sender.key                  = FormatUtil.StrNull(sheetSender.Cells[1, 2].GetValue<String>());
                sender.referenceID          = FormatUtil.StrNull(sheetSender.Cells[2, 2].GetValue<String>());
                sender.messageName          = FormatUtil.StrNull(sheetSender.Cells[3, 2].GetValue<String>());
                sender.messageAlias         = FormatUtil.StrNull(sheetSender.Cells[4, 2].GetValue<String>());
                sender.creationDateTime     = FormatUtil.StrNull(sheetSender.Cells[5, 2].GetValue<String>());
                sender.edi_location_code    = FormatUtil.StrNull(sheetSender.Cells[6, 2].GetValue<String>());

                partner.vender_name         = FormatUtil.StrNull(sheetPartner.Cells[1, 2].GetValue<String>());
                partner.vender_num          = FormatUtil.StrNull(sheetPartner.Cells[2, 2].GetValue<String>());
                partner.vender_site         = FormatUtil.StrNull(sheetPartner.Cells[3, 2].GetValue<String>());
                partner.vender_site_num     = FormatUtil.StrNull(sheetPartner.Cells[4, 2].GetValue<String>());
                partner.duns_num            = FormatUtil.StrNull(sheetPartner.Cells[5, 2].GetValue<String>());
                partner.contact_name        = FormatUtil.StrNull(sheetPartner.Cells[6, 2].GetValue<String>());
                partner.email               = FormatUtil.StrNull(sheetPartner.Cells[7, 2].GetValue<String>());
                partner.phone               = FormatUtil.StrNull(sheetPartner.Cells[8, 2].GetValue<String>());
                partner.address             = FormatUtil.StrNull(sheetPartner.Cells[9, 2].GetValue<String>());
                partner.street              = FormatUtil.StrNull(sheetPartner.Cells[10, 2].GetValue<String>());
                partner.city                = FormatUtil.StrNull(sheetPartner.Cells[11, 2].GetValue<String>());
                partner.postal_code         = FormatUtil.StrNull(sheetPartner.Cells[12, 2].GetValue<String>());
                po.filename                 = FormatUtil.StrNull(sheetPartner.Cells[13, 2].GetValue<String>());

                po.sender = sender;
                po.partner = partner;

                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                int firstrownum = worksheet.Dimension.Start.Row;
                int maxRowNum = worksheet.Dimension.End.Row;

                Tool.POHeader ph = new POHeader();
                ph.buyer                = FormatUtil.StrNull(worksheet.Cells[2, 3].GetValue<String>());
                ph.po_date              = FormatUtil.StrNull(worksheet.Cells[3, 3].GetValue<String>());
                ph.po_number            = FormatUtil.StrNull(worksheet.Cells[4, 3].GetValue<String>());
                ph.order_class          = FormatUtil.StrNull(worksheet.Cells[5, 3].GetValue<String>());
                ph.your_reference       = FormatUtil.StrNull(worksheet.Cells[6, 3].GetValue<String>());
                ph.desc                 = FormatUtil.StrNull(worksheet.Cells[7, 3].GetValue<String>());
                ph.supplier             = FormatUtil.StrNull(worksheet.Cells[8, 3].GetValue<String>());
                ph.supplier_site        = FormatUtil.StrNull(worksheet.Cells[9, 3].GetValue<String>());
                ph.supplier_address     = FormatUtil.StrNull(worksheet.Cells[10, 3].GetValue<String>());
                ph.delivery_location    = FormatUtil.StrNull(worksheet.Cells[11, 3].GetValue<String>());
                ph.delivery_address     = FormatUtil.StrNull(worksheet.Cells[12, 3].GetValue<String>());
                ph.currency             = FormatUtil.StrNull(worksheet.Cells[13, 3].GetValue<String>());
                ph.terms_of_delivery    = FormatUtil.StrNull(worksheet.Cells[14, 3].GetValue<String>());
                ph.term_of_payment      = FormatUtil.StrNull(worksheet.Cells[15, 3].GetValue<String>());


                for (int row = 18; row <= maxRowNum - 1; row++)
                {
                    POLine line = new POLine();
                    line.lineNo                  = FormatUtil.StrNull(worksheet.Cells[row, 1].GetValue<String>());
                    line.item_no                 = FormatUtil.StrNull(worksheet.Cells[row, 2].GetValue<String>());
                    line.desc                    = FormatUtil.StrNull(worksheet.Cells[row, 3].GetValue<String>());
                    line.request_qty             = FormatUtil.StrNull(worksheet.Cells[row, 4].GetValue<String>());
                    line.schedule_delivery_date  = FormatUtil.Date2Str(worksheet.Cells[row, 5].GetValue<DateTime>());
                    line.Schedule_Arrive_Date    = FormatUtil.Date2Str(worksheet.Cells[row, 6].GetValue<DateTime>());
                    line.schedule_delivery_qty   = FormatUtil.StrNull(worksheet.Cells[row, 7].GetValue<String>());
                    line.request_delivery_date   = FormatUtil.StrNull(worksheet.Cells[row, 8].GetValue<String>());
                    line.curr                    = FormatUtil.StrNull(worksheet.Cells[row, 9].GetValue<String>());
                    line.unit_price              = FormatUtil.StrNull(worksheet.Cells[row, 10].GetValue<String>());
                    line.price_unit              = FormatUtil.StrNull(worksheet.Cells[row, 11].GetValue<String>());
                    line.line_item_tatoal_amount = FormatUtil.StrNull(worksheet.Cells[row, 12].GetValue<String>());

                    ph.polines.Add(line);
                }
                list.Add(ph);
                po.headers = list;

                return po;
            }


            //finally {
            //    if (File.Exists(filePath))
            //        File.Delete(filePath);
            //}
        }

        public static void readExcel2003(string path) { 
        
        
        }

        public static void exportExcel2003(List<ROSHeader> list, string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            HSSFSheet sheet1 = (NPOI.HSSF.UserModel.HSSFSheet)hssfworkbook.GetSheet("ROS");

            int i = 0;

            foreach (ROSHeader ros in list)
            {

                sheet1.GetRow(0).CreateCell(1 + i).SetCellValue(ros.item_no);
                sheet1.GetRow(1).CreateCell(1 + i).SetCellValue(ros.category);
                sheet1.GetRow(2).CreateCell(1 + i).SetCellValue(ros.buyer);
                //sheet1.GetRow(3).CreateCell(1 + i).SetCellValue(ros.vendor_name);
                sheet1.GetRow(4).CreateCell(1 + i).SetCellValue(ros.description);
                sheet1.GetRow(5).CreateCell(1 + i).SetCellValue(ros.allocation_percent);
                sheet1.GetRow(6).CreateCell(1 + i).SetCellValue(ros.lead_time);
                sheet1.GetRow(7).CreateCell(7 + i).SetCellValue(ros.safe_stock);
                sheet1.GetRow(8).CreateCell(7 + i).SetCellValue(ros.open_po_qty);
                sheet1.GetRow(9).CreateCell(7 + i).SetCellValue(ros.stock_qty);

                var l = sheet1.GetRow(10) ?? sheet1.CreateRow(10);
                sheet1.GetRow(10).CreateCell(1 + i).SetCellValue("Demand");
                sheet1.GetRow(10).CreateCell(2 + i).SetCellValue("ETA");
                sheet1.GetRow(10).CreateCell(3 + i).SetCellValue("ETD");
                sheet1.GetRow(10).CreateCell(4 + i).SetCellValue("Balance");
                sheet1.GetRow(10).CreateCell(5 + i).SetCellValue("Cum ETA");
                sheet1.GetRow(10).CreateCell(6 + i).SetCellValue("DIO");
                sheet1.GetRow(10).CreateCell(7 + i).SetCellValue("PO No");
                int rowIndex = 11;

                foreach (ROSLine line in ros.lines)
                {
                    var row = sheet1.GetRow(rowIndex) ?? sheet1.CreateRow(rowIndex);

                    row.CreateCell(1 + i).SetCellValue(line.demand_quantity);
                    row.CreateCell(2 + i).SetCellValue(line.eta_qty);
                    row.CreateCell(3 + i).SetCellValue(line.etd_qty);
                    row.CreateCell(4 + i).SetCellValue(line.balance);
                    row.CreateCell(5 + i).SetCellValue(line.cum_eta);
                    row.CreateCell(6 + i).SetCellValue(line.dio);
                    row.CreateCell(7 + i).SetCellValue(line.po_no);
                    rowIndex++;

                }
                i = i + 7;
            }
        }
    }
}
