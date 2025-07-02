using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Data;

namespace BlueWhale.UI.baseSet
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
                    this.txtBrithDay.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["brithDay"].ToString()).ToShortDateString();
                    this.txtComeDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["comeDate"].ToString()).ToShortDateString();
                    this.hfId.Value = id.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtPhone.Text == "")
            {
                MessageBox.Show(this, "Please enter your phone number!");
                this.txtPhone.Focus();
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please enter your name!");
                this.txtNames.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());
            dal.Id = id;
            dal.ShopId = LoginUser.ShopId; // ConvertTo.ConvertInt(this.ddlShopList.SelectedValue.ToString());
            dal.LoginName = this.txtPhone.Text;
            dal.Names = this.txtNames.Text;
            dal.DeptId = 1; // ConvertTo.ConvertInt(this.ddlDeptList.SelectedValue.ToString());
            dal.RoleId = 1; // ConvertTo.ConvertInt(this.ddlRolesList.SelectedValue.ToString());
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
                    MessageBox.Show(this, "No permission for this operation!");
                    return;
                }

                if (dal.isExistsUserName(this.txtPhone.Text))
                {
                    MessageBox.Show(this, "Phone number: " + this.txtPhone.Text + " already exists!");
                    return;
                }

                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Create User: " + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();
                    MessageBox.Show(this, "Execution successful!");
                }
            }
            else
            {
                if (!CheckPower("UsersListEdit"))
                {
                    MessageBox.Show(this, "No permission for this operation!");
                    return;
                }

                if (dal.isExistsCodeEdit(id, this.txtPhone.Text))
                {
                    MessageBox.Show(this, "Phone number: " + this.txtPhone.Text + " already exists!");
                    return;
                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edit User: " + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();
                    MessageBox.Show(this, "Execution successful!");
                }
            }
        }
    }
}
