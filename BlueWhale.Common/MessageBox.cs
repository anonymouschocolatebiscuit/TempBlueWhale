using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace BlueWhale.Common
{
    public class MessageBox
    {
        private MessageBox()
        {

        }

        /// <summary>
        /// Display message box
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        public static void Show(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        /// <summary>
        /// component click, confirm message box
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// display message box，then redirect to page
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        /// <param name="url">redirect target URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            //Response.Write("<script>alert('帐户审核通过！现在去为企业充值。');window.location=\"" + pageurl + "\"</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");
        }

        /// <summary>
        /// display message box，hen redirect to page
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        /// <param name="url">redirect target URL</param>
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
        /// output custom script message
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="script">scaript name</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");

        }


        /// <summary>
        /// ajax message box
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="page"></param>
        /// <param name="msgText"></param>
        public static void ShowAjax(System.Web.UI.WebControls.Button btn, System.Web.UI.Page page, string msgText)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(btn, page.GetType(), "test", "alert('" + msgText + "');", true);
        }

        /// <summary>
        /// ajax message box -- with redirect URL
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
