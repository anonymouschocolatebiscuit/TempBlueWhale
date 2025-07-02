using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class PurOrderListSumVenderReport : BasePage
    {
        public PurReceiptDAL dal = new PurReceiptDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                string bizStartParam = Request.Params["start"].ToString();

                string bizEndParam = Request.Params["end"].ToString();

                string wlId = Request.Params["wlId"].ToString();
                
                string goodsId = Request.Params["goodsId"].ToString();
               
                string typeId = Request.Params["typeId"].ToString();

                if (bizStartParam != "" && bizEndParam != "")
                {
                    DateTime bizStart = DateTime.Parse(bizStartParam);
                    DateTime bizEnd = DateTime.Parse(bizEndParam);

                    this.GetDataList(bizStart, bizEnd, wlId, goodsId, typeId);
                }

                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string wlId, string goodsId, string typeId)
        {
            DataSet ds = dal.GetPurReceiptItemSumVender(LoginUser.ShopId, bizStart, bizEnd, typeId, wlId, goodsId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    wlId = ds.Tables[0].Rows[i]["wlId"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString()
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}