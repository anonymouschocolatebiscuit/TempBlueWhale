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

namespace Lanwei.Weixin.UI.store
{
    public partial class CostChangeListEdit : BasePage
    {
        public CostChangeDAL dal = new CostChangeDAL();

        public CostChangeItemDAL item = new CostChangeItemDAL();


        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (!this.IsPostBack)
            {
                if (!CheckPower("CostChangeListEdit"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();


                this.BindInfo();

            }

            if (Request.Params["Action"] == "GetData")
            {
                int id = ConvertTo.ConvertInt(Request.Params["id"].ToString());
                this.GetDataList(id);

                Response.End();
            }
        }


        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

          
            DataSet ds = dal.GetAllModel(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
            
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["bizDate"].ToString()).ToShortDateString();


                this.ddlBizType.Text = ds.Tables[0].Rows[0]["bizType"].ToString();

                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
               

                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

            }


        }


        void GetDataList(int id)
        {
            IList<object> list = new List<object>();

            // int pId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            DataSet ds = item.GetAllModel(id);

            int rows = ds.Tables[0].Rows.Count;

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                  
                    price = ds.Tables[0].Rows[i]["price"].ToString(),

                 

                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

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
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",

                        price = "",


                        ckId = "",

                        remarks = ""
                    });
                }
            }


            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
