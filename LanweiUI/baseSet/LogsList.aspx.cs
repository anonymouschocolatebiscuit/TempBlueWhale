﻿using System;
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
using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class LogsList : BasePage
    {

        public LogsDAL dal = new LogsDAL();

        public string start = "";
        public string end = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (!CheckPower("LogsList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                start = DateTime.Now.AddDays(-7).ToShortDateString();
                end = DateTime.Now.ToShortDateString();


                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 
               
                DateTime start = DateTime.Now.AddDays(-7);
              
                DateTime end = DateTime.Now;
            
                GetDataList(keys,start,end);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end);
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                string id = Request.Params["id"].ToString();
                DeleteRow(id);
                Response.End();
            }
        }

        void GetDataList(string key,DateTime start,DateTime end)
        {
            DataSet ds = dal.GetLogsInfo(LoginUser.ShopId, key, start, end);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    events = ds.Tables[0].Rows[i]["events"].ToString(),
                    users = ds.Tables[0].Rows[i]["users"].ToString(),
                    ip = ds.Tables[0].Rows[i]["ip"].ToString(),
                    date = ds.Tables[0].Rows[i]["date"].ToString()
                    

                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

       

            Response.Write(s);
        }


        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.Delete(id);
                if (del > 0)
                {
                   

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
