using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;
using System.Web.Script.Serialization;

using System.Data;


namespace Lanwei.Weixin.UI.kaoqin
{
    public partial class kaoqinList : BasePage
    {
        public kaoqinListWeixin dal = new kaoqinListWeixin();

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesOrderListAdd"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                DataSet ds = dal.GetList("shopId='" + LoginUser.ShopId + "' and CONVERT(varchar(100),checkin_time, 23)='" + DateTime.Now.AddDays(-1).ToShortDateString() + "' ");
               




            

            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

               
                string bizId = Request.Params["bizId"].ToString();

                string type = Request.Params["type"].ToString();

                string state = Request.Params["state"].ToString();

                this.GetDataList(bizStart, bizEnd, bizId, type, state);
                Response.End();
            }
        }

      

      



        void GetDataList(DateTime start, DateTime end, string bizId, string type,string state)
        {


            string isWhere = " shopId='" + LoginUser.ShopId + "' and CONVERT(varchar(100),checkin_time, 23)<='"
             + end.ToString("yyyy-MM-dd")
             + "'  and CONVERT(varchar(100),checkin_time, 23)>='"
             + start.ToString("yyyy-MM-dd") + "' ";


            if (bizId != "")
            {
                isWhere += " and userId in(" + bizId + ") ";
            }

            if (type != "全部")
            {
                isWhere += " and checkin_type='" + type + "' ";
            }
        

            if (state == "正常")
            {
                isWhere += " and exception_type='正常' ";
            }

            if (state == "异常")
            {
                isWhere += " and exception_type<>'正常' ";
            }



            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                  
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    userid = ds.Tables[0].Rows[i]["userid"].ToString(),
                    username = ds.Tables[0].Rows[i]["username"].ToString(),
                    groupname = ds.Tables[0].Rows[i]["groupname"].ToString(),
                    checkin_type = ds.Tables[0].Rows[i]["checkin_type"].ToString(),
                    exception_type = ds.Tables[0].Rows[i]["exception_type"].ToString(),
                    checkin_date = DateTime.Parse(ds.Tables[0].Rows[i]["checkin_time"].ToString()).ToString("yyyy-MM-dd"),
                    checkin_time = DateTime.Parse(ds.Tables[0].Rows[i]["checkin_time"].ToString()).ToString("HH:mm"),
                    location_title = ds.Tables[0].Rows[i]["location_title"].ToString(),
                    location_detail = ds.Tables[0].Rows[i]["location_detail"].ToString(),
                    wifiname = ds.Tables[0].Rows[i]["wifiname"].ToString(),
                    notes = ds.Tables[0].Rows[i]["notes"].ToString(),
                    wifimac = ds.Tables[0].Rows[i]["wifimac"].ToString(),
                    mediaids = ds.Tables[0].Rows[i]["mediaids"].ToString()

                });
            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);

          
        }

    }
}
