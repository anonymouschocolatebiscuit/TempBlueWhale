using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.BaseSet;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class ClientTypeList : BasePage
    {
        public ClientTypeDAL dal = new ClientTypeDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
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
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString()
                });
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

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
                    typeId = ds.Tables[0].Rows[i]["id"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["names"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString()
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
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Delete customer category-ID: " + id.ToString(),
                        Ip = Request.UserHostAddress.ToString()
                    };
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
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}