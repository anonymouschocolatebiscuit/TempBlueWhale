using System.Text.RegularExpressions;

namespace BlueWhale.Common
{
    public class Regexlib
    {
        /// <summary>
        /// validate stirng is in range a-zA-Z0-9_（3-50 length）
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[A-Za-z0-9_]{3,50}$");
        }
        /// <summary>
        /// Validate email
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        /// <summary>
        /// validate is mobile number
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidMobile(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[1]+[3,4,5,6,7,8,9]+\d{9}");
        }
        /// <summary>
        /// validate mobile number
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidTelPhone(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(\d{3,4}-)?\d{6,8}$");
        }
    }
}