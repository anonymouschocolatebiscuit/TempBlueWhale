using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.Model;

namespace Lanwei.Weixin.UI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
        }

    

        public SystemSetDAL setDAL = new SystemSetDAL();

       

        /// <summary>
        /// 注册Demo所用微信公众号的账号信息
        /// </summary>
        private void RegisterLanweiWeixin()
        {
           
            weixinQYInfo sys = new weixinQYInfo();
            DataSet ds = setDAL.GetWeixinQYSet();
            if (ds.Tables[0].Rows.Count > 0)
            {              
                string CorpId = ds.Tables[0].Rows[0]["CorpIdQY"].ToString();
                string CorpSecret = ds.Tables[0].Rows[0]["CorpSecretQY"].ToString();

                string SecretBuy = ds.Tables[0].Rows[0]["SecretBuy"].ToString();
                string SecretSales = ds.Tables[0].Rows[0]["SecretSales"].ToString();
                string SecretStore = ds.Tables[0].Rows[0]["SecretStore"].ToString();
                string SecretFee = ds.Tables[0].Rows[0]["SecretFee"].ToString();


                try
                {
                    if (CorpSecret != "")
                    {
                      

                    }

                    if (SecretBuy != "")
                    {
                       

                    }
                    if (SecretSales != "")
                    {
                       

                    }
                    if (SecretStore != "")
                    {
                    

                    }
                    if (SecretFee != "")
                    {
                        

                    }
                   
                }
                catch (Exception ex)
                {
                    Response.Write("Exception:" + ex.Message.ToString());

                };

            }

           

          
        }

        /// <summary>
        /// 注册阿里钉钉的账号信息
        /// </summary>
        private void RegisterLanweiDingding()
        {

            alibabaDDInfo sys = new alibabaDDInfo();
            DataSet ds = setDAL.GetDingdingSet();
            if (ds.Tables[0].Rows.Count > 0)
            {

                string CorpId = ds.Tables[0].Rows[0]["CorpId"].ToString();
                string CorpSecret = ds.Tables[0].Rows[0]["CorpSecret"].ToString();

               

            }

          
        }

        /// <summary>
        /// 注册微信支付
        /// </summary>
        private void RegisterWeixinPay()
        {
            

        }

        /// <summary>
        /// 注册微信第三方平台
        /// </summary>
        private void RegisterWeixinThirdParty()
        {
           

        }
    }
}
