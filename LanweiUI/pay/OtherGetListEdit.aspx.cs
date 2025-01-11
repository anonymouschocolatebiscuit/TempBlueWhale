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

namespace Lanwei.Weixin.UI.pay
{
    public partial class OtherGetListEdit : BasePage
    {
        public ClientDAL venderDAL = new ClientDAL();
        public CangkuDAL cangkuDAL = new CangkuDAL();

        public AccountDAL accountDAL = new AccountDAL();

        public OtherGetDAL dal = new OtherGetDAL();
        public OtherGetItemDAL item = new OtherGetItemDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("OtherGetListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
              

                this.Bind();

                this.BindInfo();

            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());

                this.GetDataList(id);
                Response.End();
            }
        }

      

        public void Bind()
        {
            ListItem items = new ListItem("(空)", "0");
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

            this.ddlVenderList.Items.Insert(0, items);

            this.ddlVenderList.SelectedValue = "0";




            this.ddlBankList.DataSource = accountDAL.GetList(isWhere);
            this.ddlBankList.DataTextField = "CodeName";
            this.ddlBankList.DataValueField = "id";
            this.ddlBankList.DataBind();


        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());


            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
                this.ddlBankList.SelectedValue = ds.Tables[0].Rows[0]["bkId"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

           
                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag == "审核")
                {
                    this.btnSave.Visible = false;
                }

            }


        }



        void GetDataList(int pId)
        {
            DataSet ds = item.GetAllModel(pId);

            int rows = ds.Tables[0].Rows.Count;

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = i,
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            if (rows < 8)//少于8行
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        typeId = "",
                        typeName = "",
                        price = "",
                        remarks = ""

                    });
                }
            }

            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
