using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using Org.BouncyCastle.Asn1.Ocsp;
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
				MessageBox.Show(this, "No permission to add new contact!");
				return;
			}

			int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

			dal.PId = id;
			dal.Names = this.txtNames.Text;
			dal.Phone = this.txtPhone.Text;
			dal.Tel = this.txtTel.Text;
			dal.Defaults = ConvertTo.ConvertInt(this.ddlDefault.SelectedValue.ToString());

			if (dal.isExistsNames(id, this.txtNames.Text))
			{
				MessageBox.Show(this, "Operation failed, duplicate contact name!");
				return;
			}

			if (dal.Add() > 0)
			{
				LogsDAL logs = new LogsDAL();

				logs.ShopId = LoginUser.ShopId;
				logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
				logs.Events = "New vender contact added, name :" + this.txtNames.Text;
				logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
				logs.Add();

				MessageBox.ShowAndRedirect(this, "Operation Success!", "VenderLinkMan.aspx?id=" + id.ToString());
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
					((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you confirm?')");

					if (((Label)e.Row.Cells[4].FindControl("lbDefault")).Text == "1")
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


			if (!CheckPower("VenderListDelete"))
			{
				MessageBox.Show(this, "No permission to delete!");
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
				logs.Events = "Delete Supplier Contact：" + Id.ToString();
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
				MessageBox.Show(this, "No permisson to udpate!");
				return;
			}



			int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
			dal.Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());

			dal.PId = id;
			dal.Names = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtNames0"))).Text.ToString().Trim();
			dal.Phone = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtPhone0"))).Text.ToString().Trim();
			dal.Tel = ((TextBox)(this.gvLevel.Rows[e.RowIndex].FindControl("txtTel0"))).Text.ToString().Trim();
			dal.Defaults = ConvertTo.ConvertInt(((DropDownList)(this.gvLevel.Rows[e.RowIndex].FindControl("ddlDefault0"))).SelectedValue.ToString());


			int t = dal.Update();

			if (t > 0)
			{
				LogsDAL logs = new LogsDAL();
				logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
				logs.Events = "Update Supplier Contact:" + this.gvLevel.Rows[e.RowIndex].Cells[0].Text.ToString();
				logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
				logs.Add();

				this.Bind();

			}
			this.gvLevel.EditIndex = -1;
			this.Bind();
		}
	}
}
