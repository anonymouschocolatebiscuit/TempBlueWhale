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

using LanweiBLL;

namespace Lanwei.Weixin.UI.src
{
    public class BasePage :Page //VersionContorl //Page
    {


        private string errmessagecode = "";

        public UserDAL powerDAL = new UserDAL();
        public SystemSetDAL setDAL = new SystemSetDAL();

        public Users LoginUser
        {
            get { return Session["userInfo"] as Users; }
        }


      
        public SystemSetModel SysInfo
        {
            get {

                SystemSetModel sys = new SystemSetModel();

                string isWhere = " shopId='"+LoginUser.ShopId+"' ";
                DataSet ds = setDAL.GetList(isWhere);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    sys.Company = ds.Tables[0].Rows[0]["Company"].ToString();

                    sys.Address = ds.Tables[0].Rows[0]["address"].ToString();
                    sys.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                    sys.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                    sys.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();

                    sys.Bwb = ds.Tables[0].Rows[0]["address"].ToString();

                    sys.Num = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Num"].ToString());
                    sys.Price = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Price"].ToString());
                    sys.PriceType = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["PriceType"].ToString());

                    sys.CheckNum =ConvertTo.ConvertInt( ds.Tables[0].Rows[0]["CheckNum"].ToString());

                    sys.UseCheck = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["UseCheck"].ToString());

                    sys.Tax =ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Tax"].ToString());

                    sys.AppId = ds.Tables[0].Rows[0]["AppId"].ToString();
                    sys.AppSecret = ds.Tables[0].Rows[0]["AppSecret"].ToString();

                    sys.FieldA = ds.Tables[0].Rows[0]["FieldA"].ToString();
                    sys.FieldB = ds.Tables[0].Rows[0]["FieldB"].ToString();
                    sys.FieldC = ds.Tables[0].Rows[0]["FieldC"].ToString();
                    sys.FieldD = ds.Tables[0].Rows[0]["FieldD"].ToString();

                    sys.CorpIdQY = ds.Tables[0].Rows[0]["CorpIdQY"].ToString();
                    sys.CorpSecretQY = ds.Tables[0].Rows[0]["CorpSecretQY"].ToString();
                   
                    sys.CorpIdDD = ds.Tables[0].Rows[0]["CorpIdDD"].ToString();
                    sys.CorpSecretDD = ds.Tables[0].Rows[0]["CorpSecretDD"].ToString();

                    sys.PermanentCodeQY = ds.Tables[0].Rows[0]["PermanentCodeQY"].ToString();

                    sys.PermanentCodeDD = ds.Tables[0].Rows[0]["PermanentCodeDD"].ToString();

                    sys.RemarksPurOrder = ds.Tables[0].Rows[0]["RemarksPurOrder"].ToString();

                    sys.RemarksSalesOrder = ds.Tables[0].Rows[0]["RemarksSalesOrder"].ToString();

                    sys.PrintLogo = ds.Tables[0].Rows[0]["PrintLogo"].ToString();

                    sys.PrintZhang = ds.Tables[0].Rows[0]["PrintZhang"].ToString();

                    sys.UserSecret = ds.Tables[0].Rows[0]["UserSecret"].ToString();
                    sys.CheckInSecret = ds.Tables[0].Rows[0]["CheckInSecret"].ToString();
                    sys.ApplySecret = ds.Tables[0].Rows[0]["ApplySecret"].ToString();

 
                }

                return sys;

            }
        }

        public weixinQYInfo qyInfo
        {
            get
            {

                weixinQYInfo sys = new weixinQYInfo();
                DataSet ds = setDAL.GetWeixinQYSet();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    sys.AppId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["AppId"].ToString());
                    sys.AppName = ds.Tables[0].Rows[0]["AppName"].ToString();


                    sys.CorpId = ds.Tables[0].Rows[0]["CorpId"].ToString();
                    sys.CorpSecret = ds.Tables[0].Rows[0]["CorpSecret"].ToString();



                }

                return sys;

            }
        }

        public alibabaDDInfo ddInfo
        {
            get
            {

                alibabaDDInfo sys = new alibabaDDInfo();
                DataSet ds = setDAL.GetDingdingSet();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    sys.AppId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["AppId"].ToString());
                    sys.AppName = ds.Tables[0].Rows[0]["AppName"].ToString();


                    sys.CorpId = ds.Tables[0].Rows[0]["CorpId"].ToString();
                    sys.CorpSecret = ds.Tables[0].Rows[0]["CorpSecret"].ToString();



                }

                return sys;

            }
        }




        #region 判断是否检查负库存

        /// <summary>
        /// 判断是否检查负库存
        /// </summary>
        /// <returns></returns>
        public bool CheckStoreNum()
        {
            bool check = true;
            string checkNum = "0";

            DataSet ds = setDAL.GetAllModel();
            if (ds.Tables[0].Rows.Count > 0)
            {
                checkNum = ds.Tables[0].Rows[0]["checkNum"].ToString();
            }
            if (checkNum == "1")//需要检查负库存
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return check;

        }

        #endregion

        #region 判断是否有权限

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckPower(string url)
        {

            return powerDAL.CheckPowerByUserIdAndUrl(LoginUser.Id, url);

        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

           



            if (!checkvalue())
            {
                string returnurl = "";
                if (errmessagecode == "1")
                {
                    returnurl = Constant.OverTimePage + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
                }
                else
                {
                    returnurl = Constant.OverTimePage + "?" + Constant.ErrMessage + "=" + Server.HtmlEncode(Constant.OverTime);
                    Response.Redirect(returnurl, true);

                }

            }
        }
   
        #region 验证

        /// <summary>
        /// 判断用户是否已经登录(解决Session超时问题)
        /// </summary>
        /// <returns></returns>
        protected virtual bool checkvalue()
        {

            if (Session["userInfo"] != null)
            {
                //确保数据库连接正确、每次加载页面自动调整过来
              
                this.errmessagecode = "1"; return false;
            }
            else
            {

                //检查Cookies
                string username = Utils.GetCookie("phone");
                string password = Utils.GetCookie("pwd");
                if (username != "" && password != "")
                {
                    if (powerDAL.isLoginValidate(username, password))
                    {

                        Lanwei.Weixin.Model.Users user = powerDAL.getByUserName(username);
                        if (user != null)
                        {
                            Session["userInfo"] = user;
                            Session["login"] = "员工";
                            Session.Timeout = 45;

                            this.errmessagecode = "1"; return false;
                        }
                        else
                        {
                            return false;
                        }

                     
                    }
                    else
                    {
                        return false;
                    }
                }
                else               
                    return false;

            }
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

        protected internal Lanwei.Weixin.Model.siteconfig siteConfig=new Lanwei.Weixin.Model.siteconfig();


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
                string adminname = Utils.GetCookie("AdminName", "MxWeiXinPF");
                string adminpwd = Utils.GetCookie("AdminPwd", "MxWeiXinPF");
                //if (adminname != "" && adminpwd != "")
                //{
                //    LanweiBLL.manager bll = new LanweiBLL.manager();
                //    Lanwei.Weixin.Model.manager model = bll.GetModel(adminname, adminpwd);
                //    if (model != null)
                //    {
                //        Session[MXKeys.SESSION_ADMIN_INFO] = model;
                //        return true;
                //    }
                //}
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Lanwei.Weixin.Model.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Lanwei.Weixin.Model.manager model = Session[MXKeys.SESSION_ADMIN_INFO] as Lanwei.Weixin.Model.manager;
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
            //Lanwei.Weixin.Model.manager model = GetAdminInfo();
            //LanweiBLL.manager_role bll = new LanweiBLL.manager_role();
            //bool result = bll.Exists(model.role_id, nav_name, action_type);

            //if (!result)
            //{
            //    string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
            //    Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
            //    Response.End();
            //}
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            //if (siteConfig.logstatus > 0)
            //{
            //    Lanwei.Weixin.Model.manager model = GetAdminInfo();
            //    int newId = new LanweiBLL.manager_log().Add(model.id, model.user_name, action_type, remark);
            //    if (newId > 0)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        #endregion



    }
}

