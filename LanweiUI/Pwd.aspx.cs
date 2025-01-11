using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI
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
                MessageBox.Show(this.Page,"请输入原密码！");
                return;
            }

            if (pwdNewA == "")
            {
                MessageBox.Show(this.Page, "请输入新密码！");
                return;
            }

            if (pwdNewA.Length <6)
            {
                MessageBox.Show(this.Page, "请输入6位以上新密码！");
                return;
            }


            if (pwdNew == "")
            {
                MessageBox.Show(this.Page, "请确认新密码！");
                return;
            }


            if (pwdNew !=pwdNewA)
            {
                MessageBox.Show(this.Page, "两次密码不一致！");
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
                    logs.Events = "修改密码";
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();


                    MessageBox.ShowAndRedirect(this, "密码修改成功，请用新密码重新登陆！", "OverPwd.htm");
                }

            }
            else
            {
                MessageBox.Show(this, "原密码错误！");

                return;

            }

        }
    }
}