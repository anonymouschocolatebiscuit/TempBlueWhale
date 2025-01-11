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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Management;
using System.Xml;

namespace LanweiWeb.BaseSet
{
    public partial class NoticeInfo : BasePage
    {
        public SystemSetDAL dal = new SystemSetDAL();
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("SystemSet"))
                {
                    Response.Redirect("../OverPower.htm");
                }

          
                this.Bind();
            }
        }

        public void Bind()
        {
            DataSet ds = dal.GetAllModelContents();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtContents.Text = ds.Tables[0].Rows[0]["contents"].ToString();
              
            }


          
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string contents = this.txtContents.Text;

            int add = dal.UpdateNoticeInfo(contents);
            if (add > 0)
            {
                MessageBox.Show(this.Page,"操作成功！");
            }


            this.Bind();

        }
    
    }
}
