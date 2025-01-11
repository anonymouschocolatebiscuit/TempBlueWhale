using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.Containers;

using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.App;

using System.Text;
using LanweiUI.src;
using LanweiCommon;
using System.Data;
using LanweiDAL;


namespace LanweiWeb.BaseSet
{
    public partial class DingdingSet : BasePage
    {
        public SystemSetDAL dal = new SystemSetDAL();
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SystemSet"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

          
                this.Bind();
            }
        }

        public void Bind()
        {
            DataSet ds = dal.GetDingdingSet();
            if (ds.Tables[0].Rows.Count > 0)
            {

              
             
                this.txtAppID.Text = ds.Tables[0].Rows[0]["corpId"].ToString();
                this.txtAppSecret.Text = ds.Tables[0].Rows[0]["corpSecret"].ToString();

              
              
            }

            if (ds.Tables[0].Rows.Count == 0)
                return;


            var accessToken = AccessTokenContainer.GetToken(LoginUser.CorpIdQY, LoginUser.CorpSecretQY);


            List<GetAppList_AppInfo> applist = AppApi.GetAppList(accessToken).agentlist;

            DataTable dt = new DataTable();
            dt.Columns.Add("agentid");
            dt.Columns.Add("name");
            dt.Columns.Add("round_logo_url");
            dt.Columns.Add("square_logo_url");



            foreach (var list in applist)
            {
                DataRow dr = dt.NewRow();
                dr["agentid"] = list.agentid;
                dr["name"] = list.name;
                dr["round_logo_url"] = list.round_logo_url;//部门ID
                dr["square_logo_url"] = list.square_logo_url;

                dt.Rows.Add(dr);
            }

            this.dlList.DataSource = dt;
            this.dlList.DataBind();


          
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int appId = 0;
            string appName = "";

            string corpId = this.txtAppID.Text;
            string corpSecret = this.txtAppSecret.Text;

            for (int i = 0; i < this.dlList.Items.Count; i++)
            {
                RadioButton cb = (RadioButton)dlList.Items[i].FindControl("appId");
                if (cb.Checked)
                {
                    appId = ConvertTo.ConvertInt(((HiddenField)dlList.Items[i].FindControl("hidId")).Value);
                    appName = ((HiddenField)dlList.Items[i].FindControl("hidName")).Value.ToString();
                   
                }
 
            }

            if (appName == "")
            {
                MessageBox.Show(this.Page, "请选择关联应用！");
                return;
            }
              
                if (corpId == "")
            {
                MessageBox.Show(this.Page, "请填写corpId！");
                return;
            }

            if (corpSecret == "")
            {
                MessageBox.Show(this.Page, "请填写corpSecret！");
                return;
            }


            int add = dal.UpdateDingdingSet(appId,appName,corpId, corpSecret);
            if (add > 0)
            {
                MessageBox.Show(this.Page, "操作成功！");
            }
            else
            {
                MessageBox.Show(this.Page, "操作失败！");
            }


            this.Bind();

        }
    
    }
}
