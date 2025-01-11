using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LanweiDAL;
using LanweiCommon;
using LanweiUI.src;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Management;
using System.Xml;

using LanweiDBUtility;


//using Senparc.Weixin.Work;

namespace LanweiWeb.BaseSet
{
    public partial class WeixinAPP : BasePage
    {
        public SystemSetDAL dal = new SystemSetDAL();
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("WeixinAPP"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtAdminURL.Enabled = false;
                this.txtAppURL.Enabled = false;

          
                this.Bind();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='"+LoginUser.ShopId+"' ";

            DataSet ds = dal.GetList(isWhere);

            if (ds.Tables[0].Rows.Count > 0)
            {

                string url = Request.Url.AbsoluteUri.ToString();

                string http = url.Substring(0, url.LastIndexOf("/") + 1).ToString();

                http = http.Replace("BaseSet/","");

                this.txtAppURL.Text = http + "WeixinQY/apiFW.aspx?weixinId=" + ds.Tables[0].Rows[0]["weixinId"].ToString();

                this.txtAdminURL.Text = http + "WeixinQYM/apiFW.aspx?weixinId=" + ds.Tables[0].Rows[0]["weixinId"].ToString();



                this.txtAppName.Text = ds.Tables[0].Rows[0]["appName"].ToString();

                this.txtWeixinId.Text = ds.Tables[0].Rows[0]["weixinId"].ToString();

                this.txtMchId.Text = ds.Tables[0].Rows[0]["mchId"].ToString();

                this.txtAppID.Text = ds.Tables[0].Rows[0]["appID"].ToString();
                this.txtAppSecret.Text = ds.Tables[0].Rows[0]["appSecret"].ToString();

                this.txtAppKeys.Text = ds.Tables[0].Rows[0]["appKey"].ToString();


                this.txtSendUrl.Text = ds.Tables[0].Rows[0]["SendUrl"].ToString();

                this.txtPayUrl.Text = ds.Tables[0].Rows[0]["PayUrl"].ToString();

                this.txtNotifyUrl.Text = ds.Tables[0].Rows[0]["NotifyUrl"].ToString();
              
            }


          
        }


       
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string appName = this.txtAppName.Text;

            string appId = this.txtAppID.Text;
            string appSecret = this.txtAppSecret.Text;

            string mchId = this.txtMchId.Text;
            string appKey = this.txtAppKeys.Text;

            string weixinId = this.txtWeixinId.Text;

            string sendUrl = this.txtSendUrl.Text;
            string payUrl = this.txtPayUrl.Text;
            string notifyUrl = this.txtNotifyUrl.Text;


            int add = dal.UpdateWeixinApp(LoginUser.ShopId,appName,weixinId,appId, appSecret, mchId, appKey,sendUrl, payUrl, notifyUrl);
            if (add > 0)
            {


                MessageBox.Show(this.Page, "操作成功！");

            }

            //数据库切换回来
           
            this.Bind();



        }
    
    }
}
