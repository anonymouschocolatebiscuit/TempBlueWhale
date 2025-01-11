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

namespace Lanwei.Weixin.UI.buy
{
    public partial class PurOrderListEdit : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        public PurOrderDAL dal = new PurOrderDAL();

        public  PurOrderItemDAL item = new PurOrderItemDAL();

        public string fromId = "0";

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {

                if (!CheckPower("PurOrderListEdit"))
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
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            
          




            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();


        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {

                this.clientId.Value = ds.Tables[0].Rows[0]["wlId"].ToString();

                this.clientName.Text = ds.Tables[0].Rows[0]["wlName"].ToString();


                this.txtClientName.Value = ds.Tables[0].Rows[0]["wlName"].ToString();

                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.txtSendDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["sendDate"].ToString()).ToShortDateString();

                this.ddlYWYList.SelectedValue = ds.Tables[0].Rows[0]["bizId"].ToString();

                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                //this.lbNumber.Text = ds.Tables[0].Rows[0]["number"].ToString();

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag != "保存")
                {
                    this.btnSave.Visible = false;
                }

            }


        }


   
        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

           // int pId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            DataSet ds = item.GetAllModel(id);

            int rows=ds.Tables[0].Rows.Count;

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
                    
                    dis = ds.Tables[0].Rows[i]["dis"].ToString(),
                    sumPriceDis = ds.Tables[0].Rows[i]["sumPriceDis"].ToString(),

                    priceNow = ds.Tables[0].Rows[i]["priceNow"].ToString(),
                    sumPriceNow = ds.Tables[0].Rows[i]["sumPriceNow"].ToString(),

                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    priceTax = ds.Tables[0].Rows[i]["priceTax"].ToString(),
                    sumPriceTax = ds.Tables[0].Rows[i]["sumPriceTax"].ToString(),
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                    itemId = ds.Tables[0].Rows[i]["itemId"].ToString(),

                    sourceNumber = ds.Tables[0].Rows[i]["sourceNumber"].ToString()

                });
            }

            if (rows < 8)//少于8行
            {
                for (var i =0; i < 8-rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",

                        num = "",

                        price = "",

                        dis = "",
                        sumPriceDis = "",

                        priceNow = "",
                        sumPriceNow = "",

                        tax = "",
                        priceTax = "",
                        sumPriceTax = "",

                        sumPriceAll = "",


                        ckId = "",
                        ckName = "",
                        remarks = "",
                        itemId = 0,
                        sourceNumber = ""
                    });
                }
            }


            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
