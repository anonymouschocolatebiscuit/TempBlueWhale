namespace BlueWhale.Common
{
    public class Constant
    {
        #region Page URLs and Session Keys

        /// <summary>
        /// Home page URL
        /// </summary>
        public static string DefaultUrl = "/Dashboard.aspx";

        /// <summary>
        /// Login page URL
        /// </summary>
        public static string LoginUrl = "/Login.aspx";

        /// <summary>
        /// Login timeout page
        /// </summary>
        public static string OverTimePage = "/OverTime.htm";

        /// <summary>
        /// Login timeout page for client
        /// </summary>
        public static string OverTimePageClient = "OverTimeClient.htm";

        /// <summary>
        /// Login timeout page for user
        /// </summary>
        public static string OverTimePageUser = "OverTimeUser.htm";

        /// <summary>
        /// Authorization expired page
        /// </summary>
        public static string OverRightPage = "OverRight.htm";

        /// <summary>
        /// Database ID
        /// </summary>
        public static string dbid = "";

        /// <summary>
        /// Error message key
        /// </summary>
        public static string ErrMessage = "errMsg";

        /// <summary>
        /// Return URL key
        /// </summary>
        public static string ReturnUrl = "retUrl";

        /// <summary>
        /// Default error page
        /// </summary>
        public const string ErrUrl = "/Error.htm";

        /// <summary>
        /// Cookie name for username
        /// </summary>
        public const string CookieName = ".userName";

        /// <summary>
        /// Session key for logged-in user info
        /// </summary>
        public const string SessionUserName = "userInfo";

        /// <summary>
        /// Session key for user
        /// </summary>
        public const string SessionUser = "User";

        #endregion

        #region Messages

        /// <summary>
        /// Login error message
        /// </summary>
        public static string LoginErrMsg = "Username or password invalid";

        /// <summary>
        /// Unauthorized access message
        /// </summary>
        public static string NoAuthority = "Unauthorize Access";

        /// <summary>
        /// Login timeout message
        /// </summary>
        public static string OverTime = "Login timeout";

        /// <summary>
        /// Authorization expired message
        /// </summary>
        public static string OverRight = "Login timeout";

        /// <summary>
        /// Logout message
        /// </summary>
        public static string LogOut = "Logout";

        #endregion

        #region Encryption Types

        /// <summary>
        /// MD5 encryption
        /// </summary>
        public const string MD5 = "md5";

        /// <summary>
        /// SHA1 encryption
        /// </summary>
        public const string Sha1 = "sha1";

        #endregion
    }
}
