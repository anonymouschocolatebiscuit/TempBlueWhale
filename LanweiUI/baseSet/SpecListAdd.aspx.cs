﻿using System;
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
    public partial class SpecListAdd : BasePage
    {
        public SpecDAL dal = new SpecDAL();

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

            if (Request.QueryString.Count>0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
              
                

            }
            else
            {
               
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckPower("SpecListAdd"))
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

            dal.Id = id;
        
            dal.Names = this.txtNames.Text;

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
                    logs.Events = "新增计量单位：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id,this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，名称重复！");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改计量单位：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }
            }



            
        }
    }
}
