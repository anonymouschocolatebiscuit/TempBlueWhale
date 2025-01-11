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

namespace Lanwei.Weixin.UI.baseSet
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
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
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

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

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
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    
                    logs.Events = "清除商品等级售价-ID：" + goodsId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("清除成功！");

                }
                else
                {
                    Response.Write("清除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

      
    }
}
