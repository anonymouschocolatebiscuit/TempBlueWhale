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
using BlueWhale.DAL.produce;
using BlueWhale.Common;
using BlueWhale.UI.src;

using System.Web.Services;
using System.Reflection;

namespace BlueWhale.UI.produce
{
    public partial class goodsBomListEdit : BasePage
    {
        public DAL.produce.goodsBomListType dalType = new DAL.produce.goodsBomListType();

        public DAL.produce.goodsBomList dal = new DAL.produce.goodsBomList();

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                //if (!CheckPower("goodsBomListEdit"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

               


                this.Bind();

            }

          


            if (Request.Params["Action"] == "GetDataSub")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataListSub(pId);
                Response.End();
            }

        }

      

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            this.ddlTypeList.DataSource = dalType.GetList(isWhere);
            this.ddlTypeList.DataTextField = "Names";
            this.ddlTypeList.DataValueField = "id";
            this.ddlTypeList.DataBind();

            string sql = " id=" + Request.QueryString["id"].ToString();
            DataSet ds = dal.GetList(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ddlTypeList.SelectedValue = ds.Tables[0].Rows[0]["typeId"].ToString();
                this.hfGoodsName.Value = ds.Tables[0].Rows[0]["goodsName"].ToString();
                this.hfGoodsId.Value = ds.Tables[0].Rows[0]["goodsId"].ToString();



                this.txtTuhao.Text = ds.Tables[0].Rows[0]["tuhao"].ToString();
                this.txtEdition.Text = ds.Tables[0].Rows[0]["edition"].ToString();
                this.txtSpec.Text = ds.Tables[0].Rows[0]["spec"].ToString();
                this.txtUnitName.Text = ds.Tables[0].Rows[0]["unitName"].ToString();

                this.txtNum.Text = ds.Tables[0].Rows[0]["num"].ToString();
                this.txtRate.Text = ds.Tables[0].Rows[0]["rate"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();



            }


        }




        void GetDataListSub(int pId)
        {
            IList<object> list = new List<object>();

            #region 


            DAL.produce.goodsBomListItem item = new goodsBomListItem();

            string sql = " pId='"+pId+"' ";
            DataSet ds = item.GetList(sql);

            int rows = ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {



                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    rate = ds.Tables[0].Rows[i]["rate"].ToString(),
                   


                    itemId = ds.Tables[0].Rows[i]["itemId"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()
                   

                });
            }

            if (rows < 8)
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        goodsId = "",
                        goodsName = "",
                        unitName = "",
                        spec = "",
                        num = "",
                        rate = "",
                        remarks = ""
                    });
                }
            }

            #endregion

          

            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
