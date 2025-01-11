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
    public partial class SalesQuoteListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();

        public SalesQuoteDAL dal = new SalesQuoteDAL();

        public SalesQuoteItemDAL item = new SalesQuoteItemDAL();

        public UserDAL userDAL = new UserDAL();

        //public SystemSetDAL sysDAL = new SystemSetDAL();

        public string fromId = "0";



        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesQuoteListEdit"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();

                this.txtDeadLine.Text = DateTime.Now.AddMonths(1).ToShortDateString();
               

                this.Bind();

                this.BindInfo();

            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);
                Response.End();
            }
        }

      

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

            this.ddlBizId.DataSource = userDAL.GetList(isWhere);
            this.ddlBizId.DataTextField = "names";
            this.ddlBizId.DataValueField = "id";
            this.ddlBizId.DataBind();

            this.ddlBizId.SelectedValue = LoginUser.Id.ToString();

        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hfNumber.Value = ds.Tables[0].Rows[0]["number"].ToString();

                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.ddlPayDateList.SelectedValue = ds.Tables[0].Rows[0]["payDate"].ToString();
                this.ddlPayWayList.SelectedValue = ds.Tables[0].Rows[0]["payWay"].ToString();


                this.ddlIsTaxList.SelectedValue = ds.Tables[0].Rows[0]["isTax"].ToString();
                this.ddlIsFreight.SelectedValue = ds.Tables[0].Rows[0]["IsFreight"].ToString();
                this.ddlFreightWayList.SelectedValue = ds.Tables[0].Rows[0]["FreightWay"].ToString();
                this.txtSendPlace.Text = ds.Tables[0].Rows[0]["sendPlace"].ToString();

                this.txtSendDate.Text = ds.Tables[0].Rows[0]["sendDate"].ToString();
                this.ddlPackageList.SelectedValue = ds.Tables[0].Rows[0]["package"].ToString();
                if (ds.Tables[0].Rows[0]["deadLine"].ToString() != "")
                {
                    this.txtDeadLine.Text = DateTime.Parse(ds.Tables[0].Rows[0]["deadLine"].ToString()).ToShortDateString();
                }
                this.ddlBizId.SelectedValue = ds.Tables[0].Rows[0]["bizId"].ToString();


                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
             
                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "审核")
                {
                    this.isCheck.Visible = true;
                    this.btnSave.Visible = false;
                }
                else
                {
                    this.isCheck.Visible = false;
                }

                

            }


        }



        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            DataSet ds = item.GetAllModel(id);

            int rows = ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                   
                    brandName = ds.Tables[0].Rows[i]["brandName"].ToString(),
                    mpq = ds.Tables[0].Rows[i]["mpq"].ToString(),
                    packages = ds.Tables[0].Rows[i]["packages"].ToString(),
                   
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
            
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),

                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),

                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            if (rows < 8)//少于8行
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        spec="",
                       
                        unitName = "",
                        brandName = "",
                        mpq = "",
                        packages = "",
                        num = "",
                     
                        price = "",
                        sumPrice = "",
                        tax = "",
                        priceTax = "",
                        sumPriceTax = "",
                        sumPriceAll = "",
                        remarks = ""
                    });
                }
            }

            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }


       

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            this.MakeFile();

            if (File.Exists(Server.MapPath("pdf/" + this.hfNumber.Value + ".pdf")))
            {
                MessageBox.ShowAndRedirect(this, "生成成功！", "pdf/" + this.hfNumber.Value + ".pdf");
            }
            else
            {
                MessageBox.Show(this,"生成失败！");
            }
        }


        public void MakeFile()
        {
            //定义一个Document，并设置页面大小为A4，竖向

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4纸横放

            //1、创建一个实例
            Document doc = new Document(PageSize.A4);

            //2、为该Document创建一个Writer实例
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + this.hfNumber.Value+".pdf"), FileMode.Create));

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

            Font fontLabel = new Font(bfChinese,12);
        

            //4.2然后添加内容

        

            //复杂的开始



            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/logo100.jpg"));

            float x = float.Parse("5");//this.TextBox1.Text
            float y = float.Parse("110");//this.TextBox2.Text


            logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

            doc.Add(logo);


            iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/zhang.jpg"));


            float x1 = float.Parse("60");//this.TextBox3.Text
            float y1 = float.Parse("20");//this.TextBox4.Text

            zhang.SetAbsolutePosition( x1, y1);

            doc.Add(zhang);

            string company = "苏州蓝微信息技术有限公司";
            string address = "江苏省苏州市吴中区东吴南路88号";
            string tel = "0512-68709837";
            string fax = "0512-65808297";
            string email = "jekyshi@lanweisoft.com";

            string isWhere = " shopId='"+LoginUser.ShopId+"' ";
            DataSet sysDS = setDAL.GetList(isWhere);
            if (sysDS.Tables[0].Rows.Count > 0)
            {
                company = sysDS.Tables[0].Rows[0]["company"].ToString();
                address = sysDS.Tables[0].Rows[0]["address"].ToString();
                tel = sysDS.Tables[0].Rows[0]["tel"].ToString();
                fax = sysDS.Tables[0].Rows[0]["fax"].ToString();
            }

            DataSet userDS = userDAL.GetAllUserList(LoginUser.Id);
            if (userDS.Tables[0].Rows.Count > 0)
            {
                email = userDS.Tables[0].Rows[0]["email"].ToString();
            }



            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);



            Paragraph addressP = new Paragraph(address, font1);
            //address.Leading = 10;
            addressP.Alignment = 1;
            doc.Add(addressP);

            string linkWay = "电话：" + tel + " 传真：" + fax + " 邮箱：" + email;

            Paragraph telP = new Paragraph(linkWay, font1);
            //tel.Leading = 10;
            telP.Alignment = 1;
            doc.Add(telP);


            Paragraph quote = new Paragraph("报 价 单", fontTitle);
            //quote.Leading = 10;
            quote.Alignment = 1;
            doc.Add(quote);

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
            string bizName = "";
            string remarks = "";
            string number = "";

          
            string checkName = "";

            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());



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

            DataSet dsClient = venderDAL.GetAllModelView(clientId);
            if (dsClient.Tables[0].Rows.Count > 0)
            {
                wlName = dsClient.Tables[0].Rows[0]["names"].ToString();
                wlTel = dsClient.Tables[0].Rows[0]["tel"].ToString();
                wlLinkMan = dsClient.Tables[0].Rows[0]["linkMan"].ToString();
                wlPhone = dsClient.Tables[0].Rows[0]["phone"].ToString();
 
            }


            #endregion


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列
          
            table.SetTotalWidth(new float[] { 65, 200, 65, 200 });// doc.PageSize.Width;//宽度530

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行



            cell.Border = 0;
            Paragraph table_t = new Paragraph("客  户：", fontTitle10);
            table_t.Alignment = 2;

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

            table_t = new Paragraph("电  话：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlTel, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);





            #endregion


            #region 第二行

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("手  机：", fontTitle10);
            table_t.Alignment = 2;
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
            table_t = new Paragraph("联系人：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            #region 第三行

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("日  期：", fontTitle10);
            table_t.Alignment = 2;
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
            table_t = new Paragraph("报价人：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(bizName, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion

            #region 第四行

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("编  号：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(number, fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("币  别：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("人民币￥", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            #endregion


            doc.Add(table);

            //复杂的结束  We have the pleasure to offer you under motioned products and service。
            Paragraph pleasure = new Paragraph("一、报价明细", font1);
            pleasure.Alignment = 0;
            pleasure.SpacingBefore = 5;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);



            #region 报价明细开始


            PdfPTable tablebj = new PdfPTable(8);//表格有7列-----------项目、品名、规格型号、品牌、最小包装、单位、单价、备注
            tablebj.SetTotalWidth(new float[] { 30, 100, 120, 50, 50, 50, 50, 80 });//列宽600
          
            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//创建单元格

            #region 第一行-----标题

            Paragraph table_tbj = new Paragraph("序号", fontTitle10);
            table_tbj.Alignment = 1;

            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("商品名称", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("规格型号", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("品牌", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("最小包装", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单位", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单价", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("数量", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("备注", fontTitle10);
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

                    table_tbj = new Paragraph((i+1).ToString(), fontTitle10);
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

                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(dsItem.Tables[0].Rows[i]["num"].ToString().Replace("0.00",""), fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);

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

                //cellbj = new PdfPCell();

                //table_tbj = new Paragraph("", fontTitle10);
                //table_tbj.Alignment = 1;
                //cellbj.AddElement(table_tbj);
                //tablebj.AddCell(cellbj);

                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                #endregion

            }

            #region 填充空白行

            if ((rowNum+1) < 8)
            {
                int rowBlank = 10 - rowNum - 1;
                for (int j = 0; j < rowBlank; j++)
                {
                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);


                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);

                    //cellbj = new PdfPCell();

                    //table_tbj = new Paragraph(" ", fontTitle10);
                    //table_tbj.Alignment = 1;
                    //cellbj.AddElement(table_tbj);
                    //tablebj.AddCell(cellbj);

                    cellbj = new PdfPCell();

                    table_tbj = new Paragraph(" ", fontTitle10);
                    table_tbj.Alignment = 1;
                    cellbj.AddElement(table_tbj);
                    tablebj.AddCell(cellbj);
                }
            }


            #endregion





            doc.Add(tablebj);



            #endregion



            //以上报价请确认回签，以便我司存档，谢谢合作！

            #region 尾部备注


           



            //复杂的结束  We have the pleasure to offer you under motioned products and service。
            Paragraph remarksP = new Paragraph("二、其他事项", font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 5;
            remarksP.SpacingAfter = 5;
            doc.Add(remarksP);

            Paragraph tax = new Paragraph("    1、含税情况："+isTax+"，付款时间："+payDate+"，付款方式："+payWay, font1);
            tax.Alignment = 0;
            doc.Add(tax);

            Paragraph send = new Paragraph("    2、运输方式：" + freightWay + "，运费承担：" + isFreight+"，包装方式："+package, font1);
            send.Alignment = 0;
            doc.Add(send);

            Paragraph sendDateP = new Paragraph("    3、交货周期：" + sendDate + "，交货地点：" + sendPlace + "", font1);
            sendDateP.Alignment = 0;
            doc.Add(sendDateP);


            Paragraph remarksFoot = new Paragraph("三、备注信息", font1);
            remarksFoot.Alignment = 0;
            remarksFoot.SpacingAfter = 5;
            doc.Add(remarksFoot);


            Paragraph footRemarks = new Paragraph("    "+remarks, font1);
            footRemarks.Alignment = 0;
            doc.Add(footRemarks);

            #endregion



            Paragraph noteP = new Paragraph("以上报价请确认回签，以便我司存档，谢谢合作！", fontLabel);
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
