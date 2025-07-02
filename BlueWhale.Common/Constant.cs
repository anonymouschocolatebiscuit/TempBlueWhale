namespace BlueWhale.Common
{
    public class Constant
    {
        #region
        /// <summary>
        /// Home Page
        /// </summary>
        public static string DefaultUrl = "/Dashboard.aspx";
        /// <summary>
        /// 登陆页
        /// </summary>
        public static string LoginUrl = "/Login.aspx";


        /// <summary>
        /// Login timeout
        /// </summary>
        public static string OverTimePage = "/OverTime.htm";

        /// <summary>
        /// Login timeout - client
        /// </summary>
        public static string OverTimePageClient = "OverTimeClient.htm";

        /// <summary>
        /// Login timeout - user
        /// </summary>
        public static string OverTimePageUser = "OverTimeUser.htm";

        /// <summary>
        /// Authorize expire
        /// </summary>
        public static string OverRightPage = "OverRight.htm";

        /// <summary>
        /// Database id
        /// </summary>
        public static string dbid = "";

        /// <summary>
        /// Error Message
        /// </summary>
        public static string ErrMessage = "errMsg";

        /// <summary>
        /// Return Address name
        /// </summary>
        public static string ReturnUrl = "retUrl";
        public const string ErrUrl = "/Error.htm";
        public const string CookieName = ".userName";

        /// <summary>
        /// Login SESSION username
        /// </summary>
        public const string SessionUserName = "userInfo";

        /// <summary>
        /// Login SESSION user
        /// </summary>
        public const string SessinUser = "User";

        #endregion

        #region

        /// <summary>
        /// Login error
        /// </summary>
        public static string LoginErrMsg = "Username or password invalid";
        /// <summary>
        /// Unauthorize
        /// </summary>
        public static string NoAuthority = "Unauthorize Access";

        /// <summary>
        /// Login timeout
        /// </summary>
        public static string OverTime = "Login timeout";

        /// <summary>
        /// Authorize expire
        /// </summary>
        public static string OverRight = "Login timeout";

        /// <summary>
        /// Logout
        /// </summary>
        public static string LogOut = "Logout";
        #endregion

        #region encryption
        /// <summary>
        /// md5 encryption
        /// </summary>
        public const string MD5 = "md5";
        /// <summary>
        /// sha1 encryption
        /// </summary>
        public const string Sha1 = "sha1";
        #endregion
    }
}
