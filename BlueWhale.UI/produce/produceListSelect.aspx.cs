using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.produce
{
    public partial class produceListSelect : BasePage
    {
        public ProduceListDAL dal = new ProduceListDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesOrderList"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}
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
            string isWhere = "  CONVERT(varchar(100),makeDate, 23)>='" + start.ToString("yyyy-MM-dd")
                + "'  and CONVERT(varchar(100),makeDate, 23)<='" + end.ToString("yyyy-MM-dd") + "' ";

            isWhere += " and shopId='" + LoginUser.ShopId + "' ";

            if (key != "")
            {
                isWhere += " and (goodsName like '%" + key + "%' or wlName like '%" + key + "%' or number like '%" + key + "%' ) ";
            }

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    finishNum = ds.Tables[0].Rows[i]["finishNum"].ToString(),
                    finishNumNo = ds.Tables[0].Rows[i]["finishNumNo"].ToString(),
                    dateStart = ds.Tables[0].Rows[i]["dateStart"].ToString(),
                    dateEnd = ds.Tables[0].Rows[i]["dateEnd"].ToString(),
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
