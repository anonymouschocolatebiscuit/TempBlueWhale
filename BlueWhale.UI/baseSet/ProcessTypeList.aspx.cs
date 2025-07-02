using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class ProcessTypeList : BasePage
    {
        public ProcessTypeDAL dal = new ProcessTypeDAL();

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
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString()
                });
            }

            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata); // Only when pass to grid

            Response.Write(s);
        }

        void GetTreeList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>
            {
                new
                {
                    id = 0,
                    text = "AllProcessType",
                    pid = -1
                }
            };

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    text = ds.Tables[0].Rows[i]["names"].ToString(),
                    pid = 0
                });
            }

            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(list); // Only when pass to grid

            Response.Write(s);
        }

        void GetDDLList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "'  ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    typeId = ds.Tables[0].Rows[i]["id"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["names"].ToString(),

                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    text = ds.Tables[0].Rows[i]["names"].ToString()
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
                    logs.Events = "Delete Process Type By ID：" + id.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("Delete Successful!");
                }
                else
                {
                    Response.Write("Delete Fail!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}
