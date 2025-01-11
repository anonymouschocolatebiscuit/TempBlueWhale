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
    public partial class ProduceListReport : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();

        public ProduceListDAL dal = new ProduceListDAL();


      
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


                string keys = Request.Params["keys"].ToString();

                string wlId = Request.Params["wlId"].ToString();
                string goodsId = Request.Params["goodsId"].ToString();

                string typeId = Request.Params["typeId"].ToString();


                this.GetDataList(keys,bizStart,bizEnd,wlId,goodsId,typeId);
                Response.End();
            }
        }

        void GetDataList(string keys,DateTime bizStart,DateTime bizEnd,string  wlId,string goodsId,string typeId)
        {



            DataSet ds = dal.GetProduceListReport(keys,LoginUser.ShopId, bizStart, bizEnd,wlId, goodsId, typeId);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sendFlag = "";
                decimal Num = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["Num"].ToString());
                decimal getNum = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["finishNum"].ToString());
                decimal getNumNo = ConvertTo.ConvertDec(ds.Tables[0].Rows[i]["finishNumNo"].ToString());

                sendFlag = (getNum / Num * 100).ToString("0")+"%";

                //if (getNumNo <= 0)
                //{
                //    sendFlag = "已完成";
                //}

                //if (getNumNo != 0 && Num > getNum)
                //{
                //    sendFlag = "进行中";
                //}
                //if (getNumNo == Num)
                //{
                //    sendFlag = "未生产";
                //}

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    number = ds.Tables[0].Rows[i]["number"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    orderNumber = ds.Tables[0].Rows[i]["orderNumber"].ToString(),

                    dateStart = ds.Tables[0].Rows[i]["dateStart"].ToString(),
                    dateEnd = ds.Tables[0].Rows[i]["dateEnd"].ToString(),
                    
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    Num = ds.Tables[0].Rows[i]["Num"].ToString(),
                    finishNum = ds.Tables[0].Rows[i]["finishNum"].ToString(),

                    finishNumNo = ds.Tables[0].Rows[i]["finishNumNo"].ToString(),
                    sendFlag = sendFlag,
                  
                    sendDate = ds.Tables[0].Rows[i]["sendDate"].ToString()

                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
