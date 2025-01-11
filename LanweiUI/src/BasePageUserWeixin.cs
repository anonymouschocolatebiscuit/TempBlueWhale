using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.UI.src
{
    public class BasePageUserWeixin :Page
    {
        private string errmessagecode = "";

        private bool isWeixinBrowser = true;

        public int userId = 0;

        public int shopId = 0;

        public string userName = SQLHelper.userName;

        public string userPwd = SQLHelper.userPwd;

        /// <summary>
        /// app平台名称
        /// </summary>
        public string appName = SQLHelper.appName;
      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //判断微信浏览器

            //if (!WeixinBrowser())
            //{
            //    string returnurl = "";
            //    if (isWeixinBrowser)
            //    {
            //        returnurl = Constant.OverTimePageClient + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
            //    }
            //    else
            //    {
            //        returnurl = Constant.OverWeixinBrowser + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
            //        Response.Redirect(returnurl, true);

            //    }

            //}


            if (!checkvalue())
            {
                string returnurl = "";
                if (errmessagecode == "1")
                {
                    returnurl = Constant.OverTimePageUser + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
                }
                else
                {
                    returnurl = Constant.OverTimePageUser + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
                    Response.Redirect(returnurl, true);

                }

            }
        }

        protected virtual bool WeixinBrowser()
        {
            string userAgent = Request.UserAgent;
            if (userAgent.ToLower().Contains("micromessenger"))
            {
                //Response.Write("欢迎您在微信中访问我。");
                this.isWeixinBrowser = true; return false;

            }
            else
            {
                //Response.Write("请在微信中访问本页。");
                this.isWeixinBrowser = false; return false;
            }
        }


        #region 验证

        protected virtual bool checkvalue()
        {


            if (userId != 0)
            {
                this.errmessagecode = "1"; return false;
            }
            else return false;
        }
        #endregion
    }
}
