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

namespace Lanwei.Weixin.UI.BaseSet
{
   
    public partial class AccountListAdd : BasePage
    {
        public AccountDAL dal = new AccountDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          

            if (!this.IsPostBack)
            {
               
                this.txtCode.Focus();
                this.txtYueDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();
            }
        }

        public void Bind()
        {

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());



                DataSet ds = dal.GetAllModel(id);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    this.txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                    this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
                    this.txtYueDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["yueDate"].ToString()).ToShortDateString();

                    this.txtYuePrice.Text = ds.Tables[0].Rows[0]["yuePrice"].ToString();

                    this.ddlTypes.SelectedValue = ds.Tables[0].Rows[0]["types"].ToString();


                }
            }           
            else
            {

               
            }
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (Request.QueryString.Count > 0)
            {
                id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            }

            if (!CheckPower("AccountListAdd"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }


            if (this.txtCode.Text == "")
            {
                MessageBox.Show(this, "请填写编号！");
                this.txtCode.Focus();
                return;

            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写名称！");
                this.txtNames.Focus();
                return;

            }
            
            
            dal.Code = this.txtCode.Text;
            dal.Names = this.txtNames.Text;
            dal.ShopId = LoginUser.ShopId;


            if (id == 0)
            {

                if (dal.isExistsCodeAdd(LoginUser.ShopId,this.txtCode.Text))
                {
                    MessageBox.Show(this, "新增失败，账户编码重复！");
                    return;
                }
                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "新增失败，账户名称重复！");
                    return;
                }

            }
            else
            {
                if (dal.isExistsCodeEdit(id, LoginUser.ShopId, this.txtCode.Text))
                {
                    MessageBox.Show(this, "修改失败，账户编码重复！");
                    return;
                }
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，账户名称重复！");
                    return;
                }
 
            }



            dal.YueDate = DateTime.Parse(this.txtYueDate.Text);
            dal.YuePrice = ConvertTo.ConvertDec(this.txtYuePrice.Text);
            dal.Types = this.ddlTypes.SelectedValue.ToString();
            dal.Id = id;

            if (id == 0)
            {
                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增账户：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();


                    MessageBox.Show(this, "操作成功！");

                }
            }
            else
            {
                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改账户：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();


                    MessageBox.Show(this, "操作成功！");

                }
            }

        }
    }
}
