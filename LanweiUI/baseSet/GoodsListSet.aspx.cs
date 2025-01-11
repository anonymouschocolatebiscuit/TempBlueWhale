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

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class GoodsListSet : BasePage
    {

        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {

                string typeId = Request.Params["typeId"].ToString();

                string code = Request.Params["goodsId"].ToString();

                GetDataList(typeId,code);

                Response.End();
            }


            if (Request.Params["Action"] == "SetShow")
            {

                string cloumn = Request.Params["showType"].ToString();

                int goodsId =ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());

                int selectYes =ConvertTo.ConvertInt(Request.Params["selectYes"].ToString());


                SetShow(goodsId,cloumn,selectYes);

                Response.End();
            }

         


       

        }



        void GetDataList(string typeId,string code)
        {
            DataSet ds = dal.GetAllModelView(typeId,code);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    barcode = ds.Tables[0].Rows[i]["barcode"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    priceSalesWhole = ds.Tables[0].Rows[i]["priceSalesWhole"].ToString(),
                    priceSalesRetail = ds.Tables[0].Rows[i]["priceSalesRetail"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    tj = ds.Tables[0].Rows[i]["tj"].ToString(),
                    xp = ds.Tables[0].Rows[i]["xp"].ToString(),
                    cx = ds.Tables[0].Rows[i]["cx"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()

                  

                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }


        void SetShow(int goodsId, string cloumn, int selectYes)
        {

            if (Session["userInfo"] != null)
            {

              

                LogsDAL logs = new LogsDAL();

               

                int num = dal.SetGoodsShow(goodsId,cloumn,selectYes);

                if (num > 0)
                {

                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "设置商品显示-ID：" + goodsId.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("设置成功");

                }
                else
                {
                    Response.Write("设置失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

    }
}
