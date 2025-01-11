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
    public partial class AccountFlowReport : BasePage
    {
        public AccountDAL dal = new AccountDAL();
       
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


          


             
                string typeId = Request.Params["typeId"].ToString();


                this.GetDataList(bizStart,bizEnd,typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,string typeId)
        {




            DataSet ds = dal.GetAllModelReport(LoginUser.ShopId,bizStart, bizEnd, typeId);




            IList<object> list = new List<object>();



            decimal priceEndRowNow = 0;//当前行余额

            decimal priceEndRowLast = 0;//上一行期末



            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bizType = ds.Tables[0].Rows[i]["bizType"].ToString();
                string priceBegin = "";
                string priceEnd = "";

                if (bizType == "期初余额") // 获取下一行的期初
                {


                    priceEndRowNow = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceEnd"].ToString());

                    priceEndRowLast = priceEndRowNow;


                    priceBegin = ds.Tables[0].Rows[i]["priceBegin"].ToString();
                    priceEnd = ds.Tables[0].Rows[i]["priceEnd"].ToString();


                }
                else //获取上一行的期末余额、并计算本行的的期末
                {

                    priceEndRowNow = (priceEndRowLast + ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceIn"].ToString()) - ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["priceOut"].ToString()));



                    priceBegin = priceEndRowLast.ToString("0.00");//本行期初=上行期末

                    priceEnd = priceEndRowNow.ToString("0.00");

                    priceEndRowLast = priceEndRowNow;



                }

                list.Add(new
                {
                    bkId = ds.Tables[0].Rows[i]["bkId"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    bkName = ds.Tables[0].Rows[i]["bkName"].ToString(),
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    priceBegin = priceBegin,
                    priceIn = ds.Tables[0].Rows[i]["priceIn"].ToString(),
                    priceOut = ds.Tables[0].Rows[i]["priceOut"].ToString(),
                    priceEnd = priceEnd


                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
