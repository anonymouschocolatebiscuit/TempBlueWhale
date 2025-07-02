using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.Model;
using System;
using System.Data;
using System.Web.UI;

namespace BlueWhale.UI.src
{
    public class BasePage : Page // Version Control //Page
    {

        public UserDAL powerDAL = new UserDAL();
        public SystemSetDAL setDAL = new SystemSetDAL();

        public Users LoginUser
        {
            get { return Session["userInfo"] as Users; }
        }

        public SystemSetModel SysInfo
        {
            get
            {

                SystemSetModel sys = new SystemSetModel();

                string isWhere = " shopId='" + LoginUser.ShopId + "' ";
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

                    sys.CheckNum = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["CheckNum"].ToString());

                    sys.UseCheck = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["UseCheck"].ToString());

                    sys.Tax = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Tax"].ToString());

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

        protected override void OnInit(EventArgs e)
        {
            if (LoginUser == null || LoginUser.Id < 0)
            {
                Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
        }

        #region Check Store Number

        /// <summary>
        /// Check Store Number
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
            if (checkNum == "1")
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

        #region Determine Permission

        /// <summary>
        /// Determine Permission
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckPower(string url)
        {
            return powerDAL.CheckPowerByUserIdAndUrl(LoginUser.Id, url);
        }

        #endregion

        #region JS Alert============================================
        /// <summary>
        /// Add,Edit,Delete Alert
        /// </summary>
        /// <param name="msgtitle">Alert Title</param>
        /// <param name="url">Navigate Url</param>
        /// <param name="msgcss">CSS Pattern</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        /// <summary>
        /// Add, edit, and delete prompts with callback function
        /// </summary>
        /// <param name="msgtitle">Alert Title</param>
        /// <param name="url">Navigate Url</param>
        /// <param name="msgcss">CSS Pattern</param>
        /// <param name="callback">JS callback function</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        #endregion
    }
}