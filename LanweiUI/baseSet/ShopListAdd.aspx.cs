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


namespace Lanwei.Weixin.UI.baseSet
{
    public partial class ShopListAdd : BasePage
    {

     

        ShopDAL dal = new ShopDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
              
                this.Bind();

            
            }
           
        }
        public void Bind()
        {
        

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                DataSet ds = dal.GetAllShopList(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.txtLoginName.Text = ds.Tables[0].Rows[0]["code"].ToString();
                    this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
                 
                    this.txtTel.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                    this.txtPhone.Text = ds.Tables[0].Rows[0]["fax"].ToString();
         
                    this.txtAddress.Value = ds.Tables[0].Rows[0]["address"].ToString();

                    this.ddlFlagList.SelectedValue = ds.Tables[0].Rows[0]["flag"].ToString();
                   
                    this.hfId.Value = id.ToString();

                }

            }


 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (this.txtLoginName.Text == "")
            {
                MessageBox.Show(this, "请填写编号！");
                this.txtLoginName.Focus();
                return;

            }


          
            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写名称！");
                this.txtNames.Focus();
                return;

            }



            int id= ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;

            dal.Code = this.txtLoginName.Text;
            dal.Names = this.txtNames.Text;
         
         
            dal.Tel = this.txtTel.Text;
         
            dal.Fax = this.txtPhone.Text;
            dal.Address = this.txtAddress.Value;
      
            dal.MakeDate = DateTime.Now;
            dal.Flag = this.ddlFlagList.SelectedValue.ToString();

        

    

            if (id == 0)
            {
                //if (CheckPower("ShopListAdd"))
                //{
                //    MessageBox.Show(this, "无此操作权限！");
                //    return;
                //}

                if (dal.isExistsCodeAdd(this.txtLoginName.Text))
                {
                    MessageBox.Show(this, "编号：" + this.txtLoginName.Text + "已经存在！");

                    return;

                }

                if (dal.isExistsNamesAdd(this.txtNames.Text))
                {
                    MessageBox.Show(this, "名称：" + this.txtNames.Text + "已经存在！");

                    return;

                }


                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增分店，Code："+ this.txtLoginName.Text +" 名称："+ this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");

                }

            }
            else
            {
                //if (CheckPower("ShopListEdit"))
                //{
                //    MessageBox.Show(this, "无此操作权限！");
                //    return;
                //}

                if (dal.isExistsCodeEdit(id,this.txtLoginName.Text))
                {
                    MessageBox.Show(this, "编号：" + this.txtLoginName.Text + "已经存在！");

                    return;

                }

                if (dal.isExistsNamesEdit(id, this.txtLoginName.Text))
                {
                    MessageBox.Show(this, "名称：" + this.txtNames.Text + "已经存在！");

                    return;

                }


                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改分店：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");

                }
 
            }






        }

    }
}
