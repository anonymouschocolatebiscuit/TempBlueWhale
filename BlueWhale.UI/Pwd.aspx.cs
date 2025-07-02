using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI
{
    public partial class Pwd : BasePage
    {
        public UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string pwd = StrHelper.ConvertSql(this.TextBoxOld.Text.ToString());

            string pwdNewA = this.TextBoxNews.Text;
            string pwdNew = StrHelper.ConvertSql(this.TextBoxNews1.Text.ToString());

            if (pwd == "")
            {
                MessageBox.Show(this.Page, "Please enter current password");
                return;
            }

            if (pwdNewA == "")
            {
                MessageBox.Show(this.Page, "Please enter new password !");
                return;
            }

            if (pwdNewA.Length < 6)
            {
                MessageBox.Show(this.Page, "Please enter longer than 6 digit or word password !");
                return;
            }


            if (pwdNew == "")
            {
                MessageBox.Show(this.Page, "Please enter confirm new password !");
                return;
            }


            if (pwdNew != pwdNewA)
            {
                MessageBox.Show(this.Page, "New password and confirm password not same");
                return;
            }


            if (pwd == LoginUser.Pwd)
            {

                int temp = dal.UpdatePwd(LoginUser.Id, pwdNew, this.TextBoxNews.Text);

                if (temp > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "change password";
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();


                    MessageBox.ShowAndRedirect(this, "Success change password, Please relogin with new password", "OverPwd.htm");
                }

            }
            else
            {
                MessageBox.Show(this, "Current Password invalid");

                return;

            }

        }
    }
}