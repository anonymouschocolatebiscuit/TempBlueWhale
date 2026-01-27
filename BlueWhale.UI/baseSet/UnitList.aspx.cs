using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.baseSet
{
    public partial class UnitList : BasePage
    {
        public UnitDAL dal = new UnitDAL();

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
            string sortColumn = Request.Params["sortname"];
            string sortOrder = Request.Params["sortorder"];

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            string orderBy = "";
            if (sortColumn != null && sortOrder != null)
            {
                orderBy = $" ORDER BY {sortColumn} {sortOrder}";
            }
            DataSet ds = dal.GetList(isWhere, orderBy);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString()
                });
            }

            object griddata = new { Rows = list };
            string json = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(json);
        }

        void GetDDLList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    typeId = ds.Tables[0].Rows[i]["id"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["names"].ToString()
                });
            }

            string json = new JavaScriptSerializer().Serialize(list);
            Response.Write(json);
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
                        Events = "Deleted unit of measurement - ID: " + id.ToString(),
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
