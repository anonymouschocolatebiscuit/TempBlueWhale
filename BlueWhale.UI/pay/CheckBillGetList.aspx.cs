using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.pay
{
    public partial class CheckBillGetList : BasePage
    {
        public CheckBillDAL dal = new CheckBillDAL();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("CheckBillGetList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";// 

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;



                GetDataList(keys, start, end);
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

        void GetDataList(string key, DateTime start, DateTime end)
        {
            DataSet ds = dal.GetAllModelClient(LoginUser.ShopId, key, start, end);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),

                    clientName = ds.Tables[0].Rows[i]["clientName"].ToString(),

                    checkPrice = ds.Tables[0].Rows[i]["checkPrice"].ToString(),

                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

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

                if (!CheckPower("CheckBillGetListDelete"))
                {
                    Response.Write("无此操作权限，请联系管理员!");
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
                            logs.Events = "删除销售收款-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功删除" + num + "条记录!");

                }
                else
                {
                    Response.Write("删除失败!");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆!");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("CheckBillGetListCheck"))
                {
                    Response.Write("无此操作权限!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "审核");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "审核销售收款-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功审核" + num + "条记录!");

                }
                else
                {
                    Response.Write("审核失败!");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆!");
            }

        }


        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("CheckBillGetListCheckNo"))
                {
                    Response.Write("无此操作权限!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "保存");
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "反审核销售收款-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功反审核" + num + "条记录!");

                }
                else
                {
                    Response.Write("反审核失败!");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆!");
            }


        }
    }
}
