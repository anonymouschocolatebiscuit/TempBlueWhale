using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
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
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }


        void GetTreeList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);
            IList<object> list = new List<object>();
            list.Add(new
            {
                id = 0,
                text = "All Category",
                pid = -1
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
            string s = new JavaScriptSerializer().Serialize(list);
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
                    logs.Events = "Delete product category-ID：" + id.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("Deletion successful!");
                }
                else
                {
                    Response.Write("Deletion failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again！");
            }
        }
    }
}