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

namespace Lanwei.Weixin.UI.produce
{
    public partial class produceInListAdd : BasePage
    {



        public static int produceId = 0;
        public static string sourceNumber = "";


        public ProduceListDAL item = new ProduceListDAL();

        public UserDAL userDAL = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {

                //if (!CheckPower("PurReceiptListAdd"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}


                this.txtBizDate.Text = DateTime.Now.ToShortDateString();



                produceId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

               // Response.Write("orderId:"+orderId.ToString()+Request.Url.ToString());

                this.Bind();

            }

            if (Request.Params["Action"] == "GetData")
            {
                string id = Request.Params["id"].ToString();
                this.GetDataList(id);
                Response.End();
            }
        }

      

        public void Bind()
        {
            ListItem items = new ListItem("(空)", "0");

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";



            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();

            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();

           // this.ddlYWYList.Enabled = false;


            



        }

   
        void GetDataList(string itemIdString)
        {
            IList<object> list = new List<object>();


            if (itemIdString != "0")
            {
                #region 如果是来自生产计划


                itemIdString = itemIdString.TrimEnd(',');

                string isWhere = " shopId='"+LoginUser.ShopId+"' and id in("+itemIdString+") ";

                DataSet ds = item.GetList(isWhere);

                int rows = ds.Tables[0].Rows.Count;

                for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    decimal num = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["num"].ToString());

                   

                    list.Add(new
                    {
                        id = ds.Tables[0].Rows[i]["id"].ToString(),
                        goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                        goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                        spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                        unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                        num = ds.Tables[0].Rows[i]["num"].ToString(),
                        price = "0",
                        dis = "0",
                        sumPriceDis = "0",
                        priceNow = "0",
                        sumPriceNow = "0",

                        tax = "0",
                        priceTax = "0",
                        sumPriceTax = "0",

                        sumPriceAll = "0",

                        ckId = "",
                        ckName = "",

                        itemId = ds.Tables[0].Rows[i]["id"].ToString(),
                        remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),
                        sourceNumber = ds.Tables[0].Rows[i]["number"].ToString()

                    });
                }

                if (rows < 8)//少于8行
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

                            itemId = 0,
                            remarks = "",
                            sourceNumber = ""

                        });
                    }
                }

                #endregion


            }
            else
            {

                #region 如果是新增的

                for (var i = 1; i < 9; i++)
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

                #endregion
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
