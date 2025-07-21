using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;


namespace BlueWhale.UI.store
{
    public partial class InventoryTransferList : BasePage
    {
        public InventoryTransferDAL dal = new InventoryTransferDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                string keys = Request.Params["keys"].ToString();

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());
                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

                string ckIdIn = Request.Params["ckIdIn"].ToString();
                string ckIdOut = Request.Params["ckIdOut"].ToString();

                this.GetDataList(keys, bizStart, bizEnd, ckIdIn, ckIdOut);
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

        void GetDataList(string key, DateTime start, DateTime end, string ckIdIn, string ckIdOut)
        {
            DataSet ds = dal.GetAllModel(LoginUser.ShopId, key, start, end, ckIdIn, ckIdOut);

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
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    ckNameOut = ds.Tables[0].Rows[i]["ckNameOut"].ToString(),
                    ckNameIn = ds.Tables[0].Rows[i]["ckNameIn"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {
                if (!CheckPower("InventoryTransferListDelete"))
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
                            logs.Events = "Delete Inventory Transfer-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Delete " + num + " record(s) successfully!");
                }
                else
                {
                    Response.Write("Fail to delete!");
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
                if (!CheckPower("InventoryTransferListCheck"))
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
                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "review");

                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "Review Inventory Transfer-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Review " + num + "records successfully!");
                }
                else
                {
                    Response.Write("Fail to review!");
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
                if (!CheckPower("InventoryTransferListCheckNo"))
                {
                    Response.Write("No permission for this operation!");

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
                            logs.Events = "Reject Inventory Transfer-ID: " + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();
                        }
                    }
                }

                if (num > 0)
                {
                    Response.Write("Cancel " + num + " Review records succssfully");
                }
                else
                {
                    Response.Write("Fail to cancel review!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}
