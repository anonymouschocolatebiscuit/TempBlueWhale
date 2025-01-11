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
    public partial class SalesOrderListAdd : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesOrderListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtSendDate.Text = DateTime.Now.ToShortDateString();

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

            //this.ddlVenderList.DataSource = venderDAL.GetAllModel();
            //this.ddlVenderList.DataTextField = "CodeName";
            //this.ddlVenderList.DataValueField = "id";
            //this.ddlVenderList.DataBind();

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();

            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();


        }

   
        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = "",
                    goodsName = "",
                    spec = "",
                    unitName = "",

                    num = "",

                    price = "",

                    dis = "",
                    sumPriceDis = "",

                    priceNow = "",
                    sumPriceNow = "",

                    tax = "",
                    priceTax = "",
                    sumPriceTax = "",

                    sumPriceAll = "",


                    ckId = "",
                    ckName = "",
                    remarks = "",
                    itemId = 0,
                    sourceNumber = ""
                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
