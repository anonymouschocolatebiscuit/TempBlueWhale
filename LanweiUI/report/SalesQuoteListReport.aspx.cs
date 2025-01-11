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
    public partial class SalesQuoteListReport : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public SalesQuoteDAL dal = new SalesQuoteDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();


              

            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());


             

                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();

           

                this.GetDataList(bizStart,bizEnd,wlId,goodsId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,string  wlId,string goodsId)
        {



            DataSet ds = dal.GetSalesQuoteListReport(bizStart, bizEnd,wlId, goodsId);
          



            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    brand = ds.Tables[0].Rows[i]["brand"].ToString(),
                    mpq = ds.Tables[0].Rows[i]["mpq"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                   
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    
                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    sumPriceCost = ds.Tables[0].Rows[i]["sumPriceCost"].ToString(),

                    profit = ds.Tables[0].Rows[i]["profit"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),

                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),

                   
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString(),
                    remarksItem = ds.Tables[0].Rows[i]["remarksItem"].ToString()
                  
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
