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
    public partial class WuliuList : BasePage//System.Web.UI.Page
    {
        public WuliuDAL dal = new WuliuDAL();

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
                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    fax = ds.Tables[0].Rows[i]["fax"].ToString(),
                    address = ds.Tables[0].Rows[i]["address"].ToString(),
                    mall = ds.Tables[0].Rows[i]["mall"].ToString(),
                    printModel = ds.Tables[0].Rows[i]["printModel"].ToString(),
                    sendCode = ds.Tables[0].Rows[i]["sendCode"].ToString(),
                    sendName = ds.Tables[0].Rows[i]["sendName"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
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
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),
                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    fax = ds.Tables[0].Rows[i]["fax"].ToString(),
                    address = ds.Tables[0].Rows[i]["address"].ToString(),
                    mall = ds.Tables[0].Rows[i]["mall"].ToString(),
                    printModel = ds.Tables[0].Rows[i]["printModel"].ToString(),
                    sendCode = ds.Tables[0].Rows[i]["sendCode"].ToString(),
                    sendName = ds.Tables[0].Rows[i]["sendName"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
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
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除物流公司-ID：" + id.ToString();
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
