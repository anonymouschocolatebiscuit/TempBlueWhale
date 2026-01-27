using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

using System.Web.Services;
using System.Reflection;

namespace BlueWhale.UI.pay
{
    public partial class ReceivableListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();

        public InventoryDAL inventoryDAL = new InventoryDAL();

        public ReceivableDAL dal = new ReceivableDAL();

        public SalesReceiptDAL dalSales = new SalesReceiptDAL();

        public ReceivableAccountItemDAL dalAccount = new ReceivableAccountItemDAL();

        public ReceivableSourceBillItemDAL dalBill = new ReceivableSourceBillItemDAL();


        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("ReceivableListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
                this.Bind();

                this.BindInfo();
            

            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());



                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = string.IsNullOrEmpty(Request.Params["end"].ToString()) ? DateTime.Now : Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end,wlId);
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
            this.ddlVenderList.DataTextField = "names";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());


            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtDisPrice.Text = ds.Tables[0].Rows[0]["disPrice"].ToString();
                this.txtPayPriceNowMore.Text = ds.Tables[0].Rows[0]["PayPriceNowMore"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "Check")
                {
                    this.btnSave.Visible = false;
                }

            }


        }


        void GetDataList(string key, DateTime start, DateTime end,int wlId)
        {
            DataSet ds = dalSales.GetAllModelByWLId(key, start, end, wlId);

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
                    bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow="0"



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//Only needed when passing to the grid



            Response.Write(s);
        }



   
        void GetDataList(int pId)
        {

            DataSet ds = dalAccount.GetAllModel(pId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    bkId = ds.Tables[0].Rows[i]["bkId"].ToString(),
                    bkName = ds.Tables[0].Rows[i]["bkName"].ToString(),
                    payPrice = ds.Tables[0].Rows[i]["payPrice"].ToString(),
                    payTypeId = ds.Tables[0].Rows[i]["payTypeId"].ToString(),
                    payTypeName = ds.Tables[0].Rows[i]["payTypeName"].ToString(),
                    payNumber = ds.Tables[0].Rows[i]["payNumber"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()

                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub(int pId)
        {

            DataSet ds = dalBill.GetAllModel(pId);

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
