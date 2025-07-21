using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.BaseSet
{
    public partial class VenderLinkMan : BasePage
    {
        public VenderLinkManDAL dal = new VenderLinkManDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("VenderListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }



                this.Bind();
            }
        }
        public void Bind()
        {

            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            dal.PId = id;

            this.gvLevel.DataSource = dal.GetMolderByPId();
            this.gvLevel.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (!CheckPower("VenderListAdd"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            dal.PId = id;
            dal.Names = this.txtNames.Text;
            dal.Phone = this.txtPhone.Text;
            dal.Tel = this.txtTel.Text;
            dal.Defaults = ConvertTo.ConvertInt(this.ddlMoren.SelectedValue.ToString());

            //if (dal.isExistsNames(id,this.txtNames.Text))
            //{
            //    MessageBox.Show(this, "操作失败，联系人姓名重复！");
            //    return;
            //}

            if (dal.Add() > 0)
            {
                LogsDAL logs = new LogsDAL();

                logs.ShopId = LoginUser.ShopId;
                logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "新增供应商联系人 姓名：" + this.txtNames.Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.ShowAndRedirect(this, "操作成功！", "VenderLinkMan.aspx?id=" + id.ToString());
            }



        }

        protected void gvLevel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvLevel.EditIndex = -1;
            this.Bind();
        }

        protected void gvLevel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)//点击Gridview行,选中/取消选中,当前行中的Checkbox
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('您确认要删除吗?')");

                    if (((Label)e.Row.Cells[4].FindControl("lbMoren")).Text == "1")
                    {
                        e.Row.Cells[4].Text = "是";
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


            if (!CheckPower("VenderListDelete"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }


            int Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());
            int t = dal.Delete(Id);
            if (t > 0)
            {
                this.Bind();

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;
                logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "删除供应商联系人：" + Id.ToString();
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();
            }
        }

        protected void gvLevel_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvLevel.EditIndex = e.NewEditIndex;
            this.Bind();
        }

        protected void gvLevel_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            if (!CheckPower("VenderListEdit"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }



            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            dal.Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());

            dal.PId = id;
            dal.Names = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtNames0"))).Text.ToString().Trim();
            dal.Phone = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtPhone0"))).Text.ToString().Trim();
            dal.Tel = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtTel0"))).Text.ToString().Trim();
            //dal.QQ = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtQQ0"))).Text.ToString().Trim();
            dal.Defaults = ConvertTo.ConvertInt(((DropDownList)(this.gvLevel.Rows[e.RowIndex].FindControl("ddlMoren0"))).SelectedValue.ToString());


            int t = dal.Update();

            if (t > 0)
            {

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "修改供应商联系人：" + this.gvLevel.Rows[e.RowIndex].Cells[0].Text.ToString();
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();



                this.Bind();

            }
            this.gvLevel.EditIndex = -1;
            this.Bind();
        }
    }
}
