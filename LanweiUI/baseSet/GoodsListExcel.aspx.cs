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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;
using ExcelReport;
using System.Data.OleDb;

namespace Lanwei.Weixin.UI.BaseSet
{
    public partial class GoodsListExcel : BasePage
    {
        public GoodsDAL goodsDAL = new GoodsDAL();
        public GoodsTypeDAL typeDAL = new GoodsTypeDAL();
        public UnitDAL unitDAL = new UnitDAL();
        public CangkuDAL ckDAL = new CangkuDAL();

        public GoodsBrandDAL brandDAL = new GoodsBrandDAL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("GoodsListExcel"))
                {
                    Response.Redirect("../OverPower.htm");
                }

              
            }
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDownExcel_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

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
        }

        protected void lbtnDownExample_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

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
        }

        public int TotalNumAll = 0;
        public int TotalNumOK = 0;
        public int TotalNumNo = 0;

        protected void btnExcelTo_Click(object sender, EventArgs e)
        {

           
            if (!CheckPower("GoodsListExcel"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

            if (fload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fload.FileName);
                if (fileExt == ".xls" || fileExt == ".xlsx")
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt;
                   
                    string oPath = Server.MapPath("excel/"+fileName);//文件路径

                    try
                    {
                        fload.PostedFile.SaveAs(oPath);

                        //ExcelHelper help = new ExcelHelper();
                        //DataSet ds = help.ExcelToDataSet(filepath);

                        DataSet ds = this.ExcelDataSource(fileName);
                        DataTable dt = ds.Tables[0];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //删除多余列
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (dt.Columns[i].ColumnName.IndexOf('F') >= 0)
                                {
                                    if (dt.Columns[i].ColumnName.Trim() != "F1")
                                    {
                                        dt.Columns.Remove(dt.Columns[i]);
                                        i--;
                                    }
                                }
                            }
                            //删除多余行
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (DBNull.Value == dt.Rows[i][0] || dt.Rows[0][0].ToString() == "")
                                {
                                    dt.Rows.Remove(dt.Rows[0]);
                                }
                            }
                            dt.AcceptChanges();
                        }

                      

                        this.Label1.Text = this.DataTableToSql(dt);

                        this.Label1.Visible = true;


                        LogsDAL logs = new LogsDAL();
                        logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                        logs.Events = "导入商品信息：" + this.Label1.Text;
                        logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        logs.Add();

                        //this.GridView1.DataSource = dt;
                        //this.GridView1.DataBind();

                        System.IO.File.Delete(oPath);

                      


                    }
                    catch (Exception ex)
                    {
                        this.Label1.Text = ex.Message;
                        this.Label1.Visible = true;
                    }
                }
                else
                {

                    this.Label1.Text = "Error!!!";

                }
            }
            else
            {

            }
        }

        public DataSet ExcelDataSource(string fileName)
        {
            string oPath = Server.MapPath("excel/" + fileName);//文件路径

            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + oPath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [商品$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            conn.Close();
            return ds;
        }

        public string DataTableToSql(DataTable dt)
        {
            int goodsId = 0;

            int goodsNum = 0;
            int kcNum = 0;

            TotalNumAll = dt.Rows.Count;

            #region 循环DataTable

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["商品编码"].ToString();
                string names = dt.Rows[i]["商品名称"].ToString();
                string barcode = dt.Rows[i]["商品条码"].ToString();
                string typeName = dt.Rows[i]["商品类别"].ToString();
                string brandName = dt.Rows[i]["品牌"].ToString();

                string spec = dt.Rows[i]["规格"].ToString();
                string unitName = dt.Rows[i]["计量单位"].ToString();
                
                string ckName = dt.Rows[i]["首选仓库"].ToString();
                string place = dt.Rows[i]["产地"].ToString();

                decimal priceCost = ConvertTo.ConvertDec(dt.Rows[i]["采购价"].ToString());

                decimal priceSalesWhole = ConvertTo.ConvertDec(dt.Rows[i]["批发价"].ToString());
                decimal priceSalesRetail = ConvertTo.ConvertDec(dt.Rows[i]["零售价"].ToString());

                int numMin = ConvertTo.ConvertInt(dt.Rows[i]["最低库存"].ToString());
                int numMax = ConvertTo.ConvertInt(dt.Rows[i]["最高库存"].ToString());
                int bzDays = ConvertTo.ConvertInt(dt.Rows[i]["保质期天数"].ToString());

               
                

                string isWeight = dt.Rows[i]["称重商品"].ToString();

                string remarks = dt.Rows[i]["备注"].ToString();


                string ckNameNm = dt.Rows[i]["仓库"].ToString();
                decimal num = ConvertTo.ConvertDec(dt.Rows[i]["期初数量"].ToString());
                decimal price = ConvertTo.ConvertDec(dt.Rows[i]["单位成本"].ToString());
                decimal sumPrice = num*price;

                string FieldA = dt.Rows[i]["自定义1"].ToString();
                string FieldB = dt.Rows[i]["自定义2"].ToString();
                string FieldC = dt.Rows[i]["自定义3"].ToString();
                string FieldD = dt.Rows[i]["自定义4"].ToString();

                int brandId = 0;
                int typeId = 0;
                int unitId = 0;
                int ckId = 0;
                int ckIdNum = 0;

                bool hasGoods = false;
                bool hasCode=false;


                hasCode = goodsDAL.isExistsAdd(LoginUser.ShopId,code, barcode, names, spec);
               
             

                if (code != "" && names != "")
                {

                    #region 如果是第一行
                 
                    if (!hasCode)
                    {
                        brandId = brandDAL.GetIdByName(LoginUser.ShopId,brandName);
                        typeId = typeDAL.GetIdByName(LoginUser.ShopId,typeName);
                        unitId = unitDAL.GetIdByName(LoginUser.ShopId, unitName);
                        ckId = ckDAL.GetIdByName(LoginUser.ShopId, ckName);

                        goodsDAL.ShopId = LoginUser.ShopId;
                        
                        goodsDAL.Code = code;
                        goodsDAL.Barcode = barcode;
                        goodsDAL.Names = names;
                        
                        goodsDAL.TypeId = typeId;
                        goodsDAL.BrandId = brandId;
                        
                        goodsDAL.Spec = spec;
                        goodsDAL.UnitId = unitId;
                        
                        goodsDAL.CkId = ckId;
                        goodsDAL.Place = place;

                        goodsDAL.PriceCost = priceCost;

                        goodsDAL.PriceSalesWhole = priceSalesWhole;
                        goodsDAL.PriceSalesRetail = priceSalesRetail;


                        goodsDAL.NumMin = numMin;
                        goodsDAL.NumMax = numMax;

                        goodsDAL.BzDays = bzDays;

                        goodsDAL.Remarks = remarks;
                       

                        if (isWeight == "是")
                        {
                            goodsDAL.IsWeight = 1;

                            if (barcode.Length != 5)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            goodsDAL.IsWeight = 0;
                        }

                        goodsDAL.MakeDate = DateTime.Now;
                        goodsDAL.ImagePath = "noPic.jpg";
                        goodsDAL.ShowType = "默认";
                        goodsDAL.FieldA = FieldA;
                        goodsDAL.FieldB = FieldB;
                        goodsDAL.FieldC = FieldC;
                        goodsDAL.FieldD = FieldD;

                        goodsDAL.Flag = "保存";
                        

                        goodsId = goodsDAL.Add();//新增商品

                        if (goodsId > 0)
                        {
                            goodsNum += 1;

                            TotalNumOK += 1;
                        }

                        if (ckNameNm != "" && num != 0)//如果存在录入仓库的情况
                        {
                            ckIdNum = ckDAL.GetIdByName(LoginUser.ShopId,ckNameNm);

                            if (ckIdNum > 0)
                            {
                                int addNum = goodsDAL.AddGoodsNumStart(goodsId, ckIdNum, num, price, sumPrice);

                                if (addNum > 0)
                                {
                                    kcNum += 1;
                                }
                            }

                        }

                    }
                    #endregion
                }
                else
                {

                    #region 如果是空行，去找后面的期初库存信息，是否为空

                    if (code != "" && names != "" && ckNameNm != "" && num != 0)
                    {
                        ckIdNum = ckDAL.GetIdByName(LoginUser.ShopId,ckNameNm);
                        int add = goodsDAL.AddGoodsNumStart(goodsId,ckIdNum,num,price,sumPrice);

                        if (add > 0)
                        {
                            kcNum += 1;
                        }
 
                    }


                    #endregion

                }

            }

            #endregion

            TotalNumNo = TotalNumAll - TotalNumOK;

            string result = "共【"+TotalNumAll.ToString()+"】条商品，成功【" + goodsNum.ToString() + "】条，失败【"+TotalNumNo.ToString()+"】条，成功导入【" + kcNum.ToString() + "】条库存。";

            return result;

        }

    }
}
