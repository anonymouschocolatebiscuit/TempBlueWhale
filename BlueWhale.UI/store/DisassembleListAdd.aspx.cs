using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.store
{
    public partial class DisassembleListAdd : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("DisassembleListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.Bind();   
            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                this.GetDataListSub();
                Response.End();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlInventoryList.DataSource = inventoryDAL.GetList(isWhere);
            this.ddlInventoryList.DataTextField = "Names";
            this.ddlInventoryList.DataValueField = "id";
            this.ddlInventoryList.DataBind();
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