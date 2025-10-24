using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.sales
{
    public partial class SalesOrderListSelect : BasePage
    {
        public SalesOrderItemDAL dal = new SalesOrderItemDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;


                GetDataList(keys, start, end);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());
                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end);
                Response.End();
            }
        }

        void GetDataList(string key, DateTime start, DateTime end)
        {
            string isWhere = "CONVERT(varchar(100),bizDate, 23)>='" + start.ToString("yyyy-MM-dd")
                + "' AND CONVERT(varchar(100),bizDate, 23)<='" + end.ToString("yyyy-MM-dd") + "' ";

            isWhere += " AND shopId='" + LoginUser.ShopId + "' ";

            if (key != "")
            {
                isWhere += " AND (goodsName like '%" + key + "%' or wlName like '%" + key + "%' or number like '%" + key + "%' ) ";
            }

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }
    }
}