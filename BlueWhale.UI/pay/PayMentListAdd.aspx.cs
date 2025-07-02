using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.pay
{
    public partial class PayMentListAdd : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public PurReceiptDAL dal = new PurReceiptDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("PayMentListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
                this.Bind();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end, wlId);
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
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();
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

            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 4; i++)
            {
                list.Add(new
                {
                    bkId = "1",
                    bkName = "1",
                    payPrice = "1",
                    payTypeId = "1",
                    payTypeName = "1",
                    payNumber = "1",

                    remarks = "1"
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 6; i++)
            {
                list.Add(new
                {

                    sourceNumber = "",
                    bizType = "",
                    bizDate = "",
                    sumPriceAll = "",
                    priceCheckNowSum = "",
                    priceCheckNo = "",
                    priceCheckNow = ""

                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
