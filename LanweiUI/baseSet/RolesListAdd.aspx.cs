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

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class RolesListAdd : BasePage
    {
        public RoleDAL dal = new RoleDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                this.BindDetail();

            }
        }

        public void BindDetail()
        {

            this.txtNames.Focus();

            if (Request.QueryString.Count > 0)
            {

                this.hfId.Value = Request.QueryString["roleId"].ToString();
                this.hfParentId.Value = Request.QueryString["parentId"].ToString();
                this.txtNames.Text = Request.QueryString["roleName"].ToString();
                this.txtFlag.Text = Request.QueryString["flag"].ToString();
                this.txtParentName.Text = Request.QueryString["parentName"].ToString();


                if (this.txtParentName.Text == "")//修改的
                {
                    this.txtParentName.Text = "顶级角色";
                }


            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (CheckPower("RolesListAdd"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

          
            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写名称！");
                this.txtNames.Focus();
                return;

            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.RoleId = id;
            dal.Flag = ConvertTo.ConvertInt(this.txtFlag.Text);
            dal.RoleName = this.txtNames.Text;
            dal.ParentId = ConvertTo.ConvertInt(this.hfParentId.Value.ToString());

            if (id == 0)
            {

               
                if (dal.isExistsNamesAdd(this.txtNames.Text))
                {
                    MessageBox.Show(this, "新增失败，名称重复！");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增角色：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }

            }
            else
            {

             

                if (dal.isExistsNamesEdit(id, this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，名称重复！");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改角色：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }
            }




        }
    }
}
