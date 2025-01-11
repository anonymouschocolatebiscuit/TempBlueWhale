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

using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Lanwei.Weixin.UI.sales
{
    public partial class SalesQuoteList : BasePage
    {
        public SalesQuoteDAL dal = new SalesQuoteDAL();

        public UserDAL userDAL = new UserDAL();


        public ClientDAL venderDAL = new ClientDAL();

        public SalesQuoteItemDAL item = new SalesQuoteItemDAL();

      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesQuoteList"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;

                GetDataList(keys, start, end);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

            

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end);
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
                int  id =ConvertTo.ConvertInt(Request.Params["id"].ToString());

                string number = Request.Params["number"].ToString();

                MakePDF(id, number);


                if (File.Exists(Server.MapPath("pdf/" + number + ".pdf")))
                {

                    int pdf = dal.MakePDF(id,number+".pdf");

                    Response.Write("生成成功！");
                }
                else
                {
                    
                    Response.Write("生成失败！");
                }



                Response.End();
            }

        }

        void GetDataList(string key, DateTime start, DateTime end)
        {
            DataSet ds = dal.GetAllModel(LoginUser.ShopId,key, start, end);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                   
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    
                    
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),

                    isTax = ds.Tables[0].Rows[i]["isTax"].ToString(),
                    isFreight = ds.Tables[0].Rows[i]["isFreight"].ToString(),
                    payWay = ds.Tables[0].Rows[i]["payWay"].ToString(),
                    payDate = ds.Tables[0].Rows[i]["payDate"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString(),
                    freightWay = ds.Tables[0].Rows[i]["freightWay"].ToString(),
                    package = ds.Tables[0].Rows[i]["package"].ToString(),
                    
                   
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    pdfURL = ds.Tables[0].Rows[i]["pdfURL"].ToString(),
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

                //if (!CheckPower("SalesQuoteListDelete"))
                //{
                //    Response.Write("无此操作权限，请联系管理员！");
                //    return;
                //}

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
                            logs.Events = "删除销售报价-ID：" + delId.ToString();
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

                //if (!CheckPower("SalesQuoteListCheck"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

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


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "审核销售报价-ID：" + delId.ToString();
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

                //if (!CheckPower("SalesQuoteListCheckNo"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

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
                            logs.Events = "反审核销售报价-ID：" + delId.ToString();
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


        void MakePDF(int id,string number)
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

        


            string company = "苏州蓝微信息技术有限公司";
            string quoteItem = "";
            string tel = "0512-68709837";
            string phone = "15950056946";
            string fax = "0512-65808297";
            string email = "jekyshi@lanweisoft.com";
            string showLogo = "否";
            string showZhang = "否";



            #region 公司信息、报价条款

            DataSet sysDS = setDAL.GetAllModel();
            if (sysDS.Tables[0].Rows.Count > 0)
            {
                company = sysDS.Tables[0].Rows[0]["company"].ToString();
                quoteItem = sysDS.Tables[0].Rows[0]["quoteItem"].ToString();
                showLogo = sysDS.Tables[0].Rows[0]["showLogo"].ToString();
                showZhang = sysDS.Tables[0].Rows[0]["showZhang"].ToString();
                
            }

            DataSet userDS = userDAL.GetAllUserList(LoginUser.Id);
            if (userDS.Tables[0].Rows.Count > 0)
            {
                
                email = userDS.Tables[0].Rows[0]["email"].ToString();
                tel = userDS.Tables[0].Rows[0]["tel"].ToString();
                phone = userDS.Tables[0].Rows[0]["phone"].ToString();

                fax = userDS.Tables[0].Rows[0]["tel"].ToString();
            }

            if (phone != "")
            {
                tel += " "+phone;
            }

            #endregion

            #region 显示LOGO-----------控制
            if (showLogo == "是")
            {

               

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/logo100.jpg"));

                float x = float.Parse("5");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region 显示印章-----------控制

            if (showZhang == "是")
            {
               

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/zhang.jpg"));


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

            Paragraph quoteEN = new Paragraph("QUOTATION", fontTitle);
            quoteEN.Alignment = 1;
            doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("报 价 单", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            #endregion


            #region 获取表头信息


            int clientId = 0;
            string bizDate = "";
            string payDate = "";
            string payWay = "";
            string isTax = "";
            string isFreight = "";
            string freightWay = "";
            string sendPlace = "";
            string sendDate = "";
            string package = "";
            string deadLine = "";
            string bizName = "蓝微";
            string remarks = "";
           


            string checkName = "";

          //  int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());



            DataSet dsQuote = dal.GetAllModel(id);
            if (dsQuote.Tables[0].Rows.Count > 0)
            {
                clientId = ConvertTo.ConvertInt(dsQuote.Tables[0].Rows[0]["wlId"].ToString());
                number = dsQuote.Tables[0].Rows[0]["number"].ToString();

                bizDate = DateTime.Parse(dsQuote.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();

                payDate = dsQuote.Tables[0].Rows[0]["payDate"].ToString();
                payWay = dsQuote.Tables[0].Rows[0]["payWay"].ToString();


                isTax = dsQuote.Tables[0].Rows[0]["isTax"].ToString();
                isFreight = dsQuote.Tables[0].Rows[0]["IsFreight"].ToString();
                freightWay = dsQuote.Tables[0].Rows[0]["FreightWay"].ToString();
                sendPlace = dsQuote.Tables[0].Rows[0]["sendPlace"].ToString();

                sendDate = dsQuote.Tables[0].Rows[0]["sendDate"].ToString();
                package = dsQuote.Tables[0].Rows[0]["package"].ToString();

                if (dsQuote.Tables[0].Rows[0]["deadLine"].ToString() != "")
                {
                    deadLine = DateTime.Parse(dsQuote.Tables[0].Rows[0]["deadLine"].ToString()).ToShortDateString();
                }

                bizName = dsQuote.Tables[0].Rows[0]["bizName"].ToString();

                checkName = dsQuote.Tables[0].Rows[0]["checkName"].ToString();
                sendDate = dsQuote.Tables[0].Rows[0]["sendDate"].ToString();

                remarks = dsQuote.Tables[0].Rows[0]["remarks"].ToString();

            }


            #endregion

            #region 获取客户信息

            string wlName = "深圳蓝微电子科技有限公司";
            string wlTel = "0512-68709837";
            string wlLinkMan = "";
            string wlPhone = "";
            string wlFax="";
            string wlAddress="";

            DataSet dsClient = venderDAL.GetAllModelView(clientId);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlFax = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlLinkMan = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlAddress = dsClient.Tables[0].Rows[0]["address"].ToString();

            }

            if (wlPhone != "")
            {
                wlTel += " " + wlPhone;
            }

            #endregion


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列

            table.SetTotalWidth(new float[] { 100, 180, 100, 180 });// doc.PageSize.Width;//宽度560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行-----------客户和供应商名称



            cell.Border = 0;
            Paragraph table_t = new Paragraph("客户(Buyer)：", fontTitle10);
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
            table_t = new Paragraph("报价(Saler)：", fontTitle10);
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
            table_t = new Paragraph(wlTel, fontTitle10);
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

            #region 第四行-------------Fax

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("传真(Fax)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlFax,fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("传真(Fax)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(fax, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第五行-------------Add

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("地址(Add)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlAddress, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("日期(Date)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            doc.Add(table);

            //复杂的结束  We have the pleasure to offer you under motioned products and service。

            Paragraph pleasure = new Paragraph("一、报价明细（Items）", font1);
            pleasure.Alignment = 0;
            pleasure.SpacingBefore = 5;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);



            #region 报价明细开始


            PdfPTable tablebj = new PdfPTable(9);//表格有7列-----------项目、品名、规格型号、品牌、最小包装、单位、单价、税率、备注
            tablebj.SetTotalWidth(new float[] { 30, 100, 110, 50, 60, 50, 50,40,70 });//列宽560

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

            table_tbj = new Paragraph("最小包装 MPQ", fontTitle10);
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

            table_tbj = new Paragraph("税率 Tax", fontTitle10);
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

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["brand"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["mpq"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);




                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["unitName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    if (isTax == "含税")
                    {

                        cellbj = new PdfPCell();

                        table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["priceTax"].ToString(), fontTitle10);
                        table_tbj.Alignment = 1;
                        cellbj.AddElement(table_tbj);
                        tablebj.AddCell(cellbj);
                    }
                    else
                    {
                        cellbj = new PdfPCell();

                        table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["price"].ToString(), fontTitle10);
                        table_tbj.Alignment = 1;
                        cellbj.AddElement(table_tbj);
                        tablebj.AddCell(cellbj);
                    }

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["tax"].ToString().Replace(".00", ""), fontTitle10);
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






            //复杂的结束  We have the pleasure to offer you under motioned products and service。

            Paragraph remarksP = new Paragraph("二、其他条款（Terms and Conditions）", font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 5;
            remarksP.SpacingAfter = 5;
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

            Paragraph footRemarks = new Paragraph(quoteItem, font1);
            footRemarks.Alignment = 0;
            doc.Add(footRemarks);

            #endregion



            Paragraph noteP = new Paragraph(" ", fontLabel);//以上报价请确认回签，以便我司存档，谢谢合作！
            noteP.Alignment = 0;
            noteP.SpacingBefore = 20;
            noteP.SpacingAfter = 20;
            doc.Add(noteP);


            #region 落款签名

            PdfPTable tableFooter = new PdfPTable(4);//表格有4列

            tableFooter.SetTotalWidth(new float[] { 65, 200, 65, 200 });// doc.PageSize.Width;//宽度530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//创建单元格

            #region 第一行



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("报  价：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph(bizName, fontTitle10);//客户名称
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("客户签名：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("  ", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);





            #endregion


            #region 第二行

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("批  准：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph(checkName, fontTitle10);
            table_Footer.Alignment = 0;
            cell.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("客户盖章：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("   ", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            #endregion







            doc.Add(tableFooter);


            #endregion


            //5、关闭Document

            doc.Close();

        }


        void MakeSendPDF(int id, string number)
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




            string company = "苏州蓝微信息技术有限公司";
            string quoteItem = "";
            string tel = "0512-68709837";
            string phone = "15950056946";
            string fax = "0512-65808297";
            string email = "jekyshi@lanweisoft.com";
            string showLogo = "否";
            string showZhang = "否";



            #region 公司信息、报价条款

            DataSet sysDS = setDAL.GetAllModel();
            if (sysDS.Tables[0].Rows.Count > 0)
            {
                company = sysDS.Tables[0].Rows[0]["company"].ToString();
                quoteItem = sysDS.Tables[0].Rows[0]["quoteItem"].ToString();
                showLogo = sysDS.Tables[0].Rows[0]["showLogo"].ToString();
                showZhang = sysDS.Tables[0].Rows[0]["showZhang"].ToString();

            }

            DataSet userDS = userDAL.GetAllUserList(LoginUser.Id);
            if (userDS.Tables[0].Rows.Count > 0)
            {

                email = userDS.Tables[0].Rows[0]["email"].ToString();
                tel = userDS.Tables[0].Rows[0]["tel"].ToString();
                phone = userDS.Tables[0].Rows[0]["phone"].ToString();

                fax = userDS.Tables[0].Rows[0]["tel"].ToString();
            }

            if (phone != "")
            {
                tel += " " + phone;
            }

            #endregion

            #region 显示LOGO-----------控制
            if (showLogo == "是")
            {



                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/logo100.jpg"));

                float x = float.Parse("5");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region 显示印章-----------控制

            if (showZhang == "是")
            {


                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/zhang.jpg"));


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

            Paragraph quoteEN = new Paragraph("QUOTATION", fontTitle);
            quoteEN.Alignment = 1;
            doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("报 价 单", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            #endregion


            #region 获取表头信息


            int clientId = 0;
            string bizDate = "";
            string payDate = "";
            string payWay = "";
            string isTax = "";
            string isFreight = "";
            string freightWay = "";
            string sendPlace = "";
            string sendDate = "";
            string package = "";
            string deadLine = "";
            string bizName = "蓝微";
            string remarks = "";



            string checkName = "";

            //  int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());



            DataSet dsQuote = dal.GetAllModel(id);
            if (dsQuote.Tables[0].Rows.Count > 0)
            {
                clientId = ConvertTo.ConvertInt(dsQuote.Tables[0].Rows[0]["wlId"].ToString());
                number = dsQuote.Tables[0].Rows[0]["number"].ToString();

                bizDate = DateTime.Parse(dsQuote.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();

                payDate = dsQuote.Tables[0].Rows[0]["payDate"].ToString();
                payWay = dsQuote.Tables[0].Rows[0]["payWay"].ToString();


                isTax = dsQuote.Tables[0].Rows[0]["isTax"].ToString();
                isFreight = dsQuote.Tables[0].Rows[0]["IsFreight"].ToString();
                freightWay = dsQuote.Tables[0].Rows[0]["FreightWay"].ToString();
                sendPlace = dsQuote.Tables[0].Rows[0]["sendPlace"].ToString();

                sendDate = dsQuote.Tables[0].Rows[0]["sendDate"].ToString();
                package = dsQuote.Tables[0].Rows[0]["package"].ToString();

                if (dsQuote.Tables[0].Rows[0]["deadLine"].ToString() != "")
                {
                    deadLine = DateTime.Parse(dsQuote.Tables[0].Rows[0]["deadLine"].ToString()).ToShortDateString();
                }

                bizName = dsQuote.Tables[0].Rows[0]["bizName"].ToString();

                checkName = dsQuote.Tables[0].Rows[0]["checkName"].ToString();
                sendDate = dsQuote.Tables[0].Rows[0]["sendDate"].ToString();

                remarks = dsQuote.Tables[0].Rows[0]["remarks"].ToString();

            }


            #endregion

            #region 获取客户信息

            string wlName = "深圳蓝微电子科技有限公司";
            string wlTel = "0512-68709837";
            string wlLinkMan = "";
            string wlPhone = "";
            string wlFax = "";
            string wlAddress = "";

            DataSet dsClient = venderDAL.GetAllModelView(clientId);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlFax = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlLinkMan = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlAddress = dsClient.Tables[0].Rows[0]["address"].ToString();

            }

            if (wlPhone != "")
            {
                wlTel += " " + wlPhone;
            }

            #endregion


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列

            table.SetTotalWidth(new float[] { 100, 180, 100, 180 });// doc.PageSize.Width;//宽度560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行-----------客户和供应商名称



            cell.Border = 0;
            Paragraph table_t = new Paragraph("客户(Buyer)：", fontTitle10);
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
            table_t = new Paragraph("报价(Saler)：", fontTitle10);
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
            table_t = new Paragraph(wlTel, fontTitle10);
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

            #region 第四行-------------Fax

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("传真(Fax)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlFax, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("传真(Fax)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(fax, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第五行-------------Add

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("地址(Add)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlAddress, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("日期(Date)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizDate, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            doc.Add(table);

            //复杂的结束  We have the pleasure to offer you under motioned products and service。

            Paragraph pleasure = new Paragraph("一、报价明细（Items）", font1);
            pleasure.Alignment = 0;
            pleasure.SpacingBefore = 5;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);



            #region 报价明细开始


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

            table_tbj = new Paragraph("最小包装 MPQ", fontTitle10);
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

            table_tbj = new Paragraph("税率 Tax", fontTitle10);
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

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["brand"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["mpq"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);




                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["unitName"].ToString(), fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    if (isTax == "含税")
                    {

                        cellbj = new PdfPCell();

                        table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["priceTax"].ToString(), fontTitle10);
                        table_tbj.Alignment = 1;
                        cellbj.AddElement(table_tbj);
                        tablebj.AddCell(cellbj);
                    }
                    else
                    {
                        cellbj = new PdfPCell();

                        table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["price"].ToString(), fontTitle10);
                        table_tbj.Alignment = 1;
                        cellbj.AddElement(table_tbj);
                        tablebj.AddCell(cellbj);
                    }

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["tax"].ToString().Replace(".00", ""), fontTitle10);
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






            //复杂的结束  We have the pleasure to offer you under motioned products and service。

            Paragraph remarksP = new Paragraph("二、其他条款（Terms and Conditions）", font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 5;
            remarksP.SpacingAfter = 5;
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

            Paragraph footRemarks = new Paragraph(quoteItem, font1);
            footRemarks.Alignment = 0;
            doc.Add(footRemarks);

            #endregion



            Paragraph noteP = new Paragraph(" ", fontLabel);//以上报价请确认回签，以便我司存档，谢谢合作！
            noteP.Alignment = 0;
            noteP.SpacingBefore = 20;
            noteP.SpacingAfter = 20;
            doc.Add(noteP);


            #region 落款签名

            PdfPTable tableFooter = new PdfPTable(4);//表格有4列

            tableFooter.SetTotalWidth(new float[] { 65, 200, 65, 200 });// doc.PageSize.Width;//宽度530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//创建单元格

            #region 第一行



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("报  价：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph(bizName, fontTitle10);//客户名称
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("客户签名：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("  ", fontTitle10);
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);





            #endregion


            #region 第二行

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("批  准：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph(checkName, fontTitle10);
            table_Footer.Alignment = 0;
            cell.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("客户盖章：", fontTitle10);
            table_Footer.Alignment = 2;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("   ", fontTitle10);
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
