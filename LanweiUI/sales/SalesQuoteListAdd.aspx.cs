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

using System.Web.Services;
using System.Reflection;

namespace Lanwei.Weixin.UI.sales
{
    public partial class SalesQuoteListAdd : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesQuoteListAdd"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();

                this.txtDeadLine.Text = DateTime.Now.AddMonths(1).ToShortDateString();
               

                this.Bind();

            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }
        }

      

        public void Bind()
        {
            string isWhere = " shopId='"+LoginUser.ShopId+"' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

            this.ddlBizId.DataSource =userDAL.GetList(isWhere);
            this.ddlBizId.DataTextField = "names";
            this.ddlBizId.DataValueField = "id";
            this.ddlBizId.DataBind();

            this.ddlBizId.SelectedValue = LoginUser.Id.ToString();

            int wlId = ConvertTo.ConvertInt(this.ddlVenderList.SelectedValue.ToString());
            DataSet ds = venderDAL.GetClientAddress(wlId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtSendPlace.Text = ds.Tables[0].Rows[0]["address"].ToString();
                
            }

        }

   
        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId ="",
                    goodsName = "",
                    spec = "",
                    unitName = "",
                    brandName = "",
                    mpq = "",
                    packages="",
                    num ="",
                  
                    price = "",
                    sumPrice = "",
                    tax = "",
                    priceTax = "",
                    sumPriceTax="",
                    sumPriceAll = "",
                    remarks = ""
                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        protected void ddlVenderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int wlId = ConvertTo.ConvertInt(this.ddlVenderList.SelectedValue.ToString());
            DataSet ds = venderDAL.GetClientAddress(wlId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtSendPlace.Text = ds.Tables[0].Rows[0]["address"].ToString();

            }
        }

    }
}
