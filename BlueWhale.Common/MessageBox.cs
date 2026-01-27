using System.Text;
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
            page.ClientScript.RegisterStartupScript(
                page.GetType(),
                "message",
                "<script language='javascript' defer>alert('" + msg + "');</script>"
            );
        }

        /// <summary>
        /// component click, confirm message box
        /// </summary>
        /// <param name="Control">Web control</param>
        /// <param name="msg">display message</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// display message box，then redirect to page
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        /// <param name="url">redirect target URL</param>
        public static void ShowAndRedirect(Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(
                page.GetType(),
                "message",
                "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>"
            );
        }

        /// <summary>
        /// display message box，hen redirect to page
        /// </summary>
        /// <param name="page">current page pointer，always as this</param>
        /// <param name="msg">display message</param>
        /// <param name="url">redirect target URL</param>
        public static void ShowAndRedirects(Page page, string msg, string url)
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
        public static void ResponseScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(
                page.GetType(),
                "message",
                "<script language='javascript' defer>" + script + "</script>"
            );
        }

        /// <summary>
        /// ajax message box
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="page"></param>
        /// <param name="msgText"></param>
        public static void ShowAjax(System.Web.UI.WebControls.Button btn, Page page, string msgText)
        {
            ScriptManager.RegisterStartupScript(btn, page.GetType(), "test", "alert('" + msgText + "');", true);
        }

        /// <summary>
        /// ajax message box -- with redirect URL
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="page"></param>
        /// <param name="msgText"></param>
        /// <param name="url"></param>
        public static void ShowAjax(System.Web.UI.WebControls.Button btn, Page page, string msgText, string url)
        {
            ScriptManager.RegisterStartupScript(
                btn,
                page.GetType(),
                "test",
                "alert('" + msgText + "');window.location.href='" + url + "';",
                true
            );
        }
    }
}
