using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace BlueWhale.Common
{
    public abstract class StrHelper
    {
        private static string passWord;	//encrypt string
        /// <summary>
        /// client popout message box, pause current page operation, click confirm to navigate new page
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        public static void ToRedirect(string msg, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">" + "\n");
            sb.Append("alert('" + msg + "');" + "\n");
            sb.Append("location.href='" + url + "';" + "\n");
            sb.Append("</script>" + "\n");
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Ajax start up for using AJAX component page
        /// AjaxAlert:pop out message box
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void AjaxAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ajaxjs", string.Format("alert('{0}！')", msg), true);
        }

        //字符串清理
        public static string InputText(string inputString, int maxLength)
        {
            StringBuilder retVal = new StringBuilder();

            // check is null or empty
            if ((inputString != null) && (inputString != String.Empty))
            {
                inputString = inputString.Trim();

                //check length
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                //replace symbol
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");// replace single quote
            }
            return retVal.ToString();

        }
        /// <summary>
        /// popo out message
        /// </summary>
        /// <param name="msg">message content</param>
        public static void Show(string msg)
        {

            HttpContext.Current.Response.Write("<script language='javascript'>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// check is number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static public bool VldInt(string num)
        {
            #region
            int ResultNum;
            return int.TryParse(num, out ResultNum);
            #endregion
        }

        /// <summary>
        /// get current timestamp
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }

        /// <summary>
        /// replace symbol
        /// </summary>
        /// <param name="str">string to replace</param>
        /// <returns></returns>
        static public string GetHtmlEditReplace(string str)
        {
            #region
            return str.Replace("'", "’").Replace("&nbsp;", " ").Replace(",", "，").Replace("%", "％").
                Replace("script", "").Replace(".js", "");
            #endregion
        }

        /// <summary>
        /// string word count
        /// </summary>
        /// <param name="str">input string</param>
        /// <param name="num">start from</param>
        /// <returns></returns>
        static public string GetSubString(string str, int num)
        {
            #region
            return (str.Length > num) ? str.Substring(0, num) + "..." : str;
            #endregion
        }

        /// <summary>
        /// string extract
        /// </summary>
        /// <param name="stringToSub">input string</param>
        /// <param name="length">start from</param>
        /// <returns></returns>
        public static string GetFirstString(string stringToSub, int length)
        {
            #region
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            if (isCut)
                return sb.ToString() + "..";
            else
                return sb.ToString();
            #endregion
        }



        static public string getSpells(string input)
        {
            #region
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
            #endregion
        }

        static public string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
            #endregion
        }


        /// <summary>
        /// Half-width to Full-width symbol
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetFullWidth(string BJstr)
        {
            #region
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

            #endregion
        }

        /// <summary>
        /// Full-width to Half-width symbol
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetHalfWidth(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }

        #region encrypt type
        /// <summary>
        ///  encrypt type
        /// </summary>
        public enum PasswordType
        {
            SHA1,
            MD5
        }
        #endregion


        /// <summary>
        /// encrypt password
        /// </summary>
        /// <param name="PasswordString">to be encrypt string</param>
        /// <param name="PasswordFormat">encrypt format</param>
        /// <returns></returns>
        static public string EncryptPassword(string PasswordString, PasswordType PasswordFormat)
        {
            #region
            switch (PasswordFormat)
            {
                case PasswordType.SHA1:
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
#pragma warning restore CS0618 // Type or member is obsolete
                        break;
                    }
                case PasswordType.MD5:
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5").Substring(8, 16).ToLower();
#pragma warning restore CS0618 // Type or member is obsolete
                        break;
                    }
                default:
                    {
                        passWord = string.Empty;
                        break;
                    }
            }
            return passWord;
            #endregion
        }

        /// <summary>
        /// replace symbol to html readable symbol
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {
            #region
            str = str.Replace("&", "&amp;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\r\n", "<br>");

            return str;
            #endregion
        }

        /// <summary>
        /// html convert to string
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToString(string strHtml)
        {
            #region
            strHtml = strHtml.Replace("<br>", "\r\n");
            strHtml = strHtml.Replace(@"<br />", "\r\n");
            strHtml = strHtml.Replace(@"<br/>", "\r\n");
            strHtml = strHtml.Replace("&gt;", ">");
            strHtml = strHtml.Replace("&lt;", "<");
            strHtml = strHtml.Replace("&nbsp;", " ");
            strHtml = strHtml.Replace("&quot;", "\"");

            strHtml = Regex.Replace(strHtml, @"<\/?[^>]+>", "", RegexOptions.IgnoreCase);

            return strHtml;
            #endregion
        }

        /// <summary>
        /// convert chinese day to Datetime.day
        /// </summary>
        /// <returns></returns>
        public static string GetChineseWeek(DateTime t)
        {
            #region
            string week = "";

            switch (t.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "三";
                    break;
                case DayOfWeek.Thursday:
                    week = "四";
                    break;
                case DayOfWeek.Friday:
                    week = "五";
                    break;
                case DayOfWeek.Saturday:
                    week = "六";
                    break;
                case DayOfWeek.Sunday:
                    week = "日";
                    break;
            }

            return "星期" + week;
            #endregion
        }


        /// <summary>
        /// generate string to specific length
        /// </summary>
        /// <param name="strLong">generate lenght</param>
        /// <param name="str">string to generate</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            #region
            string ReturnStr = string.Empty;
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }
            return ReturnStr;
            #endregion
        }

        public static string FilterStr(string str)
        {
            return str.Replace("'", " ").Replace(".", "").Replace("\r\n", "　");
        }

        /// <summary>
        /// generate barcode：example：bar_code("20070520122334", 20, 1, 1);
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch">height</param>
        /// <param name="cw">width</param>
        /// <param name="type_code">output number, 1 as true</param>
        /// <returns></returns>
        public static string BarCode(object str, int ch, int cw, int type_code)
        {
            #region
            string strTmp = str.ToString();
            string code = strTmp;
            strTmp = strTmp.ToLower();
            int height = ch;
            int width = cw;

            strTmp = strTmp.Replace("0", "_|_|__||_||_|"); ;
            strTmp = strTmp.Replace("1", "_||_|__|_|_||");
            strTmp = strTmp.Replace("2", "_|_||__|_|_||");
            strTmp = strTmp.Replace("3", "_||_||__|_|_|");
            strTmp = strTmp.Replace("4", "_|_|__||_|_||");
            strTmp = strTmp.Replace("5", "_||_|__||_|_|");
            strTmp = strTmp.Replace("7", "_|_|__|_||_||");
            strTmp = strTmp.Replace("6", "_|_||__||_|_|");
            strTmp = strTmp.Replace("8", "_||_|__|_||_|");
            strTmp = strTmp.Replace("9", "_|_||__|_||_|");
            strTmp = strTmp.Replace("a", "_||_|_|__|_||");
            strTmp = strTmp.Replace("b", "_|_||_|__|_||");
            strTmp = strTmp.Replace("c", "_||_||_|__|_|");
            strTmp = strTmp.Replace("d", "_|_|_||__|_||");
            strTmp = strTmp.Replace("e", "_||_|_||__|_|");
            strTmp = strTmp.Replace("f", "_|_||_||__|_|");
            strTmp = strTmp.Replace("g", "_|_|_|__||_||");
            strTmp = strTmp.Replace("h", "_||_|_|__||_|");
            strTmp = strTmp.Replace("i", "_|_||_|__||_|");
            strTmp = strTmp.Replace("j", "_|_|_||__||_|");
            strTmp = strTmp.Replace("k", "_||_|_|_|__||");
            strTmp = strTmp.Replace("l", "_|_||_|_|__||");
            strTmp = strTmp.Replace("m", "_||_||_|_|__|");
            strTmp = strTmp.Replace("n", "_|_|_||_|__||");
            strTmp = strTmp.Replace("o", "_||_|_||_|__|");
            strTmp = strTmp.Replace("p", "_|_||_||_|__|");
            strTmp = strTmp.Replace("r", "_||_|_|_||__|");
            strTmp = strTmp.Replace("q", "_|_|_|_||__||");
            strTmp = strTmp.Replace("s", "_|_||_|_||__|");
            strTmp = strTmp.Replace("t", "_|_|_||_||__|");
            strTmp = strTmp.Replace("u", "_||__|_|_|_||");
            strTmp = strTmp.Replace("v", "_|__||_|_|_||");
            strTmp = strTmp.Replace("w", "_||__||_|_|_|");
            strTmp = strTmp.Replace("x", "_|__|_||_|_||");
            strTmp = strTmp.Replace("y", "_||__|_||_|_|");
            strTmp = strTmp.Replace("z", "_|__||_||_|_|");
            strTmp = strTmp.Replace("-", "_|__|_|_||_||");
            strTmp = strTmp.Replace("*", "_|__|_||_||_|");
            strTmp = strTmp.Replace("/", "_|__|__|_|__|");
            strTmp = strTmp.Replace("%", "_|_|__|__|__|");
            strTmp = strTmp.Replace("+", "_|__|_|__|__|");
            strTmp = strTmp.Replace(".", "_||__|_|_||_|");
            strTmp = strTmp.Replace("_", "<span   style='height:" + height + ";width:" + width + ";background:#FFFFFF;'></span>");
            strTmp = strTmp.Replace("|", "<span   style='height:" + height + ";width:" + width + ";background:#000000;'></span>");

            if (type_code == 1)
            {
                return strTmp + "<BR>" + code;
            }
            else
            {
                return strTmp;
            }
            #endregion
        }

        public static string ClearHtml(string HtmlString)
        {
            string pn = "(</?.*?/?>)";
            HtmlString = Regex.Replace(HtmlString, pn, "");
            return HtmlString;
        }

        public static string ClearFormat(string HtmlString)
        {
            HtmlString = HtmlString.Replace("\r\n", string.Empty).Replace(" ", string.Empty);
            return HtmlString.Trim();
        }

        #region Get N lines of data
        /// <summary>
        /// Retrieve a specified number of lines of data
        /// </summary>
        /// <param name="str">Input string to process</param>
        /// <param name="rowsnum">Number of lines to retrieve</param>
        /// <param name="strnum">Number of English characters or bytes per line</param>
        /// <returns></returns>
        public static string GetContent(string str, int rowsnum, int strnum)
        {
            // Step 1: Calculate content block          
            string content = str.Replace("\r\n", "§");
            string[] strContent = content.Split(Convert.ToChar("§"));

            int strCount = rowsnum * strnum;
            int cutrow = rowsnum - strContent.Length;
            cutrow = rowsnum > 10 ? rowsnum : 10;
            int pStrCount;
            string setOkStr = "";


            // Step 2: Process content block
            for (int i = 0; i < strContent.Length; i++)
            {
                pStrCount = System.Text.Encoding.Default.GetBytes(strContent[i]).Length;
                if (pStrCount < strCount)
                {
                    setOkStr += strContent[i] + "<br>";
                    rowsnum -= Convert.ToInt32(Math.Ceiling((double)pStrCount / (double)strnum));
                    strCount = rowsnum * strnum;
                }
                else
                {
                    if (rowsnum > 0)
                    {
                        setOkStr += CutStr(strContent[i], rowsnum * strnum, cutrow);
                    }
                    else
                    {
                        // Subtract rowsnum to avoid cases where some lines are 90 and others are 89
                        setOkStr = setOkStr.Substring(0, setOkStr.Length - cutrow / 2) + "...";
                    }
                    break;
                }
            }

            setOkStr = setOkStr.Replace("  ", "　"); // Convert soft (half-width) spaces to hard (full-width) spaces
            return setOkStr;

        }

        //String truncation function
        public static string CutStr(string str, int length, int rowsnum)
        {
            if (System.Text.Encoding.Default.GetBytes(str).Length < length)
                return str;

            length = length - rowsnum;
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                    i += 2;
                else
                    i++;
                if (i > length)
                {
                    str = str.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return str;

        }
        #endregion

        #region Get integer
        public static int ConvertToInt(string Str)
        {
            return Str.Trim() == string.Empty ? 0 : int.Parse(Str);
        }

        public static int GetInt(object o)
        {
            #region
            if (o == DBNull.Value || o == null)
                return 0;
            else
                return Convert.ToInt32(o);
            #endregion
        }
        #endregion

        /// <summary>
        /// convert to DateTime
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static object ConvertDate(object o)
        {
            DateTime dt;
            if (DateTime.TryParse(o.ToString(), out dt))
                return dt;
            else
                return DBNull.Value;
        }

        #region close HTML code
        public static string CloseHTML(string str)
        {
            string[] HtmlTag = new string[] { "p", "div", "span", "table", "ul", "font", "b", "u", "i", "a", "h1", "h2", "h3", "h4", "h5", "h6" };

            for (int i = 0; i < HtmlTag.Length; i++)
            {
                int OpenNum = 0, CloseNum = 0;
                Regex re = new Regex("<" + HtmlTag + "[^>]*" + ">", RegexOptions.IgnoreCase);
                MatchCollection m = re.Matches(str);
                OpenNum = m.Count;
                re = new Regex("</" + HtmlTag + ">", RegexOptions.IgnoreCase);
                m = re.Matches(str);
                CloseNum = m.Count;

                for (int j = 0; j < OpenNum - CloseNum; j++)
                {
                    str += "</" + HtmlTag + ">";
                }
            }

            return str;
        }
        #endregion

        /// <summary>
        ///  Get IP address in the format 192.248.23.*
        /// </summary>
        /// <param name="Str">IP address</param>
        /// <returns></returns>
        public static string GetSortIp(string Str)
        {
            int x = Str.LastIndexOf('.') - 1;
            return Str.Substring(0, x) + "*.*";
        }

        /// <summary>
        /// Get Year Month
        /// </summary>
        /// <returns></returns>
        public static string GetYearMonth()
        {
            return DateTime.Now.ToString("yyyyMM");
        }

        #region get remote page content
        public static string GetHttpData(string Url)
        {
            //string sException = null;
            string sRslt = null;
            WebResponse oWebRps = null;
            WebRequest oWebRqst = WebRequest.Create(Url);
            oWebRqst.Timeout = 50000;
            try
            {
                oWebRps = oWebRqst.GetResponse();
            }
            catch (WebException)
            {
                //sException = e.Message.ToString();
                //Response.Write(sException);
            }
            catch (Exception)
            {
                //sException = e.ToString();
                //Response.Write(sException);
            }
            finally
            {
                if (oWebRps != null)
                {
                    StreamReader oStreamRd = new StreamReader(oWebRps.GetResponseStream(), Encoding.GetEncoding("UTF-8"));//GB2312|UTF-8"
                    sRslt = oStreamRd.ReadToEnd();
                    oStreamRd.Close();
                    oWebRps.Close();
                }
            }
            return sRslt;
        }

        public string[] GetData(string Html)
        {
            String[] rS = new String[2];
            string s = Html;
            s = Regex.Replace(s, "\\s{3,}", "");
            s = s.Replace("\r", "");
            s = s.Replace("\n", "");
            string Pat = "<td align=\"center\" class=\"24p\"><B>(.*)</B></td></tr><tr>.*(<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"10\">.*</table>)<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">(.*)<td align=center class=l6h>";
            Regex Re = new Regex(Pat);
            Match Ma = Re.Match(s);
            if (Ma.Success)
            {
                rS[0] = Ma.Groups[1].ToString();
                rS[1] = Ma.Groups[2].ToString();
                //pgStr = Ma.Groups[3].ToString();
            }
            return rS;
        }
        #endregion

        /// <summary>
        /// check Url Exist
        /// </summary>
        /// <param name="sURL"></param>
        /// <returns></returns>
        public static bool UrlExist(string sURL)
        {
            #region
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
                //WebProxy   proxy   =   new   WebProxy("your   proxy   server",   8080);   
                //request.Proxy   =   proxy;   
                request.Method = "HEAD";
                request.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                bool result = false;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        result = true;
                        break;
                    case HttpStatusCode.Moved:
                        break;
                    case HttpStatusCode.NotFound:
                        break;
                }
                response.Close();
                return result;
            }
            catch
            {
                return false;
            }
            #endregion
        }

        #region Get connection string
        /// <summary>
        /// Get Page Url
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public static ArrayList GetPageUrl(string HtmlCode)
        {
            ArrayList my_list = new ArrayList();
            string p = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Regex re = new Regex(p, RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(HtmlCode);

            for (int i = 0; i <= mc.Count - 1; i++)
            {
                string name = mc[i].ToString();
                if (!my_list.Contains(name))//排除重复URL
                {
                    my_list.Add(name);
                }
            }
            return my_list;
        }
        #endregion


        /// <summary>
        /// convert Stream to string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertStreamToString(Stream s)
        {
            #region
            string strResult = "";
            StreamReader sr = new StreamReader(s, Encoding.UTF8);

            Char[] read = new Char[256];

            // Read 256 charcters at a time.    
            int count = sr.Read(read, 0, 256);

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console.
                string str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }


            // release resource
            sr.Close();

            return strResult;
            #endregion
        }

        /// <summary>
        /// Prevent SQL injection by sanitizing the input string
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>String</returns>
        public static string ConvertSql(string str)
        {
            #region
            str = str.Trim();
            str = str.Replace("'", "''");
            str = str.Replace(";--", "");
            str = str.Replace("=", "");
            str = str.Replace(" or ", "");
            str = str.Replace(" and ", "");

            return str;
            #endregion
        }

        /// <summary>
        /// formet size output
        /// </summary>
        /// <param name="size">size</param>
        /// <returns>return String</returns>
        public static string FormatNUM(long size)
        {
            #region
            decimal NUM;
            string strResult;

            if (size > 1073741824)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1073741824));
                strResult = NUM.ToString("N") + " M";
            }
            else if (size > 1048576)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1048576));
                strResult = NUM.ToString("N") + " M";
            }
            else if (size > 1024)
            {
                NUM = (Convert.ToDecimal(size) / Convert.ToDecimal(1024));
                strResult = NUM.ToString("N") + " KB";
            }
            else
            {
                strResult = size + " 字节";
            }

            return strResult;
            #endregion
        }

        /// <summary>
        /// combine string array into single string
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            #region
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
            #endregion
        }

        /// <summary>
        /// onvert Chinese characters to their pinyin initials, retaining non-Chinese characters as-is
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertSpellFirst(string text)
        {
            #region
            char pinyin;
            byte[] array;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                pinyin = c;
                array = Encoding.Default.GetBytes(new char[] { c });

                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];

                    #region condition match

                    if (i < 0xB0A1) pinyin = c;
                    else if (i < 0xB2C1) pinyin = 'b';
                    else if (i < 0xB4EE) pinyin = 'c';
                    else if (i < 0xB6EA) pinyin = 'd';
                    else if (i < 0xB7A2) pinyin = 'e';
                    else if (i < 0xB8C1) pinyin = 'f';
                    else if (i < 0xB9FE) pinyin = 'g';
                    else if (i < 0xBBF7) pinyin = 'h';
                    else if (i < 0xBFA6) pinyin = 'g';
                    else if (i < 0xC0AC) pinyin = 'k';
                    else if (i < 0xC2E8) pinyin = 'l';
                    else if (i < 0xC4C3) pinyin = 'm';
                    else if (i < 0xC5B6) pinyin = 'n';
                    else if (i < 0xC5BE) pinyin = 'o';
                    else if (i < 0xC6DA) pinyin = 'p';
                    else if (i < 0xC8BB) pinyin = 'q';
                    else if (i < 0xC8F6) pinyin = 'r';
                    else if (i < 0xCBFA) pinyin = 's';
                    else if (i < 0xCDDA) pinyin = 't';
                    else if (i < 0xCEF4) pinyin = 'w';
                    else if (i < 0xD1B9) pinyin = 'x';
                    else if (i < 0xD4D1) pinyin = 'y';
                    else if (i < 0xD7FA) pinyin = 'z';

                    #endregion
                }

                sb.Append(pinyin);
            }

            return sb.ToString();
            #endregion
        }

        public static string RandomNumber(string TxtName)
        {
            string RNumber = null;
            string DateString = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            switch (TxtName)
            {
                case "HT":
                    RNumber = "HT-" + DateString;
                    break;
                case "CP":
                    RNumber = "CP-" + DateString;
                    break;
                case "JL":
                    RNumber = "JL-" + DateString;
                    break;
                case "SP":
                    RNumber = "SP-" + DateString;
                    break;
                case "SJ":
                    RNumber = "SJ-" + DateString;
                    break;
                case "FP":
                    RNumber = "FP-" + DateString;
                    break;
                case "SH":
                    RNumber = "SH-" + DateString;
                    break;
                case "BJ":
                    RNumber = "BJ-" + DateString;
                    break;
                case "CG":
                    RNumber = "CG-" + DateString;
                    break;
                case "RK":
                    RNumber = "RK-" + DateString;
                    break;
                case "CK":
                    RNumber = "CK-" + DateString;
                    break;
                default:
                    break;
            }

            return RNumber;
        }

        /// <summary>
        /// close current window return values
        /// </summary>
        public static void CloseWindowReturnValues(string value)
        {
            System.Text.StringBuilder Str = new System.Text.StringBuilder();
            Str.Append("<Script language='JavaScript' type=\"text/javascript\">");
            Str.Append("var str='" + value + "';");
            Str.Append("top.returnValue=str;");
            Str.Append("top.close();</Script>");

            HttpContext.Current.Response.Write(Str.ToString());
            HttpContext.Current.Response.End();
        }
    }
}
