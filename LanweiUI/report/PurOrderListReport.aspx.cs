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
    public partial class PurOrderListReport :BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public PurOrderDAL dal = new PurOrderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {

                this.txtDateStart.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();


              

            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());


                DateTime sendStart = DateTime.Parse("2000-1-1");
                DateTime sendEnd = DateTime.Parse("2050-1-1");


                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();

                string typeId = Request.Params["typeId"].ToString();


                this.GetDataList(bizStart,bizEnd,sendStart,sendEnd,wlId,goodsId,typeId);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart,DateTime bizEnd,DateTime sendStart,DateTime sendEnd,string  wlId,string goodsId,string typeId)
        {

           

            DataSet ds = dal.GetPurOrderListReport(LoginUser.ShopId,bizStart, bizEnd, sendStart, sendEnd, wlId, goodsId, typeId);
          



            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                string sendFlag = "";
                decimal Num = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["Num"].ToString());
                decimal getNum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["getNum"].ToString());
                decimal getNumNo = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["getNumNo"].ToString());
                if (getNumNo <= 0)
                {
                    sendFlag = "全部入库";
                }

                if (getNumNo != 0 && Num > getNum)
                {
                    sendFlag = "部分入库";
                }
                if (getNumNo == Num)
                {
                    sendFlag = "未入库";
                }

                list.Add(new
                {
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),

                    Num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    getNum = ds.Tables[0].Rows[i]["getNum"].ToString(),

                    getNumNo = ds.Tables[0].Rows[i]["getNumNo"].ToString(),
                    sendFlag=sendFlag,
                    sumPriceAll = ds.Tables[0].Rows[i]["sumPriceAll"].ToString(),
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString()
                  
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
