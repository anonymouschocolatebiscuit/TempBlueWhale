using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.report
{
    public partial class OtherGetPayFlowReport : BasePage
    {
        public AccountDAL dal = new AccountDAL();

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
                this.Bind();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

                string bizType = Request.Params["bizType"].ToString();

                string typeIdString = Request.Params["typeIdString"].ToString();

                string bizId = Request.Params["bizId"].ToString();

                string down = Request.Params["down"].ToString();

                string path = Request.Params["path"].ToString();

                this.GetDataList(bizStart, bizEnd, bizType, typeIdString, bizId, down, path);

                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();
            this.ddlYWYList.Items.Insert(0, new ListItem("ALL", "0"));
            this.ddlYWYList.SelectedValue = "0";
        }

        void GetDataList(DateTime start, DateTime end, string bizType, string typeIdString, string bizId, string down, string path)
        {
            DataTable dt = new DataTable();

            DataSet ds = dal.GetOtherGetPayFlowReport(LoginUser.ShopId, start, end, bizType, typeIdString, bizId);

            dt = ds.Tables[0];

            IList<object> list = new List<object>();

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                decimal priceIn = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceIn"].ToString());

                decimal priceOut = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceOut"].ToString());
                string aaa = "";
                string bbb = "";

                if (priceIn != 0)
                {
                    aaa = priceIn.ToString("0.00");
                }

                if (priceOut != 0)
                {
                    bbb = priceOut.ToString("0.00");
                }

                list.Add(new
                {
                    bkId = ds.Tables[0].Rows[i]["bkId"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    bkName = ds.Tables[0].Rows[i]["bkName"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    bizId = ds.Tables[0].Rows[i]["bizId"].ToString(),
                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    priceIn = aaa,
                    priceOut = bbb,
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    remarksMain = ds.Tables[0].Rows[i]["remarksMain"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);

            if (down == "1")
            {
                string filePath = Server.MapPath("../excel/OtherGetPayFlowReport" + path) + ".xls";
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

                row.CreateCell(0).SetCellValue("Bussiness Date");
                row.CreateCell(1).SetCellValue("Account Number");
                row.CreateCell(2).SetCellValue("Account Name");
                row.CreateCell(3).SetCellValue("Receipt Number");

                row.CreateCell(4).SetCellValue("Bussiness Category");
                row.CreateCell(5).SetCellValue("Business Partner");
                row.CreateCell(6).SetCellValue("Get Pay");
                row.CreateCell(7).SetCellValue("Income");

                row.CreateCell(8).SetCellValue("Expense");
                row.CreateCell(9).SetCellValue("Handle By");
                row.CreateCell(10).SetCellValue("Summary");
                row.CreateCell(11).SetCellValue("Remark");

                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 15 * 256);
                sheet.SetColumnWidth(2, 15 * 256);
                sheet.SetColumnWidth(3, 10 * 256);

                sheet.SetColumnWidth(4, 10 * 256);
                sheet.SetColumnWidth(5, 10 * 256);
                sheet.SetColumnWidth(6, 10 * 256);
                sheet.SetColumnWidth(7, 10 * 256);

                sheet.SetColumnWidth(8, 10 * 256);
                sheet.SetColumnWidth(9, 10 * 256);
                sheet.SetColumnWidth(10, 10 * 256);
                sheet.SetColumnWidth(11, 10 * 256);

                decimal sumPriceIn = 0;
                decimal sumPriceOut = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sumPriceIn += ConvertTo.ConvertDec(dt.Rows[i]["priceIn"].ToString());
                    sumPriceOut += ConvertTo.ConvertDec(dt.Rows[i]["priceOut"].ToString());

                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);

                    row2.CreateCell(0).SetCellValue(Convert.ToString(dt.Rows[i]["bizDate"]));
                    row2.CreateCell(1).SetCellValue(Convert.ToString(dt.Rows[i]["code"]));
                    row2.CreateCell(2).SetCellValue(Convert.ToString(dt.Rows[i]["bkName"]));
                    row2.CreateCell(3).SetCellValue(Convert.ToString(dt.Rows[i]["number"]));
                    row2.CreateCell(4).SetCellValue(Convert.ToString(dt.Rows[i]["bizType"]));
                    row2.CreateCell(5).SetCellValue(Convert.ToString(dt.Rows[i]["wlName"]));
                    row2.CreateCell(6).SetCellValue(Convert.ToString(dt.Rows[i]["typeName"]));
                    row2.CreateCell(7).SetCellValue(Convert.ToString(dt.Rows[i]["priceIn"]));
                    row2.CreateCell(8).SetCellValue(Convert.ToString(dt.Rows[i]["priceOut"]));
                    row2.CreateCell(9).SetCellValue(Convert.ToString(dt.Rows[i]["bizName"]));
                    row2.CreateCell(10).SetCellValue(Convert.ToString(dt.Rows[i]["remarks"]));
                    row2.CreateCell(11).SetCellValue(Convert.ToString(dt.Rows[i]["remarksMain"]));
                    row2.CreateCell(12).SetCellValue(Convert.ToString(dt.Rows[i]["flag"]));
                }
                NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(dt.Rows.Count + 1);
                row3.CreateCell(0).SetCellValue("");
                row3.CreateCell(1).SetCellValue("Total:");
                row3.CreateCell(2).SetCellValue("");
                row3.CreateCell(3).SetCellValue("");
                row3.CreateCell(4).SetCellValue("");
                row3.CreateCell(7).SetCellValue(sumPriceIn.ToString("0.00"));
                row3.CreateCell(8).SetCellValue(sumPriceOut.ToString("0.00"));

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
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                catch (Exception) { }

                #endregion
            }
        }
    }
}
