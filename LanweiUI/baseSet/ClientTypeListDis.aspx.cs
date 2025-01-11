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
    public partial class ClientTypeListDis : BasePage
    {

        public ClientTypeDAL dal = new ClientTypeDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

         
            if (Request.Params["Action"] == "edit")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                decimal dis = ConvertTo.ConvertDec(Request.Params["dis"].ToString());

               EditRow(id,dis);
               Response.End();
            }


        }

        void GetDataList()
        {
            DataSet ds = dal.GetALLModelList();

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                   // typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    Names = ds.Tables[0].Rows[i]["names"].ToString(),
                    Dis = ds.Tables[0].Rows[i]["dis"].ToString()
                 
                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }


    
        void EditRow(int id,decimal dis)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.UpdateClientTypeDis(id,dis);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改客户类别折扣-ID：" + id.ToString() + " 为：" + dis.ToString();
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
