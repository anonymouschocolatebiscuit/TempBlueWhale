using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.store
{
    public partial class AssembleListAdd : BasePage
    {
        public InventoryDAL inventoryDAL = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckPower("AssembleListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                txtBizDate.Text = DateTime.Now.ToShortDateString();
                Bind();
            }

            if (Request.Params["Action"] == "GetData")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                GetDataListSub();
                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            ddlInventoryList.DataSource = inventoryDAL.GetList(isWhere);
            ddlInventoryList.DataTextField = "Names";
            ddlInventoryList.DataValueField = "id";
            ddlInventoryList.DataBind();
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 2; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = "",
                    goodsName = "",
                    unitName = "",
                    spec = "",
                    num = "",
                    price = "",
                    sumPrice = "",
                    ckId = "",
                    ckName = "",
                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = "",
                    goodsName = "",
                    unitName = "",
                    spec = "",
                    num = "",
                    price = "",
                    sumPrice = "",
                    ckId = "",
                    ckName = "",
                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
