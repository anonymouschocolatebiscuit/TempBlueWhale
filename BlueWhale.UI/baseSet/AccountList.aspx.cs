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
using BlueWhale.DAL;
using BlueWhale.Common;

using BlueWhale.UI.src;


namespace BlueWhale.UI.baseSet
{
    
    public partial class AccountList : BasePage
    {
        public AccountDAL dal = new AccountDAL();

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
            string isWhere = " shopId='"+LoginUser.ShopId+"' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    yuePrice = ds.Tables[0].Rows[i]["yuePrice"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString()
                  
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
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    yuePrice = ds.Tables[0].Rows[i]["yuePrice"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString()

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
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Delete Account - ID: " + id.ToString();
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
                Response.Write("Login timed out, please log in again!");
            }

        }

        

    }
}
