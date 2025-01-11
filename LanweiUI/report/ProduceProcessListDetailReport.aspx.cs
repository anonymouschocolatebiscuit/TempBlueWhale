using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;

using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.report
{
    public partial class ProduceProcessListDetailReport : BasePage
    {
        public ProduceProcessListDAL dal = new ProduceProcessListDAL();

        public UserDAL dalUser = new UserDAL();

        public Lanwei.Weixin.DAL.processTypeDAL typeDAL = new Lanwei.Weixin.DAL.processTypeDAL();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (!CheckPower("produceList"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}

                this.txtDateStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();

                this.BindList();
            }

            if (Request.Params["Action"] == "GetDataList")
            {

                DateTime bizStart = DateTime.Parse(Request.Params["start"].ToString());

                DateTime bizEnd = DateTime.Parse(Request.Params["end"].ToString());



                string typeId = Request.Params["typeId"].ToString();

                string itemIdString = Request.Params["itemIdString"].ToString();

                string bizId = Request.Params["bizId"].ToString();


                GetDataList(bizStart, bizEnd, typeId, itemIdString, bizId);
                Response.End();
            }

            
         
        }

        void GetDataList(DateTime start, DateTime end, string typeId, string itemIdString, string bizId)
        {
            string isWhere = " shopId='"+LoginUser.ShopId+"' ";

             isWhere += " and CONVERT(varchar(100),bizDate, 23)>='" + start.ToString("yyyy-MM-dd")
              + "'  and CONVERT(varchar(100),bizDate, 23)<='" + end.ToString("yyyy-MM-dd") + "' ";


             if (typeId != "")
             {
                 isWhere += " and typeId='" + typeId + "'  ";
             }

             if (itemIdString != "")
             {
                 isWhere += " and processId in(" + itemIdString + ") ";
             }



             if (bizId != "0")
             {
                 isWhere += " and bizId='" + bizId + "'  ";
             }


            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),


                    bizDate = ds.Tables[0].Rows[i]["bizDate"].ToString(),
                    bizName = ds.Tables[0].Rows[i]["bizName"].ToString(),
                    processName = ds.Tables[0].Rows[i]["processName"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    num = ds.Tables[0].Rows[i]["num"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    sumPrice = ds.Tables[0].Rows[i]["sumPrice"].ToString(),

                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    
                    wlName = ds.Tables[0].Rows[i]["wlName"].ToString(),              
                    orderNumber = ds.Tables[0].Rows[i]["orderNumber"].ToString(),
            
                    

                    
                    

                  
                    flag = ds.Tables[0].Rows[i]["flag"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    checkName = ds.Tables[0].Rows[i]["checkName"].ToString(),
                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString()



                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要



            Response.Write(s);
        }


        public void BindList()
        {
            DataSet cateList = typeDAL.GetList("shopid=" + LoginUser.ShopId);

            this.ddlTypeList.DataValueField = "id";
            this.ddlTypeList.DataTextField = "names";
            this.ddlTypeList.DataSource = cateList;
            this.ddlTypeList.DataBind();

            this.ddlTypeList.Items.Insert(0, new ListItem("全部类别", "0"));


            DataSet ds = dalUser.GetList(" shopId=" + Utils.GetCookie("shopId"));

            this.ddlUserList.DataValueField = "id";
            this.ddlUserList.DataTextField = "names";
            this.ddlUserList.DataSource = ds;
            this.ddlUserList.DataBind();

            this.ddlUserList.Items.Insert(0, new ListItem("全部员工", "0"));


        }


    }
}
