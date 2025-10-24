using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class PurOrderListDetailReport : BasePage
    {
        public PurReceiptDAL dal = new PurReceiptDAL();
        public VenderDAL venderDAL = new VenderDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());
                DateTime bizEnd = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());
                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();
                string typeId = Request.Params["typeId"].ToString();
                string down = Request.Params["down"].ToString();
                string path = Request.Params["path"].ToString();
                this.GetDataList(bizStart, bizEnd, wlId, goodsId, typeId, down, path);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string wlId, string goodsId, string typeId, string down, string path)
        {
            DataSet ds = dal.GetPurReceiptItemDetail(LoginUser.ShopId, bizStart, bizEnd, typeId, wlId, goodsId);

            #region 创建临时表
            DataTable dt = new DataTable("ProductPurchaseOrderDetails");

            dt.Columns.Add("Purchase Date", Type.GetType("System.String"));
            dt.Columns.Add("Receipt Number", Type.GetType("System.String"));
            dt.Columns.Add("Business Type", Type.GetType("System.String"));
            dt.Columns.Add("Vender", Type.GetType("System.String"));
            dt.Columns.Add("Item Code", Type.GetType("System.String"));
            dt.Columns.Add("Goods Name", Type.GetType("System.String"));
            dt.Columns.Add("Specification", Type.GetType("System.String"));
            dt.Columns.Add("Unit", Type.GetType("System.String"));
            dt.Columns.Add("Inventory", Type.GetType("System.String"));
            dt.Columns.Add("Quantity", Type.GetType("System.String"));
            dt.Columns.Add("Original Price", Type.GetType("System.String"));
            dt.Columns.Add("Discount%", Type.GetType("System.String"));
            dt.Columns.Add("Discount amount", Type.GetType("System.String"));
            dt.Columns.Add("Current Price", Type.GetType("System.String"));
            dt.Columns.Add("Amount", Type.GetType("System.String"));
            dt.Columns.Add("Tax rate%", Type.GetType("System.String"));
            dt.Columns.Add("Unit Price Including Tax", Type.GetType("System.String"));
            dt.Columns.Add("Tax amount", Type.GetType("System.String"));
            dt.Columns.Add("Total Price Including Tax", Type.GetType("System.String"));

            #endregion

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                #region Assign values to temporary table

                DataRow newRow = dt.NewRow();
                newRow["Purchase Date"] = ds.Tables[0].Rows[i]["bizDate"].ToString();
                newRow["Receipt Number"] = ds.Tables[0].Rows[i]["number"].ToString();
                newRow["Business Type"] = ds.Tables[0].Rows[i]["types"].ToString();
                newRow["Vender"] = ds.Tables[0].Rows[i]["wlName"].ToString();
                newRow["Item Code"] = ds.Tables[0].Rows[i]["code"].ToString();
                newRow["Goods Name"] = ds.Tables[0].Rows[i]["goodsName"].ToString();
                newRow["Specification"] = ds.Tables[0].Rows[i]["spec"].ToString();
                newRow["Unit"] = ds.Tables[0].Rows[i]["unitName"].ToString();
                newRow["Inventory"] = ds.Tables[0].Rows[i]["ckName"].ToString();
                newRow["Quantity"] = ds.Tables[0].Rows[i]["num"].ToString();
                newRow["Original Price"] = ds.Tables[0].Rows[i]["price"].ToString();
                newRow["Discount%"] = ds.Tables[0].Rows[i]["dis"].ToString();
                newRow["Discount amount"] = ds.Tables[0].Rows[i]["sumPriceDis"].ToString();
                newRow["Current Price"] = ds.Tables[0].Rows[i]["priceNow"].ToString();
                newRow["Amount"] = ds.Tables[0].Rows[i]["sumPriceNow"].ToString();
                newRow["Tax rate%"] = ds.Tables[0].Rows[i]["tax"].ToString();
                newRow["Unit Price Including Tax"] = ds.Tables[0].Rows[i]["priceTax"].ToString();
                newRow["Tax amount"] = ds.Tables[0].Rows[i]["sumPriceTax"].ToString();
                newRow["Total Price Including Tax"] = ds.Tables[0].Rows[i]["sumPriceAll"].ToString();
                dt.Rows.Add(newRow);

                #endregion

                list.Add(new
                {
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    priceNow = ds.Tables[0].Rows[i]["priceNow"].ToString(),
                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),

                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString()
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);

            if (down == "1")
            {
                string filePath = Server.MapPath("../excel/ProductPurchaseOrderDetails" + path) + ".xls";
                this.WriteExcel(dt, filePath);
            }
        }

        public void WriteExcel(DataTable dt, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
            {
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet = book.CreateSheet(dt.TableName);
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }

                decimal sumNum = 0;
                decimal sumPriceDis = 0;
                decimal sumPriceNow = 0;
                decimal sumPriceTax = 0;
                decimal sumPriceAll = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
                    sumNum += ConvertTo.ConvertDec(dt.Rows[i][9].ToString());
                    sumPriceDis += ConvertTo.ConvertDec(dt.Rows[i][12].ToString());
                    sumPriceNow += ConvertTo.ConvertDec(dt.Rows[i][14].ToString());
                    sumPriceTax += ConvertTo.ConvertDec(dt.Rows[i][17].ToString());
                    sumPriceAll += ConvertTo.ConvertDec(dt.Rows[i][18].ToString());

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }

                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(dt.Rows.Count + 1);

                row3.CreateCell(0).SetCellValue("");//Item Code
                row3.CreateCell(1).SetCellValue("Total：");//Goods Name
                row3.CreateCell(9).SetCellValue(sumNum.ToString("0.00"));
                row3.CreateCell(12).SetCellValue(sumPriceDis.ToString("0.00"));
                row3.CreateCell(14).SetCellValue(sumPriceNow.ToString("0.00"));
                row3.CreateCell(17).SetCellValue(sumPriceTax.ToString("0.00"));
                row3.CreateCell(18).SetCellValue(sumPriceAll.ToString("0.00"));

                // Write to the client
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    book.Write(ms);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                }

                #region Download

                string path = filePath;

                try
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                    Response.Clear();
                    Response.Charset = "GB2312";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    // Add header information to specify the default file name for the "File Download/Save As" dialog box
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                    // Add header information to specify the file size, so the browser can show the download progress
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    // Specify that the response is a stream that cannot be read by the client and must be downloaded
                    Response.ContentType = "application/octet-stream";
                    // Send the file stream to the client
                    Response.WriteFile(file.FullName);
                    // Stop the execution of the page
                    Response.End();


                }
                catch (Exception) { }

                #endregion
            }
        }
    }
}
