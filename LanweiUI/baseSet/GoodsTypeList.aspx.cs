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
    public partial class GoodsTypeList : BasePage
    {

        public GoodsTypeDAL dal = new GoodsTypeDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetTreeList")
            {
                GetTreeList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDDLList")
            {
                GetDDLList();
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                DeleteRow(id);
                Response.End();
            }


        }

        void GetDataList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    picUrl = ds.Tables[0].Rows[i]["picUrl"].ToString(),
                    isShowXCX = ds.Tables[0].Rows[i]["isShowXCX"].ToString(),
                    isShowGZH = ds.Tables[0].Rows[i]["isShowGZH"].ToString(),
                    parentId = ds.Tables[0].Rows[i]["parentId"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    parentName = ds.Tables[0].Rows[i]["parentName"].ToString()

                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

         


            Response.Write(s);
        }


        void GetTreeList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();

            
            
            list.Add(new { 
               id=0,
               text="所有类别",
               pid=-1
            
            
            });
            
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    text = ds.Tables[0].Rows[i]["names"].ToString(),
                    pid = ds.Tables[0].Rows[i]["parentId"].ToString()
                  
                });

            }


           
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(list);//传给grid的时候才要




            Response.Write(s);
        }


        void GetDDLList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    parentId = ds.Tables[0].Rows[i]["parentId"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    parentName = ds.Tables[0].Rows[i]["parentName"].ToString()

                });

            }
           // var griddata = new { Rows = list };

          //  string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

            string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }

        void DeleteRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.Delete(id);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除商品类别-ID：" + id.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("删除成功！");

                }
                else
                {
                    Response.Write("删除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }
    }
}
