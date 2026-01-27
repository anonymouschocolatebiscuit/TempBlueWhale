using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class GoodsOutInDetailReport : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckPower("GoodsOutInDetailReport"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());
                DateTime bizEnd = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());
                string goodsId = Request.Params["goodsId"].ToString();
                string ckId = Request.Params["ckId"].ToString();

                GetDataList(bizStart, bizEnd, goodsId, ckId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string goodsId, string typeId)
        {
            DataSet ds = dal.GetGoodsBalanceDetail(LoginUser.ShopId, bizStart, bizEnd, goodsId, typeId);
            decimal numIn = 0m;
            decimal sumPriceIn = 0m;

            decimal numOut = 0m;
            decimal priceOut = 0m;
            decimal sumPriceOut = 0m;

            decimal numEndLast = 0m;
            decimal priceEndLast = 0m;
            decimal sumPriceEndLast = 0m;

            IList<object> list = new List<object>();
            int rowCount = ds.Tables[0].Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string bizType = row["bizType"].ToString();


                decimal numBegin;
                decimal priceBegin;
                decimal sumPriceBegin;

                decimal numEnd;
                decimal sumPriceEnd;

                if (bizType == "Opening Balance")
                {
                    numBegin = ConvertTo.ConvertDec(row["numBegin"].ToString());
                    priceBegin = ConvertTo.ConvertDec(row["priceBegin"].ToString());
                    sumPriceBegin = ConvertTo.ConvertDec(row["sumPriceBegin"].ToString());

                    numEnd = ConvertTo.ConvertDec(row["numEnd"].ToString());
                    decimal priceEnd = ConvertTo.ConvertDec(row["priceEnd"].ToString());
                    sumPriceEnd = ConvertTo.ConvertDec(row["sumPriceEnd"].ToString());

                    numEndLast = numEnd;
                    sumPriceEndLast = sumPriceEnd;
                    priceEndLast = priceEnd;
                }
                else
                {
                    numBegin = numEndLast;
                    priceBegin = priceEndLast;
                    sumPriceBegin = sumPriceEndLast;

                    numIn = ConvertTo.ConvertDec(row["numIn"].ToString());
                    sumPriceIn = ConvertTo.ConvertDec(row["sumPriceIn"].ToString());

                    numOut = ConvertTo.ConvertDec(row["numOut"].ToString());
                    priceOut = priceBegin;
                    sumPriceOut = numOut * priceOut;

                    numEnd = numBegin + numIn - numOut;
                    sumPriceEnd = sumPriceBegin + sumPriceIn - sumPriceOut;

                    if (numEnd != 0m)
                    {
                        priceEndLast = sumPriceEnd / numEnd;
                    }

                    numEndLast = numEnd;
                    sumPriceEndLast = sumPriceEnd;
                }

                list.Add(new
                {
                    code = row["code"].ToString(),
                    goodsName = row["goodsName"].ToString(),
                    spec = row["spec"].ToString(),
                    unitName = row["unitName"].ToString(),
                    bizDate = row["bizDate"].ToString(),
                    number = row["number"].ToString(),
                    bizType = bizType,
                    wlName = row["wlName"].ToString(),
                    ckName = row["ckName"].ToString(),

                    numBegin = numBegin.ToString("0.00"),
                    priceBegin = priceBegin.ToString("0.00"),
                    sumPriceBegin = sumPriceBegin.ToString("0.00"),

                    numIn = numIn.ToString("0.00"),
                    sumPriceIn = sumPriceIn.ToString("0.00"),

                    numOut = numOut.ToString("0.00"),
                    priceOut = priceOut.ToString("0.00"),
                    sumPriceOut = sumPriceOut.ToString("0.00"),

                    numEnd = numEnd.ToString("0.00"),
                    priceEnd = priceEndLast.ToString("0.00"),
                    sumPriceEnd = sumPriceEnd.ToString("0.00")
                });
            }

            var gridData = new { Rows = list, Total = list.Count.ToString() };
            string json = new JavaScriptSerializer().Serialize(gridData);
            Response.Write(json);
        }
    }
}
