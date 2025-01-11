using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Web.Script.Serialization;

using System.Collections.Generic;
using Lanwei.Weixin.UI.src;
using Lanwei.Weixin.Model;

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using System.Text;

using System.Web.SessionState;//用Session必须引用

namespace Lanwei.Weixin.UI
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class HandlerBaiduECharts : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
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

                context.Response.Write("登陆超时，请重新登陆系统！");
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
        /// 销售占比-----饼状图
        /// </summary>
        /// <param name="context"></param>
        public void GetPie(HttpContext context,int shopId)
        {
            DateTime start = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1");
            DateTime end = DateTime.Now;

            DataSet ds = salesDAL.getSalesPercent(shopId,start, end);

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
        /// 销售汇总---柱状图
        /// </summary>
        /// <param name="context"></param>
        public void GetBars(HttpContext context,int shopId)
        {

            int years = DateTime.Now.Year;

            DataSet ds = salesDAL.getSalesSumByMoth(years, shopId);

            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var obj = new { name = dr["months"].ToString()+"月", value = dr["sumPrice"] };//years.ToString()+"-"+
                lists.Add(obj);

            }

            jsS = new JavaScriptSerializer();
            result = jsS.Serialize(lists);
            context.Response.Write(result);


        }

        /// <summary>
        /// 销售量汇总---曲线图
        /// </summary>
        /// <param name="context"></param>
        public void GetLine(HttpContext context,int shopId)
        {

            int years = DateTime.Now.Year;

            DataSet ds = salesDAL.getSalesSumNumByMoth(years,shopId);

            lists = new List<object>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var obj = new { name = dr["months"].ToString()+"月", value = dr["sumPrice"] };//years.ToString() + "-" + 
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
