using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.baseSet
{
    public partial class UsersList : BasePage
    {
        public UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (LoginUser.RoleId != 0)
                {
                    if (!CheckPower("UsersList"))
                    {
                        Response.Redirect("../OverPower.htm");
                    }
                }
            }

            // Handle different request actions
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

            if (Request.Params["Action"] == "setPwd")
            {
                int empId = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                SetPwd(empId);
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                DeleteRow(id);
                Response.End();
            }
        }

        // Method to fetch and return data list as JSON
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
                    loginName = ds.Tables[0].Rows[i]["phone"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    pwd = ds.Tables[0].Rows[i]["pwd"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    email = ds.Tables[0].Rows[i]["email"].ToString(),
                    roleId = ds.Tables[0].Rows[i]["roleId"].ToString(),
                    deptId = ds.Tables[0].Rows[i]["deptId"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    address = ds.Tables[0].Rows[i]["address"].ToString(),
                    brithDay = ds.Tables[0].Rows[i]["brithDay"].ToString(),
                    comeDate = ds.Tables[0].Rows[i]["comeDate"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    deptName = ds.Tables[0].Rows[i]["deptName"].ToString(),
                    roleName = ds.Tables[0].Rows[i]["roleName"].ToString()
                });
            }

            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata); // Serialize grid data for response
            Response.Write(s);
        }

        // Method to fetch and return dropdown list data as JSON
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
                    loginName = ds.Tables[0].Rows[i]["phone"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    pwd = ds.Tables[0].Rows[i]["pwd"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    email = ds.Tables[0].Rows[i]["email"].ToString(),
                    roleId = ds.Tables[0].Rows[i]["roleId"].ToString(),
                    deptId = ds.Tables[0].Rows[i]["deptId"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    address = ds.Tables[0].Rows[i]["address"].ToString(),
                    brithDay = ds.Tables[0].Rows[i]["brithDay"].ToString(),
                    comeDate = ds.Tables[0].Rows[i]["comeDate"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    deptName = ds.Tables[0].Rows[i]["deptName"].ToString(),
                    roleName = ds.Tables[0].Rows[i]["roleName"].ToString()
                });
            }

            string s = new JavaScriptSerializer().Serialize(list); // Serialize for dropdown
            Response.Write(s);
        }

        // Method to set the password of a user
        void SetPwd(int empId)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.ChangPassword(empId);

                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Initialize user password-ID: " + empId.ToString(),
                        Ip = Request.UserHostAddress.ToString()
                    };
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

        // Method to delete a user
        void DeleteRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                if (id == 1)
                {
                    Response.Write("The system reserves this and it cannot be deleted!");
                    return;
                }

                int del = dal.Delete(id);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Delete User-ID: " + id.ToString(),
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
