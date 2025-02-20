﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal Model.siteconfig siteConfig;
        
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new BLL.siteconfig().loadConfig();
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                //Response.Write("<script>parent.location.href='" + siteConfig.webpath +"/webwxopen/"+ siteConfig.webmanagepath + "/index.aspx'</script>");
                
                Response.Write("<script>parent.location.href='" + siteConfig.webpath + "/index.aspx'</script>");
                
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
                //检查Cookies
                string adminname = Utils.GetCookie("AdminName", "Lanwei.Weixin");
                string adminpwd = Utils.GetCookie("AdminPwd", "Lanwei.Weixin");
                if (adminname != "" && adminpwd != "")
                {
                    BLL.manager bll = new BLL.manager();
                    Model.manager model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        Session[MXKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.manager model = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }



        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            Model.manager model = GetAdminInfo();
            BLL.manager_role bll = new BLL.manager_role();
            bool result = bll.Exists(model.role_id, nav_name, action_type);

            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (siteConfig.logstatus > 0)
            {
                Model.manager model = GetAdminInfo();
                int newId = new BLL.manager_log().Add(model.id, model.user_name, action_type, remark);
                if (newId > 0)
                {
                    return true;
                }
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
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        
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


        public bool IsWeiXinCode()
        {
             
            //如果Session为Null
            if (Session["nowweixin"] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string uweixinId = Utils.GetCookie("nowweixinId", "Lanwei.Weixin");
                if (uweixinId != "")
                {
                    BLL.wx_userweixin bll = new BLL.wx_userweixin();
                    Model.wx_userweixin model = bll.GetModel(int.Parse(uweixinId));
                    if (model != null)
                    {
                        Session["nowweixin"] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得当前微信帐号信息
        /// </summary>
        public Model.wx_userweixin GetWeiXinCode()
        {

            if (IsWeiXinCode())
            {
                Model.wx_userweixin model = Session["nowweixin"] as Model.wx_userweixin;
                if (model != null)
                {
                    return model;
                }
            }
            else
            {
                Response.Write("<script>parent.location.href='http://" + HttpContext.Current.Request.Url.Authority + "/admin/weixin/myweixinlist.aspx'</script>");
                Response.End();
            }
            return null;
        }



    }
}
