using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.store
{
    public partial class AssembleListEdit : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        public AssembleDAL dal = new AssembleDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (!CheckPower("AssembleListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();

                this.BindInfo();

            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                this.GetDataList(id);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                this.GetDataListSub(id);
                Response.End();
            }

        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                txtFee.Text = ds.Tables[0].Rows[0]["fee"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "Review")
                {
                    btnSave.Visible = false;
                }

            }

        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            ddlCangkuList.DataSource = inventoryDAL.GetList(isWhere);
            ddlCangkuList.DataTextField = "Names";
            ddlCangkuList.DataValueField = "id";
            ddlCangkuList.DataBind();
        }

        void GetDataList(int id)
        {
            DataSet ds = dal.GetAllModel(id);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
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
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
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
