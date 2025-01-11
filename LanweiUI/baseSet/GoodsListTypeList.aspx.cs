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
    public partial class GoodsListTypeList : BasePage
    {

        public DAL.wx_wxmall_goods_category dal = new wx_wxmall_goods_category();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }


            if (Request.Params["Action"] == "checkRow")
            {
                string idString = Request.Params["idString"].ToString();
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                CheckRow(idString, goodsId);
                Response.End();
            }



        }

        void GetDataList()
        {
            DataSet ds = dal.GetList(" shopId='"+LoginUser.ShopId+"' and isStart=1 ");

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    categoryName = ds.Tables[0].Rows[i]["categoryName"].ToString()
                 
                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }

        void CheckRow(string id,int goodsId)
        {
            if (Session["userInfo"] != null)
            {

               


                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {

                    bool delete = dal.DeleteTypeList(goodsId);

                    for (int i = 0; i < idString.Length; i++)
                    {
                        int typeId = ConvertTo.ConvertInt(idString[i].ToString());

                        int add = dal.AddTypeList(goodsId, typeId);
                        if (add > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "设置商品导航-ID：" + typeId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("设置成功" + num + "条记录！");

                }
                else
                {
                    Response.Write("设置失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

    
       

    }
}
