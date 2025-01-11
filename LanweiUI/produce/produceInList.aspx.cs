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

using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Lanwei.Weixin.UI.produce
{
    public partial class produceInList : BasePage
    {
        public ProduceInDAL dal = new ProduceInDAL();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("produceInList"))
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

                GetDataList(keys, start, end,types);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int types =ConvertTo.ConvertInt(Request.Params["types"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end,types);
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

        void GetDataList(string key, DateTime start, DateTime end,int types)
        {
            DataSet ds = dal.GetAllModel(LoginUser.ShopId, key, start, end, types);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                   
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),

                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                  
                  
               

                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                   
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                
                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                   


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

                //if (!CheckPower("SalesReceiptListDelete"))
                //{
                //    Response.Write("无此操作权限，请联系管理员！");
                //    return;
                //}

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

                            
                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "删除生产入库-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {

                   
                    Response.Write("成功删除"+num+"条记录！");

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

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                //if (!CheckPower("SalesReceiptListCheck"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId,LoginUser.Id,LoginUser.Names, DateTime.Now, "审核");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "审核生产入库-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("审核失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }


        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                //if (!CheckPower("SalesReceiptListCheckNo"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateCheck(delId, LoginUser.Id,LoginUser.Names, DateTime.Now, "保存");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "反审核生产入库-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功反审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("反审核失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }


        }

       
    }
}
