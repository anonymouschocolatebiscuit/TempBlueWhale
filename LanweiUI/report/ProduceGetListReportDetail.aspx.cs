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
    public partial class ProduceGetListReportDetail : BasePage
    {
        public DAL.produceGetListItem dal = new produceGetListItem();

        public ClientDAL venderDAL = new ClientDAL();
        public GoodsDAL goodsDAL = new GoodsDAL();
        public CangkuDAL ckDAL = new CangkuDAL();

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

                string code = Request.Params["code"].ToString();

                string ckName = Request.Params["ckName"].ToString();


                this.GetDataList(bizStart, bizEnd, code, ckName);
                Response.End();
            }
        }

        void GetDataList(DateTime bizStart, DateTime bizEnd,string code, string ckName)
        {



            DataSet ds = dal.GetProduceGetListItemDetail(LoginUser.ShopId, bizStart, bizEnd, ckName, code);




            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),                  

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),


                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString()
                  


                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
