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

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class CangkuList :BasePage
    {
        public CangkuDAL dal = new CangkuDAL();

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

            if (Request.Params["Action"] == "stop")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                StopRow(id);
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
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString()

                });
            }
            //var griddata = new { total = ds.Tables[0].Rows.Count, rows = list };easyui
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

                    ckId = ds.Tables[0].Rows[i]["id"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["names"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString()

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
                    logs.Events = "删除仓库-ID：" + id.ToString();
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


        void StopRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.UpdateFlag(id);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改仓库状态-ID：" + id.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("修改成功！");

                }
                else
                {
                    Response.Write("修改失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

        
    }
}
