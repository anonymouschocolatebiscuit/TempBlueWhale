using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace BlueWhale.UI.pay
{
    public partial class CheckBillGetListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();

        public ReceivableDAL dalGet = new ReceivableDAL();
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public CheckBillDAL dalCheck = new CheckBillDAL();
        public CheckBillItemReceivableDAL itemRecei = new CheckBillItemReceivableDAL();
        public CheckBillItemSalesDAL itemSales = new CheckBillItemSalesDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                if (!CheckPower("CheckBillGetListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                this.txtDateStar1.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd1.Text = DateTime.Now.ToShortDateString();


                this.Bind();

                this.BindInfo();

            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());



                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end, wlId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearchGet")
            {

                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());



                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataListGet(keys, start, end, wlId);
                Response.End();
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

        public void Bind()
        {


            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();


        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());


            DataSet ds = dalCheck.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["ClientIdA"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();


                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "审核")
                {
                    this.btnSave.Visible = false;
                }

            }


        }


        void GetDataListGet(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dalGet.GetAllModel(wlId, start, end, key);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();


                decimal sumPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payPriceSum"].ToString());
                decimal priceCheckNowSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal priceCheckNo = sumPrice - priceCheckNowSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["payPriceSum"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow = "0"



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);



            Response.Write(s);
        }


        void GetDataList(string key, DateTime start, DateTime end, int wlId)
        {
            DataSet ds = dal.GetAllModelByWLId(key, start, end, wlId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();
                if (bizType == "1")
                {
                    bizType = "Sales";
                }
                if (bizType == "-1")
                {
                    bizType = "Sales Refund";

                }

                decimal sumPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceAll"].ToString());
                decimal priceCheckNowSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal priceCheckNo = sumPrice - priceCheckNowSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow = "0"



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);



            Response.Write(s);
        }


        void GetDataList(int pId)
        {

            DataSet ds = itemRecei.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceBill"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["sumPriceCheck"].ToString(),
                    priceCheckNo = ds.Tables[0].Rows[i]["sumPriceCheckNo"].ToString(),
                    priceCheckNow = ds.Tables[0].Rows[i]["priceCheckNow"].ToString()
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub(int pId)
        {
            DataSet ds = itemSales.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {

                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceBill"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["sumPriceCheck"].ToString(),
                    priceCheckNo = ds.Tables[0].Rows[i]["sumPriceCheckNo"].ToString(),
                    priceCheckNow = ds.Tables[0].Rows[i]["priceCheckNow"].ToString()

                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
