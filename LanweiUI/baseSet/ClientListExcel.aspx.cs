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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;
using ExcelReport;
using System.Data.OleDb;

namespace Lanwei.Weixin.UI.BaseSet
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

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDownExcel_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载 
                Response.ContentType = "application/octet-stream";
                // 把文件流发送到客户端 
                Response.WriteFile(file.FullName);
                // 停止页面的执行 
                Response.End();


            }
            catch (Exception ex) { }
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
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载 
                Response.ContentType = "application/octet-stream";
                // 把文件流发送到客户端 
                Response.WriteFile(file.FullName);
                // 停止页面的执行 
                Response.End();


            }
            catch (Exception ex) { }
        }

        protected void btnExcelTo_Click(object sender, EventArgs e)
        {
          
            if (!CheckPower("ClientListExcel"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

            if (fload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fload.FileName);
                if (fileExt == ".xls" || fileExt == ".xlsx")
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt;
                   
                    string oPath = Server.MapPath("excel/"+fileName);//文件路径


                    try
                    {
                        fload.PostedFile.SaveAs(oPath);

                        //ExcelHelper help = new ExcelHelper();
                        //DataSet ds = help.ExcelToDataSet(oPath);

                        DataSet ds = this.ExcelDataSource(fileName);
                       

                        DataTable dt = ds.Tables[0];


                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //删除多余列
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
                            //删除多余行
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
                        logs.Events = "导入客户资料：" + this.Label1.Text;
                        logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        logs.Add();


                        //this.GridView1.DataSource = dt;
                        //this.GridView1.DataBind();

                      

                      


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
                MessageBox.Show(this,"请选择导入的Excel文件！");
                return;

            }
        }

        public DataSet ExcelDataSource(string fileName)
        {
            string oPath = Server.MapPath("excel/" + fileName);//文件路径

            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + oPath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [客户$]", strConn);
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


            #region 循环DataTable

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["客户编号"].ToString();
                string names = dt.Rows[i]["客户名称"].ToString();
                string typeName = dt.Rows[i]["客户类别"].ToString();
                int tax = ConvertTo.ConvertInt(dt.Rows[i]["税率"].ToString());
                DateTime yueDate = DateTime.Now;


                if (dt.Rows[i][3].ToString().Trim() != "")
                {
                    yueDate = Convert.ToDateTime(dt.Rows[i]["余额日期"].ToString());
                }


                decimal payNeed =ConvertTo.ConvertDec(dt.Rows[i]["期初应收款"].ToString());
                decimal payReady = ConvertTo.ConvertDec(dt.Rows[i]["期初预收款"].ToString());

                string taxNumber = dt.Rows[i]["税号"].ToString();
                string bankName = dt.Rows[i]["开户银行"].ToString();
                string bankNumber = dt.Rows[i]["银行账号"].ToString();
                string dizhi = dt.Rows[i]["地址"].ToString();

                string remarks = dt.Rows[i]["备注"].ToString();


                //以下是联系人
                string linkManName = dt.Rows[i]["联系人"].ToString();
                string phone = dt.Rows[i]["手机"].ToString();
                string tel = dt.Rows[i]["座机"].ToString();
                string QQ = dt.Rows[i]["QQ"].ToString();
                string address = dt.Rows[i]["送货地址"].ToString();

                int moren=0;
                if (dt.Rows[i]["首要联系人"].ToString() == "是")
                {
                    moren = 1;
                }

              
                int typeId = 0;
             
             
               
                bool hasCode=false;
                bool hasNames = false;

                hasCode = dal.isExistsCodeAdd(LoginUser.ShopId,code);
                hasNames = dal.isExistsNamesAdd(LoginUser.ShopId,names);


                if (code != "" && names != "")
                {

                    #region 如果是第一行
                    if (!(hasCode || hasNames))
                    {
                        typeId = typeDAL.GetIdByName(LoginUser.ShopId,typeName);

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
                        dal.Flag = "保存";

                        dal.openId = "";
                        dal.nickname = "";
                        dal.headimgurl = "";
                        dal.province = "";
                        dal.country = "";
                        dal.city = "";

                        clientId = dal.Add();//新增商品

                        if (clientId > 0)
                        {
                            clientNum += 1;
                        }


                        if (linkManName != "")//如果存在录入联系人的情况
                        {

                            linkDAL.PId = clientId;
                            linkDAL.Names = linkManName;
                            linkDAL.Phone = phone;
                            linkDAL.Tel = tel;
                            linkDAL.QQ = QQ;
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
                    #region 如果是空行，去找后面的期初库存信息，是否为空

                    if (code == "" && names == "" && linkManName != "")//如果存在录入联系人的情况
                    {

                        linkDAL.PId = clientId;
                        linkDAL.Names = linkManName;
                        linkDAL.Phone = phone;
                        linkDAL.Tel = tel;
                        linkDAL.QQ = QQ;
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


            string result = "导入了【" + clientNum.ToString() + "】条客户信息，【" + linkManNum.ToString() + "】条联系人信息。";

            return result;

        }

    }
}
