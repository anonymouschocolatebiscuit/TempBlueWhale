using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Data;
using System.Data.OleDb;

namespace BlueWhaleUI.baseSet
{
    public partial class VenderListExcel : BasePage
    {
        public VenderDAL dal = new VenderDAL();
        public VenderTypeDAL typeDAL = new VenderTypeDAL();
        public VenderLinkManDAL linkDAL = new VenderLinkManDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("VenderListExcel"))
                {
                    Response.Redirect("../OverPower.htm");
                }
            }
        }

        /// <summary>
        /// Download Import Template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDownExcel_Click(object sender, EventArgs e)
        {
            string path = "excel/vendor_excel.xls";

            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            catch (Exception ex) { }
        }

        protected void lbtnDownExample_Click(object sender, EventArgs e)
        {
            string path = "excel/vendor_excel_example.xls";

            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            catch (Exception ex) { }
        }

        protected void btnExcelTo_Click(object sender, EventArgs e)
        {
            if (!CheckPower("VenderListExcel"))
            {
                MessageBox.Show(this, "No permission for this action! ");
                return;
            }

            if (fload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fload.FileName);
                if (fileExt == ".xls" || fileExt == ".xlsx")
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt;
                    string oPath = Server.MapPath("excel/" + fileName);

                    try
                    {
                        fload.PostedFile.SaveAs(oPath);

                        DataSet ds = this.ExcelDataSource(fileName);
                        DataTable dt = ds.Tables[0];

                        if (dt != null && dt.Rows.Count > 0)
                        {
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

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (DBNull.Value == dt.Rows[i][0] || dt.Rows[0][0].ToString() == "")
                                {
                                    dt.Rows.Remove(dt.Rows[0]);
                                }
                            }
                            dt.AcceptChanges();
                        }


                        System.IO.File.Delete(oPath);

                        this.Label1.Text = this.DataTableToSql(dt);

                        this.Label1.Visible = true;


                        LogsDAL logs = new LogsDAL();
                        logs.ShopId = LoginUser.ShopId;
                        logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                        logs.Events = "Import vender: " + this.Label1.Text;
                        logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        logs.Add();
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
                    this.Label1.Visible = true;


                }
            }
            else
            {
                MessageBox.Show(this, "Please choose an Excel file to import");
                return;
            }
        }

        public DataSet ExcelDataSource(string fileName)
        {
            string oPath = Server.MapPath("excel/" + fileName);
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + oPath + ";Extended Properties=Excel 8.0;";

            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [Vender$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            conn.Close();
            return ds;
        }

        public string DataTableToSql(DataTable dt)
        {
            int VenderId = 0;
            int VenderNum = 0;
            int linkManNum = 0;

            #region Loop DataTable

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["Vender Number"].ToString();
                string names = dt.Rows[i]["Vender Name"].ToString();
                string typeName = dt.Rows[i]["Vender Category"].ToString();
                int tax = ConvertTo.ConvertInt(dt.Rows[i]["Tax Rate"].ToString());
                DateTime dueDate = DateTime.Now;

                if (dt.Rows[i][3].ToString().Trim() != "")
                {
                    dueDate = Convert.ToDateTime(dt.Rows[i]["Balance Date"].ToString());
                }

                decimal payNeed = ConvertTo.ConvertDec(dt.Rows[i]["Beginning Payable Amount"].ToString());
                decimal payReady = ConvertTo.ConvertDec(dt.Rows[i]["Beginning Prepayment Amount"].ToString());
                string taxNumber = dt.Rows[i]["Tax Number"].ToString();
                string bankName = dt.Rows[i]["Opening Bank"].ToString();
                string bankNumber = dt.Rows[i]["Bank Account Number"].ToString();
                string address = dt.Rows[i]["Address"].ToString();

                string remarks = dt.Rows[i]["Remark"].ToString();

                string linkManName = dt.Rows[i]["Contact"].ToString();
                string phone = dt.Rows[i]["Mobile Phone"].ToString();
                string tel = dt.Rows[i]["Landline Phone"].ToString();

                int defaults = 0;
                if (dt.Rows[i]["Primary Contact"].ToString() == "Yes")
                {
                    defaults = 1;
                }

                int typeId = 0;
                bool hasCode = false;
                bool hasNames = false;

                hasCode = dal.isExistsCodeAdd(LoginUser.ShopId, code);
                hasNames = dal.isExistsNamesAdd(LoginUser.ShopId, names);

                if (code != "" && names != "")
                {
                    #region If isFirstRow

                    if (!(hasCode || hasNames))
                    {
                        typeId = typeDAL.GetIdByName(LoginUser.ShopId, typeName);

                        dal.ShopId = LoginUser.ShopId;
                        dal.Code = code;
                        dal.Names = names;
                        dal.Tax = tax;
                        dal.TypeId = typeId;
                        dal.DueDate = dueDate;
                        dal.PayNeed = payNeed;
                        dal.PayReady = payReady;
                        dal.Remarks = remarks;
                        dal.MakeDate = DateTime.Now;
                        dal.TaxNumber = taxNumber;
                        dal.BankName = bankName;
                        dal.BankNumber = bankNumber;
                        dal.Address = address;
                        dal.Flag = "Save";

                        VenderId = dal.Add();

                        if (VenderId > 0)
                        {
                            VenderNum += 1;
                        }

                        if (linkManName != "")
                        {

                            linkDAL.PId = VenderId;
                            linkDAL.Names = linkManName;
                            linkDAL.Phone = phone;
                            linkDAL.Tel = tel;
                            linkDAL.Defaults = defaults;

                            int addNum = linkDAL.Add();

                            if (addNum > 0)
                            {
                                linkManNum += 1;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region If is empty row, check opening stock details is empty or not

                    if (code == "" && names == "" && linkManName != "")
                    {

                        linkDAL.PId = VenderId;
                        linkDAL.Names = linkManName;
                        linkDAL.Phone = phone;
                        linkDAL.Tel = tel;
                        linkDAL.Defaults = defaults;

                        int addNum = linkDAL.Add();

                        if (addNum > 0)
                        {
                            linkManNum += 1;
                        }

                    }

                    #endregion
                }
            }

            #endregion

            string result = "Has imported【" + VenderNum.ToString() + "】rows vender detail,【" + linkManNum.ToString() + "】rows contact detail.";

            return result;

        }
    }
}