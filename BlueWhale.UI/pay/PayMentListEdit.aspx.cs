using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.pay
{
    public partial class PayMentListEdit : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public PayMentDAL dal = new PayMentDAL();

        public PurReceiptDAL dalSales = new PurReceiptDAL();

        public PayMentAccountItemDAL dalAccount = new PayMentAccountItemDAL();

        public PayMentSourceBillItemDAL dalBill = new PayMentSourceBillItemDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckPower("PayMentListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                txtBizDate.Text = DateTime.Now.ToShortDateString();
                txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                txtDateEnd.Text = DateTime.Now.ToShortDateString();
                Bind();

                BindInfo();
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

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                GetDataList(id);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                GetDataListSub(id);
                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            ddlVenderList.DataTextField = "CodeName";
            ddlVenderList.DataValueField = "id";
            ddlVenderList.DataBind();
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dal.GetAllModel(id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                txtDisPrice.Text = ds.Tables[0].Rows[0]["disPrice"].ToString();
                txtPayPriceNowMore.Text = ds.Tables[0].Rows[0]["PayPriceNowMore"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();

                if (flag == "Review")
                {
                    btnSave.Visible = false;
                }
            }
        }

        void GetDataList(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dalSales.GetAllModelByWLId(key, start, end, wlId);

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
                    bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow = "0"
                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataList(int pId)
        {
            DataSet ds = dalAccount.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    bkId = ds.Tables[0].Rows[i]["bkId"].ToString(),
                    bkName = ds.Tables[0].Rows[i]["bkName"].ToString(),
                    payPrice = ds.Tables[0].Rows[i]["payPrice"].ToString(),
                    payTypeId = ds.Tables[0].Rows[i]["payTypeId"].ToString(),
                    payTypeName = ds.Tables[0].Rows[i]["payTypeName"].ToString(),
                    payNumber = ds.Tables[0].Rows[i]["payNumber"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub(int pId)
        {
            DataSet ds = dalBill.GetAllModel(pId);

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
