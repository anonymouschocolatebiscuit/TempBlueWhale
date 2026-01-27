using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.sales
{
    public partial class SalesOrderListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public UserDAL userDAL = new UserDAL();
        public SalesOrderDAL dal = new SalesOrderDAL();
        public SalesOrderItemDAL item = new SalesOrderItemDAL();

        public string fromId = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesOrderListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtSendDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();
                this.BindInfo();
            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"]);
                this.GetDataList(id);
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

            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"]);
            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                this.clientId.Value = row["wlId"].ToString();
                this.clientName.Text = row["wlName"].ToString();
                this.txtClientName.Value = row["wlName"].ToString();

                this.txtBizDate.Text = DateTime.Parse(row["bizDate"].ToString()).ToShortDateString();
                this.txtSendDate.Text = DateTime.Parse(row["sendDate"].ToString()).ToShortDateString();

                this.ddlYWYList.SelectedValue = row["bizId"].ToString();
                this.txtRemarks.Text = row["remarks"].ToString();

                string flag = row["flag"].ToString();
                if (flag != "Save")
                {
                    this.btnSave.Visible = false;
                }
            }
        }

        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            DataSet ds = item.GetAllModel(id);
            int rows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                list.Add(new
                {
                    id = row["id"].ToString(),
                    goodsId = row["goodsId"].ToString(),
                    goodsName = row["goodsName"].ToString(),
                    spec = row["spec"].ToString(),
                    unitName = row["unitName"].ToString(),
                    num = row["num"].ToString(),
                    price = row["price"].ToString(),
                    dis = row["dis"].ToString(),
                    sumPriceDis = row["sumPriceDis"].ToString(),
                    priceNow = row["priceNow"].ToString(),
                    sumPriceNow = row["sumPriceNow"].ToString(),
                    tax = row["tax"].ToString(),
                    priceTax = row["priceTax"].ToString(),
                    sumPriceTax = row["sumPriceTax"].ToString(),
                    sumPriceAll = row["sumPriceAll"].ToString(),
                    ckId = row["ckId"].ToString(),
                    ckName = row["ckName"].ToString(),
                    remarks = row["remarks"].ToString(),
                    itemId = row["itemId"].ToString(),
                    sourceNumber = row["sourceNumber"].ToString()
                });
            }

            // Add empty rows if less than 8
            if (rows < 8)
            {
                for (int i = 0; i < 8 - rows; i++)
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

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
