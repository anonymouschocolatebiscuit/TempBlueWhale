using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.BaseSet;
using BlueWhale.UI.src;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;


namespace BlueWhale.UI.sales
{
    public partial class SalesReceiptList : BasePage
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public ClientDAL venderDAL = new ClientDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hfShopId.Value = LoginUser.ShopId.ToString();

            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesReceiptList"))
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

                GetDataList(keys, start, end, types);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int types = ConvertTo.ConvertInt(Request.Params["types"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end, types);
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
                    Response.Write("Generate Successfully !");
                }
                else
                {

                    Response.Write("Genereate Failed !");
                }
                Response.End();
            }

        }

        void GetDataList(string key, DateTime start, DateTime end, int types)
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

            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesReceiptListDelete"))
                {
                    Response.Write("You do not have this permission, please contact the administrator!");
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
                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Delete Sales Receipt-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete Success" + num + "records!");
                }
                else
                {
                    Response.Write("Delete Fail !");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again !");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesReceiptListCheck"))
                {
                    Response.Write("No Permission For The Action!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Review");
                        if (del > 0)
                        {
                            num += 1;
                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Review Sales Receipt-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review Sucess");

                }
                else
                {
                    Response.Write("Review Fail");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again !");
            }
        }

        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesReceiptListCheckNo"))
                {
                    Response.Write("No Permission For The Action!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Save");
                        if (del > 0)
                        {
                            num += 1;
                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Reject Sales Receipt-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Reject Sucess");

                }
                else
                {
                    Response.Write("Reject Fail");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again !");
            }
        }

        void MakePDF(int id, string number)
        {
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf"), FileMode.Create));

            doc.Open();

            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);

            string company = SysInfo.Company;

            string signTitle = "Delivery Unit (Signature and Seal): __________     Receiving Unit (Signature and Seal): ________";

            string footerTitle = "White Copy: Sales Department        Red Copy: Client       Blue Copy: Finance       Yellow Copy: General Manager"; ;


            string tel = SysInfo.Tel;
            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;
            string showZhang = SysInfo.PrintZhang;

            if (showLogo == "Yes")
            {


                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {


                    Response.Write("Please set the logo first!");
                    Response.End();

                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("5");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            if (showZhang == "Yes")
            {

                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {


                    Response.Write("Please set the logo first!");
                    Response.End();

                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("Sales Invoice", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            int clientId = 0;
            string bizDate = "";



            string bizName = "BlueWhale";
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

            string wlName = "Blue Whale Sdn.Bhd";

            string wlPhone = "012-1595005";

            string wlTel = "07-6827683";

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

            string titleRowOne = "ID: " + number + "   Client: " + wlName + "   Date: " + DateTime.Now.ToString("yyyy-MM-dd");

            Paragraph pleasure = new Paragraph(titleRowOne, font1);
            pleasure.Alignment = 0;
            pleasure.SpacingBefore = 5;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);

            PdfPTable tablebj = new PdfPTable(9);
            tablebj.SetTotalWidth(new float[] { 30, 100, 110, 50, 60, 50, 50, 40, 70 });

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();

            Paragraph table_tbj = new Paragraph("NO", fontTitle10);
            table_tbj.Alignment = 1;

            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Goods Name", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("SPECIFICATION", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Brand", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Quantity", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Price", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Total", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Remarks", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            int rowNum = 0;

            SalesReceiptItemDAL item = new SalesReceiptItemDAL();

            DataSet dsItem = item.GetAllModel(id);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                rowNum = dsItem.Tables[0].Rows.Count;

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
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
                }
            }

            if (rowNum < 9)
            {
                cellbj = new PdfPCell();

                table_tbj = new Paragraph("", fontTitle10);
                table_tbj.Alignment = 1;
                cellbj.AddElement(table_tbj);
                tablebj.AddCell(cellbj);


                cellbj = new PdfPCell();

                table_tbj = new Paragraph("The following is blank", fontTitle10);
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
            }

            doc.Add(tablebj);

            string sumPriceAllLast = (ConvertTo.ConvertDec(sumPriceAll) - ConvertTo.ConvertDec(disPrice)).ToString("0.00");

            string disPriceTitle = "Note: Retained warranty deposit " + dis + "%, total " + disPrice + " Remaining payment due: " + sumPriceAllLast;

            Paragraph remarksP = new Paragraph(disPriceTitle, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 10;
            remarksP.SpacingAfter = 10;
            doc.Add(remarksP);

            Paragraph footRemarks = new Paragraph(signTitle, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 20;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            Paragraph noteP = new Paragraph(footerTitle, fontLabel);
            noteP.Alignment = 0;
            noteP.SpacingBefore = 20;
            noteP.SpacingAfter = 20;
            doc.Add(noteP);

            doc.Close();

        }
    }
}