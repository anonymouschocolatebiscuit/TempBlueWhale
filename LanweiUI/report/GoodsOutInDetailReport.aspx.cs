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

namespace Lanwei.Weixin.UI.report
{
    public partial class GoodsOutInDetailReport : BasePage
    {
       
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {

                if (!CheckPower("GoodsOutInDetailReport"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());
                string goodsId = Request.Params["goodsId"].ToString();
                string ckId = Request.Params["ckId"].ToString();
                this.GetDataList(bizStart, bizEnd, goodsId, ckId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,string goodsId,string typeId)
        {

            DataSet ds = dal.GetGoodsBalanceDetail(LoginUser.ShopId,bizStart, bizEnd, goodsId, typeId);


            decimal numBegin = 0;//获取期初余额行的
            decimal priceBegin = 0;//获取期初余额行的
            decimal sumPriceBegin = 0;//获取期初余额行的

            decimal numIn = 0;
            decimal priceIn = 0;
            decimal sumPriceIn = 0;

            decimal numOut = 0;
            decimal priceOut = 0;
            decimal sumPriceOut = 0;

            decimal numEnd = 0;
            decimal priceEnd = 0;
            decimal sumPriceEnd = 0;

            decimal numEndLast = 0;
            decimal priceEndLast = 0;
            decimal sumPriceEndLast = 0;


            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                if (ds.Tables[0].Rows[i]["bizType"].ToString() == "期初余额")
                {
                    numBegin = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["numBegin"].ToString());
                    priceBegin = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceBegin"].ToString());
                    sumPriceBegin = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceBegin"].ToString());

                    numEnd = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["numEnd"].ToString());
                    priceEnd = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceEnd"].ToString());
                    sumPriceEnd = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceEnd"].ToString());

                    numEndLast = numEnd;
                    sumPriceEndLast = sumPriceEnd;



                }
                else
                {
                    //如果不是期初余额，那么取上面行的期末为期初。

                    numBegin = numEndLast; 
                    priceBegin = priceEnd;
                    sumPriceBegin = sumPriceEndLast;

                    numIn = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["numIn"].ToString());
                    priceIn = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceIn"].ToString());
                    sumPriceIn = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["sumPriceIn"].ToString());

                    numOut = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["numOut"].ToString());
                    priceOut = priceBegin;
                    sumPriceOut = numOut * priceOut;

                    //先计算
                    numEnd = numBegin + numIn - numOut;
                    sumPriceEnd = sumPriceBegin + sumPriceIn - sumPriceOut;

                    //然后赋值给中间变量

                    if (numEnd != 0)
                    {
                       
                        priceEndLast = sumPriceEnd / numEnd;
                    }



                    numEndLast = numEnd;
                    sumPriceEndLast = sumPriceEnd;




                }

                string bizType = ds.Tables[0].Rows[i]["bizType"].ToString();

                list.Add(new
                {
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    numBegin = numBegin.ToString("0.00"),
                    priceBegin = priceBegin.ToString("0.00"),
                    sumPriceBegin = sumPriceBegin.ToString("0.00"),

                    numIn =numIn.ToString("0.00"),
                    priceIn =priceIn.ToString("0.00"),
                    sumPriceIn =sumPriceIn.ToString("0.00"),

                    numOut =numOut.ToString("0.00"),
                    priceOut = priceOut.ToString("0.00"),
                    sumPriceOut =sumPriceOut.ToString("0.00"),

                    numEnd =numEnd.ToString("0.00"),
                    priceEnd = priceEnd.ToString("0.00"),
                    sumPriceEnd =sumPriceEnd.ToString("0.00") 
                 
                  
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
