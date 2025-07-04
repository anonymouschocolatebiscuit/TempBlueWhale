using BlueWhale.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using BlueWhale.UI.src;
using BlueWhale.DAL;

namespace BlueWhale.UI.produce
{
    public partial class goodsBomList : BasePage //System.Web.UI.Page
    {
        DAL.produce.goodsBomListType dalType = new DAL.produce.goodsBomListType();
        DAL.produce.goodsBomList dal = new DAL.produce.goodsBomList();
        DAL.produce.goodsBomListItem itemDAL = new DAL.produce.goodsBomListItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["Action"] == "GetTreeList")
            {
                GetTreeList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                int typeId = ConvertTo.ConvertInt(Request.Params["typeId"].ToString());
                GetDataList(typeId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSub")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["pId"].ToString());
                GetDataListSub(pId);
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                string id = Request.Params["id"].ToString();
                DeleteRow(id);
                Response.End();
            }

            if (Request.Params["Action"] == "checkRow")
            {
                string id = Request.Params["id"].ToString();
                CheckRow(id);
                Response.End();
            }

            if (Request.Params["Action"] == "checkNoRow")
            {
                string id = Request.Params["id"].ToString();
                CheckNoRow(id);
                Response.End();
            }

            if (!Page.IsPostBack)
            {

            }
        }

        void GetTreeList()
        {
            IList<object> listTree = new List<object>();

            DataSet ds = dalType.GetList(" shopId='" + LoginUser.ShopId + "' ");
            DataTable dt = ds.Tables[0];

            listTree.Add(new
            {
                id = "0",
                text = "All",
                sortId = 1,
                pid = -1

            });

            for (var i = 0; i < dt.Rows.Count; i++)
            {

                listTree.Add(new
                {
                    id = dt.Rows[i]["id"].ToString(),
                    text = dt.Rows[i]["names"].ToString(),
                    sortId = dt.Rows[i]["sortId"].ToString(),
                    pid = 0

                });

            }

            var griddata = new { Rows = listTree };

            string s = new JavaScriptSerializer().Serialize(listTree);

            Response.Write(s);
        }

        void GetDataList(int typeId)
        {
            IList<object> list = new List<object>();
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            if (typeId != 0)
            {
                isWhere += "  and typeId='" + typeId + "'  ";
            }
            DataSet ds = dal.GetList(isWhere);
            DataTable dt = ds.Tables[0];

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new
                {
                    id = dt.Rows[i]["id"].ToString(),
                    typeName = dt.Rows[i]["typeName"].ToString(),
                    number = dt.Rows[i]["number"].ToString(),
                    edition = dt.Rows[i]["edition"].ToString(),
                    tuhao = dt.Rows[i]["tuhao"].ToString(),
                    flagUse = dt.Rows[i]["flagUse"].ToString(),
                    flagCheck = dt.Rows[i]["flagCheck"].ToString(),
                    code = dt.Rows[i]["code"].ToString(),
                    goodsName = dt.Rows[i]["goodsName"].ToString(),
                    spec = dt.Rows[i]["spec"].ToString(),
                    unitName = dt.Rows[i]["unitName"].ToString(),
                    num = dt.Rows[i]["num"].ToString(),
                    rate = dt.Rows[i]["rate"].ToString(),
                    remarks = dt.Rows[i]["remarks"].ToString(),

                    makeId = dt.Rows[i]["makeId"].ToString(),
                    makeName = dt.Rows[i]["makeName"].ToString(),
                    makeDate = dt.Rows[i]["makeDate"].ToString(),

                    checkId = dt.Rows[i]["checkId"].ToString(),
                    checkName = dt.Rows[i]["checkName"].ToString(),
                    checkDate = dt.Rows[i]["checkDate"].ToString()
                });
            }

            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataListSub(int pId)
        {
            IList<object> list = new List<object>();

            DataSet ds = itemDAL.GetList(" pId='" + pId + "' ");
            DataTable dt = ds.Tables[0];

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new
                {
                    itemId = dt.Rows[i]["itemId"].ToString(),
                    code = dt.Rows[i]["code"].ToString(),
                    goodsName = dt.Rows[i]["goodsName"].ToString(),
                    spec = dt.Rows[i]["spec"].ToString(),
                    unitName = dt.Rows[i]["unitName"].ToString(),
                    num = dt.Rows[i]["num"].ToString(),
                    rate = dt.Rows[i]["rate"].ToString(),
                    remarks = dt.Rows[i]["remarks"].ToString()

                });
            }

            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();

                int delId = ConvertTo.ConvertInt(id);

                int num = 0;

                bool del = dal.Delete(delId);
                if (del)
                {
                    num += 1;

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Delete BOM checklist - ID：" + delId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();
                }

                if (num > 0)
                {
                    Response.Write("Delete successfully " + num + "rows records!");
                }
                else
                {
                    Response.Write("Delete failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();

                int delId = ConvertTo.ConvertInt(id);

                int num = 0;

                int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Reviewed");
                if (del > 0)
                {
                    num += 1;

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Review BOM checklist - ID：" + delId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();
                }

                if (num > 0)
                {
                    Response.Write("Review successfully " + num + "rows records!");
                }
                else
                {
                    Response.Write("Review failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }

        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();

                int delId = ConvertTo.ConvertInt(id);

                int num = 0;

                int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Pending");
                if (del > 0)
                {
                    num += 1;

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Cancel review BOM checklist - ID：" + delId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();
                }

                if (num > 0)
                {
                    Response.Write("Cancel review successfully " + num + "rows records!");
                }
                else
                {
                    Response.Write("Cancel review failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}