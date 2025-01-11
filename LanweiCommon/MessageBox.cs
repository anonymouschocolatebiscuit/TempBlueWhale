using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Lanwei.Weixin.Common
{
   public  class MessageBox
    {
       private MessageBox()
        {

        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            //Response.Write("<script>alert('帐户审核通过！现在去为企业充值。');window.location=\"" + pageurl + "\"</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");


        }
        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirects(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");

        }


        /// <summary>
        /// 带有Ajax的提示框
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="page"></param>
        /// <param name="msgText"></param>
        public static void ShowAjax(System.Web.UI.WebControls.Button btn, System.Web.UI.Page page, string msgText)
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(btn, page.GetType(), "test", "alert('" + msgText + "');", true);



        }

        /// <summary>
        ///  带有Ajax的提示框--有跳转地址
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="page"></param>
        /// <param name="msgText"></param>
        /// <param name="url"></param>
        public static void ShowAjax(System.Web.UI.WebControls.Button btn, System.Web.UI.Page page, string msgText, string url)
        {

            System.Web.UI.ScriptManager.RegisterStartupScript(btn, page.GetType(), "test", "alert('" + msgText + "');window.location.href='" + url + "';", true);



        }



    }
}
