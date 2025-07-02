using System;
using System.Web.UI.WebControls;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.BaseSet
{
    public partial class ClientLinkMan : BasePage
    {
        public ClientLinkManDAL dal = new ClientLinkManDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Bind();
            }
        }

        public void Bind()
        {
            int id =ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            dal.PId = id;

            this.gvLevel.DataSource = dal.GetMolderByPId();
            this.gvLevel.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            dal.PId = id;
            dal.Names = this.txtNames.Text;
            dal.Phone = this.txtPhone.Text;
            dal.Tel =this.txtTel.Text;
            dal.Address = this.txtAddress.Text;
            dal.Moren = ConvertTo.ConvertInt(this.ddlMoren.SelectedValue.ToString());

            if (dal.isExistsNames(id,this.txtNames.Text))
            {
                MessageBox.Show(this, "Operation failed, contact name is duplicated！");
                return;
            }

            if (dal.Add() > 0)
            {
                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "Add new customer contact name：" + this.txtNames.Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.ShowAndRedirect(this, "Operation Successful！", "ClientLinkMan.aspx?id=" + id);
            }
        }

        protected void gvLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvLevel.EditIndex = -1;
            this.Bind();
        }

        protected void gvLevel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete?')");

                    if (((Label)e.Row.Cells[4].FindControl("lbMoren")).Text == "1")
                    {
                        e.Row.Cells[4].Text = "Yes";
                    }
                    else
                    {
                        e.Row.Cells[4].Text = "";
                    }
                }
            }
        }

        protected void gvLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());
            int t = dal.Delete(Id);
            if (t > 0)
            {
                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "Deleting a Customer Contact：" + Id.ToString();
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();
                this.Bind();
            }
        }

        protected void gvLevel_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvLevel.EditIndex = e.NewEditIndex;
            this.Bind();
        }

        protected void gvLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            dal.Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());
            dal.PId =ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            dal.Names = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtNames0"))).Text.ToString().Trim();
            dal.Phone = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtPhone0"))).Text.ToString().Trim();
            dal.Tel = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtTel0"))).Text.ToString().Trim();
            dal.Address = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtAddress0"))).Text.ToString().Trim();
            dal.Moren = ConvertTo.ConvertInt(((DropDownList)(this.gvLevel.Rows[e.RowIndex].FindControl("ddlMoren0"))).SelectedValue.ToString());

            int t = dal.Update();

            if (t > 0)
            {

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "Modify customer contact：" + this.gvLevel.Rows[e.RowIndex].Cells[0].Text.ToString();
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();
                this.Bind();
            }
            this.gvLevel.EditIndex = -1;
            this.Bind();
        }
    }
}
