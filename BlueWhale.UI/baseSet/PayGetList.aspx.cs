using System;
using System.Data;

using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using BlueWhale.Common;

namespace BlueWhale.UI.baseSet
{
    public partial class PayGetList : BasePage
    {

        public PayGetDAL dal = new PayGetDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDDLList")
            {
                string type = Request.Params["type"].ToString();

                GetDDLList(type);
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
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString()
                 
                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

        

            Response.Write(s);
        }


        void GetDDLList(string types)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' and types='"+types+"' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    
                    typeId = ds.Tables[0].Rows[i]["id"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["names"].ToString(),

                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    text = ds.Tables[0].Rows[i]["names"].ToString()

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
                    logs.Events = "删除收支类别-ID：" + id.ToString();
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
