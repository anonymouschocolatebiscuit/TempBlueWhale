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


using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Lanwei.Weixin.UI.sales
{
    public partial class SalesOrderList : BasePage
    {
        public SalesOrderDAL dal = new SalesOrderDAL();
        public DAL.ClientDAL venderDAL = new ClientDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hfShopId.Value = LoginUser.ShopId.ToString();

            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesOrderList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;

                int types = 0;

                GetDataList(keys, start, end,types);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int types =ConvertTo.ConvertInt(Request.Params["types"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end,types);
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                string id = Request.Params["id"].ToString();
                DeleteRow(id);
                Response.End();
            }

            if (Request.Params["Action"] == "checkRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "checkNoRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckNoRow(idString);
                Response.End();
            }


            if (Request.Params["Action"] == "makePDF")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                string number = Request.Params["number"].ToString();



                MakePDF(id, number);


                if (File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf")))
                {

                    Response.Write("生成成功！");

                }
                else
                {

                    Response.Write("生成失败！");
                }
                Response.End();
            }

            if (Request.Params["Action"] == "makePDFNoPrice")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                string number = Request.Params["number"].ToString();



                MakePDFNoPrice(id, number);


                if (File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf")))
                {

                    Response.Write("生成成功！");

                }
                else
                {

                    Response.Write("生成失败！");
                }
                Response.End();
            }


        }

        void GetDataList(string key, DateTime start, DateTime end,int types)
        {
            DataSet ds = dal.GetAllModel(LoginUser.ShopId, key, start, end, types);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               

                string flag = ds.Tables[0].Rows[i]["flag"].ToString();

                //if(flag == "未处理")
                //{
                //    flag = "<font color='red'>未处理</font>";
                //}
                //else
                //{
                //    flag = "<font color='green'>已审核</font>";
                //}



                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),


                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                    


                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要



            Response.Write(s);
        }


        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListDelete"))
                {
                    Response.Write("无此操作权限，请联系管理员！");
                    return;
                }

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.Delete(delId);
                        if (del > 0)
                        {
                            num += 1;

                            
                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "删除销售订单-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {

                   
                    Response.Write("成功删除"+num+"条记录！");

                }
                else
                {
                    Response.Write("删除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListCheck"))
                {
                    Response.Write("无此操作权限！");

                    return;
                }

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId,LoginUser.Id,LoginUser.Names, DateTime.Now, "审核");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "审核销售订单-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("审核失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }


        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListCheckNo"))
                {
                    Response.Write("无此操作权限！");

                    return;
                }

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId, LoginUser.Id,LoginUser.Names, DateTime.Now, "保存");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "反审核销售订单-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功反审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("反审核失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }


        }


        void MakePDF(int id, string number)
        {
            //定义一个Document，并设置页面大小为A4，竖向

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4纸横放

            //1、创建一个实例
            Document doc = new Document(PageSize.A4);

            //2、为该Document创建一个Writer实例
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf"), FileMode.Create));

            //3、打开当前Document

            doc.Open();

            //4、为当前Document添加内容

            //4.1先添加中文字体
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            //定义字体格式
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);


            //4.2然后添加内容



            //复杂的开始




            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksSalesOrder;




            string tel = SysInfo.Tel;
            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;
            string showZhang = SysInfo.PrintZhang;

            #region 显示LOGO-----------控制
            if (showLogo == "是")
            {

                if (!File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {


                    Response.Write("请先设置Logo！");
                    Response.End();

                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("15");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region 显示印章-----------控制

            if (showZhang == "是")
            {

                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {


                    Response.Write("请先设置印章！");
                    Response.End();

                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            #endregion

            #region 获取表头信息


            int clientId = 0;
            string bizDate = "";



            string bizName = "蓝微";

            string sumNum = "";
            string sumPriceAll = "";

            string checkName = "";
            string sendDate = "";
            string remarksOrder = "";

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clientId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["wlId"].ToString());
                number = ds.Tables[0].Rows[0]["number"].ToString();

                bizDate = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                sendDate = DateTime.Parse(ds.Tables[0].Rows[0]["sendDate"].ToString()).ToShortDateString();

                sumNum = ds.Tables[0].Rows[0]["sumNum"].ToString();
                sumPriceAll = ds.Tables[0].Rows[0]["sumPriceAll"].ToString();

                bizName = ds.Tables[0].Rows[0]["bizName"].ToString();

                checkName = ds.Tables[0].Rows[0]["checkName"].ToString();

                remarksOrder = ds.Tables[0].Rows[0]["remarks"].ToString();

            }


            #endregion

            #region 标题----销售订单---编号

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("销售订单", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);
            quoteNumber.Alignment = 1;
            doc.Add(quoteNumber);

            #endregion




            #region 获取客户信息

            string wlName = "苏州蓝微电子科技有限公司";
            string wlPhone = "15950056946";
            string wlTel = "0512-68276837";
            string wlLinkMan = "";


          
            string isWhere = " id='" + clientId + "' ";
            DataSet dsClient = venderDAL.GetList(isWhere);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["phone"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlLinkMan = dsClient.Tables[0].Rows[0]["linkMan"].ToString();
             

            }

            if (wlPhone == "")
            {
                wlPhone = wlTel;
            }

            #endregion


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列

            table.SetTotalWidth(new float[] { 90, 190, 90, 190 });// doc.PageSize.Width;//宽度560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行-----------客户和供应商名称



            cell.Border = 0;
            Paragraph table_t = new Paragraph("需方(Buyer)：", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10);//客户名称
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("供方(Vendor)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(company, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);





            #endregion


            #region 第二行-----------联系人姓名

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("联系人(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10);//客户联系人
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("联系人(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizName, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第三行-----------Tel

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("电话(Tel)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlPhone, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("电话(Tel)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(tel, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第四行-------------日期

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("下单日期：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("交货日期：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(sendDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion





            doc.Add(table);

            //复杂的结束  We have the pleasure to offer you under motioned products and service。


            //string titleRowOne = "编号:" + number + "  供方:" + wlName + "  采购日期:" + bizDate + " 交货日期：" + sendDate;

            //Paragraph pleasure = new Paragraph(titleRowOne, font1);
            //pleasure.Alignment = 0;
            //pleasure.SpacingBefore = 5;
            //pleasure.SpacingAfter = 5;
            //doc.Add(pleasure);



            #region 明细开始


            PdfPTable tablebj = new PdfPTable(7);//表格有7列-----------项目、品名、规格型号、品牌、最小包装、单位、单价、税率、备注
            tablebj.SetTotalWidth(new float[] { 30, 170, 110, 60, 60, 60, 70 });//列宽560

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//创建单元格

            #region 第一行-----标题

            Paragraph table_tbj = new Paragraph("序号  NO", fontTitle10);
            table_tbj.Alignment = 1;

            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("商品名称   Goods Name", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("规格型号 SPECIFICATION", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("品牌 Brand", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单 位  Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("数 量 Quantity", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单 价  Price", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("小 计  Total", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("备 注  Remarks", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);



            #endregion

            int rowNum = 0;

            SalesOrderItemDAL item = new SalesOrderItemDAL();

            DataSet dsItem = item.GetAllModel(id);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                rowNum = dsItem.Tables[0].Rows.Count;

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    #region 第二行-----明细


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph((i + 1).ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["goodsName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["spec"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["brandName"].ToString(), fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["unitName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);



                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["num"].ToString(), fontTitle10);
                    table_tbj.Alignment = 2;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);






                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["priceTax"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["sumPriceAll"].ToString(), fontTitle10);
                    table_tbj.Alignment = 2;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["remarks"].ToString(), fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);



                    #endregion
                }



            }

            //if (rowNum < 9)
            //{



            //}

            #region 插入、合计行

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("合计", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph(sumNum, fontTitle10);
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph(sumPriceAll, fontTitle10);//小计
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            //cellbj = new PdfPCell();
            //table_tbj = new Paragraph("", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);


            #endregion

            #region 填充空白行

            //if ((rowNum + 1) < 8)
            //{
            //    int rowBlank = 10 - rowNum - 1;
            //    for (int j = 0; j < rowBlank; j++)
            //    {
            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);


            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);


            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);


            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);


            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);

            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);


            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);

            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);

            //        cellbj = new PdfPCell();

            //        table_tbj = new Paragraph(" ", fontTitle10);
            //        table_tbj.Alignment = 1;
            //        cellbj.AddElement(table_tbj);
            //        tablebj.AddCell(cellbj);
            //    }
            //}


            #endregion





            doc.Add(tablebj);



            #endregion



            //以上报价请确认回签，以便我司存档，谢谢合作！

            #region 尾部备注


            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);




            //备注信息

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion



            //Paragraph noteP = new Paragraph(footerTitle, fontLabel);//以上报价请确认回签，以便我司存档，谢谢合作！
            //noteP.Alignment = 0;
            //noteP.SpacingBefore = 20;
            //noteP.SpacingAfter = 20;
            //doc.Add(noteP);


            #region 落款签名

            PdfPTable tableFooter = new PdfPTable(4);//表格有4列

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 });// doc.PageSize.Width;//宽度530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//创建单元格

            #region 第一行



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("需方(签字盖章)：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);//客户名称
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("供方(签字盖章)：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);





            #endregion


            #region 第二行

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("日 期：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("日 期：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            #endregion







            doc.Add(tableFooter);


            #endregion



            //5、关闭Document

            doc.Close();

        }

        

        void MakePDFNoPrice(int id, string number)
        {
            //定义一个Document，并设置页面大小为A4，竖向

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4纸横放

            //1、创建一个实例
            Document doc = new Document(PageSize.A4);

            //2、为该Document创建一个Writer实例
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + "-01.pdf"), FileMode.Create));

            //3、打开当前Document

            doc.Open();

            //4、为当前Document添加内容

            //4.1先添加中文字体
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            //定义字体格式
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);


            //4.2然后添加内容



            //复杂的开始




            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksSalesOrder;




            string tel = SysInfo.Tel;
            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;
            string showZhang = SysInfo.PrintZhang;

            #region 显示LOGO-----------控制
            if (showLogo == "是")
            {

                if (!File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {


                    Response.Write("请先设置Logo！");
                    Response.End();

                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("15");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region 显示印章-----------控制

            if (showZhang == "是")
            {

                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {


                    Response.Write("请先设置印章！");
                    Response.End();

                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            #endregion

            #region 获取表头信息


            int clientId = 0;
            string bizDate = "";



            string bizName = "蓝微";

            string sumNum = "";
            string sumPriceAll = "";

            string checkName = "";
            string sendDate = "";
            string remarksOrder = "";

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                clientId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["wlId"].ToString());
                number = ds.Tables[0].Rows[0]["number"].ToString();

                bizDate = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                sendDate = DateTime.Parse(ds.Tables[0].Rows[0]["sendDate"].ToString()).ToShortDateString();

                sumNum = ds.Tables[0].Rows[0]["sumNum"].ToString();
                sumPriceAll = ds.Tables[0].Rows[0]["sumPriceAll"].ToString();

                bizName = ds.Tables[0].Rows[0]["bizName"].ToString();

                checkName = ds.Tables[0].Rows[0]["checkName"].ToString();

                remarksOrder = ds.Tables[0].Rows[0]["remarks"].ToString();

            }


            #endregion

            #region 标题----销售订单---编号

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("销售订单", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);
            quoteNumber.Alignment = 1;
            doc.Add(quoteNumber);

            #endregion




            #region 获取客户信息

            string wlName = "苏州蓝微电子科技有限公司";
            string wlPhone = "15950056946";
            string wlTel = "0512-68276837";
            string wlLinkMan = "";



            string isWhere = " id='" + clientId + "' ";
            DataSet dsClient = venderDAL.GetList(isWhere);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["phone"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlLinkMan = dsClient.Tables[0].Rows[0]["linkMan"].ToString();


            }

            if (wlPhone == "")
            {
                wlPhone = wlTel;
            }

            #endregion


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列

            table.SetTotalWidth(new float[] { 90, 190, 90, 190 });// doc.PageSize.Width;//宽度560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行-----------客户和供应商名称



            cell.Border = 0;
            Paragraph table_t = new Paragraph("需方(Buyer)：", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10);//客户名称
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("供方(Vendor)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(company, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);





            #endregion


            #region 第二行-----------联系人姓名

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("联系人(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10);//客户联系人
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("联系人(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizName, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第三行-----------Tel

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("电话(Tel)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlPhone, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("电话(Tel)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(tel, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第四行-------------日期

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("下单日期：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("交货日期：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(sendDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion





            doc.Add(table);

            //复杂的结束  We have the pleasure to offer you under motioned products and service。


            //string titleRowOne = "编号:" + number + "  供方:" + wlName + "  采购日期:" + bizDate + " 交货日期：" + sendDate;

            //Paragraph pleasure = new Paragraph(titleRowOne, font1);
            //pleasure.Alignment = 0;
            //pleasure.SpacingBefore = 5;
            //pleasure.SpacingAfter = 5;
            //doc.Add(pleasure);



            #region 明细开始


            PdfPTable tablebj = new PdfPTable(5);//表格有5列-----------
            tablebj.SetTotalWidth(new float[] { 60, 270, 110, 60, 60});//列宽560  130

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//创建单元格

            #region 第一行-----标题

            Paragraph table_tbj = new Paragraph("序号  NO", fontTitle10);
            table_tbj.Alignment = 1;

            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("商品名称   Goods Name", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("规格型号 SPECIFICATION", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("品牌 Brand", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单 位  Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("数 量 Quantity", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


        


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("备 注  Remarks", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);



            #endregion

            int rowNum = 0;

            SalesOrderItemDAL item = new SalesOrderItemDAL();

            DataSet dsItem = item.GetAllModel(id);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                rowNum = dsItem.Tables[0].Rows.Count;

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    #region 第二行-----明细


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph((i + 1).ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["goodsName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["spec"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["brandName"].ToString(), fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["unitName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);



                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["num"].ToString(), fontTitle10);
                    table_tbj.Alignment = 2;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);






               

               

                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["remarks"].ToString(), fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);



                    #endregion
                }



            }

            //if (rowNum < 9)
            //{



            //}

            #region 插入、合计行

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("合计", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph(sumNum, fontTitle10);
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


          

       


            //cellbj = new PdfPCell();
            //table_tbj = new Paragraph("", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);


            #endregion

            






            doc.Add(tablebj);



            #endregion



            //以上报价请确认回签，以便我司存档，谢谢合作！

            #region 尾部备注


            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);




            //备注信息

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion



            //Paragraph noteP = new Paragraph(footerTitle, fontLabel);//以上报价请确认回签，以便我司存档，谢谢合作！
            //noteP.Alignment = 0;
            //noteP.SpacingBefore = 20;
            //noteP.SpacingAfter = 20;
            //doc.Add(noteP);


            #region 落款签名

            PdfPTable tableFooter = new PdfPTable(4);//表格有4列

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 });// doc.PageSize.Width;//宽度530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//创建单元格

            #region 第一行



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("需方(签字盖章)：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);//客户名称
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("供方(签字盖章)：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);





            #endregion


            #region 第二行

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("日 期：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("日 期：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("_______________", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            #endregion







            doc.Add(tableFooter);


            #endregion



            //5、关闭Document

            doc.Close();

        }

    }
}
