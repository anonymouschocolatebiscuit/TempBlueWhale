using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.sales
{
    public partial class SalesReceiptListAdd : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public AccountDAL accountDAL = new AccountDAL();

        public static int orderId = 0;
        public static string sourceNumber = "";

        public static string wlName = "";
        public static string wlId = "";

        public SalesOrderDAL dal = new SalesOrderDAL();
        public SalesOrderItemDAL item = new SalesOrderItemDAL();

        public UserDAL userDAL = new UserDAL();

        public LogisticsDAL wuliuDAL = new LogisticsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (!CheckPower("SalesReceiptListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();

                this.txtPayNow.Attributes.Add("onkeyup", "return calcPayNo(this.value)");

                this.txtDis.Attributes.Add("onkeyup", "return calcDisPrice(this.value)");

                //this.txtDisPrice.Attributes.Add("onkeyup", "return calcDis(this.value)");

                orderId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

                // Response.Write("orderId:"+orderId.ToString()+Request.Url.ToString());

                this.Bind();
            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);
                Response.End();
            }
        }

        public void Bind()
        {
            ListItem items = new ListItem("(Please Select)", "0");

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlBankList.DataSource = accountDAL.GetList(isWhere);
            this.ddlBankList.DataTextField = "CodeName";
            this.ddlBankList.DataValueField = "id";
            this.ddlBankList.DataBind();
            this.ddlBankList.Items.Insert(0, items);

            this.ddlBankList.SelectedValue = "0";


            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();

            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();

            this.ddlSendCompanyList.DataSource = wuliuDAL.GetList(isWhere);
            this.ddlSendCompanyList.DataTextField = "names";
            this.ddlSendCompanyList.DataValueField = "id";
            this.ddlSendCompanyList.DataBind();
            this.ddlSendCompanyList.Items.Insert(0, items);

            #region Find out if it comes from the order

            //Find out if it comes from the order

            if (orderId != 0)
            {
                SalesOrderDAL orderDAL = new SalesOrderDAL();

                DataSet ds = orderDAL.GetAllModel(orderId);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    this.clientId.Value = ds.Tables[0].Rows[0]["wlId"].ToString();

                    this.clientName.Text = ds.Tables[0].Rows[0]["wlName"].ToString();

                    wlName = ds.Tables[0].Rows[0]["wlName"].ToString();
                    wlId = ds.Tables[0].Rows[0]["wlId"].ToString();

                    this.txtPayNo.Text = ds.Tables[0].Rows[0]["sumPriceAll"].ToString();
                    this.txtDis.Text = "0";
                    this.txtDisPrice.Text = "0";

                    sourceNumber = ds.Tables[0].Rows[0]["number"].ToString();

                    DataSet dsLinkMan = dal.GetSalesOrdersAddress(orderId);
                    if (dsLinkMan.Tables[0].Rows.Count > 0)
                    {
                        this.txtGetName.Text = dsLinkMan.Tables[0].Rows[0]["names"].ToString();
                        this.txtAddress.Text = dsLinkMan.Tables[0].Rows[0]["dizhi"].ToString();
                        this.txtPhone.Text = dsLinkMan.Tables[0].Rows[0]["phone"].ToString() + dsLinkMan.Tables[0].Rows[0]["tel"].ToString();
                    }
                }
            }

            #endregion
        }


        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            if (id != 0)
            {
                #region If it is from an order

                DataSet ds = item.GetAllModel(id);

                int rows = ds.Tables[0].Rows.Count;
                    
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(new
                    {
                        id = ds.Tables[0].Rows[i]["id"].ToString(),
                        goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                        goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                        spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                        unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                        num = ds.Tables[0].Rows[i]["num"].ToString(),
                        price = ds.Tables[0].Rows[i]["price"].ToString(),
                        dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                        sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                        priceNow = ds.Tables[0].Rows[i]["priceNow"].ToString(),
                        sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),

                        tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                        priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),
                        sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),

                        sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                        ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                        ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                        itemId = 0,
                        remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                        sourceNumber = sourceNumber

                    });
                }

                if (rows < 8)//Less than 8 lines
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

                #endregion
            }
            else
            {
                #region If it is newly added

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

                #endregion
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}