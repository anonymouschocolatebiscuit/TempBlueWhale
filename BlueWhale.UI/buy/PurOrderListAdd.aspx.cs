using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.buy
{
    public partial class PurOrderListAdd : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();

        public InventoryDAL inventoryDAL = new InventoryDAL();

        public UserDAL userDAL = new UserDAL();

        public SalesOrderDAL dal = new SalesOrderDAL();

        public SalesOrderItemDAL item = new SalesOrderItemDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("PurOrderListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtSendDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();
            }

            if (Request.Params["Action"] == "GetData")
            {
                string idParam = Request.Params["id"];

                if (idParam != "null")
                {
                    int id = Int32.Parse(idParam);
                    this.GetDataList(id);
                }

                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();
        }

        void GetDataList(int itemIdString)
        {
            IList<object> list = new List<object>();

            if (itemIdString != 0)
            {
                #region IfFromSalesOrder

                DataSet ds = item.GetAllModel(itemIdString);

                int rows = ds.Tables[0].Rows.Count;

                for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    decimal num = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["num"].ToString());

                    decimal priceCost = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCost"].ToString());

                    list.Add(new
                    {
                        id = ds.Tables[0].Rows[i]["id"].ToString(),
                        goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                        goodsName = ds.Tables[0].Rows[i]["names"].ToString(),
                        spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                        unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                        num = ds.Tables[0].Rows[i]["num"].ToString(),

                        price = priceCost.ToString("0.00"),

                        dis = "",
                        sumPriceDis = "",

                        priceNow = priceCost.ToString("0.00"),
                        sumPriceNow = (num * priceCost).ToString("0.00"),

                        tax = "",
                        priceTax = priceCost.ToString("0.00"),
                        sumPriceTax = "",

                        sumPriceAll = (num * priceCost).ToString("0.00"),

                        itemId = ds.Tables[0].Rows[i]["id"].ToString(),
                        remarks = "",
                        sourceNumber = ds.Tables[0].Rows[i]["number"].ToString()
                    });
                }
                if (rows < 8)
                {
                    for (var i = 0; i < 8 - rows; i++)
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
                }

                #endregion
            }
            else
            {
                #region ifIsCreate

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

                #endregion
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}