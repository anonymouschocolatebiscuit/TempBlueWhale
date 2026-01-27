using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System.Data;

namespace BlueWhale.UI.produce
{
    public partial class produceGetListEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Bind();
                this.BindInfo();
                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
                this.txtNum.Attributes.Add("onkeyup", "return getBomList()");
            }

            if (Request.Params["Action"] == "GetData")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["pId"].ToString());
                this.GetDataListGet(pId);
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataBom")
            {
                int pId = ConvertTo.ConvertInt(Request.Params["pId"].ToString());
                int goodsId = ConvertTo.ConvertInt(Request.Params["goodsId"].ToString());
                int num = ConvertTo.ConvertInt(Request.Params["num"].ToString());
                this.GetDataListBom(pId, goodsId, num);
                Response.End();
            }
        }

        public void Bind()
        {
            ListItem items = new ListItem("(Please Select)", "0");

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            UserDAL userDAL = new UserDAL();

            this.ddlYWYList.DataSource = userDAL.GetList(isWhere);
            this.ddlYWYList.DataTextField = "names";
            this.ddlYWYList.DataValueField = "id";
            this.ddlYWYList.DataBind();
            this.ddlYWYList.SelectedValue = LoginUser.Id.ToString();
        }

        public void BindInfo()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["pId"].ToString());

            string isWhere = " id='" + id + "'";

            DAL.produce.ProduceGetList dal = new DAL.produce.ProduceGetList();

            string planNumber = "";
            DataSet ds = dal.GetList(isWhere);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 绑定
                this.hfGoodsId.Value = ds.Tables[0].Rows[0]["goodsId"].ToString();
                planNumber = ds.Tables[0].Rows[0]["planNumber"].ToString();
                this.hfGoodsName.Value = ds.Tables[0].Rows[0]["goodsName"].ToString();
                this.txtGoodsName.Text = ds.Tables[0].Rows[0]["goodsName"].ToString();
                this.hfOrderNumber.Value = ds.Tables[0].Rows[0]["planNumber"].ToString();
                this.txtOrderNumber.Text = ds.Tables[0].Rows[0]["planNumber"].ToString();
                this.txtBizDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["BizDate"].ToString()).ToShortDateString();
                this.ddlYWYList.SelectedValue = ds.Tables[0].Rows[0]["bizId"].ToString();
                this.txtSpec.Text = ds.Tables[0].Rows[0]["spec"].ToString();
                this.txtUnitName.Text = ds.Tables[0].Rows[0]["unitName"].ToString();
                this.txtNum.Text = ds.Tables[0].Rows[0]["num"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                int pId = ConvertTo.ConvertInt(ds.Tables[0].Rows[0]["id"].ToString());

                string flag = ds.Tables[0].Rows[0]["flag"].ToString();
                if (flag != "Save")
                {
                    this.btnSave.Visible = false;
                }
                #endregion
            }

            ProduceListDAL planDAL = new ProduceListDAL();
            DataSet dsPlan = planDAL.GetList(" number='" + planNumber + "' and shopId='" + LoginUser.ShopId + "' ");
            if (dsPlan.Tables[0].Rows.Count > 0)
            {
                this.hfPId.Value = dsPlan.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                this.hfPId.Value = "0";
            }

        }

        void GetDataListBom(int pId, int goodsId, int produceNumNo)
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
                if (rate == 0)
                {
                    rate = 100;
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
                    pihao = "",
                    numApply = numApply,
                    price = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    rate = ds.Tables[0].Rows[i]["rate"].ToString(),
                    num = numApply,
                    sumPrice = price * numApply,
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
                        ckId = "",
                        ckName = "",
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

        void GetDataListGet(int pId)
        {
            IList<object> list = new List<object>();

            //从生产领料获取清单
            ProduceGetListItem dal = new ProduceGetListItem();

            DataSet ds = dal.GetList(" pId='" + pId + "' ");

            int rows = ds.Tables[0].Rows.Count;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

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

                    pihao = ds.Tables[0].Rows[i]["pihao"].ToString(),

                    numApply = ds.Tables[0].Rows[i]["numApply"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),
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
                        code = "",
                        goodsId = "",
                        goodsName = "",
                        spec = "",
                        unitName = "",
                        ckId = "",
                        ckName = "",
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