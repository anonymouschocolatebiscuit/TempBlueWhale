using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.BaseSet;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.baseSet
{
    public partial class ClientListSelect : BasePage
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
                int typeId = ConvertTo.ConvertInt(Request.Params["typeId"].ToString());

                string keys = Request.Params["keys"].ToString();

                if (!System.Text.RegularExpressions.Regex.IsMatch(keys, @"^[a-zA-Z0-9\s]*$"))
                {
                    keys = "";
                }

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

            ListItem item = new ListItem("Select Client Type", "0");

            this.ddlTypeList.Items.Insert(0, item);

            this.ddlTypeList.SelectedValue = "0";
        }

        void GetDataList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
                    flag = ds.Tables[0].Rows[i]["flag"].ToString()
                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);

            Response.Write(s);
        }

        void GetDataListSearch(int typeId, string key)
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            if (typeId != 0)
            {
                isWhere += " and typeId='" + typeId + "'";
            }

            isWhere += " and (names like'%" + key + "%'" +
                   " or  tel like'%" + key + "%' " +
                   " or  phone like'%" + key + "%' " +
                   " or  linkMan like'%" + key + "%') ";

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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