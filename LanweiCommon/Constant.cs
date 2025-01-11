using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Common
{
    public class Constant
    {
        #region
        /// <summary>
        /// 首页
        /// </summary>
        public static string DefaultUrl = "/Default.aspx";
        /// <summary>
        /// 登陆页
        /// </summary>
        public static string LoginUrl = "/Index.aspx";


        /// <summary>
        /// 登录超时页
        /// </summary>
        public static string OverTimePage = "/OverTime.htm";

        /// <summary>
        /// 登录超时页---客户
        /// </summary>
        public static string OverTimePageClient = "OverTimeClient.htm";

        /// <summary>
        /// 登录超时页---用户
        /// </summary>
        public static string OverTimePageUser = "OverTimeUser.htm";


        /// <summary>
        /// 请从微信打开页面---用户
        /// </summary>
        public static string OverWeixinBrowser = "isWeixinBrowser.aspx";




        /// <summary>
        /// 授权过期
        /// </summary>
        public static string OverRightPage = "OverRight.htm";


        /// <summary>
        /// 数据库id
        /// </summary>
        public static string dbid = "";

        /// <summary>
        /// 错误参数名
        /// </summary>
        public static string ErrMessage = "errMsg";
        /// <summary>
        /// 返回地址参数名
        /// </summary>
        public static string ReturnUrl = "retUrl";
        public const string ErrUrl = "/Error.htm";
        public const string CookieName = ".userName";
        /// <summary>
        /// SESSION登陆用户名
        /// </summary>
        public const string SessionUserName = "userInfo";
        /// <summary>
        /// SESSION登陆用户对象名
        /// </summary>
        public const string SessinUser = "User";
        #endregion

        #region
        /// <summary>
        /// 登陆验证报错
        /// </summary>
        public static string LoginErrMsg = "用户名或密码错误！";
        /// <summary>
        /// 无权限
        /// </summary>
        public static string NoAuthority = "无权限访问";

        /// <summary>
        /// 登录超时
        /// </summary>
        public static string OverTime = "登录超时";

        /// <summary>
        /// 授权过期
        /// </summary>
        public static string OverRight = "登录超时";

        /// <summary>
        /// 注销
        /// </summary>
        public static string LogOut = "已注销";
        #endregion

        #region 加密类型
        /// <summary>
        /// md5加密
        /// </summary>
        public const string MD5 = "md5";
        /// <summary>
        /// sha1加密
        /// </summary>
        public const string Sha1 = "sha1";
        #endregion
    }
}
