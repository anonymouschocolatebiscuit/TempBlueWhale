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
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;


namespace BlueWhale.UI.BaseSet
{

	public partial class VenderListAdd : BasePage
	{
		public VenderDAL dal = new VenderDAL();

		protected void Page_Load(object sender, EventArgs e)
		{


			if (!this.IsPostBack)
			{
				//if (!CheckPower("VenderListAdd"))
				//{
				//    Response.Redirect("../OverPower.htm");
				//}


				this.txtCode.Focus();
				this.txtDueDate.Text = DateTime.Now.ToShortDateString();

				this.Bind();
			}
		}
		public void Bind()
		{
			VenderTypeDAL typesDal = new VenderTypeDAL();

			string isWhere = " shopId='" + LoginUser.ShopId + "' ";
			DataSet dsType = typesDal.GetList(isWhere);


			this.ddlVenderTypeList.DataSource = dsType;
			this.ddlVenderTypeList.DataTextField = "names";
			this.ddlVenderTypeList.DataValueField = "id";
			this.ddlVenderTypeList.DataBind();



			int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());



			dal.Id = id;

			DataSet ds = dal.GetModelByCode();
			if (ds.Tables[0].Rows.Count > 0)
			{
				this.hfId.Value = ds.Tables[0].Rows[0]["id"].ToString();
				this.txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
				this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
				this.ddlVenderTypeList.SelectedValue = ds.Tables[0].Rows[0]["typeId"].ToString();
				this.txtDueDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["yueDate"].ToString()).ToShortDateString();
				this.txtPayNeed.Text = ds.Tables[0].Rows[0]["payNeed"].ToString();
				this.txtTax.Text = ds.Tables[0].Rows[0]["tax"].ToString();
				this.txtPayReady.Text = ds.Tables[0].Rows[0]["payReady"].ToString();
				this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

				this.txtTaxNumber.Text = ds.Tables[0].Rows[0]["taxNumber"].ToString();
				this.txtBankName.Text = ds.Tables[0].Rows[0]["bankName"].ToString();
				this.txtBankNumber.Text = ds.Tables[0].Rows[0]["bankNumber"].ToString();
				this.txtAddress.Text = ds.Tables[0].Rows[0]["dizhi"].ToString();

			}


		}





		protected void btnSave_Click(object sender, EventArgs e)
		{


			int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

			if (!CheckPower("VenderListAdd"))
			{
				MessageBox.Show(this, "No permssion for this action!");
				return;
			}


			string code = this.txtCode.Text;

			if (this.txtCode.Text.Trim() == "")
			{
				MessageBox.Show(this, "Please enter vender number!");
				return;
			}

			if (this.txtNames.Text == "")
			{
				MessageBox.Show(this, "Please enter vender name!");
				return;
			}


			dal.Id = id;
			dal.ShopId = LoginUser.ShopId;
			dal.Code = this.txtCode.Text.Trim();
			dal.Names = this.txtNames.Text;
			dal.TypeId = ConvertTo.ConvertInt(this.ddlVenderTypeList.SelectedValue.ToString());
			dal.DueDate = DateTime.Parse(this.txtDueDate.Text);
			dal.PayNeed = ConvertTo.ConvertDec(this.txtPayNeed.Text);
			dal.PayReady = ConvertTo.ConvertDec(this.txtPayReady.Text);
			dal.Tax = ConvertTo.ConvertInt(this.txtTax.Text);
			dal.Remarks = this.txtRemarks.Text;

			dal.MakeDate = DateTime.Now;
			dal.BankName = this.txtBankName.Text;
			dal.TaxNumber = this.txtTaxNumber.Text;
			dal.BankNumber = this.txtBankNumber.Text;
			dal.Address = this.txtAddress.Text;

			dal.Flag = "Save";

				if (dal.isExistsCodeAdd(LoginUser.ShopId, this.txtCode.Text))
				{
					MessageBox.Show(this, "Add failed, vender number repeat!");
					return;
				}
				if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
				{
					MessageBox.Show(this, "Add failed, vender name repeat!");
					return;
				}

				if (dal.Add() > 0)
				{

					LogsDAL logs = new LogsDAL();

					logs.ShopId = LoginUser.ShopId;
					logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
					logs.Events = "Add Supplier:" + this.txtCode.Text + " Name: " + this.txtNames.Text;
					logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
					logs.Add();

					MessageBox.ShowAndRedirect(this, "Operation Successful!", "VenderListAdd.aspx?id=" + id.ToString());
				}

		}
	}
}
