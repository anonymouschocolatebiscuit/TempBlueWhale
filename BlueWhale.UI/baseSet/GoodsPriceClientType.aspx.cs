using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.baseSet
{
    public partial class GoodsPriceClientType : BasePage
    {

        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                int goodsId = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                GetDataList(goodsId);
                Response.End();
            }

            if (Request.Params["Action"] == "clear")
            {
                int goodsId = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                DeleteRow(goodsId);
                Response.End();
            }



        }

        void GetDataList(int goodsId)
        {
            DataSet ds = dal.GetAllClientTypePrice(goodsId);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    typeId = ds.Tables[0].Rows[i]["id"].ToString(),

                    names = ds.Tables[0].Rows[i]["names"].ToString(),


                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString()

                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }

        void DeleteRow(int goodsId)
        {
            if (Session["userInfo"] != null)
            {

                int del = dal.DeleteGoodsPriceClientType(goodsId);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;

                    logs.Events = "Clear product tier pricing - ID: " + goodsId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("Cleared successfully!");
                }
                else
                {
                    Response.Write("Clearing failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }

        }


    }
}
