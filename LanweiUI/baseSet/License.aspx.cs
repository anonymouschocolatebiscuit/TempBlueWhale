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
    public partial class License : BasePage
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
            DataSet ds = dal.GetAllModel();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.TextBox1.Text = ds.Tables[0].Rows[0]["company"].ToString();
                this.TextBox2.Text = ds.Tables[0].Rows[0]["address"].ToString();
                this.TextBox3.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                this.TextBox4.Text = ds.Tables[0].Rows[0]["fax"].ToString();
                this.TextBox5.Text = ds.Tables[0].Rows[0]["postCode"].ToString();




                this.txtDateStart.Text = ds.Tables[0].Rows[0]["dateStart"].ToString();
                this.txtDateEnd.Text = ds.Tables[0].Rows[0]["dateEnd"].ToString();
                this.txtUserNum.Text = "不限用户数";// ds.Tables[0].Rows[0]["userNum"].ToString();
                this.txtMsgNum.Text = ds.Tables[0].Rows[0]["msgNum"].ToString()+" 条";

              
            }


          
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            dal.Company = this.TextBox1.Text;
            dal.Address = this.TextBox2.Text;
            dal.Tel = this.TextBox3.Text;
            dal.Fax = this.TextBox4.Text;
            dal.PostCode = this.TextBox5.Text;

     
        }
    
    }
}
