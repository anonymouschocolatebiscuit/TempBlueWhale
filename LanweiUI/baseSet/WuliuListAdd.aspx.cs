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


using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;

using Lanwei.Weixin.UI.src;


namespace Lanwei.Weixin.UI.baseSet
{
    public partial class WuliuListAdd : BasePage
    {
        public WuliuDAL dal = new WuliuDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {



                //if (!CheckPower("WuliuList"))
                //{
                //    MessageBox.Show(this, "无此操作权限！");
                //    return;
                //}

               


                this.Bind();
            }
        }
        public void Bind()
        {


            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            if (id == 0)//新增
            {
                this.Title = "新增物流公司";
            }
            else
            {
                this.Title = "修改物流公司";
            }

            this.ddlPrintModel.DataSource = dal.GetWuliuCodeList();

            this.ddlPrintModel.DataTextField = "names";
            this.ddlPrintModel.DataValueField = "code";
            this.ddlPrintModel.DataBind();

            DataSet ds = dal.GetModelById(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hf.Value = ds.Tables[0].Rows[0]["id"].ToString();
                this.txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();



                this.txtLinkMan.Text = ds.Tables[0].Rows[0]["linkMan"].ToString();
                this.txtTel.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                this.txtPhone.Text = ds.Tables[0].Rows[0]["phone"].ToString();
                this.txtFax.Text = ds.Tables[0].Rows[0]["fax"].ToString();
                this.txtAddress.Text = ds.Tables[0].Rows[0]["address"].ToString();



                this.ddlMallList.SelectedValue = ds.Tables[0].Rows[0]["mall"].ToString();
                this.ddlPrintModel.SelectedValue = ds.Tables[0].Rows[0]["printModel"].ToString();



            }
            else
            {

                this.ddlPrintModel.SelectedValue = "0";
                this.ddlMallList.SelectedValue = "0";

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            //if (!CheckPower("WuliuList"))
            //{
            //    MessageBox.Show(this, "无此操作权限！");
            //    return;
            //}

            if (this.txtCode.Text=="")
            {
                MessageBox.Show(this, "请填写编号！");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写名称！");
                return;
            }


            dal.Id = ConvertTo.ConvertInt(this.hf.Value.ToString());
            dal.ShopId = LoginUser.ShopId;
            dal.Code = this.txtCode.Text;
            dal.Names = this.txtNames.Text;
            dal.Tel = this.txtTel.Text;
            dal.LinkMan = this.txtLinkMan.Text;
            dal.Phone = this.txtPhone.Text;
            dal.Fax = this.txtFax.Text;
            dal.Address = this.txtAddress.Text;
            dal.Mall = this.ddlMallList.SelectedValue.ToString();
            dal.PrintModel = this.ddlPrintModel.SelectedValue.ToString();
            dal.MakeId = LoginUser.Id;

            if (id.ToString() == "0")//Add
            {
                if (dal.isExistsCodeAdd(LoginUser.ShopId,this.txtCode.Text))
                {
                    MessageBox.Show(this, "新增失败，公司编码重复！");
                    return;
                }
                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "新增失败，公司名称重复！");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增物流公司：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "操作成功！", "WuliuListAdd.aspx?id=" + id.ToString());
                }



            }
            else //Edit
            {
                if (dal.isExistsCodeEdit(ConvertTo.ConvertInt(this.hf.Value.ToString()), LoginUser.ShopId, this.txtCode.Text))
                {
                    MessageBox.Show(this, "修改失败，编码重复！");
                    return;
                }
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，名称重复！");
                    return;
                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改物流公司：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "操作成功！", "WuliuListAdd.aspx?id=" + id.ToString());
                }

            }





        }




    }
}
