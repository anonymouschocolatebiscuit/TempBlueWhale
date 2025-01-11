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
using System.Web.Services;
using System.Reflection;

using System.Collections.Generic;
using System.Web.Script.Serialization;

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;

using Lanwei.Weixin.UI.src;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class UsersListAdd : BasePage
    {


        ShopDAL shopDAL = new ShopDAL();
        DeptDAL deptDAL = new DeptDAL();
        RoleDAL roleDAL = new RoleDAL();

        UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                
                
          
                this.Bind();

            
            }
           
        }
        public void Bind()
        {
            this.txtBrithDay.Text = DateTime.Now.ToShortDateString();
            this.txtComeDate.Text = DateTime.Now.ToShortDateString();

            //this.ddlShopList.DataSource = shopDAL.GetAllShopList();
            //this.ddlShopList.DataTextField = "names";
            //this.ddlShopList.DataValueField = "id";
            //this.ddlShopList.DataBind();
           
            //this.ddlDeptList.DataSource = deptDAL.GetAllDept();
            //this.ddlDeptList.DataTextField = "deptName";
            //this.ddlDeptList.DataValueField = "deptId";
            //this.ddlDeptList.DataBind();


            //this.ddlRolesList.DataSource = roleDAL.GetAllRoleDataSet();
            //this.ddlRolesList.DataTextField = "roleName";
            //this.ddlRolesList.DataValueField = "roleId";
            //this.ddlRolesList.DataBind();

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                DataSet ds = dal.GetAllUserList(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();

                 

                    this.ddlFlagList.SelectedValue = ds.Tables[0].Rows[0]["flag"].ToString();
                    
                    this.txtTel.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                    this.txtPhone.Text = ds.Tables[0].Rows[0]["phone"].ToString();
                    this.txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    this.txtQQ.Text = ds.Tables[0].Rows[0]["qq"].ToString();
                    this.txtAddress.Value = ds.Tables[0].Rows[0]["address"].ToString();

                    this.txtBrithDay.Text =Convert.ToDateTime(ds.Tables[0].Rows[0]["brithDay"].ToString()).ToShortDateString();
                    this.txtComeDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["comeDate"].ToString()).ToShortDateString();

                    this.hfId.Value = id.ToString();

                }

            }


 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (this.txtPhone.Text == "")
            {
                MessageBox.Show(this, "请填写手机！");
                this.txtPhone.Focus();
                return;

            }


            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写姓名！");
                this.txtNames.Focus();
                return;

            }



            int id= ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;

            dal.ShopId = LoginUser.ShopId; //ConvertTo.ConvertInt(this.ddlShopList.SelectedValue.ToString());
            dal.LoginName = this.txtPhone.Text;
            dal.Names = this.txtNames.Text;
            dal.DeptId = 1;// ConvertTo.ConvertInt(this.ddlDeptList.SelectedValue.ToString());
            dal.RoleId = 1;// ConvertTo.ConvertInt(this.ddlRolesList.SelectedValue.ToString());
            dal.QQ = this.txtQQ.Text;
            dal.Tel = this.txtTel.Text;
            dal.Phone = this.txtPhone.Text;
            dal.Address = this.txtAddress.Value;
            dal.Email = this.txtEmail.Text;
            dal.MakeDate = DateTime.Now;
           
            dal.Pwd = "123456";
            dal.Pwds = "123456";

            dal.BrithDay = DateTime.Parse(this.txtBrithDay.Text);
            dal.ComeDate = DateTime.Parse(this.txtComeDate.Text);

            dal.Flag = this.ddlFlagList.SelectedValue.ToString();

            if (id == 0)
            {
                if (!CheckPower("UsersListAdd"))
                {
                    MessageBox.Show(this, "无此操作权限！");
                    return;
                }

                //切换数据库

             

                if (dal.isExistsUserName(this.txtPhone.Text))
                {
                    MessageBox.Show(this, "手机号：" + this.txtPhone.Text + "已经存在！");

                    return;

                }

                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增用户：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");

                }

            }
            else
            {
                if (!CheckPower("UsersListEdit"))
                {
                    MessageBox.Show(this, "无此操作权限！");
                    return;
                }

                if (dal.isExistsCodeEdit(id,this.txtPhone.Text))
                {
                    MessageBox.Show(this, "手机号：" + this.txtPhone.Text + "已经存在！");

                    return;

                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改用户：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");

                }
 
            }






        }

    }
}
