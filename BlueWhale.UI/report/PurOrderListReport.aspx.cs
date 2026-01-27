using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class PurOrderListReport : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public PurOrderDAL dal = new PurOrderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());
                DateTime bizEnd = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());
                DateTime sendStart = DateTime.Parse("2000-1-1");
                DateTime sendEnd = DateTime.Parse("2050-1-1");

                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();
                string typeId = Request.Params["typeId"].ToString();

                this.GetDataList(bizStart, bizEnd, sendStart, sendEnd, wlId, goodsId, typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, DateTime sendStart, DateTime sendEnd, string wlId, string goodsId, string typeId)
        {
            DataSet ds = dal.GetPurOrderListReport(LoginUser.ShopId, bizStart, bizEnd, sendStart, sendEnd, wlId, goodsId, typeId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sendFlag = "";
                decimal Num = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["Num"].ToString());
                decimal getNum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["getNum"].ToString());
                decimal getNumNo = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["getNumNo"].ToString());
                if (getNumNo <= 0)
                {
                    sendFlag = "All in stock";
                }

                if (getNumNo != 0 && Num > getNum)
                {
                    sendFlag = "Partial storage";
                }
                if (getNumNo == Num)
                {
                    sendFlag = "Not in stock";
                }

                list.Add(new
                {
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    Num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    getNum = ds.Tables[0].Rows[i]["getNum"].ToString(),
                    getNumNo = ds.Tables[0].Rows[i]["getNumNo"].ToString(),
                    sendFlag = sendFlag,
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
