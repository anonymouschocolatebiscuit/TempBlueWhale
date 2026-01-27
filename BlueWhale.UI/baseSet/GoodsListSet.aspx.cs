using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.baseSet
{
    public partial class GoodsListSet : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["Action"] == "GetDataList")
            {
                string typeId = Request.Params["typeId"].ToString();
                string code = Request.Params["goodsId"].ToString();
                GetDataList(typeId, code);
                Response.End();
            }

            if (Request.Params["Action"] == "SetShow")
            {
                string cloumn = Request.Params["showType"].ToString();
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                int selectYes = ConvertTo.ConvertInt(Request.Params["selectYes"].ToString());
                SetShow(goodsId, cloumn, selectYes);
                Response.End();
            }
        }

        void GetDataList(string typeId, string code)
        {
            DataSet ds = dal.GetAllModelView(typeId, code);
            IList<object> list = new List<object>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    barcode = ds.Tables[0].Rows[i]["barcode"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    priceSalesWhole = ds.Tables[0].Rows[i]["priceSalesWhole"].ToString(),
                    priceSalesRetail = ds.Tables[0].Rows[i]["priceSalesRetail"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    tj = ds.Tables[0].Rows[i]["tj"].ToString(),
                    xp = ds.Tables[0].Rows[i]["xp"].ToString(),
                    cx = ds.Tables[0].Rows[i]["cx"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),
                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),
                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),
                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()
                });
            }

            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            //string s = new JavaScriptSerializer().Serialize(list);
            Response.Write(s);
        }

        void SetShow(int goodsId, string cloumn, int selectYes)
        {
            if (Session["userInfo"] != null)
            {
                LogsDAL logs = new LogsDAL();
                int num = dal.SetGoodsShow(goodsId, cloumn, selectYes);

                if (num > 0)
                {
                    logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Set Good Display-ID：" + goodsId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();
                    Response.Write("Set Successfully!");
                }
                else
                {
                    Response.Write("Set Failed!");
                }
            }
            else
            {
                Response.Write("Login timed out, please log in again!");
            }
        }
    }
}
