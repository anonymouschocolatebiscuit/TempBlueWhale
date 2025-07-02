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
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

using System.Web.Services;
using System.Reflection;

namespace BlueWhale.UI.store
{
    public partial class OtherInListEdit : BasePage
    {
        public VenderDAL venderDAL = new VenderDAL();
        public StorageDAL storageDAL = new StorageDAL();

        public OtherInDAL dal = new OtherInDAL();

        public OtherInItemDAL item = new OtherInItemDAL();

        public string fromId = "0";

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {

                if (!CheckPower("OtherInListEdit"))
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
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlVenderList.DataSource = venderDAL.GetList(isWhere);
            this.ddlVenderList.DataTextField = "CodeName";
            this.ddlVenderList.DataValueField = "id";
            this.ddlVenderList.DataBind();

            this.ddlVenderList.Items.Insert(0, new ListItem("(Please select)", "0"));

            this.ddlVenderList.SelectedValue = "0";


        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            fromId = id.ToString();

            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlVenderList.SelectedValue = ds.Tables[0].Rows[0]["wlId"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();
              

                int typeId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["types"].ToString());
                if (typeId == 1)
                    this.rb1.Checked = true;
                if (typeId == -1)
                    this.rb2.Checked = true;

                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
               

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag != "Save")
                {
                    this.btnSave.Visible = false;
                }

            }


        }


   
        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

           // int pId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            DataSet ds = item.GetAllModel(id);

            int rows=ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),

                
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                });
            }

            if (rows < 8)//less than 8 rows
            {
                for (var i =0; i < 8-rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        unitName = "",
                        spec="",
                        num = "",
                        price = "",
                    
                        sumPrice = "",

                   
                        ckId = "",
                        ckName = "",

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
