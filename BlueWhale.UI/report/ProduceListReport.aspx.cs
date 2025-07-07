using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL.produce;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.report
{
    public partial class ProduceListReport : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public ProduceListDAL dal = new ProduceListDAL();

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
                DateTime bizEnd = DateTime.Now;
                if (!string.IsNullOrEmpty(Request.Params["end"]))
                {
                     bizEnd = DateTime.Parse(Request.Params["end"].ToString());
                }

                string keys = Request.Params["keys"].ToString();
                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();
                string typeId = Request.Params["typeId"].ToString();

                this.GetDataList(keys, bizStart, bizEnd, wlId, goodsId, typeId);
                Response.End();
            }
        }

        //private void GetDataList(string keys, DateTime bizStart, DateTime bizEnd, string wlId, string goodsId, string typeId)
        //{
        //    DataSet ds = dal.GetProduceListReport(keys, LoginUser.ShopId, bizStart, bizEnd, wlId, goodsId, typeId);

        //    IList<object> list = new List<object>();
        //    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        list.Add(new
        //        {
        //            id = ds.Tables[0].Rows[i]["id"].ToString(),
        //            number = ds.Tables[0].Rows[i]["number"].ToString(),
        //            typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
        //            orderNumber = ds.Tables[0].Rows[i]["orderNumber"].ToString(),
        //            dateStart = ds.Tables[0].Rows[i]["dateStart"].ToString(),
        //            dateEnd = ds.Tables[0].Rows[i]["dateEnd"].ToString(),
        //            wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
        //            goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
        //            goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
        //            code = ds.Tables[0].Rows[i]["code"].ToString(),
        //            spec = ds.Tables[0].Rows[i]["spec"].ToString(),
        //            unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
        //            num = ds.Tables[0].Rows[i]["num"].ToString(),
        //            flag = ds.Tables[0].Rows[i]["flag"].ToString(),
        //            makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
        //            makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
        //            checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
        //            remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
        //            Num = ds.Tables[0].Rows[i]["Num"].ToString(),
        //            finishNum = ds.Tables[0].Rows[i]["finishNum"].ToString(),
        //            finishNumNo = ds.Tables[0].Rows[i]["finishNumNo"].ToString(),
        //            sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString()

        //        });
        //    }
        //    var griddata = new { Rows = list, Total = list.Count.ToString() };
        //    string s = new JavaScriptSerializer().Serialize(griddata);
        //    Response.Write(s);
        //}

        private void GetDataList(string keys, DateTime bizStart, DateTime bizEnd, string wlId, string goodsId, string typeId)
        {
            DataSet ds = dal.GetProduceListReport(keys, LoginUser.ShopId, bizStart, bizEnd, wlId, goodsId, typeId);

            IList<object> list = new List<object>();

            // Add data from database
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    orderNumber = ds.Tables[0].Rows[i]["orderNumber"].ToString(),
                    dateStart = ds.Tables[0].Rows[i]["dateStart"].ToString(),
                    dateEnd = ds.Tables[0].Rows[i]["dateEnd"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    Num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    finishNum = ds.Tables[0].Rows[i]["finishNum"].ToString(),
                    finishNumNo = ds.Tables[0].Rows[i]["finishNumNo"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString()
                });
            }

            // Add hardcoded data row
            list.Add(new
            {
                id = "9999",
                number = "INV-HARD001",
                typeName = "Manual Entry",
                orderNumber = "ORD-0001",
                dateStart = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"),
                dateEnd = DateTime.Now.ToString("yyyy-MM-dd"),
                wlName = "Test Vendor",
                goodsId = "G123",
                goodsName = "Hardcoded Product",
                code = "HC-001",
                spec = "Standard Spec",
                unitName = "pcs",
                num = "100",
                flag = "1",
                makeDate = DateTime.Now.ToString("yyyy-MM-dd"),
                makeName = "Admin",
                checkName = "Reviewer",
                remarks = "This is a test row",
                Num = "100",
                finishNum = "80",
                finishNumNo = "20",
                sendDate = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd")
            });

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
