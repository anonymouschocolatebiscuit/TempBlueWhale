﻿using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class SalesOrderListDetailReport : BasePage
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();
        public ClientDAL venderDAL = new ClientDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public InventoryDAL inventoryDAL = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());
                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();
                string typeId = Request.Params["typeId"].ToString();

                this.GetDataList(bizStart, bizEnd, wlId, goodsId, typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string wlId, string goodsId, string typeId)
        {
            DataSet ds = dal.GetSalesReceiptItemDetail(LoginUser.ShopId, bizStart, bizEnd, typeId, wlId, goodsId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    types = ds.Tables[0].Rows[i]["types"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    priceNow = ds.Tables[0].Rows[i]["priceNow"].ToString(),
                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}