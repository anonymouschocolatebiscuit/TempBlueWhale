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
    public partial class GoodsList : BasePage
    {

        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (!CheckPower("GoodsList"))
                {
                    Response.Redirect("../OverPower.htm");
                }

             
            }

            if (Request.Params["Action"] == "GetDataList")
            {
                //if (Request.Params.Count > 2)
                //{
                //LogsDAL logs = new LogsDAL();

                //logs.ShopId = LoginUser.ShopId;
                //logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                //logs.Events = "url:" + Request.Url.PathAndQuery.ToString();
                
                //logs.Ip = Request.UserHostAddress.ToString();
                //logs.Add();

                //}

                int typeId = 0;

                string keys = "";
                int isShow = -1;

                GetDataListSearch(typeId, keys,isShow);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                int typeId = ConvertTo.ConvertInt(Request.Params["typeId"].ToString());
                int isShow = ConvertTo.ConvertInt(Request.Params["isShow"].ToString());
                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(typeId, keys,isShow);
                Response.End();
            }


            if (Request.Params["Action"] == "GetDDLList")
            {
                GetDDLList();
                Response.End();
            }

          
            if (Request.Params["Action"] == "delete")
            {
                string idString = Request.Params["idString"].ToString();
                DeleteRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "checkRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckRow(idString);
                Response.End();
            }

            if (Request.Params["Action"] == "checkNoRow")
            {
                string idString = Request.Params["idString"].ToString();
                CheckNoRow(idString);
                Response.End();
            }


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
            var griddata = new { Rows = list,Total=ds.Tables[0].Rows.Count };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }


        void GetDataListSearch(int typeId, string key,int isShow)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            if (typeId != 0)
            {
                isWhere += " and typeId ='" + typeId + "' or typeId in (select id from goodsType where parentId = " + typeId + " )";
            }

            if (isShow != -1)
            {
                isWhere += " and isShow ='" + isShow + "' ";
            }


            if (key != "")
            {
                isWhere += " and (names like'%" + key + "%' or  code like'%" + key + "%' or  spec like'%" + key + "%') ";
            }

            

            DataSet ds = dal.GetList(isWhere);

          //  DataSet ds = dal.GetAllModelViewBaseSet(typeId, keys,isShow);

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


                string isShowFlag = ds.Tables[0].Rows[i]["isShow"].ToString();

                if (isShowFlag == "0")
                {
                    isShowFlag = "<font color='red'>下架</font>";
                }
                else
                {
                    isShowFlag = "<font color='green'>在售</font>";
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

                    isShow = isShowFlag,

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


        void GetDDLList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    priceSales = ds.Tables[0].Rows[i]["priceSales"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()

                 

                });

            }
            // var griddata = new { Rows = list };

            //  string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

            string s = new JavaScriptSerializer().Serialize(list);//传给dropdownList


            Response.Write(s);
        }

        
        void DeleteRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.Delete(id);

                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除商品-ID：" + id.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("删除成功！");

                }
                else
                {
                    Response.Write("删除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

        void DeleteRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                if (!CheckPower("GoodsListDelete"))
                {
                    Response.Write("无此操作权限，请联系管理员！");
                    return;
                }

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.Delete(delId);
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "删除商品-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功删除" + num + "条记录！");

                }
                else
                {
                    Response.Write("删除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }

        void CheckRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                //if (!CheckPower("SalesOrderListCheck"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateGoodsIsShow(delId,1);
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "上架商品-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("上架成功" + num + "条记录！");

                }
                else
                {
                    Response.Write("上架失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }


        void CheckNoRow(string id)
        {
            if (Session["userInfo"] != null)
            {

                //if (!CheckPower("SalesOrderListCheckNo"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}

                LogsDAL logs = new LogsDAL();

                string[] idString = id.Split(',');

                int num = 0;

                if (idString.Length > 0)
                {
                    for (int i = 0; i < idString.Length; i++)
                    {
                        int delId = ConvertTo.ConvertInt(idString[i].ToString());

                        int del = dal.UpdateGoodsIsShow(delId, 0);
                        if (del > 0)
                        {
                            num += 1;


                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "下架商品-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("下架成功" + num + "条记录！");

                }
                else
                {
                    Response.Write("下架失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }


        }


    }
}
