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
    public partial class SalesReceiptListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();

        public InventoryDAL inventoryDAL = new InventoryDAL();

        public AccountDAL accountDAL = new AccountDAL();

        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public SalesReceiptItemDAL item = new SalesReceiptItemDAL();

        public string fromId = "0";

        public UserDAL userDAL = new UserDAL();

        public LogisticsDAL wuliuDAL = new LogisticsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                if (!CheckPower("SalesReceiptListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtPayNow.Attributes.Add("onkeyup", "return calcPayNo(this.value)");
                this.txtDis.Attributes.Add("onkeyup", "return calcDisPrice(this.value)");
                this.Bind();
                this.BindInfo();
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
            this.ddlBankList.Items.Clear();
            this.ddlSendCompanyList.Items.Clear();
            this.ddlYWYList.Items.Clear();
            ListItem items = new ListItem("(Please select)", "0");
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            this.ddlBankList.DataSource = accountDAL.GetList(isWhere);
            this.ddlBankList.DataTextField = "CodeName";
            this.ddlBankList.DataValueField = "id";
            this.ddlBankList.DataBind();
            this.ddlBankList.Items.Insert(0, new ListItem("Please select", "0"));
            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();
            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();
            this.ddlSendCompanyList.DataSource = wuliuDAL.GetList(isWhere);
            this.ddlSendCompanyList.DataTextField = "names";
            this.ddlSendCompanyList.DataValueField = "id";
            this.ddlSendCompanyList.DataBind();
            this.ddlSendCompanyList.Items.Insert(0, new ListItem("Please select", "0"));
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region Bind

                this.clientId.Value = ds.Tables[0].Rows[0]["wlId"].ToString();

                this.clientName.Text = ds.Tables[0].Rows[0]["wlName"].ToString();


                this.txtClientName.Value = ds.Tables[0].Rows[0]["wlName"].ToString();

                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();

                this.ddlYWYList.SelectedValue = ds.Tables[0].Rows[0]["bizId"].ToString();


                this.txtPayNow.Text = ds.Tables[0].Rows[0]["PayNow"].ToString();
                this.txtPayNo.Text = ds.Tables[0].Rows[0]["PayNowNo"].ToString();

                this.ddlBankList.SelectedValue = ds.Tables[0].Rows[0]["bkId"].ToString();


                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                this.txtDis.Text = ds.Tables[0].Rows[0]["dis"].ToString();
                this.txtDisPrice.Text = ds.Tables[0].Rows[0]["disPrice"].ToString();


                this.ddlSendCompanyList.SelectedValue = ds.Tables[0].Rows[0]["sendId"].ToString();

                this.txtSendNumber.Text = ds.Tables[0].Rows[0]["sendNumber"].ToString();
                this.ddlSendPayTypeList.SelectedValue = ds.Tables[0].Rows[0]["sendPayType"].ToString();
                this.txtSendPrice.Text = ds.Tables[0].Rows[0]["sendPrice"].ToString();

                this.txtGetName.Text = ds.Tables[0].Rows[0]["getName"].ToString();
                this.txtPhone.Text = ds.Tables[0].Rows[0]["phone"].ToString();
                this.txtAddress.Text = ds.Tables[0].Rows[0]["address"].ToString();


                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag != "Save")
                {
                    this.btnSave.Visible = false;
                }


                #endregion
            }
            else
            {
            }
        }

        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            // int pId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
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
                    itemId = ds.Tables[0].Rows[i]["itemId"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString()

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

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}