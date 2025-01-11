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
using Lanwei.Weixin.DBUtility;

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;



namespace Lanwei.Weixin.UI.baseSet
{
    public partial class DeptList : BasePage
    {

        public DeptDAL dal = new DeptDAL();

        public CorpDAL dalCorp = new CorpDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList(0);
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

     


        void GetDataList(int parentId)
        {
            DataSet ds = dal.GetAllDept();

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    deptId = ds.Tables[0].Rows[i]["deptId"].ToString(),
                    deptName = ds.Tables[0].Rows[i]["deptName"].ToString(),
                    parentId = ds.Tables[0].Rows[i]["parentId"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    parentName = ds.Tables[0].Rows[i]["parentName"].ToString()

                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

         


            Response.Write(s);
        }


        void GetDDLList()
        {
            DataSet ds = dal.GetAllDept();

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    deptId = ds.Tables[0].Rows[i]["deptId"].ToString(),
                    deptName = ds.Tables[0].Rows[i]["deptName"].ToString(),
                    parentId = ds.Tables[0].Rows[i]["parentId"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
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
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除部门-ID：" + id.ToString();
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
