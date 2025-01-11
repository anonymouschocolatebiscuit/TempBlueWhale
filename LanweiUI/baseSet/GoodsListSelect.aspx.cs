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
    public partial class GoodsListSelect : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.Bind();
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                int typeId = ConvertTo.ConvertInt(Request.Params["typeId"].ToString());

                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(typeId, keys);
                Response.End();
            }


            if (Request.Params["Action"] == "SearchKeys")
            {
                string keysWords = Request.Params["keysWords"].ToString();

                GetDataListKeysWord(keysWords);

                Response.End();
            }




        }


        public void Bind()
        {
           


        }

        void GetDataList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string isWeight = ds.Tables[0].Rows[i]["isWeight"].ToString();

                if (isWeight == "0")
                {
                    isWeight = "否";
                }
                else
                {
                    isWeight = "是";
                }

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    barcode = ds.Tables[0].Rows[i]["barcode"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),

                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    brandId = ds.Tables[0].Rows[i]["brandId"].ToString(),
                    brandName = ds.Tables[0].Rows[i]["brandName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),


                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    place = ds.Tables[0].Rows[i]["place"].ToString(),

                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    priceSalesWhole = ds.Tables[0].Rows[i]["priceSalesWhole"].ToString(),
                    priceSalesRetail = ds.Tables[0].Rows[i]["priceSalesRetail"].ToString(),

                    numMin = ds.Tables[0].Rows[i]["numMin"].ToString(),
                    numMax = ds.Tables[0].Rows[i]["numMax"].ToString(),

                    bzDays = ds.Tables[0].Rows[i]["bzDays"].ToString(),

                    isWeight = isWeight,

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要




            Response.Write(s);
        }

        void GetDataListKeysWord(string key)
        {
            if (Session["userInfo"] == null)
            {
                Response.Write("登录超时！");
                Response.End();
                return;
            }

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";         

            if (key != "")
            {
                isWhere += " and ( code ='" + key + "' ) ";
            }

            DataSet ds = dal.GetList(isWhere);

          

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string isWeight = ds.Tables[0].Rows[i]["isWeight"].ToString();

                if (isWeight == "0")
                {
                    isWeight = "否";
                }
                else
                {
                    isWeight = "是";
                }

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    barcode = ds.Tables[0].Rows[i]["barcode"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),

                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    brandId = ds.Tables[0].Rows[i]["brandId"].ToString(),
                    brandName = ds.Tables[0].Rows[i]["brandName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),


                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    place = ds.Tables[0].Rows[i]["place"].ToString(),

                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    priceSalesWhole = ds.Tables[0].Rows[i]["priceSalesWhole"].ToString(),
                    priceSalesRetail = ds.Tables[0].Rows[i]["priceSalesRetail"].ToString(),

                    numMin = ds.Tables[0].Rows[i]["numMin"].ToString(),
                    numMax = ds.Tables[0].Rows[i]["numMax"].ToString(),

                    bzDays = ds.Tables[0].Rows[i]["bzDays"].ToString(),

                    isWeight = isWeight,

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()



                });

            }

           // var griddata = new { Rows = list };


            string s = "无此商品！";

            if (ds.Tables[0].Rows.Count > 0)
            {
                s = new JavaScriptSerializer().Serialize(list);
            }
            
            Response.Write(s);



        }

        void GetDataListSearch(int typeId, string key)
        {
            
            
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            if (typeId != 0)
            {
                isWhere += " and (typeId ='" + typeId + "' or typeId in (select id from goodsType where parentId = " + typeId + " )  )";
            }

            if (key != "")
            {
                isWhere += " and (names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%') ";
            }

            DataSet ds = dal.GetList(isWhere);

            //DataSet ds = dal.GetAllModelView(typeId, key);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                string isWeight = ds.Tables[0].Rows[i]["isWeight"].ToString();

                if (isWeight == "0")
                {
                    isWeight = "否";
                }
                else
                {
                    isWeight = "是";
                }

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),

                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    barcode = ds.Tables[0].Rows[i]["barcode"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),

                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    brandId = ds.Tables[0].Rows[i]["brandId"].ToString(),
                    brandName = ds.Tables[0].Rows[i]["brandName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),


                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    place = ds.Tables[0].Rows[i]["place"].ToString(),

                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    priceSalesWhole = ds.Tables[0].Rows[i]["priceSalesWhole"].ToString(),
                    priceSalesRetail = ds.Tables[0].Rows[i]["priceSalesRetail"].ToString(),

                    numMin = ds.Tables[0].Rows[i]["numMin"].ToString(),
                    numMax = ds.Tables[0].Rows[i]["numMax"].ToString(),

                    bzDays = ds.Tables[0].Rows[i]["bzDays"].ToString(),

                    isWeight = isWeight,

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()



                });

            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
