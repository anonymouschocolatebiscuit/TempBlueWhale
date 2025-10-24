using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.store
{
    public partial class InventoryTransferListEdit : BasePage
    {
        public InventoryTransferDAL dal = new InventoryTransferDAL();
        public InventoryTransferDetailsDAL itemDAL = new InventoryTransferDetailsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("InventoryTransferListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }
                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.BindInfo();
            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);
                Response.End();
            }
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {

                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());
                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag != "Save")
                {
                    this.btnSave.Visible = false;
                }
            }
        }

        void GetDataList(int id)
        {
            IList<object> list = new List<object>();
            DataSet ds = itemDAL.GetAllModel(id);
            int rows = ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    ckIdIn = ds.Tables[0].Rows[i]["ckIdIn"].ToString(),
                    ckIdOut = ds.Tables[0].Rows[i]["ckIdOut"].ToString(),
                    ckNameIn = ds.Tables[0].Rows[i]["ckNameIn"].ToString(),
                    ckNameOut = ds.Tables[0].Rows[i]["ckNameOut"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            if (rows < 8)
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",
                        num = "",
                        ckIdIn = "",
                        ckIdOut = "",
                        ckNameIn = "",
                        ckNameOut = "",
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