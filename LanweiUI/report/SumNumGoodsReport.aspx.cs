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
    public partial class SumNumGoodsReport : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public CangkuDAL ckDAL = new CangkuDAL();
       
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

              
                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

                string code = Request.Params["goodsId"].ToString();

                string ckName = Request.Params["typeId"].ToString();

                string down = Request.Params["down"].ToString();

                string path = Request.Params["path"].ToString();


                this.GetDataList(bizEnd, code, ckName, down, path);
                Response.End();
            }
        }

        void GetDataList(DateTime bizEnd,string code,string ckName,string down,string path)
        {

            DataTable dt = new DataTable();

            DataSet ds = dal.GetGoodsStoreNum(LoginUser.ShopId,bizEnd, code, ckName);

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
                string filePath = Server.MapPath("../excel/商品库存余额表" +path) + ".xls";

                this.hfPath.Value = filePath;
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

                
              
                row.CreateCell(0).SetCellValue("商品编码");
                row.CreateCell(1).SetCellValue("商品名称");
                row.CreateCell(2).SetCellValue("规格");
                row.CreateCell(3).SetCellValue("单位");

                row.CreateCell(4).SetCellValue(SysInfo.FieldA);
                row.CreateCell(5).SetCellValue(SysInfo.FieldB);
                row.CreateCell(6).SetCellValue(SysInfo.FieldC);
                row.CreateCell(7).SetCellValue(SysInfo.FieldD);


                row.CreateCell(8).SetCellValue("仓库");
                row.CreateCell(9).SetCellValue("总数量");
                row.CreateCell(10).SetCellValue("库存单价");
                row.CreateCell(11).SetCellValue("总库存成本");


                sheet.SetColumnWidth(0, 15 * 256);//30个字符
                sheet.SetColumnWidth(1, 30 * 256);//30个字符
                sheet.SetColumnWidth(2, 30 * 256);//30个字符
                sheet.SetColumnWidth(3, 10 * 256);//30个字符

                sheet.SetColumnWidth(4, 10 * 256);//30个字符
                sheet.SetColumnWidth(5, 10 * 256);//30个字符
                sheet.SetColumnWidth(6, 10 * 256);//30个字符
                sheet.SetColumnWidth(7, 10 * 256);//30个字符

                sheet.SetColumnWidth(8, 10 * 256);//30个字符
                sheet.SetColumnWidth(9, 10 * 256);//30个字符
                sheet.SetColumnWidth(10, 10 * 256);//30个字符
                sheet.SetColumnWidth(11, 10 * 256);//30个字符


                decimal sumNum = 0;
                decimal sumPrice = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sumNum += ConvertTo.ConvertDec(dt.Rows[i]["sumNum"].ToString());
                    sumPrice += ConvertTo.ConvertDec(dt.Rows[i]["sumPriceStore"].ToString());

                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);

                    //goodsId,code,goodsName,spec,unitName,ckId,ckName,priceCost,fieldA,fieldB,fieldC,fieldD
                    row2.CreateCell(0).SetCellValue(Convert.ToString(dt.Rows[i]["code"]));//商品编码
                    row2.CreateCell(1).SetCellValue(Convert.ToString(dt.Rows[i]["goodsName"]));//商品名称
                    row2.CreateCell(2).SetCellValue(Convert.ToString(dt.Rows[i]["spec"]));//规格

                    row2.CreateCell(3).SetCellValue(Convert.ToString(dt.Rows[i]["unitName"]));//单位

                    row2.CreateCell(4).SetCellValue(Convert.ToString(dt.Rows[i]["fieldA"]));//单位
                    row2.CreateCell(5).SetCellValue(Convert.ToString(dt.Rows[i]["fieldB"]));//单位
                    row2.CreateCell(6).SetCellValue(Convert.ToString(dt.Rows[i]["fieldC"]));//单位
                    row2.CreateCell(7).SetCellValue(Convert.ToString(dt.Rows[i]["fieldD"]));//单位


                    row2.CreateCell(8).SetCellValue(Convert.ToString(dt.Rows[i]["ckName"]));//仓库

                    row2.CreateCell(9).SetCellValue(Convert.ToString(dt.Rows[i]["sumNum"]));//数量
                    row2.CreateCell(10).SetCellValue(Convert.ToString(dt.Rows[i]["priceCost"]));//成本价
                    row2.CreateCell(11).SetCellValue(Convert.ToString(dt.Rows[i]["sumPriceStore"]));//库存金额



                }


                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(dt.Rows.Count + 1);

                row3.CreateCell(0).SetCellValue("");//商品编码
                row3.CreateCell(1).SetCellValue("合计：");//商品名称
                row3.CreateCell(2).SetCellValue("");//规格
                row3.CreateCell(3).SetCellValue("");//单位
                row3.CreateCell(4).SetCellValue("");//仓库
                row3.CreateCell(9).SetCellValue(sumNum.ToString("0.00"));//数量
                row3.CreateCell(11).SetCellValue(sumPrice.ToString("0.00"));//数量




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
