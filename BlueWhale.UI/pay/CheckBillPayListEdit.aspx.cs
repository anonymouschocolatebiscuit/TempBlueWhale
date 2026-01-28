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
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

using System.Web.Services;
using System.Reflection;

namespace BlueWhale.UI.pay
{
    public partial class CheckBillPayListEdit : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public PayMentDAL dalGet = new PayMentDAL();
        public PurReceiptDAL dal = new PurReceiptDAL();

        public CheckBillDAL dalCheck = new CheckBillDAL();
        public CheckBillItemPayMentDAL itemRecei = new CheckBillItemPayMentDAL();
        public CheckBillItemPurDAL itemSales = new CheckBillItemPurDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Check permissions for "CheckBillPayListEdit"
                if (!CheckPower("CheckBillPayListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                // Set default dates for date fields
                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                this.txtDateStar1.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd1.Text = DateTime.Now.ToShortDateString();

                // Bind data
                this.Bind();

                // Bind page details
                this.BindInfo();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());
                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end, wlId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearchGet")
            {
                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());
                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataListGet(keys, start, end, wlId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataListSub(id);
                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dalCheck.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // Bind Supplier dropdown
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["venderIdA"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "Approved")
                {
                    this.btnSave.Visible = false; // Hide save button if approved
                }
            }
        }

        void GetDataListGet(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dalGet.GetAllModel(wlId, start, end, key);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();

                decimal sumPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payPriceSum"].ToString());
                decimal priceCheckNowSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal priceCheckNo = sumPrice - priceCheckNowSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["payPriceSum"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow = "0"
                });
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata); // Serialize to JSON for grid
            Response.Write(s);
        }

        void GetDataList(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dal.GetAllModelByWLId(key, start, end, wlId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();
                if (bizType == "1")
                {
                    bizType = "Regular Purchase";
                }
                if (bizType == "-1")
                {
                    bizType = "Purchase Return";
                }

                decimal sumPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceAll"].ToString());
                decimal priceCheckNowSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal priceCheckNo = sumPrice - priceCheckNowSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow = "0"
                });
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata); // Serialize to JSON for grid
            Response.Write(s);
        }

        void GetDataList(int pId)
        {
            DataSet ds = itemRecei.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceBill"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["sumPriceCheck"].ToString(),
                    priceCheckNo = ds.Tables[0].Rows[i]["sumPriceCheckNo"].ToString(),
                    priceCheckNow = ds.Tables[0].Rows[i]["priceCheckNow"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub(int pId)
        {
            DataSet ds = itemSales.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceBill"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["sumPriceCheck"].ToString(),
                    priceCheckNo = ds.Tables[0].Rows[i]["sumPriceCheckNo"].ToString(),
                    priceCheckNow = ds.Tables[0].Rows[i]["priceCheckNow"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}

