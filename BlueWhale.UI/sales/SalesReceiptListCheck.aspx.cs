﻿using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.sales
{
    public partial class SalesReceiptListCheck : BasePage
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public ClientDAL venderDAL = new ClientDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesReceiptListCheck"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}
                
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;

                int types = 0;

                GetDataList(keys, start, end, types);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int types = ConvertTo.ConvertInt(Request.Params["types"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end, types);
                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                string id = Request.Params["id"].ToString();
                DeleteRow(id);
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
        }

        void GetDataList(string key, DateTime start, DateTime end, int types)
        {
            DataSet ds = dal.GetAllModel(LoginUser.ShopId, key, start, end, types);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),

                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),

                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    sendPayType = ds.Tables[0].Rows[i]["sendPayType"].ToString(),
                    sendName = ds.Tables[0].Rows[i]["sendName"].ToString(),
                    sendCode = ds.Tables[0].Rows[i]["sendCode"].ToString(),
                    sendNumber = ds.Tables[0].Rows[i]["sendNumber"].ToString(),

                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    disPrice = ds.Tables[0].Rows[i]["disPrice"].ToString(),

                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesReceiptListDelete"))
                {
                    Response.Write("You do not have this permission, please contact the administrator!");
                    return;
                }

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

                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Delete Sales Outbound Order Inquiry-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();

                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete Successfully, " + num + "rows records！");
                }
                else
                {
                    Response.Write("Delete Failed！");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!！");
            }
        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                if (!CheckPower("SalesReceiptListCheck"))
                {
                    Response.Write("No permission for this operation!！");

                    return;
                }

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

                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Review Sales Outbound Order-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review Successfully" + num + "rows records！");
                }
                else
                {
                    Response.Write("Review Failed！");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!！");
            }
        }

        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("SalesReceiptListCheckNo"))
                {
                    Response.Write("No permission for this operation!！");

                    return;
                }

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


                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Decline Sales Outbound Order-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();

                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Decline" + num + "rows records！");
                }
                else
                {
                    Response.Write("Failed！");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!！");
            }
        }
    }
}