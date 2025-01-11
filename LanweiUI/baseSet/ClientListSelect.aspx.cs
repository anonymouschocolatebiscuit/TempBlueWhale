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
    public partial class ClientListSelect :BasePage //System.Web.UI.Page
    {
        public ClientDAL dal = new ClientDAL();

      


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

                int typeId =ConvertTo.ConvertInt(Request.Params["typeId"].ToString());

                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(typeId, keys);
                Response.End();
            }

        }


        public void Bind()
        {
            ClientTypeDAL typesDal = new ClientTypeDAL();

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlTypeList.DataSource = typesDal.GetList(isWhere);
            this.ddlTypeList.DataTextField = "names";
            this.ddlTypeList.DataValueField = "id";
            this.ddlTypeList.DataBind();

            ListItem item = new ListItem("选择客户类别","0");

            this.ddlTypeList.Items.Insert(0,item);

            this.ddlTypeList.SelectedValue = "0";



        }

        void GetDataList()
        {
            //DataSet ds = dal.GetAllModel();

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

        


            Response.Write(s);
        }
                

        void GetDataListSearch(int typeId,string key)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            if (typeId != 0)
            {
                isWhere += " and typeId='" + typeId + "'";
            }

            isWhere += " and (names like'%" + key + "%'" +
                   " or  code like'%" + key + "%' " +
                   " or  tel like'%" + key + "%' " +
                   " or  remarks like'%" + key + "%' " +
                   " or  address like'%" + key + "%') ";

            DataSet ds = dal.GetList(isWhere);

           // DataSet ds = dal.GetList(isWhere);

            //DataSet ds = dal.GetAllModelView(typeId, key);

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
    }
}
