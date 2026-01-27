using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class GoodsOutInSumReport : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckPower("GoodsOutInSumReport"))
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
            DataSet ds = dal.GetGoodsOutInSumReport(LoginUser.ShopId, bizStart, bizEnd, goodsId, typeId);
            IList<object> list = new List<object>();
            int rowCount = ds.Tables[0].Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];

                list.Add(new
                {
                    code = row["code"].ToString(),
                    goodsName = row["goodsName"].ToString(),
                    spec = row["spec"].ToString(),
                    unitName = row["unitName"].ToString(),
                    ckName = row["ckName"].ToString(),

                    sumNumBegin = row["sumNumBegin"].ToString(),
                    sumPriceBegin = row["sumPriceBegin"].ToString(),

                    sumNumIn = row["sumNumIn"].ToString(),
                    sumPriceIn = row["sumPriceIn"].ToString(),

                    sumNumInAll = row["sumNumInAll"].ToString(),
                    sumPriceInAll = row["sumPriceInAll"].ToString(),

                    sumNumOut = row["sumNumOut"].ToString(),
                    sumPriceOut = row["sumPriceOut"].ToString(),

                    sumNumOutAll = row["sumNumOutAll"].ToString(),
                    sumPriceOutAll = row["sumPriceOutAll"].ToString(),

                    sumNumEnd = row["sumNumEnd"].ToString(),
                    sumPriceEnd = row["sumPriceEnd"].ToString()
                });
            }

            var gridData = new { Rows = list, Total = list.Count.ToString() };
            string json = new JavaScriptSerializer().Serialize(gridData);
            Response.Write(json);
        }
    }
}
