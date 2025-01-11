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

using Lanwei.Weixin.DBUtility;

using Lanwei.Weixin.Common;
using Lanwei.Weixin.DAL;

namespace Lanwei.Weixin.UI
{
    public partial class LoginOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                LogsDAL logs = new LogsDAL();
                logs.ShopId = ConvertTo.ConvertInt(Utils.GetCookie("shopId").ToString());
                logs.Users = Utils.GetCookie("phone").ToString() + "-" + Utils.GetCookie("userName").ToString();
                logs.Events = "退出系统";
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();

                logs.Add();


                this.DelCookeis();

                Session["userInfo"] = null;

                Response.Redirect("index.aspx");


            }
        }

        #region  ##删除cookies
        ///<summary>
        /// 删除cookies
        ///</summary>
        public void DelCookeis()
        {
            foreach (string cookiename in Request.Cookies.AllKeys)
            {
                HttpCookie cookies = Request.Cookies[cookiename];
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    Response.Cookies.Add(cookies);
                    Request.Cookies.Remove(cookiename);
                }
            }
        }
        #endregion
    }
}
