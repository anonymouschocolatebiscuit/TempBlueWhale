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
    public partial class welcomeSalesQuoteList : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public SalesQuoteDAL dal = new SalesQuoteDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


         

            if (Request.Params["Action"] == "GetDataList")
            {



                string keys = Request.Params["keys"].ToString();

           

                this.GetDataList(keys);
                Response.End();
            }
        }

        void GetDataList(string keys)
        {



            DataSet ds = dal.GetSalesQuoteListReport(keys);
          



            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    brandName = ds.Tables[0].Rows[i]["brandName"].ToString(),
                    mpq = ds.Tables[0].Rows[i]["mpq"].ToString(),
                    packages = ds.Tables[0].Rows[i]["packages"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                   
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    
                 
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
