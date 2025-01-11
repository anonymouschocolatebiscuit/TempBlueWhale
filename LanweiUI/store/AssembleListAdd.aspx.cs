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
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;

namespace Lanwei.Weixin.UI.store
{
    public partial class AssembleListAdd : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("AssembleListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
              

                this.Bind();

            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                this.GetDataListSub();
                Response.End();
            }

        }

      

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlCangkuList.DataSource =cangkuDAL.GetList(isWhere);
            this.ddlCangkuList.DataTextField = "Names";
            this.ddlCangkuList.DataValueField = "id";
            this.ddlCangkuList.DataBind();


        }

   
        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 2; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId ="",
                    goodsName = "",
                    unitName = "",
                    spec = "",
                    num ="",
                    price = "",
                    sumPrice = "",
                    ckId ="",
                    ckName = "",

                    remarks = ""
                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = "",
                    goodsName = "",
                    unitName = "",
                    spec = "",
                    num = "",
                    price = "",
                    sumPrice = "",
                    ckId = "",
                    ckName = "",

                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
