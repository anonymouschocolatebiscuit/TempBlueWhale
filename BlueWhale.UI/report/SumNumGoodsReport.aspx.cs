using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.report
{
    public partial class SumNumGoodsReport : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.hfFieldA.Value = SysInfo.FieldA;
                this.hfFieldB.Value = SysInfo.FieldB;
                this.hfFieldC.Value = SysInfo.FieldC;
                this.hfFieldD.Value = SysInfo.FieldD;
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizEnd = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());
                string code = Request.Params["goodsId"].ToString();
                string ckName = Request.Params["typeId"].ToString();
                string down = Request.Params["down"].ToString();
                string path = Request.Params["path"].ToString();
                this.GetDataList(bizEnd, code, ckName, down, path);
                Response.End();
            }
        }

        void GetDataList(DateTime bizEnd, string code, string ckName, string down, string path)
        {
            DataTable dt = new DataTable();

            DataSet ds = dal.GetGoodsStoreNum(LoginUser.ShopId, bizEnd, code, ckName);

            dt = ds.Tables[0];

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    fieldA = ds.Tables[0].Rows[i]["fieldA"].ToString(),
                    fieldB = ds.Tables[0].Rows[i]["fieldB"].ToString(),
                    fieldC = ds.Tables[0].Rows[i]["fieldC"].ToString(),
                    fieldD = ds.Tables[0].Rows[i]["fieldD"].ToString(),
                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPriceStore = ds.Tables[0].Rows[i]["sumPriceStore"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);

            if (down == "1")
            {
                string fileName = "InventoryBalanceSheet_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                byte[] excelData = GenerateExcelData(dt, fileName);

                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                Response.AddHeader("Content-Length", excelData.Length.ToString());
                Response.BinaryWrite(excelData);
                Response.Flush();
                Response.End();
            }
        }

        public byte[] GenerateExcelData(DataTable dt, string filePath)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return new byte[0];
            }

            byte[] excelBytes;

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet(dt.TableName);
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("Product Code");
            row.CreateCell(1).SetCellValue("Product Name");
            row.CreateCell(2).SetCellValue("Specification");
            row.CreateCell(3).SetCellValue("Unit");

            row.CreateCell(4).SetCellValue(SysInfo.FieldA);
            row.CreateCell(5).SetCellValue(SysInfo.FieldB);
            row.CreateCell(6).SetCellValue(SysInfo.FieldC);
            row.CreateCell(7).SetCellValue(SysInfo.FieldD);

            row.CreateCell(8).SetCellValue("Storehouse");
            row.CreateCell(9).SetCellValue("Total quantity");
            row.CreateCell(10).SetCellValue("Stock price");
            row.CreateCell(11).SetCellValue("Total inventory cost");

            sheet.SetColumnWidth(0, 15 * 256); //30characters
            sheet.SetColumnWidth(1, 30 * 256); //30characters
            sheet.SetColumnWidth(2, 30 * 256); //30characters
            sheet.SetColumnWidth(3, 10 * 256); //30characters

            sheet.SetColumnWidth(4, 10 * 256); //30characters
            sheet.SetColumnWidth(5, 10 * 256); //30characters
            sheet.SetColumnWidth(6, 10 * 256); //30characters
            sheet.SetColumnWidth(7, 10 * 256); //30characters

            sheet.SetColumnWidth(8, 10 * 256); //30characters
            sheet.SetColumnWidth(9, 10 * 256); //30characters
            sheet.SetColumnWidth(10, 10 * 256); //30characters
            sheet.SetColumnWidth(11, 10 * 256); //30characters

            decimal sumNum = 0;
            decimal sumPrice = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sumNum += ConvertTo.ConvertDec(dt.Rows[i]["sumNum"].ToString());
                sumPrice += ConvertTo.ConvertDec(dt.Rows[i]["sumPriceStore"].ToString());

                NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);

                //goodsId, code, goodsName, spec, unitName, ckId, ckName, priceCost, fieldA, fieldB, fieldC, fieldD
                row2.CreateCell(0).SetCellValue(Convert.ToString(dt.Rows[i]["code"])); //Product Code
                row2.CreateCell(1).SetCellValue(Convert.ToString(dt.Rows[i]["goodsName"])); //Product Name
                row2.CreateCell(2).SetCellValue(Convert.ToString(dt.Rows[i]["spec"])); //Specification

                row2.CreateCell(3).SetCellValue(Convert.ToString(dt.Rows[i]["unitName"])); //Unit

                row2.CreateCell(4).SetCellValue(Convert.ToString(dt.Rows[i]["fieldA"])); //Unit
                row2.CreateCell(5).SetCellValue(Convert.ToString(dt.Rows[i]["fieldB"])); //Unit
                row2.CreateCell(6).SetCellValue(Convert.ToString(dt.Rows[i]["fieldC"])); //Unit
                row2.CreateCell(7).SetCellValue(Convert.ToString(dt.Rows[i]["fieldD"])); //Unit

                row2.CreateCell(8).SetCellValue(Convert.ToString(dt.Rows[i]["ckName"])); //Storehouse

                row2.CreateCell(9).SetCellValue(Convert.ToString(dt.Rows[i]["sumNum"])); //Quantity
                row2.CreateCell(10).SetCellValue(Convert.ToString(dt.Rows[i]["priceCost"])); //Cost price
                row2.CreateCell(11).SetCellValue(Convert.ToString(dt.Rows[i]["sumPriceStore"])); //Warehouse amount
            }

            NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(dt.Rows.Count + 1);

            row3.CreateCell(0).SetCellValue(""); //Product code
            row3.CreateCell(1).SetCellValue("Total:"); //Product name
            row3.CreateCell(2).SetCellValue(""); //Specifications
            row3.CreateCell(3).SetCellValue(""); //Unit
            row3.CreateCell(4).SetCellValue(""); //Warehouse
            row3.CreateCell(9).SetCellValue(sumNum.ToString("0.00")); //Quantity
            row3.CreateCell(11).SetCellValue(sumPrice.ToString("0.00")); //Quantity

                // Writing to the client side  
                using (MemoryStream ms = new MemoryStream())
                {
                    book.Write(ms);
                    excelBytes = ms.ToArray();
                }

            File.WriteAllBytes(filePath, excelBytes);

            #region download

            string path = filePath;

                try
                {
                    FileInfo file = new FileInfo(Server.MapPath(path));

                    Response.Clear();
                    Response.Charset = "GB2312";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    // Add header information to specify the default file name for the "File Download/Save As" dialog box
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                    // Add header information, specify the file size, and allow the browser to display the download progress
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    // Specifies that the returned stream cannot be read by the client and must be downloaded
                    Response.ContentType = "application/octet-stream";
                    // Send the file stream to the client
                    Response.WriteFile(file.FullName);
                    // Stop the execution of the page
                    Response.End();
                }
                catch (Exception) { }

            #endregion
            return excelBytes;
        }
    }
}
