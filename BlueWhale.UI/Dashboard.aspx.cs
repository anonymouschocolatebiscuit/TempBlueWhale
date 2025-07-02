using BlueWhale.Common;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI
{
    public partial class Dashboard : BasePage
    {
        public string companyName = "";
        public string userName = "";
        public string roleName = "";
        public string deptName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                companyName = Utils.GetCookie("shopName").ToString();

                userName = LoginUser.Names;
                roleName = LoginUser.RoleName;
                deptName = LoginUser.DeptName;
            }
            else
            {
                if (LoginUser == null)
                {
                    Response.Redirect("Login.aspx");
                }

                companyName = Utils.GetCookie("shopName").ToString();
                userName = Utils.GetCookie("userName").ToString();
                roleName = Utils.GetCookie("roleName").ToString();
                deptName = Utils.GetCookie("deptName").ToString();
            }
        }
    }
}
