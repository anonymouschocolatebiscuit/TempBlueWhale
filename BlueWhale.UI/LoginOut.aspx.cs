using BlueWhale.Common;
using BlueWhale.DAL;
using System;
using System.Web;

namespace BlueWhale.UI
{
    public partial class LoginOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogsDAL logs = new LogsDAL
                {
                    ShopId = ConvertTo.ConvertInt(Utils.GetCookie("shopId").ToString()),
                    Users = Utils.GetCookie("phone").ToString() + "-" + Utils.GetCookie("userName").ToString(),
                    Events = "Logout",
                    Ip = HttpContext.Current.Request.UserHostAddress.ToString()
                };

                logs.Add();

                DelCookeis();

                Session["userInfo"] = null;

                Response.Redirect("Login.aspx");
            }
        }

        #region Delete cookies

        ///<summary>
        /// Delete cookies
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
