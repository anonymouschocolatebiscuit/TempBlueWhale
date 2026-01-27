using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using BlueWhale.DAL;
using BlueWhale.Model;

namespace BlueWhale.UI
{

    /// <summary>
    /// $codebehindclassname$ 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class HandlerSaleEChart : IHttpHandler, IRequiresSessionState
    {
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();
        string result = "";

        public SalesReceiptDAL salesDAL = new SalesReceiptDAL();

        public void ProcessRequest(HttpContext context)
        {
            string command = context.Request["cmd"];

            if (context.Session["userInfo"] == null)
            {
                context.Response.Write("Login timeout, please log in again!");
                return;
            }

            Users users = context.Session["userInfo"] as Users;

            switch (command)
            {
                case "pie":
                    GetPie(context, users.ShopId);
                    break;

                case "bar":
                    GetBars(context, users.ShopId);
                    break;

                case "line":
                    GetLine(context, users.ShopId);
                    break;
            };
        }


        /// <summary>
        /// Sales proportion - Pie Chart
        /// </summary>
        /// <param name="context"></param>
        public void GetPie(HttpContext context, int shopId)
        {
            DateTime start = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1");
            DateTime end = DateTime.Now;
            DataSet ds = salesDAL.getSalesPercent(shopId, start, end);
            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var obj = new { name = dr["typeName"], value = dr["sumPriceAll"] };
                lists.Add(obj);
            }

            jsS = new JavaScriptSerializer();
            result = jsS.Serialize(lists);
            context.Response.Write(result);
        }

        /// <summary>
        /// Sales Summary ---historgram
        /// </summary>
        /// <param name="context"></param>
        public void GetBars(HttpContext context, int shopId)
        {
            int years = DateTime.Now.Year;
            DataSet ds = salesDAL.getSalesSumByMoth(years, shopId);
            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                var obj = new { name = new CultureInfo("en-US").DateTimeFormat.GetMonthName(Convert.ToInt32(dr["months"].ToString())), value = dr["sumPrice"] };
                lists.Add(obj);
            }

            jsS = new JavaScriptSerializer();
            result = jsS.Serialize(lists);
            context.Response.Write(result);
        }

        /// <summary>
        /// Total Sales---Line chart
        /// </summary>
        /// <param name="context"></param>
        public void GetLine(HttpContext context, int shopId)
        {
            int years = DateTime.Now.Year;
            DataSet ds = salesDAL.getSalesSumNumByMoth(years, shopId);
            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                var obj = new { name = new CultureInfo("en-US").DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(dr["months"].ToString())), value = dr["sumPrice"] };
                lists.Add(obj);
            }

            jsS = new JavaScriptSerializer();
            result = jsS.Serialize(lists);
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}