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
using LanweiDAL;
using LanweiCommon;
using LanweiUI.src;

namespace LanweiUI.buy
{
    public partial class PurOrderListAddBack : System.Web.UI.Page
    {
        public VenderDAL venderDAL = new VenderDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtSendDate.Text = DateTime.Now.ToShortDateString();

                this.Bind();

            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }
        }

        public void Bind()
        {

            //this.ddlVenderList.DataSource = venderDAL.GetAllModel();
            //this.ddlVenderList.DataTextField = "CodeName";
            //this.ddlVenderList.DataValueField = "id";
            //this.ddlVenderList.DataBind();


        }

       public string  GetList()
        {
            string sql = "select * from cangku ";
            DataSet ds = venderDAL.GetAllModel();

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    ckId = ds.Tables[0].Rows[i]["id"].ToString(),
                    // text = ds.Tables[0].Rows[i]["code"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["names"].ToString()

                });
            }
           // var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(list);
            return s;
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = i,
                    goodsName = "名称" + i,
                    unitName = "单位" + i,
                    num = i * 2,
                    price = i * 10,
                    dis = 0,
                    disPrice = 0,
                    sumPrice = i * i * 20,

                    tax = 17,
                    taxPrice = i * i * 20 * 0.17,
                    sumPriceAll = i * i * 20 * 1.17,
                    ckId = "仓库Id" + i,
                    ckNames = "仓库Name" + i,

                    remark = "部门" + i + " 备注"
                });
            }
            var griddata = new { Rows = list,Total=list.Count.ToString()};
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }


       

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
