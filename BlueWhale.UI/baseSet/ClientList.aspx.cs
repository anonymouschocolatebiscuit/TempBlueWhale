using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.baseSet
{
    public partial class ClientList : BasePage
    {
        public ClientDAL dal = new ClientDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(keys);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDDLList")
            {
                GetDDLList();
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                string idString = Request.Params["idString"].ToString();
                DeleteRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "checkRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "checkNoRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckNoRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "setPwd")
            {
                int empId = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                this.SetPwd(empId);
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
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),
                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),
                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                });
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataListSearch(string key)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            isWhere += " and (names like'%" + key + "%'" +
                    " or  code like'%" + key + "%' " +
                    " or  tel like'%" + key + "%' " +
                    " or  remarks like'%" + key + "%' " +
                    " or  address like'%" + key + "%') ";

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),
                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),
                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
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
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),
                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),
                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                });
            }

            string s = new JavaScriptSerializer().Serialize(list);

            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());
                        int del = dal.Delete(delId);

                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Delete Client-ID: " + id.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete Successfully " + num + " rows records!");
                }
                else
                {
                    Response.Write("Delete Failed!");
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

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());
                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Review");

                        if (del > 0)
                        {
                            num += 1;
                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Review customer-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review Successfully " + num + " rows records!");
                }
                else
                {
                    Response.Write("Review Failed!");
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

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "Save");
                        if (del > 0)
                        {
                            num += 1;
                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Reverse review customer-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Reject Successfully " + num + " rows records!");
                }
                else
                {
                    Response.Write("Reject Failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }

        void SetPwd(int empId)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.UpdateClientPwd(empId);

                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Initialize user password-ID: " + empId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("Execution successful!");
                }
                else
                {
                    Response.Write("Operation failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}