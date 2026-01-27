using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.pay
{
    public partial class OtherGetListAdd : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public InventoryDAL cangkuDAL = new InventoryDAL();
        public AccountDAL accountDAL = new AccountDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("OtherGetListAdd"))
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
        }

        public void Bind()
        {
            ListItem item = new ListItem("(Empty)", "0");
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();
            this.ddlVenderList.Items.Insert(0, item);
            this.ddlVenderList.SelectedValue = "0";

            this.ddlBankList.DataSource = accountDAL.GetList(isWhere);
            this.ddlBankList.DataTextField = "CodeName";
            this.ddlBankList.DataValueField = "id";
            this.ddlBankList.DataBind();
        }

        private void GetDataList()
        {
            IList<object> list = new List<object>();

            for (int i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    typeId = "",
                    typeName = "",
                    price = "",
                    remarks = ""
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string jsonString = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(jsonString);
        }
    }
}
