using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.store
{
    public partial class AssembleList : BasePage
    {
        public AssembleDAL dal = new AssembleDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("AssembleList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";

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

            if (Request.Params["Action"] == "GetDataListSearchSub")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["pId"].ToString());

                GetDataListSub(pId);
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
            DataSet ds = dal.GetAllModel(LoginUser.ShopId, key, start, end);

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
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    remarksItem = ds.Tables[0].Rows[i]["remarksItem"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
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
                if (!CheckPower("AssembleListDelete"))
                {
                    Response.Write("You do not have this permission, please contact the administrator");
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
                            logs.Events = "Delete Assemble Item-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete successfully " + num + " record!");
                }
                else
                {
                    Response.Write("Delete Failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                if (!CheckPower("AssembleListCheck"))
                {
                    Response.Write("You do not have this permission!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, DateTime.Now, "Check");
                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Check Assemble Item-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Check successfully " + num + " record!");
                }
                else
                {
                    Response.Write("Check Failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                if (!CheckPower("AssembleListCheckNo"))
                {
                    Response.Write("You do not have this permission!");

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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, DateTime.Now, "Save");
                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Check No Assemble Item-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Check No successfully " + num + " record!");
                }
                else
                {
                    Response.Write("Check No Failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }

        void GetDataListSub(int pId)
        {
            AssembleItemDAL item = new AssembleItemDAL();

            DataSet ds = item.GetAllModel(pId, -1);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
