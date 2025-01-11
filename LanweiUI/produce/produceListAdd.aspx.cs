using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

using System.Web.Services;
using System.Reflection;
using System.Data;

namespace Lanwei.Weixin.UI.produce
{
    public partial class produceListAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                //if (!CheckPower("SalesOrderListAdd"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtDateStart.Text = DateTime.Now.ToShortDateString();
                this.txtDateEnd.Text = DateTime.Now.AddDays(1).ToShortDateString();

                this.txtNum.Attributes.Add("onkeyup", "return getBomList()");


            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataBom")
            {
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                int num = ConvertTo.ConvertInt(Request.Params["num"].ToString());
                this.GetDataListBom(goodsId, num);
                Response.End();
            }

            if (Request.Params["Action"] == "GetData")
            {
              

                this.GetDataListBom();
                Response.End();
            }

        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    processId = "",
                    processName = "",
                 
                    num = "",

                    price = "",

                  

                    sumPrice = "",


             
                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListBom(int goodsId,int numGoods)
        {
            IList<object> list = new List<object>();

            DAL.goodsBomListItem dal = new goodsBomListItem();

            DataSet ds = dal.GetList(" pId=(select top 1 id from goodsBomList where goodsId='" + goodsId + "' ) ");

            int rows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int numBom = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["num"].ToString());
                int rate = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["rate"].ToString());

                //bom数量/损耗率*生产数量=计划用量
                if(rate==0)
                {
                    rate=100;
                }

                float num = numBom * numGoods * rate / 100;


                list.Add(new
                {
                    id = i,
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    numBom = ds.Tables[0].Rows[i]["num"].ToString(),
                    rate = ds.Tables[0].Rows[i]["rate"].ToString(),
                    num = num.ToString(),
                    //ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    //ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    remarks = ""
                });
            }


            if (rows < 8)//少于8行
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        code = "",
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",
                        numBom = "",
                        rate = "",
                        num = "",
                        //ckId = "0",
                        //ckName = "",
                        remarks = ""
                    });
                }
            }

            
             
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListBom()
        {
            IList<object> list = new List<object>();

            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    code = "",
                    goodsId = "",
                    goodsName = "",
                    spec = "",
                    unitName = "",
                    numBom = "",
                    rate = "",
                    num = "",
                    ckId = "",
                    ckName = "",
                    remarks = ""
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);




            

        }

    }
}