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
    public partial class VenderNeedPayReport : BasePage
    {
        public AccountDAL dal = new AccountDAL();
       
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                if (!CheckPower("VenderNeedPayReport"))
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


          


             
                string typeId = Request.Params["typeId"].ToString();


                this.GetDataList(bizStart,bizEnd,typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,string typeId)
        {



            DataSet ds = dal.GetAllModelReportVenderNeedPay(LoginUser.ShopId,bizStart, bizEnd, typeId);

            decimal payEnd = 0;
            decimal payEndNow = 0;
            decimal payEndLastRow = 0;


            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["bizType"].ToString() == "期初余额")
                {
                    payEnd = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payEnd"].ToString());
                    payEndLastRow = payEnd;

                }
                else
                {
                    payEndNow = (payEndLastRow + ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payNeed"].ToString()) - ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["payReady"].ToString()));
                    payEndLastRow = payEndNow;


                }


                list.Add(new
                {
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    bizType = ds.Tables[0].Rows[i]["bizType"].ToString(),
                    
               
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),
                    payEnd = (ds.Tables[0].Rows[i]["bizType"].ToString() == "期初余额") ? payEnd.ToString() : payEndNow.ToString()
                    

                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
