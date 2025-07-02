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
    public partial class ProcessList : BasePage
    {
        public ProcessListDAL dal = new ProcessListDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDDLList")
            {
                string typeId = Request.Params["typeId"].ToString();
                GetDDLList(typeId);
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
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString()
                });
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDDLList(string typeId)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            if (typeId != "0")
            {
                isWhere += " and typeId='" + typeId + "' ";
            }
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    processId = ds.Tables[0].Rows[i]["id"].ToString(),
                    processName = ds.Tables[0].Rows[i]["names"].ToString(),

                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    text = ds.Tables[0].Rows[i]["names"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString()
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
                        Events = "Delete process-ID:" + id.ToString(),
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
