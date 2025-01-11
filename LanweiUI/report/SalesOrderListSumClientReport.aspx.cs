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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;

namespace Lanwei.Weixin.UI.report
{
    public partial class SalesOrderListSumClientReport : BasePage
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();
     
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

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());


          


                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();

                string typeId = Request.Params["typeId"].ToString();


                this.GetDataList(bizStart,bizEnd,wlId,goodsId,typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,string  wlId,string goodsId,string typeId)
        {



            DataSet ds = dal.GetSalesReceiptItemSumClient(LoginUser.ShopId,bizStart, bizEnd, typeId, wlId, goodsId);




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
