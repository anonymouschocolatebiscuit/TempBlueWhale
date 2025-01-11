using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;
using System.IO;

namespace Lanwei.Weixin.UI.report
{
    public partial class PurOrderListDetailReport : BasePage
    {
        public PurReceiptDAL dal = new PurReceiptDAL();
        public VenderDAL venderDAL = new VenderDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public CangkuDAL ckDAL = new CangkuDAL();

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

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());


          


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

           
            DataSet ds = dal.GetPurReceiptItemDetail(LoginUser.ShopId,bizStart,bizEnd,typeId,wlId,goodsId);

            #region 创建临时表
            DataTable dt = new DataTable("商品采购明细表");

            dt.Columns.Add("采购日期", Type.GetType("System.String"));
            dt.Columns.Add("单据编号", Type.GetType("System.String"));
            dt.Columns.Add("业务类别", Type.GetType("System.String"));
            dt.Columns.Add("供应商", Type.GetType("System.String"));
            dt.Columns.Add("商品编码", Type.GetType("System.String"));
            dt.Columns.Add("商品名称", Type.GetType("System.String"));
            dt.Columns.Add("规格", Type.GetType("System.String"));
            dt.Columns.Add("单位", Type.GetType("System.String"));
            dt.Columns.Add("仓库", Type.GetType("System.String")); 
            dt.Columns.Add("数量", Type.GetType("System.String"));
            dt.Columns.Add("原价", Type.GetType("System.String"));
            dt.Columns.Add("折扣%", Type.GetType("System.String"));
            dt.Columns.Add("折扣金额", Type.GetType("System.String"));
            dt.Columns.Add("现价", Type.GetType("System.String"));
            dt.Columns.Add("金额", Type.GetType("System.String"));
            dt.Columns.Add("税率%", Type.GetType("System.String"));            
            dt.Columns.Add("含税单价", Type.GetType("System.String"));
            dt.Columns.Add("税额", Type.GetType("System.String"));
            dt.Columns.Add("价税合计", Type.GetType("System.String"));

            #endregion

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                #region 临时表赋值

                DataRow newRow = dt.NewRow();
                newRow["采购日期"] = ds.Tables[0].Rows[i]["bizDate"].ToString();
                newRow["单据编号"] = ds.Tables[0].Rows[i]["number"].ToString();
                newRow["业务类别"] = ds.Tables[0].Rows[i]["types"].ToString();
                newRow["供应商"] = ds.Tables[0].Rows[i]["wlName"].ToString();
                newRow["商品编码"] = ds.Tables[0].Rows[i]["code"].ToString();
                newRow["商品名称"] = ds.Tables[0].Rows[i]["goodsName"].ToString();
                newRow["规格"] = ds.Tables[0].Rows[i]["spec"].ToString();
                newRow["单位"] = ds.Tables[0].Rows[i]["unitName"].ToString();
                newRow["仓库"] = ds.Tables[0].Rows[i]["ckName"].ToString();
                newRow["数量"] = ds.Tables[0].Rows[i]["num"].ToString();
                newRow["原价"] = ds.Tables[0].Rows[i]["price"].ToString();
                newRow["折扣%"] = ds.Tables[0].Rows[i]["dis"].ToString();
                newRow["折扣金额"] = ds.Tables[0].Rows[i]["sumPriceDis"].ToString();
                newRow["现价"] = ds.Tables[0].Rows[i]["priceNow"].ToString();
                newRow["金额"] = ds.Tables[0].Rows[i]["sumPriceNow"].ToString();
                newRow["税率%"] = ds.Tables[0].Rows[i]["tax"].ToString();
                newRow["含税单价"] = ds.Tables[0].Rows[i]["priceTax"].ToString();
                newRow["税额"] = ds.Tables[0].Rows[i]["sumPriceTax"].ToString();
                newRow["价税合计"] = ds.Tables[0].Rows[i]["sumPriceAll"].ToString();
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
               
                string filePath = Server.MapPath("../excel/商品采购明细表" + path) + ".xls";               
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

                row3.CreateCell(0).SetCellValue("");//商品编码
                row3.CreateCell(1).SetCellValue("合计：");//商品名称
                row3.CreateCell(9).SetCellValue(sumNum.ToString("0.00"));
                row3.CreateCell(12).SetCellValue(sumPriceDis.ToString("0.00"));
                row3.CreateCell(14).SetCellValue(sumPriceNow.ToString("0.00"));
                row3.CreateCell(17).SetCellValue(sumPriceTax.ToString("0.00"));
                row3.CreateCell(18).SetCellValue(sumPriceAll.ToString("0.00"));


                // 写入到客户端  
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


                #region 下载

                string path = filePath;

                try
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                    Response.Clear();
                    Response.Charset = "GB2312";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                    // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    // 指定返回的是一个不能被客户端读取的流，必须被下载 
                    Response.ContentType = "application/octet-stream";
                    // 把文件流发送到客户端 
                    Response.WriteFile(file.FullName);
                    // 停止页面的执行 
                    Response.End();


                }
                catch (Exception ex) { }

                #endregion



            }
        }


      



    }
}
