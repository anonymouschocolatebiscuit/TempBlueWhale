﻿using BlueWhale.DAL.BaseSet;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using BlueWhale.Common;
using System.Data;

namespace BlueWhale.UI.BaseSet
{
    public partial class ClientListAdd : BasePage
    {
        public ClientDAL dal = new ClientDAL();
        public UserDAL userDAL = new UserDAL();
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
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            ClientTypeDAL typesDal = new ClientTypeDAL();
            this.ddlVenderTypeList.DataSource = typesDal.GetList(isWhere);
            this.ddlVenderTypeList.DataTextField = "names";
            this.ddlVenderTypeList.DataValueField = "id";
            this.ddlVenderTypeList.DataBind();
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            dal.Id = id;

            DataSet ds = dal.GetModelById(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hfId.Value = ds.Tables[0].Rows[0]["id"].ToString();
                this.txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
                this.ddlVenderTypeList.SelectedValue = ds.Tables[0].Rows[0]["typeId"].ToString();
                this.txtYueDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["yueDate"].ToString()).ToShortDateString();
                this.txtPayNeed.Text = ds.Tables[0].Rows[0]["payNeed"].ToString();
                this.txtTax.Text = ds.Tables[0].Rows[0]["tax"].ToString();
                this.txtPayReady.Text = ds.Tables[0].Rows[0]["payReady"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                this.txtTaxNumber.Text = ds.Tables[0].Rows[0]["taxNumber"].ToString();
                this.txtBankName.Text = ds.Tables[0].Rows[0]["bankName"].ToString();
                this.txtBankNumber.Text = ds.Tables[0].Rows[0]["bankNumber"].ToString();
                this.txtDizhi.Text = ds.Tables[0].Rows[0]["dizhi"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            if (!CheckPower("ClientListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            string code = this.txtCode.Text;

            if (this.txtCode.Text.Trim() == "")
            {
                MessageBox.Show(this, "Please fill in the customer code！");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please fill in the customer name！");
                return;
            }

            dal.Id = id;
            dal.ShopId = LoginUser.ShopId;
            dal.Code = this.txtCode.Text.Trim();
            dal.Names = this.txtNames.Text;
            dal.TypeId = ConvertTo.ConvertInt(this.ddlVenderTypeList.SelectedValue.ToString());
            dal.YueDate = DateTime.Parse(this.txtYueDate.Text);
            dal.PayNeed = ConvertTo.ConvertDec(this.txtPayNeed.Text);
            dal.PayReady = ConvertTo.ConvertDec(this.txtPayReady.Text);
            dal.Tax = ConvertTo.ConvertInt(this.txtTax.Text);
            dal.Remarks = this.txtRemarks.Text;
            dal.MakeDate = DateTime.Now;
            dal.BankName = this.txtBankName.Text;
            dal.TaxNumber = this.txtTaxNumber.Text;
            dal.BankNumber = this.txtBankNumber.Text;
            dal.Dizhi = this.txtDizhi.Text;
            dal.Flag = "Save";
            dal.openId = "";
            dal.nickname = "";
            dal.headimgurl = "";
            dal.province = "";
            dal.country = "";
            dal.city = "";

            if (id == 0)
            {
                if (dal.isExistsCodeAdd(LoginUser.ShopId, this.txtCode.Text.Trim()))
                {
                    MessageBox.Show(this, "Add failed, customer number is duplicated！");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Add Customer：" + this.txtCode.Text + " Name：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "Execution successful！", "ClientListAdd.aspx?id=" + id.ToString());
                }
                else
                {
                    MessageBox.Show(this, "Add failure！");
                }
            }
            else
            {
                if (dal.isExistsCodeEdit(id, LoginUser.ShopId, this.txtCode.Text.Trim()))
                {
                    MessageBox.Show(this, "Modification failed, customer number is duplicated！");
                    return;
                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Modify Customer：" + this.txtCode.Text + " Name：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Execution successful！");
                }
                else
                {
                    MessageBox.Show(this, "Modification failed！");
                }
            }
        }
    }
}