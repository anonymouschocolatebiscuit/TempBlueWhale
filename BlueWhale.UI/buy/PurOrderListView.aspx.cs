using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.buy
{
    public partial class PurOrderListView : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();
        public PurOrderDAL dal = new PurOrderDAL();
        public PurOrderItemDAL item = new PurOrderItemDAL();
        public string fromId = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("PurOrderList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtSendDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();
                this.BindInfo();
            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);
                Response.End();
            }
        }

        public void Bind()
        {
            this.ddlVenderList.DataSource = venderDAL.GetAllModel();
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

            this.ddlInventoryList.DataSource = inventoryDAL.GetALLModelList();
            this.ddlInventoryList.DataTextField = "Names";
            this.ddlInventoryList.DataValueField = "id";
            this.ddlInventoryList.DataBind();
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtSendDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["sendDate"].ToString()).ToShortDateString();

                int typeId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["types"].ToString());
                if (typeId == 1)
                    this.rb1.Checked = true;
                if (typeId == -1)
                    this.rb2.Checked = true;

                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                //this.lbNumber.Text = ds.Tables[0].Rows[0]["number"].ToString();

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

            }
        }

        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            // int pId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            DataSet ds = item.GetAllModel(id);

            int rows = ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    disPrice = ds.Tables[0].Rows[i]["disPrice"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    taxPrice = ds.Tables[0].Rows[i]["taxPrice"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            if (rows < 8) // less than 8 line
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        unitName = "",
                        num = "",
                        price = "",
                        dis = "",
                        disPrice = "",
                        sumPrice = "",
                        tax = "",
                        taxPrice = "",
                        sumPriceAll = "",
                        ckId = "",
                        ckName = "",
                        remarks = ""
                    });
                }
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}