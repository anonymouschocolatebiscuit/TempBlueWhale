using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.produce
{
    public partial class ProduceGetList : BasePage
    {
        public DAL.produce.ProduceGetList dal = new DAL.produce.ProduceGetList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                string keys = "";

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;

                GetDataList(keys, start, end);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int types = ConvertTo.ConvertInt(Request.Params["types"].ToString());

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

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
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            isWhere += " and CONVERT(varchar(100),bizDate, 23)>='" + start.ToString("yyyy-MM-dd")
             + "'  and CONVERT(varchar(100),bizDate, 23)<='" + end.ToString("yyyy-MM-dd") + "' ";

            if (key != "")
            {
                isWhere += " and (goodsName like '%" + key + "%' or number like '%" + key + "%' or remarks like '%" + key + "%') ";
            }

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),

                    planNumber = ds.Tables[0].Rows[i]["planNumber"].ToString(),

                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    num = ds.Tables[0].Rows[i]["num"].ToString(),

                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),

                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),

                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata); //when send to grid only need

            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        bool del = dal.Delete(delId);
                        if (del)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Delete Production Material Collection-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete Successfully" + num + "rows records!");
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
                            logs.Events = "Review-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review Success" + num + "rows records!");
                }
                else
                {
                    Response.Write("Review Failed！");
                }
            }
            else
            {
                Response.Write("Login timeout, please log in again!");
            }
        }

        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {
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
                            logs.Events = "Cancel Review-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Cancel Review Success" + num + "rows records!");
                }
                else
                {
                    Response.Write("Cancel Review Failed!");
                }
            }
            else
            {
                Response.Write("Login timeout, please log in again!");
            }
        }
    }
}