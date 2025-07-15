using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.Model;
using System;
using System.Web;
using System.Web.UI;

namespace BlueWhale.UI
{
    public partial class Login : Page
    {

        public UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ipString = Request.UserHostAddress.ToString();

                if (ipString == "127.0.0.1" || ipString == "" || ipString == "::1")
                {
                    return;
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            #region Validate Login Credential

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show(this, "Please enter user account!");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(this, "Please enter password!");
                txtPassword.Focus();
                return;
            }

            string captcha = this.captcha.Text;

            if (string.IsNullOrEmpty(captcha))
            {
                MessageBox.Show(this, "Please enter captcha !");
                this.captcha.Focus();
                return;
            }

            if (captcha != Session["verify"].ToString())
            {
                MessageBox.Show(this, "Captcha invalid, please retry!");
                this.captcha.Text = "";
                this.captcha.Focus();
                return;
            }

            #endregion Validate Login Credential

            if (Page.IsValid)
            {
                string userName = StrHelper.ConvertSql(txtUserName.Text.ToString());
                string userPwd = StrHelper.ConvertSql(txtPassword.Text.ToString());

                Users user = dal.getByUserName(userName);

                if (user == null)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = 0,
                        Users = txtUserName.Text,
                        Events = "account not exist, password: " + txtPassword.Text + " captcha: " + this.captcha.Text,
                        Ip = HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Account not exist");
                    return;
                }

                if (user.Pwd != userPwd)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = 0,
                        Users = txtUserName.Text,
                        Events = "account or password invalid, password: " + txtPassword.Text + " captcha: " + this.captcha.Text,
                        Ip = HttpContext.Current.Request.UserHostAddress.ToString()
                    };

                    logs.Add();

                    this.captcha.Text = "";

                    MessageBox.Show(this, "Account or password invalid, please try again!");
                    return;
                }

                if (user.Flag == "Active")
                {
                    Session["userInfo"] = user;
                    Session["login"] = "employee";
                    Session.Timeout = 45;

                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = user.ShopId,
                        Users = user.Phone + "-" + user.Names,
                        Events = "Login system into Dashboard.aspx page",
                        Ip = HttpContext.Current.Request.UserHostAddress.ToString()
                    };

                    logs.Add();

                    // Write Cookies
                    Utils.WriteCookie("shopId", user.ShopId.ToString(), 14400);
                    Utils.WriteCookie("shopName", user.ShopName, 14400);
                    Utils.WriteCookie("userName", user.Names, 14400);
                    Utils.WriteCookie("roleName", user.RoleName, 14400);
                    Utils.WriteCookie("deptName", user.DeptName, 14400);
                    Utils.WriteCookie("phone", user.Phone, 14400);

                    Response.Redirect("Dashboard.aspx");
                }
                if (user.Flag == "Disable")
                {
                    MessageBox.Show(this, "Account is locked, please contact admin");
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}