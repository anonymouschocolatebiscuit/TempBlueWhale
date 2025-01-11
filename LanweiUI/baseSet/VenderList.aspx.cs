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
    public partial class VenderList : BasePage
    {

        public VenderDAL dal = new VenderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(keys);
                Response.End();
            }


            if (Request.Params["Action"] == "GetDDLList")
            {
                GetDDLList();
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
          
            if (Request.Params["Action"] == "delete")
            {
                string idString = Request.Params["idString"].ToString();
                DeleteRow(idString);
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

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),
                   
                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),


                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),

                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                 
                 

                });
            
            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要

          //string s = new JavaScriptSerializer().Serialize(list);


            Response.Write(s);
        }


        void GetDataListSearch(string key)
        {
            //DataSet ds = dal.GetAllModelView(keys);

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            isWhere += " and (names like'%" + key + "%' or  code like'%" + key + "%' or  linkMan like'%" + key + "%') ";
            DataSet ds = dal.GetList(isWhere);


            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),

                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),

                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),

                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()

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

                    shopId = ds.Tables[0].Rows[i]["shopId"].ToString(),
                    shopName = ds.Tables[0].Rows[i]["shopName"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    yueDate = ds.Tables[0].Rows[i]["yueDate"].ToString(),
                    payNeed = ds.Tables[0].Rows[i]["payNeed"].ToString(),
                    payReady = ds.Tables[0].Rows[i]["payReady"].ToString(),

                    tax = ds.Tables[0].Rows[i]["tax"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    taxNumber = ds.Tables[0].Rows[i]["taxNumber"].ToString(),
                    bankName = ds.Tables[0].Rows[i]["bankName"].ToString(),
                    bankNumber = ds.Tables[0].Rows[i]["bankNumber"].ToString(),
                    dizhi = ds.Tables[0].Rows[i]["dizhi"].ToString(),

                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),
                    checkId = ds.Tables[0].Rows[i]["checkId"].ToString(),
                    checkDate = ds.Tables[0].Rows[i]["checkDate"].ToString(),

                    balance = ds.Tables[0].Rows[i]["balance"].ToString(),

                    linkMan = ds.Tables[0].Rows[i]["linkMan"].ToString(),

                    phone = ds.Tables[0].Rows[i]["phone"].ToString(),
                    tel = ds.Tables[0].Rows[i]["tel"].ToString(),
                    qq = ds.Tables[0].Rows[i]["qq"].ToString(),
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                 

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
                    logs.Events = "删除供应商-ID：" + id.ToString();
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
                            logs.Events = "删除供应商-ID：" + id.ToString();
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
                    Response.Write("成功失败！");
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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "审核");
                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "审核供应商-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("审核失败！");
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

                        int del = dal.UpdateCheck(delId, LoginUser.Id, LoginUser.Names, DateTime.Now, "保存");
                        if (del > 0)
                        {
                            num += 1;

                            logs.ShopId = LoginUser.ShopId;
                            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                            logs.Events = "反审核供应商-ID：" + delId.ToString();
                            logs.Ip = Request.UserHostAddress.ToString();
                            logs.Add();


                        }
                    }
                }


                if (num > 0)
                {


                    Response.Write("成功反审核" + num + "条记录！");

                }
                else
                {
                    Response.Write("反审核失败！");
                }




            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }


        }

    }
}
