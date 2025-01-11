using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                //Response.Write("<script>parent.location.href='http://erp.lanweiyun.com/loginAdmin.aspx'</script>");
                Response.End();
            }
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[MXKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                ////检查Cookies
                //string adminname = Utils.GetCookie("AdminName", "Lanwei.Weixin");
                //string adminpwd = Utils.GetCookie("AdminPwd", "Lanwei.Weixin");
                //if (adminname != "" && adminpwd != "")
                //{
                //    BLL.manager bll = new BLL.manager();
                //    Model.manager model = bll.GetModel(adminname, adminpwd);
                //    if (model != null)
                //    {
                //        Session[MXKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                //    }
                //}
            }
            return false;
        }

     



     
        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        #endregion


       


    }
}
