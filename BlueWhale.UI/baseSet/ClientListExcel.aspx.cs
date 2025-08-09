using System;
using System.Data;
using System.Data.OleDb;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.BaseSet;
using BlueWhale.UI.src;

namespace BlueWhale.UI.BaseSet
{
    public partial class ClientListExcel : BasePage
    {
        public ClientDAL dal = new ClientDAL();
        public ClientTypeDAL typeDAL = new ClientTypeDAL();
        public ClientLinkManDAL linkDAL = new ClientLinkManDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("ClientListExcel"))
                {
                    Response.Redirect("../OverPower.htm");
                }
            }
        }

        protected void lbtnDownExcel_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

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
            catch (Exception) { }
        }

        protected void lbtnDownExample_Click(object sender, EventArgs e)
        {
            string path = "excel/excel_example.xls";

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
            catch (Exception) { }
        }

        protected void btnExcelTo_Click(object sender, EventArgs e)
        {
            if (!CheckPower("ClientListExcel"))
            {
                MessageBox.Show(this, "No permission for this operation!");
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
                        logs.Events = "Import customer information: " + this.Label1.Text;
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
                MessageBox.Show(this, "Please select the Excel file to import!");
                return;
            }
        }

        public DataSet ExcelDataSource(string fileName)
        {
            string oPath = Server.MapPath("excel/" + fileName);

            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + oPath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [Customer$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            conn.Close();
            return ds;
        }

        public string DataTableToSql(DataTable dt)
        {
            int clientId = 0;

            int clientNum = 0;
            int linkManNum = 0;


            #region

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["Client Number"].ToString();
                string names = dt.Rows[i]["Client Name"].ToString();
                string typeName = dt.Rows[i]["Client Category"].ToString();
                int tax = ConvertTo.ConvertInt(dt.Rows[i]["Tax rate"].ToString());
                DateTime yueDate = DateTime.Now;

                if (dt.Rows[i][3].ToString().Trim() != "")
                {
                    yueDate = Convert.ToDateTime(dt.Rows[i]["Balance Date"].ToString());
                }

                decimal payNeed = ConvertTo.ConvertDec(dt.Rows[i]["Beginning receivables"].ToString());
                decimal payReady = ConvertTo.ConvertDec(dt.Rows[i]["Opening advance receipts"].ToString());
                string taxNumber = dt.Rows[i]["Tax Number"].ToString();
                string bankName = dt.Rows[i]["Opening Bank"].ToString();
                string bankNumber = dt.Rows[i]["Bank Account Number"].ToString();
                string dizhi = dt.Rows[i]["Address"].ToString();
                string remarks = dt.Rows[i]["Remarks"].ToString();
                string linkManName = dt.Rows[i]["Contact"].ToString();
                string phone = dt.Rows[i]["Mobile Phone"].ToString();
                string tel = dt.Rows[i]["Landline Phone"].ToString();
                string address = dt.Rows[i]["Shipping Address"].ToString();

                int moren = 0;
                if (dt.Rows[i]["Primary contact person"].ToString() == "True")
                {
                    moren = 1;
                }

                int typeId = 0;

                bool hasCode = false;
                bool hasNames = false;

                hasCode = dal.isExistsCodeAdd(LoginUser.ShopId, code);
                hasNames = dal.isExistsNamesAdd(LoginUser.ShopId, names);

                if (code != "" && names != "")
                {

                    #region 
                    if (!(hasCode || hasNames))
                    {
                        typeId = typeDAL.GetIdByName(LoginUser.ShopId, typeName);

                        dal.ShopId = LoginUser.ShopId;
                        dal.Code = code;
                        dal.Names = names;

                        dal.YueDate = yueDate;
                        dal.TypeId = typeId;
                        dal.TaxNumber = taxNumber;
                        dal.BankName = bankName;
                        dal.BankNumber = bankNumber;
                        dal.Dizhi = dizhi;

                        dal.Remarks = remarks;
                        dal.MakeDate = DateTime.Now;
                        dal.Flag = "Save";

                        dal.openId = "";
                        dal.nickname = "";
                        dal.headimgurl = "";
                        dal.province = "";
                        dal.country = "";
                        dal.city = "";

                        clientId = dal.Add();

                        if (clientId > 0)
                        {
                            clientNum += 1;
                        }

                        if (linkManName != "")
                        {
                            linkDAL.PId = clientId;
                            linkDAL.Names = linkManName;
                            linkDAL.Phone = phone;
                            linkDAL.Tel = tel;
                            linkDAL.Address = address;
                            linkDAL.Moren = moren;

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
                    #region 

                    if (code == "" && names == "" && linkManName != "")
                    {
                        linkDAL.PId = clientId;
                        linkDAL.Names = linkManName;
                        linkDAL.Phone = phone;
                        linkDAL.Tel = tel;
                        linkDAL.Address = address;
                        linkDAL.Moren = moren;

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

            string result = "Imported【" + clientNum.ToString() + "】Client information，【" + linkManNum.ToString() + "】Contact information。";

            return result;
        }
    }
}
