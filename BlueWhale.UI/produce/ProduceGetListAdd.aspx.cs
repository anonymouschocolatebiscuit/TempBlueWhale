using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.produce
{
    public partial class produceGetListAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Bind();

                this.txtBizDate.Text = DateTime.Now.ToShortDateString();

                this.txtNum.Attributes.Add("onkeyup", "return getBomList()");
            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataBom")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["pId"].ToString());
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                int num = ConvertTo.ConvertInt(Request.Params["num"].ToString());
                this.GetDataListBom(pId,goodsId, num);
                Response.End();
            }
        }

        public void Bind()
        {
            ListItem items = new ListItem("(请选择)", "0");

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            UserDAL userDAL = new UserDAL();

            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();
            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();

            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    processId = "",
                    processName = "",
                    num = "",
                    price = "",
                    sumPrice = "",
                    remarks = ""
                });
            }

            var griddata = new { Rows = list, Total = list.Count.ToString() };

            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void GetDataListBom(int pId,int goodsId,int produceNumNo)
        {
            IList<object> list = new List<object>();

            //从生产计划获取清单
            ProduceListItemBomDAL dal = new ProduceListItemBomDAL();

            DataSet ds = dal.GetList(" pId='" + pId + "' ");

            int rows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int numBom = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["numBom"].ToString());

                int rate = ConvertTo.ConvertInt(ds.Tables[0].Rows[i]["rate"].ToString());

                float price = ConvertTo.ConvertFloat(ds.Tables[0].Rows[i]["priceCost"].ToString());

                //bom数量/损耗率*生产数量=计划用量
                if(rate==0)
                {
                    rate=100;
                }

                float numApply = numBom * produceNumNo * rate / 100;

                list.Add(new
                {
                    id = i,
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    pihao="",
                    numApply = numApply,
                    price = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    rate = ds.Tables[0].Rows[i]["rate"].ToString(),
                    num = numApply,
                    sumPrice=price*numApply,
                    remarks = ""
                });
            }

            if (rows < 8)//少于8行
            {
                for (var i = 0; i < 8 - rows; i++)
                {
                    list.Add(new
                    {
                        id = i,
                        code = "",
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",
                        ckId="",
                        ckName="",
                        pihao = "",
                        numApply = "",
                        price = "",
                        rate = "",
                        num = "",
                        sumPrice = "",
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