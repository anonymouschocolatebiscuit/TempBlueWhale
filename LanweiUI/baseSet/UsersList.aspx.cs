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

                this.SetPwd(empId);
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

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


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
            // var griddata = new { Rows = list };

            //  string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

            string s = new JavaScriptSerializer().Serialize(list);//传给dropdownList


            Response.Write(s);
        }

        void SetPwd(int empId)
        {

            if (Session["userInfo"] != null)
            {
                int del = dal.ChangPassword(empId);
               

                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "初始化用户密码-ID：" + empId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("操作成功！");

                }
                else
                {
                    Response.Write("操作失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }



            
 
        
        }

        void DeleteRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                if (id == 1)
                {
                    Response.Write("系统保留、不能删除！");
                    return;
                }

                int del = dal.Delete(id);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除用户-ID：" + id.ToString();
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
