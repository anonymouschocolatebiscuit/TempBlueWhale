using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.report
{
    public partial class ProduceGetListReportDetail : BasePage
    {
        public ProduceGetListItem dal = new ProduceGetListItem();

        public ClientDAL venderDAL = new ClientDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();




            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());

                string code = Request.Params["code"].ToString();

                string ckName = Request.Params["ckName"].ToString();


                this.GetDataList(bizStart, bizEnd, code, ckName);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd, string code, string ckName)
        {



            DataSet ds = dal.GetProduceGetListItemDetail(LoginUser.ShopId, bizStart, bizEnd, ckName, code);




            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),


                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString()



                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}