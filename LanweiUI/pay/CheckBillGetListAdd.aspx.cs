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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;

namespace Lanwei.Weixin.UI.pay
{
    public partial class CheckBillGetListAdd : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        public ReceivableDAL dalGet = new ReceivableDAL();
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("CheckBillGetListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                this.txtDateStar1.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd1.Text = DateTime.Now.ToShortDateString();


                this.Bind();
            

            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());



                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end,wlId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearchGet")
            {

                string keys = Request.Params["keys"].ToString();

                int wlId = ConvertTo.ConvertInt(Request.Params["wlId"].ToString());



                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataListGet(keys, start, end, wlId);
                Response.End();
            }


            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataSub")
            {
                this.GetDataListSub();
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

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要



            Response.Write(s);
        }

       
        void GetDataList(string key, DateTime start, DateTime end,int wlId)
        {
            DataSet ds = dal.GetAllModelByWLId(key, start, end,wlId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["types"].ToString();
                if (bizType == "1")
                {
                    bizType = "普通销售";
                }
                if (bizType == "-1")
                {
                    bizType = "销售退货";

                }

                decimal sumPrice = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceAll"].ToString());
                decimal priceCheckNowSum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString());

                decimal priceCheckNo = sumPrice - priceCheckNowSum;

                list.Add(new
                {
                    sourceNumber = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType =bizType,
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    priceCheckNowSum = ds.Tables[0].Rows[i]["priceCheckNowSum"].ToString(),
                    priceCheckNo = priceCheckNo.ToString("0.00"),
                    priceCheckNow="0"



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要



            Response.Write(s);
        }

   
        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 5; i++)
            {
                list.Add(new
                {
                    sourceNumber = "",
                    bizType = "",
                    bizDate = "",
                    sumPriceAll = "",
                    priceCheckNowSum = "",
                    priceCheckNo = "",
                    priceCheckNow = ""
                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListSub()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 5; i++)
            {
                list.Add(new
                {

                    sourceNumber = "",
                    bizType = "",
                    bizDate = "",
                    sumPriceAll = "",
                    priceCheckNowSum = "",
                    priceCheckNo = "",
                    priceCheckNow = ""
                 
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
