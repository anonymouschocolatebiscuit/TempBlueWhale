using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.buy
{
    public partial class PurOrderList : BasePage
    {
        public PurOrderDAL dal = new PurOrderDAL();

        public VenderDAL venderDAL = new VenderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.hfShopId.Value = LoginUser.ShopId.ToString();

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

            if (Request.Params["Action"] == "makePDFNoPrice")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                string number = Request.Params["number"].ToString();

                MakePDFNoPrice(id, number);

                if (File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf")))
                {
                    Response.Write("Genereate Successfully !");
                }
                else
                {
                    Response.Write("Genereate Failed !");
                }
                Response.End();
            }
        }

        void GetDataList(string key, DateTime start, DateTime end)
        {
            int shopId = ConvertTo.ConvertInt(Utils.GetCookie("shopId"));
            DataSet ds = dal.GetAllModel(key, start, end, shopId);

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

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                if (!CheckPower("PurOrderListDelete"))
                {
                    Response.Write("You do not have this permission, please contact the administrator");
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
                            logs.Events = "Delete Purchase Order - ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete Successfully" + num + "purchase order record");
                }
                else
                {
                    Response.Write("Delete Failed !");
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
                if (!CheckPower("PurOrderListCheck"))
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

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Review Purchase Order - ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review Successfully" + num + "purchase order record");
                }
                else
                {
                    Response.Write("Review Failed !");
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
                if (!CheckPower("PurOrderListCheckNo"))
                {
                    Response.Write("No Permission For The Action!");
                    return;
                }

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    Response.Write("Cancel Review Successfully");
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "save");

                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Cancel Review Purchase Order - ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Cancel Review Successfully" + num + "purchase order record");
                }
                else
                {
                    Response.Write("Cancel Review Failed !");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again !");
            }
        }

        void MakePDF(int id, string number)
        {
            // Define a Document and set the page size to A4, portrait orientation

            // iTextSharp.text.Document doc = new Document(PageSize.A4);

            // iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation()); // A4 paper in landscape orientation

            // 1. Create an instance
            Document doc = new Document(PageSize.A4);

            // 2. Create a Writer instance for the Document
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf"), FileMode.Create));

            // 3. Open the current Document
            doc.Open();

            // 4. Add content to the current Document
            // 4.1 First, add Chinese fonts
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            // Define font styles
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);
            Font fontLabel = new Font(bfChinese, 12);

            // 4.2 Then, add content
            // Complex content begins
            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksPurOrder;

            string tel = SysInfo.Tel;

            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;

            string showZhang = SysInfo.PrintZhang;

            #region Display LOGO ----------- Control

            if (showLogo == "Yes")
            {
                if (!File.Exists(Server.MapPath("../Sales/pdf/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {
                    Response.Write("Please Set Logo First !");
                    Response.End();
                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("../Sales/img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("15");  // this.TextBox1.Text

                float y = float.Parse("110"); // this.TextBox2.Text

                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);
            }

            #endregion

            #region Display Seal ----------- Control

            if (showZhang == "Yes")
            {
                if (!File.Exists(Server.MapPath("../Sales/img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {
                    Response.Write("Please Set Up Seal First !");
                    Response.End();
                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));

                float x1 = float.Parse("60"); // this.TextBox3.Text
                float y1 = float.Parse("20"); // this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);
            }

            #endregion

            #region Get Table Information

            int clientId = 0;

            string bizDate = "";

            string bizName = "Blue Whale";

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

            #region Tittle ---- Purchase Order --- No

            Paragraph Title = new Paragraph(company, fontTitle);

            Title.Alignment = 1;

            doc.Add(Title);

            Paragraph quoteCH = new Paragraph("Purchase Order", fontTitle);

            quoteCH.Alignment = 1;

            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);

            quoteNumber.Alignment = 1;

            doc.Add(quoteNumber);

            #endregion

            #region Get Customer Information

            string wlName = "Bluw Whale Sdn.Bhd";

            string wlPhone = "012-1595005";

            string wlTel = "07-6827683";

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

            // Define the table

            PdfPTable table = new PdfPTable(4); // The table has 4 columns

            table.SetTotalWidth(new float[] { 90, 190, 90, 190 }); // doc.PageSize.Width; //Width = 560

            table.LockedWidth = true;

            PdfPCell cell = new PdfPCell(); // Create Cell

            #region First Line ----------- Customer And Vender Name

            cell.Border = 0;
            Paragraph table_t = new Paragraph("Vendor：", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10); // Customer Name
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("Buyer：", fontTitle10);
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

            #region Second Line ----------- Contact Name

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Contact(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10); // Customer contact person
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Contact(Attn)：", fontTitle10);
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

            #region Third Line-----------Tel

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Telephone(Tel)：", fontTitle10);
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
            table_t = new Paragraph("Telephone(Tel)：", fontTitle10);
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

            #region Fourth Line ------------- Date

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Order Date：", fontTitle10);
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
            table_t = new Paragraph("Delivery Date：", fontTitle10);
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

            //The complex is over - We have the pleasure to offer you under motioned products and service。

            #region Details

            PdfPTable tablebj = new PdfPTable(7); // Table has 7 columns: Item, Product Name, Specification/Model, Brand, Minimum Package, Unit, Unit Price, Tax Rate, Remarks

            tablebj.SetTotalWidth(new float[] { 30, 170, 110, 60, 60, 60, 70 }); // Width = 560

            tablebj.LockedWidth = true;

            PdfPCell cellbj = new PdfPCell(); // Create Cell

            #region First Line ----- Tittle

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

            table_tbj = new Paragraph("Specification model", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Quantity", fontTitle10);
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

            #endregion

            int rowNum = 0;

            PurOrderItemDAL item = new PurOrderItemDAL();

            DataSet dsItem = item.GetAllModel(id);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                rowNum = dsItem.Tables[0].Rows.Count;

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    #region Second Line ----- Details

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

                    #endregion
                }
            }

            #region Insert, total row

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Total", fontTitle10);
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

            table_tbj = new Paragraph(sumPriceAll, fontTitle10); // SubTotal
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            #endregion

            doc.Add(tablebj);

            #endregion

            // Please confirm and countersign the above quotation for our company's records. Thank you for your cooperation !

            #region End Note

            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);

            // Remark Information

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion

            #region Signature

            PdfPTable tableFooter = new PdfPTable(4); // The table has 4 columns

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 }); // doc.PageSize.Width; // Width = 530

            tableFooter.LockedWidth = true;

            PdfPCell cellFooter = new PdfPCell();// Create Cell

            #region First Line

            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("Vender(Signature and Seal)：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10); // Customer Name
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("Buyer(Signature and Seal)：", fontTitle10);
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

            #region Second Line

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("Date：", fontTitle10);
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
            table_Footer = new Paragraph("Date：", fontTitle10);
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

            //5、Close Document
            doc.Close();
        }

        void MakePDFNoPrice(int id, string number)
        {
            // Define a Document and set the page size to A4, portrait orientation

            // iTextSharp.text.Document doc = new Document(PageSize.A4);

            // iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation()); // A4 paper in landscape orientation

            // 1. Create an instance
            Document doc = new Document(PageSize.A4);

            // 2. Create a Writer instance for the Document
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf"), FileMode.Create));

            // 3. Open the current Document
            doc.Open();

            // 4. Add content to the current Document
            // 4.1 First, add Chinese fonts
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            // Define font styles
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);

            // 4.2 Then, add content
            // Complex content begins
            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksPurOrder;

            string tel = SysInfo.Tel;

            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;

            string showZhang = SysInfo.PrintZhang;

            #region Display LOGO ----------- Control
            if (showLogo == "Yes")
            {
                if (!File.Exists(Server.MapPath("../Sales/img/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {
                    Response.Write("Please Set Logo First !");
                    Response.End();
                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("../Sales/img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("5"); // this.TextBox1.Text

                float y = float.Parse("110"); // this.TextBox2.Text

                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);
            }

            #endregion

            #region Display Seal ----------- Control

            if (showZhang == "Yes")
            {
                if (!File.Exists(Server.MapPath("../Sales/img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {
                    Response.Write("Please Set Up Seal First !");
                    Response.End();
                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));

                float x1 = float.Parse("60"); // this.TextBox3.Text

                float y1 = float.Parse("20"); // this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);
            }

            #endregion

            #region Get Table Information

            int clientId = 0;

            string bizDate = "";

            string bizName = "Blue Whale";

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
            }

            #endregion

            #region Tittle ---- Purchase Order --- No

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            Paragraph quoteCH = new Paragraph("Purchase Order", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);
            quoteNumber.Alignment = 1;
            doc.Add(quoteNumber);

            #endregion

            #region Get Customer Information

            string wlName = "Blue Whale Sdn.Bhd";

            string wlPhone = "012-1595005";

            string wlTel = "07-6827683";

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

            // Define the table
            PdfPTable table = new PdfPTable(4); // The table has 4 columns

            table.SetTotalWidth(new float[] { 100, 180, 100, 180 });// doc.PageSize.Width; //Width = 560

            table.LockedWidth = true;

            PdfPCell cell = new PdfPCell(); // Create cell

            #region First Line ----------- Customer And Vender Name

            cell.Border = 0;
            Paragraph table_t = new Paragraph("Vendor：", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10); // Customer Name
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("Buyer：", fontTitle10);
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

            #region Second Line ----------- Contact Name

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Contact(Attn)：", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10); // Customer contact person
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Contact(Attn)：", fontTitle10);
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

            #region Third Line-----------Tel

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Telephone(Tel)：", fontTitle10);
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
            table_t = new Paragraph("Telephone(Tel)：", fontTitle10);
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

            #region Fourth Line ------------- Date

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Order Date：", fontTitle10);
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
            table_t = new Paragraph("Delivery Date：", fontTitle10);
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

            //The complex is over - We have the pleasure to offer you under motioned products and service.

            #region Details

            PdfPTable tablebj = new PdfPTable(7); // Table has 7 columns: Item, Product Name, Specification/Model, Brand, Minimum Package, Unit, Unit Price, Tax Rate, Remarks

            tablebj.SetTotalWidth(new float[] { 30, 170, 110, 60, 60, 60, 70 }); // Width = 560

            tablebj.LockedWidth = true;

            PdfPCell cellbj = new PdfPCell(); // Create Cell

            #region First Line ----- Tittle

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

            table_tbj = new Paragraph("Specification model", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Unit", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Quantity", fontTitle10);
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

            #endregion

            int rowNum = 0;

            PurOrderItemDAL item = new PurOrderItemDAL();

            DataSet dsItem = item.GetAllModel(id);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                rowNum = dsItem.Tables[0].Rows.Count;

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    #region Second Line ----- Details

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

                    #endregion
                }
            }

            #region Insert, total row

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("Total", fontTitle10);
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

            table_tbj = new Paragraph(sumPriceAll, fontTitle10); // SubTotal
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            #endregion

            doc.Add(tablebj);

            #endregion

            // Please confirm and countersign the above quotation for our company's records. Thank you for your cooperation !

            #region End Note

            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);

            // Remark Information

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion

            #region Signature

            PdfPTable tableFooter = new PdfPTable(4); // The table has 4 columns

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 }); // doc.PageSize.Width; // Width = 530

            tableFooter.LockedWidth = true;

            PdfPCell cellFooter = new PdfPCell();// Create Cell

            #region First Line

            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("Vender(Signature and Seal)：", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10); // Customer Name
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("Buyer(Signature and Seal)：", fontTitle10);
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

            #region Second Line

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("Date：", fontTitle10);
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
            table_Footer = new Paragraph("Date：", fontTitle10);
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

            //5、Close Document
            doc.Close();

        }
    }
}