using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.sales
{
    public partial class SalesOrderListAdd : BasePage
    {
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

            this.ddlSalesPersonList.DataSource = userDAL.GetList(isWhere);
            this.ddlSalesPersonList.DataTextField = "names";
            this.ddlSalesPersonList.DataValueField = "id";
            this.ddlSalesPersonList.DataBind();

            this.ddlSalesPersonList.Items.Insert(0, new ListItem("Select Salesperson", ""));
            this.ddlSalesPersonList.SelectedIndex = 0;
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (int i = 1; i < 9; i++)
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

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }
    }
}