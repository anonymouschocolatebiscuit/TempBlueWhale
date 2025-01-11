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
    public class BasePageAppId :Page //VersionContorl //Page
    {

        private string errmessagecode = "";
    
        public SystemSetDAL setDAL = new SystemSetDAL();

        public string appName = SQLHelper.appName;

        
        public SystemSetModel SysInfo
        {
            get {


                SystemSetModel sys = new SystemSetModel();

                DataSet ds = setDAL.GetAllModel();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    sys.Company = ds.Tables[0].Rows[0]["Company"].ToString();

                    sys.Address = ds.Tables[0].Rows[0]["address"].ToString();
                    sys.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                    sys.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                    sys.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                    
                    sys.DateStart = DateTime.Parse(ds.Tables[0].Rows[0]["DateStart"].ToString());
                  

                    sys.Bwb = ds.Tables[0].Rows[0]["address"].ToString();

                    sys.Num = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Num"].ToString());
                    sys.Price = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Price"].ToString());
                    sys.PriceType = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["PriceType"].ToString());

                    sys.CheckNum =ConvertTo.ConvertInt( ds.Tables[0].Rows[0]["CheckNum"].ToString());

                    sys.UseCheck = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["UseCheck"].ToString());

                    sys.Tax =ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["Tax"].ToString());


                  

           
                 





                    sys.FieldA = ds.Tables[0].Rows[0]["FieldA"].ToString();
                    sys.FieldB = ds.Tables[0].Rows[0]["FieldB"].ToString();
                    sys.FieldC = ds.Tables[0].Rows[0]["FieldC"].ToString();
                    sys.FieldD = ds.Tables[0].Rows[0]["FieldD"].ToString();
                  
 
                }

                return sys;

            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!checkvalue())
            {
                string returnurl = "";
                if (errmessagecode == "1")
                {
                    returnurl = Constant.OverWeixinBrowser + "?" + Constant.OverWeixinBrowser + "=" + Server.HtmlEncode(Constant.OverTime);
                }
                else
                {
                    returnurl = Constant.OverWeixinBrowser + "?" + Constant.OverWeixinBrowser + "=" + Server.HtmlEncode(Constant.OverTime);
                    Response.Redirect(returnurl, true);

                }

            }
        }


        #region 验证

        protected virtual bool checkvalue()
        {

            string userAgent = Request.UserAgent;
            if (userAgent.ToLower().Contains("micromessenger"))
            {
                //Response.Write("欢迎您在微信中访问我。");

                this.errmessagecode = "1"; return false;

            }
            else
            {
                //Response.Write("请在微信中访问本页。");
                return false;
            }


            //if (clientId != 0) //条件成立、是合法用户
            //{
            //    this.errmessagecode = "1"; return false;
            //}
            //else
            //{
            //    return false;
            //}
        }
        #endregion


    }
}

