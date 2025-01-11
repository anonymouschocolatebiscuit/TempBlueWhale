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
    public partial class SalesReceiptListCheck : BasePage
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public ClientDAL venderDAL = new ClientDAL();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesReceiptListCheck"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
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


                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {


                    Response.Write("请先设置Logo！");
                    Response.End();

                }
                    
                MakePDF(id, number);


                if (File.Exists(Server.MapPath("pdf/" + number + ".pdf")))
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

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),

                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),

                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    sendPayType = ds.Tables[0].Rows[i]["sendPayType"].ToString(),
                    sendName = ds.Tables[0].Rows[i]["sendName"].ToString(),
                    sendCode = ds.Tables[0].Rows[i]["sendCode"].ToString(),
                    sendNumber = ds.Tables[0].Rows[i]["sendNumber"].ToString(),

                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    disPrice = ds.Tables[0].Rows[i]["disPrice"].ToString(),

                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
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

                if (!CheckPower("SalesReceiptListDelete"))
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

                            
                            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "删除销售出库-ID：" + delId.ToString();
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

                if (!CheckPower("SalesReceiptListCheck"))
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
                            logs.Events = "审核销售出库-ID：" + delId.ToString();
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

                if (!CheckPower("SalesReceiptListCheckNo"))
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


                            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "反审核销售出库-ID：" + delId.ToString();
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
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + number + ".pdf"), FileMode.Create));

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

            string signTitle = "送货单位（签字盖章）：__________     收货单位（签字盖章）：________";

            string footerTitle = "白联：销售部        红联：客户       蓝联：财务      黄联：总经理";


            string tel = SysInfo.Tel;         
            string fax = SysInfo.Fax;

            string showLogo = "是";
            string showZhang = "否";

            #region 显示LOGO-----------控制
            if (showLogo == "是")
            {



                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/"+LoginUser.ShopId.ToString()+"logo.jpg"));

                float x = float.Parse("5");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region 显示印章-----------控制

            if (showZhang == "是")
            {


                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/"+LoginUser.ShopId.ToString()+"zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            #endregion
           
            #region 标题----

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("销货单", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            #endregion


            #region 获取表头信息


            int clientId = 0;
            string bizDate = "";
          
           
         
            string bizName = "蓝微";
            string remarks = "";
            string dis = "";
            string disPrice = "";
            string sumPriceAll = "";

            string checkName = "";

          

            DataSet dsQuote = dal.GetAllModel(id);
            if (dsQuote.Tables[0].Rows.Count > 0)
            {
                clientId = ConvertTo.ConvertInt(dsQuote.Tables[0].Rows[0]["wlId"].ToString());
                number = dsQuote.Tables[0].Rows[0]["number"].ToString();

                bizDate = DateTime.Parse(dsQuote.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();


                dis = dsQuote.Tables[0].Rows[0]["dis"].ToString();
                disPrice = dsQuote.Tables[0].Rows[0]["disPrice"].ToString();

                sumPriceAll = dsQuote.Tables[0].Rows[0]["sumPriceAll"].ToString();

                bizName = dsQuote.Tables[0].Rows[0]["bizName"].ToString();

                checkName = dsQuote.Tables[0].Rows[0]["checkName"].ToString();
               
                remarks = dsQuote.Tables[0].Rows[0]["remarks"].ToString();

            }


            #endregion

            #region 获取客户信息

            string wlName = "深圳蓝微电子科技有限公司";
            string wlPhone = "15950056946";
            string wlTel = "0512-68276837";
            string wlLinkMan = "";
           
           
            string wlAddress = "";

            DataSet dsClient = venderDAL.GetAllModelView(clientId);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["phone"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString(); 
                wlLinkMan = dsClient.Tables[0].Rows[0]["linkMan"].ToString();               
                wlAddress = dsClient.Tables[0].Rows[0]["address"].ToString();

            }

            if (wlPhone == "")
            {
                wlPhone = wlTel;
            }

            #endregion




            string titleRowOne = "编号:"+number+"  客户:"+wlName+" 地址："+wlAddress+"  日期:"+DateTime.Now.ToString("yyyy-MM-dd");

            Paragraph pleasure = new Paragraph(titleRowOne, font1);
            pleasure.Alignment = 0;
            pleasure.SpacingBefore = 5;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);



            #region 明细开始


            PdfPTable tablebj = new PdfPTable(9);//表格有7列-----------项目、品名、规格型号、品牌、最小包装、单位、单价、税率、备注
            tablebj.SetTotalWidth(new float[] { 30, 100, 110, 50, 60, 50, 50, 40, 70 });//列宽560

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//创建单元格

            #region 第一行-----标题

            Paragraph table_tbj = new Paragraph("序号 NO", fontTitle10);
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


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("品牌 Brand", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("数量 Quantity", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单位 Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单价 Price", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("小计 Total", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("备注 Remarks", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion

            int rowNum = 0;

            SalesReceiptItemDAL item = new SalesReceiptItemDAL();

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


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["brandName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["num"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);




                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["unitName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["priceTax"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["sumPriceAll"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["remarks"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);



                    #endregion
                }



            }

            if (rowNum < 9)
            {

                #region 插入、以下空白行

                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("以下空白", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);

                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);

                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);

                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                #endregion

            }

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





            string sumPriceAllLast=(ConvertTo.ConvertDec(sumPriceAll)-ConvertTo.ConvertDec(disPrice)).ToString("0.00");

            //复杂的结束  We have the pleasure to offer you under motioned products and service。
            string disPriceTitle = "备注：压质保金" + dis + "%，合计" + disPrice + " 应付剩余货款：" + sumPriceAllLast;

            Paragraph remarksP = new Paragraph(disPriceTitle, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 10;
            remarksP.SpacingAfter = 10;
            doc.Add(remarksP);

            //Paragraph tax = new Paragraph("    1、含税情况：" + isTax + "，付款时间：" + payDate + "，付款方式：" + payWay, font1);
            //tax.Alignment = 0;
            //doc.Add(tax);

            //Paragraph send = new Paragraph("    2、运输方式：" + freightWay + "，运费承担：" + isFreight + "，包装方式：" + package, font1);
            //send.Alignment = 0;
            //doc.Add(send);

            //Paragraph sendDateP = new Paragraph("    3、交货周期：" + sendDate + "，交货地点：" + sendPlace + "", font1);
            //sendDateP.Alignment = 0;
            //doc.Add(sendDateP);


            //报价条款

            Paragraph footRemarks = new Paragraph(signTitle, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 20;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion



            Paragraph noteP = new Paragraph(footerTitle, fontLabel);//以上报价请确认回签，以便我司存档，谢谢合作！
            noteP.Alignment = 0;
            noteP.SpacingBefore = 20;
            noteP.SpacingAfter = 20;
            doc.Add(noteP);


            


            //5、关闭Document

            doc.Close();

        }
    }
}
