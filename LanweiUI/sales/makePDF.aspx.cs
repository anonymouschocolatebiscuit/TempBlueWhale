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

using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Lanwei.Weixin.UI.sales
{
    public partial class makePDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            MakeFile();
        }

        public void MakeFile()
        {
            //定义一个Document，并设置页面大小为A4，竖向

            //iTextSharp.text.Document doc = new Document(PageSize.A4);

            //iTextSharp.text.Rectangle rec = new Rectangle(PageSize.A4.Rotation());//A4纸横放

            //1、创建一个实例
            Document doc = new Document(PageSize.A4);

            //2、为该Document创建一个Writer实例
            PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("pdf/" + "shimeng.pdf"), FileMode.Create));

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

            //4.2然后添加内容

            //doc.Add(new Paragraph("Hello 我是市招服务管理平台！",fontChinese));

            //复杂的开始

       

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/logo100.jpg"));

            float x = float.Parse(this.TextBox1.Text);
            float y = float.Parse(this.TextBox2.Text);


            logo.SetAbsolutePosition(x, doc.PageSize.Height - y);

            doc.Add(logo);


            iTextSharp.text.Image zhang = iTextSharp.text.Image.GetInstance(Server.MapPath("pdf/zhang.jpg"));


            float x1 = float.Parse(this.TextBox3.Text);
            float y1 = float.Parse(this.TextBox4.Text);

            zhang.SetAbsolutePosition(doc.PageSize.Width-x1, y1);

            doc.Add(zhang);




            Paragraph Title = new Paragraph("苏州蓝微信息技术有限公司", fontTitle);
            Title.Alignment = 1;
            doc.Add(Title);



            Paragraph address = new Paragraph("江苏省苏州市吴中区东吴南路88号", font1);
            //address.Leading = 10;
            address.Alignment = 1;
            doc.Add(address);

            Paragraph tel = new Paragraph("电话：0512-68709837 传真：0512-65808297 邮箱：jekyshi@lanweisoft.com", font1);
            //tel.Leading = 10;
            tel.Alignment = 1;
            doc.Add(tel);


            Paragraph quote = new Paragraph("报 价 单", fontTitle);
            //quote.Leading = 10;
            quote.Alignment = 1;
            doc.Add(quote);

            //Paragraph number = new Paragraph("编号：XSBJ201506260008   ", font1);
            ////tel.Leading = 10;
            //number.Alignment = 2;
            //doc.Add(number);


            //定义表格

            PdfPTable table = new PdfPTable(4);//表格有4列
            table.SetWidths(new int[] { 100, 250, 100, 250});//列宽
            table.TotalWidth = 700;// doc.PageSize.Width;//宽度450 =225*2 

          
            PdfPCell cell = new PdfPCell();//创建单元格

            #region 第一行



            cell.Border = 0;
            Paragraph table_t = new Paragraph("客  户：", fontTitle10);
            table_t.Alignment = 2;

            cell.AddElement(table_t);
            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = 0;
     
            table_t = new Paragraph("深圳蓝微信息技术有限公司", fontTitle10);
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

            table_t = new Paragraph("0755-888888888", fontTitle10);
            table_t.Alignment = 0;
            cell.AddElement(table_t);
            table.AddCell(cell);

          



            #endregion


            #region 第二行

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("传  真：", fontTitle10);
            table_t.Alignment = 2;
            cell.AddElement(table_t);
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = 0;
            table_t = new Paragraph("0755-66666666", fontTitle10);
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
            table_t = new Paragraph("石R", fontTitle10);
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
            table_t = new Paragraph("2015-06-25", fontTitle10);
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
            table_t = new Paragraph("李R", fontTitle10);
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
            table_t = new Paragraph("XSBJ201506260008", fontTitle10);
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
            Paragraph pleasure = new Paragraph("         一、报价明细", font1);
            pleasure.Alignment = 0;
            pleasure.SpacingAfter = 5;
            doc.Add(pleasure);

          

            #region 报价明细开始

            PdfPTable tablebj = new PdfPTable(7);//表格有7列-----------项目、品名、规格、单位、单价、数量、备注
            tablebj.SetWidths(new int[] { 50, 150, 150,50,75,75, 150 });//列宽
            tablebj.TotalWidth = 700;// doc.PageSize.Width;//宽度450 =225*2 


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

            table_tbj = new Paragraph("规格", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单位", fontTitle10);
            table_tbj.Alignment =1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("单价", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("数量", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("备注", fontTitle10);
            table_tbj.Alignment =1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion


            #region 第二行-----明细


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("进销存系统", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("V3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("套", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("3688", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("免费培训", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion


            #region 第三行-----明细

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("2", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("在线订货系统", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("V3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("套", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("2688", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("试用一个月", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion


            #region 第四行-----明细

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("云POS系统", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("V3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("套", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("3688", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("试用一个月", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion

            #region 第五行-----明细

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("4", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("客户管理系统", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("V3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("套", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("3688", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("试用一个月", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion

            #region 第六行-----明细

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("5", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("微信公众平台", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("V3", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("套", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("3688", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);


            cellbj = new PdfPCell();

            table_tbj = new Paragraph("1", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);

            cellbj = new PdfPCell();

            table_tbj = new Paragraph("试用一个月", fontTitle10);
            table_tbj.Alignment = 1;
            cellbj.AddElement(table_tbj);
            tablebj.AddCell(cellbj);



            #endregion


            #region 第七行-----明细

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



            #endregion


            doc.Add(tablebj);



            #endregion



            //复杂的结束  We have the pleasure to offer you under motioned products and service。
            Paragraph remarks = new Paragraph("         二、其他事项", font1);
            remarks.Alignment = 0;
            remarks.SpacingAfter = 5;
            doc.Add(remarks);

            Paragraph tax = new Paragraph("           1、含税情况：不含税", font1);
            tax.Alignment = 0;
            doc.Add(tax);

            Paragraph send = new Paragraph("           2、运输方式：快递，运费卖方承担", font1);
            send.Alignment = 0;
            doc.Add(send);

            Paragraph sendDate = new Paragraph("           3、交货周期：正常1-2周", font1);
            sendDate.Alignment = 0;
            doc.Add(sendDate);


            //5、关闭Document

            doc.Close();

        }
    }
}
