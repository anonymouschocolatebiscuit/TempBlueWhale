using BlueWhale.Common;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.produce
{
    public partial class produceListAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtDateStart.Text = DateTime.Now.ToShortDateString();
                this.txtDateEnd.Text = DateTime.Now.AddDays(1).ToShortDateString();

                this.txtNum.Attributes.Add("onkeyup", "return getBomList()");
            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataBom")
            {
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                int num = ConvertTo.ConvertInt(Request.Params["num"].ToString());
                this.GetDataListBom(goodsId, num);

                Response.End();
            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataListBom();
                Response.End();
            }
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    processId = "",
                    processName = "",
                    num = "",
                    price = "",
                    sumPrice = "",
                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataListBom(int goodsId, int numGoods)
        {
            IList<object> list = new List<object>();

            goodsBomListItem dal = new goodsBomListItem();

            DataSet ds = dal.GetList(" pId=(select top 1 id from goodsBomList where goodsId='" + goodsId + "' ) ");

            int rows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int numBom = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["num"].ToString());
                int rate = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["rate"].ToString());

                // bom quantity/loss rate * goods number = plan quantity
                if (rate == 0)
                {
                    rate = 100;
                }

                float num = numBom * numGoods * rate / 100;

                list.Add(new
                {
                    id = i,
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    numBom = ds.Tables[0].Rows[i]["num"].ToString(),
                    rate = ds.Tables[0].Rows[i]["rate"].ToString(),
                    num = num.ToString(),
                    remarks = ""
                });
            }

            if (rows < 8)
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        code = "",
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",
                        numBom = "",
                        rate = "",
                        num = "",
                        remarks = ""
                    });
                }
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataListBom()
        {
            IList<object> list = new List<object>();

            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    code = "",
                    goodsId = "",
                    goodsName = "",
                    spec = "",
                    unitName = "",
                    numBom = "",
                    rate = "",
                    num = "",
                    remarks = ""
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }
    }
}