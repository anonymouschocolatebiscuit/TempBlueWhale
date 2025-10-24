using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.report
{
    public partial class GoodsOutInSumReport : BasePage
    {

        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (!CheckPower("GoodsOutInSumReport"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());
                string goodsId = Request.Params["goodsId"].ToString();
                string ckId = Request.Params["ckId"].ToString();
                this.GetDataList(bizStart, bizEnd, goodsId, ckId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string goodsId, string typeId)
        {

            DataSet ds = dal.GetGoodsOutInSumReport(LoginUser.ShopId, bizStart, bizEnd, goodsId, typeId);
            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    sumNumBegin = ds.Tables[0].Rows[i]["sumNumBegin"].ToString(),
                    sumPriceBegin = ds.Tables[0].Rows[i]["sumPriceBegin"].ToString(),

                    sumNumIn = ds.Tables[0].Rows[i]["sumNumIn"].ToString(),
                    sumPriceIn = ds.Tables[0].Rows[i]["sumPriceIn"].ToString(),

                    sumNumInAll = ds.Tables[0].Rows[i]["sumNumInAll"].ToString(),
                    sumPriceInAll = ds.Tables[0].Rows[i]["sumPriceInAll"].ToString(),

                    sumNumOut = ds.Tables[0].Rows[i]["sumNumOut"].ToString(),
                    sumPriceOut = ds.Tables[0].Rows[i]["sumPriceOut"].ToString(),

                    sumNumOutAll = ds.Tables[0].Rows[i]["sumNumOutAll"].ToString(),
                    sumPriceOutAll = ds.Tables[0].Rows[i]["sumPriceOutAll"].ToString(),

                    sumNumEnd = ds.Tables[0].Rows[i]["sumNumEnd"].ToString(),
                    sumPriceEnd = ds.Tables[0].Rows[i]["sumPriceEnd"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
