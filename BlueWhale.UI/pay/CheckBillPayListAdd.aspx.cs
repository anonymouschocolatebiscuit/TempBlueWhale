using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.pay
{
    public partial class CheckBillPayListAdd : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public PayMentDAL dalGet = new PayMentDAL();
        public PurReceiptDAL dal = new PurReceiptDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("CheckBillPayListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                this.txtDateStart1.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd1.Text = DateTime.Now.ToShortDateString();

                this.Bind();
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
            string condition = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(condition);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();
        }

        void GetDataListGet(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dalGet.GetAllModel(wlId, start, end, key);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();

                decimal totalPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payPriceSum"].ToString());
                decimal currentCheckedPriceSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal uncheckedPrice = totalPrice - currentCheckedPriceSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    totalPrice = ds.Tables[0].Rows[i]["payPriceSum"].ToString(),
                    currentCheckedPriceSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    uncheckedPrice = uncheckedPrice.ToString("0.00"),
                    currentCheckPrice = "0"
                });
            }
            var gridData = new { Rows = list };

            string result = new JavaScriptSerializer().Serialize(gridData);

            Response.Write(result);
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

                decimal totalPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceAll"].ToString());
                decimal currentCheckedPriceSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal uncheckedPrice = totalPrice - currentCheckedPriceSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    totalPrice = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    currentCheckedPriceSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    uncheckedPrice = uncheckedPrice.ToString("0.00"),
                    currentCheckPrice = "0"
                });
            }
            var gridData = new { Rows = list };

            string result = new JavaScriptSerializer().Serialize(gridData);

            Response.Write(result);
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 5; i++)
            {
                list.Add(new
                {
                    sourceNumber = "",
                    bizType = "",
                    bizDate = "",
                    totalPrice = "",
                    currentCheckedPriceSum = "",
                    uncheckedPrice = "",
                    currentCheckPrice = ""
                });
            }
            var gridData = new { Rows = list, Total = list.Count.ToString() };
            string result = new JavaScriptSerializer().Serialize(gridData);
            Response.Write(result);
        }

        void GetDataListSub()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 5; i++)
            {
                list.Add(new
                {
                    sourceNumber = "",
                    bizType = "",
                    bizDate = "",
                    totalPrice = "",
                    currentCheckedPriceSum = "",
                    uncheckedPrice = "",
                    currentCheckPrice = ""
                });
            }
            var gridData = new { Rows = list, Total = list.Count.ToString() };
            string result = new JavaScriptSerializer().Serialize(gridData);
            Response.Write(result);
        }
    }
}
