using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Paragraph = iTextSharp.text.Paragraph;
using System.Windows.Documents;

namespace BlueWhale.UI.sales
{
    public partial class SalesOrderList : BasePage
    {
        public SalesOrderDAL dal = new SalesOrderDAL();
        public ClientDAL venderDAL = new ClientDAL();

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

                    Response.Write("Generate Successfully!");

                }
                else
                {

                    Response.Write("Generate Failed!");
                }
                Response.End();
            }

            if (Request.Params["Action"] == "makePDFNoPrice")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                string number = Request.Params["number"].ToString();



                /*                MakePDFNoPrice(id, number);*/

                if (File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf")))
                {

                    Response.Write("Generate Successfully!");

                }
                else
                {

                    Response.Write("Generate Failed!");
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


                string flag = ds.Tables[0].Rows[i]["flag"].ToString();

                //if(flag == "Pending")
                //{
                //    flag = "<font color='red'>Pending</font>";
                //}
                //else
                //{
                //    flag = "<font color='green'>Reviewed</font>";
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

            string s = new JavaScriptSerializer().Serialize(griddata);//Only need it when passing to the grid



            Response.Write(s);
        }


        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListDelete"))
                {
                    Response.Write("No permission for this operation, please contact the administrator!");
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
                            logs.Events = "Delete sale orders-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("Successfully deleted" + num + "records!");

                }
                else
                {
                    Response.Write("Delete failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListCheck"))
                {
                    Response.Write("No permission for this operation!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "review");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "review sales order-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("Successfully review" + num + "records!");

                }
                else
                {
                    Response.Write("Review failed!");
                }




            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }

        }


        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesOrderListCheckNo"))
                {
                    Response.Write("No permission for this operation!");

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


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Cancel review sales order-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("Successfully cancel review" + num + "records!");

                }
                else
                {
                    Response.Write("Cancel review failed!");
                }




            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }


        }


        void MakePDF(int id, string number)
        {
            //Define a Document and set the page size to A4 in portrait orientation

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4landscape

            //1、Create document
            Document doc = new Document(PageSize.A4);

            //2、Create a Writer instance for this Document
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + ".pdf"), FileMode.Create));

            //3、Open Document

            doc.Open();

            //4、Add content to current document

            //4.1Add Fony
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            //Font
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);


            //4.2 Content



            //Beginning of nightmare




            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksSalesOrder;




            string tel = SysInfo.Tel;
            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;
            string showZhang = SysInfo.PrintZhang;

            #region Display LOGO ----------- Control
            if (showLogo == "yes")
            {

                if (!File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {
                    Response.Write("Please set up the Logo first.");
                    Response.End();
                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("15");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region Display seal - Control

            if (showZhang == "yes")
            {

                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {


                    Response.Write("Please set up the seal first!");
                    Response.End();

                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            #endregion

            #region Get header information


            int clientId = 0;
            string bizDate = "";



            string bizName = "BlueWhale";

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

            #region Title - Sales Order - Number

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("Sales Order", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);
            quoteNumber.Alignment = 1;
            doc.Add(quoteNumber);

            #endregion




            #region Get customer information

            string wlName = "电子科技有限公司";
            string wlPhone = "1056946";
            string wlTel = "03-68276837";
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


            //Define table

            PdfPTable table = new PdfPTable(4);//The table has 4 columns

            table.SetTotalWidth(new float[] { 90, 190, 90, 190 });// doc.PageSize.Width;//Width560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//Create Cell

            #region First row - Client and vender name



            cell.Border = 0;
            Paragraph table_t = new Paragraph("Buyer: ", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10);//Client Name
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("Supplier: ", fontTitle10);
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


            #region Second row - Contact person's name

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Attn: ", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10);//Client contact person
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Attn: ", fontTitle10);
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


            #region Third row - Tel

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Tel: ", fontTitle10);
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
            table_t = new Paragraph("Tel: ", fontTitle10);
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


            #region Forth row - Date

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("PurchaseDate: ", fontTitle10);
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
            table_t = new Paragraph("DeliveryDate: ", fontTitle10);
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

            //End of the nightmare  We have the pleasure to offer you under motioned products and service。


            //string titleRowOne = "No:" + number + "  Supplier:" + wlName + "  Purchase Date:" + bizDate + " Delivery Date: " + sendDate;

            //Paragraph pleasure = new Paragraph(titleRowOne, font1);
            //pleasure.Alignment = 0;
            //pleasure.SpacingBefore = 5;
            //pleasure.SpacingAfter = 5;
            //doc.Add(pleasure);



            #region Start of the details


            PdfPTable tablebj = new PdfPTable(7);//The table has 7 columns - Item, Product Name, Specifications/Model, Brand, Minimum Packaging, Unit, Unit Price, Tax Rate, Remarks
            tablebj.SetTotalWidth(new float[] { 30, 170, 110, 60, 60, 60, 70 });//Width 560

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//Create cell

            #region First row - Title

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


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("Brand", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);

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



            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("Remarks", fontTitle10);
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
                    #region Second row - Details


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

            #region Insert, Total Row

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

            table_tbj = new Paragraph(sumPriceAll, fontTitle10);//Subtotal
            table_tbj.Alignment = 2;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            //cellbj = new PdfPCell();
            //table_tbj = new Paragraph("", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);


            #endregion

            #region Fill in the blank rows

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



            //Please confirm and sign the above quotation for our company's record. Thank you for your cooperation!

            #region Footer


            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);




            //Remark

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion



            //Paragraph noteP = new Paragraph(footerTitle, fontLabel);//Please confirm and sign the above quotation for our company’s record. Thank you for your cooperation!
            //noteP.Alignment = 0;
            //noteP.SpacingBefore = 20;
            //noteP.SpacingAfter = 20;
            //doc.Add(noteP);


            #region Signature

            PdfPTable tableFooter = new PdfPTable(4);//The table has 4 columns

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 });// doc.PageSize.Width;//Width 530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//Create cell

            #region First row



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("Buyer (Signature and Seal): ", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);//Client name
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("Supplier (Signature and Seal): ", fontTitle10);
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


            #region Second row

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("Date: ", fontTitle10);
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
            table_Footer = new Paragraph("Date: ", fontTitle10);
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
            //Define a Document and set the page size to A4, portrait

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4 landscape

            //1、Create an instance
            Document doc = new Document(PageSize.A4);

            //2、Create a Writer instance for this Document
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "-" + number + "-01.pdf"), FileMode.Create));

            //3、Open Current Document

            doc.Open();

            //4、Add content for current document

            //4.1Add font
            BaseFont bfChinese = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fontChinese = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(0, 0, 0));

            //Font
            Font fontTitle = new Font(bfChinese, 20);
            Font font1 = new Font(bfChinese, 12);
            Font fontTitle10 = new Font(bfChinese, 12);

            Font fontLabel = new Font(bfChinese, 12);


            //4.2 Content



            // The nightmare begin




            string company = SysInfo.Company;

            string remarks = SysInfo.RemarksSalesOrder;




            string tel = SysInfo.Tel;
            string fax = SysInfo.Fax;

            string showLogo = SysInfo.PrintLogo;
            string showZhang = SysInfo.PrintZhang;

            #region Display LOGO - Control
            if (showLogo == "yes")
            {

                if (!File.Exists(Server.MapPath("pdf/" + LoginUser.ShopId.ToString() + "logo.jpg")))
                {


                    Response.Write("Please set the logo first!");
                    Response.End();

                }

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "logo.jpg"));

                float x = float.Parse("15");//this.TextBox1.Text
                float y = float.Parse("110");//this.TextBox2.Text


                logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

                doc.Add(logo);


            }

            #endregion

            #region Display seal - Control

            if (showZhang == "yes")
            {

                if (!File.Exists(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg")))
                {


                    Response.Write("Please set the seal first!");
                    Response.End();

                }

                iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("img/" + LoginUser.ShopId.ToString() + "zhang.jpg"));


                float x1 = float.Parse("60");//this.TextBox3.Text
                float y1 = float.Parse("20");//this.TextBox4.Text

                zhang.SetAbsolutePosition(x1, y1);

                doc.Add(zhang);



            }

            #endregion

            #region Retrieve header information


            int clientId = 0;
            string bizDate = "";



            string bizName = "BlueWhale";

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

            #region Title - Sales Order - Number

            Paragraph Title = new Paragraph(company, fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);

            //Paragraph quoteEN = new Paragraph("SalesList", fontTitle);
            //quoteEN.Alignment = 1;
            //doc.Add(quoteEN);

            Paragraph quoteCH = new Paragraph("Sales order", fontTitle);
            quoteCH.Alignment = 1;
            doc.Add(quoteCH);

            Paragraph quoteNumber = new Paragraph(number, fontTitle10);
            quoteNumber.Alignment = 1;
            doc.Add(quoteNumber);

            #endregion




            #region Retrieve customer information

            string wlName = "电子科技有限公司";
            string wlPhone = "0056946";
            string wlTel = "03-68276837";
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


            //Define table

            PdfPTable table = new PdfPTable(4);//The table has 4 columns

            table.SetTotalWidth(new float[] { 90, 190, 90, 190 });// doc.PageSize.Width;//Width 560

            table.LockedWidth = true;


            PdfPCell cell = new PdfPCell();//Create cell

            #region First row - Client and Supplier Name



            cell.Border = 0;
            Paragraph table_t = new Paragraph("Buyer: ", fontTitle10);
            table_t.Alignment = 0;

            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph(wlName, fontTitle10);//Client name
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;

            table_t = new Paragraph("Supplier: ", fontTitle10);
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

            #region Second row - Contact person's name


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Attn: ", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph(wlLinkMan, fontTitle10);//Client contact person
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Attn: ", fontTitle10);
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

            #region Third row - Tel


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Tel: ", fontTitle10);
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
            table_t = new Paragraph("Tel: ", fontTitle10);
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

            #region Fourth row - Date"


            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("Order Date: ", fontTitle10);
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
            table_t = new Paragraph("Delivery Date: ", fontTitle10);
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

            //We have the pleasure to offer you under motioned products and service。


            //string titleRowOne = "编号:" + number + "  供方:" + wlName + "  采购日期:" + bizDate + " 交货日期: " + sendDate;

            //Paragraph pleasure = new Paragraph(titleRowOne, font1);
            //pleasure.Alignment = 0;
            //pleasure.SpacingBefore = 5;
            //pleasure.SpacingAfter = 5;
            //doc.Add(pleasure);



            #region Start of details


            PdfPTable tablebj = new PdfPTable(5);//The table has 5 columns
            tablebj.SetTotalWidth(new float[] { 60, 270, 110, 60, 60 });//Column width 560  130

            tablebj.LockedWidth = true;


            PdfPCell cellbj = new PdfPCell();//Create cell

            #region First row ----- Title

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


            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("Brand", fontTitle10);
            //table_tbj.Alignment = 1;
            //cellbj.AddElement(table_tbj);
            //tablebj.AddCell(cellbj);

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





            //cellbj = new PdfPCell();

            //table_tbj = new Paragraph("Remarks", fontTitle10);
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
                    #region Second Row - Details


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

            #region Insert, Total row

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



            //Please confirm and sign the above quotation for our records. Thank you for your cooperation!

            #region Footer remarks


            Paragraph remarksP = new Paragraph(remarksOrder, font1);
            remarksP.Alignment = 0;
            remarksP.SpacingBefore = 0;
            remarksP.SpacingAfter = 0;
            doc.Add(remarksP);



           
            //Remark

            Paragraph footRemarks = new Paragraph(remarks, font1);
            footRemarks.Alignment = 0;
            footRemarks.SpacingBefore = 0;
            footRemarks.SpacingAfter = 20;
            doc.Add(footRemarks);

            #endregion



            //Paragraph noteP = new Paragraph(footerTitle, fontLabel); //Please confirm and sign the above quotation for our records. Thank you for your cooperation!
            //noteP.Alignment = 0;
            //noteP.SpacingBefore = 20;
            //noteP.SpacingAfter = 20;
            //doc.Add(noteP);


            #region Signature

            PdfPTable tableFooter = new PdfPTable(4);//The table has 4 columns

            tableFooter.SetTotalWidth(new float[] { 120, 140, 120, 140 });// doc.PageSize.Width;//Wdith 530

            tableFooter.LockedWidth = true;



            PdfPCell cellFooter = new PdfPCell();//Create cell

            #region First row



            cellFooter.Border = 0;
            Paragraph table_Footer = new Paragraph("Buyer(Signature and Seal):", fontTitle10);
            table_Footer.Alignment = 2;

            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("_______________", fontTitle10);//CustomerName
            table_Footer.Alignment = 0;
            cellFooter.AddElement(table_Footer);
            tableFooter.AddCell(cellFooter);


            cellFooter = new PdfPCell();
            cellFooter.Border = 0;

            table_Footer = new Paragraph("Supplier(Signature and Seal): ", fontTitle10);
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


            #region Second Row

            cellFooter = new PdfPCell();
            cellFooter.Border = 0;
            table_Footer = new Paragraph("Date: ", fontTitle10);
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
            table_Footer = new Paragraph("Date: ", fontTitle10);
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
