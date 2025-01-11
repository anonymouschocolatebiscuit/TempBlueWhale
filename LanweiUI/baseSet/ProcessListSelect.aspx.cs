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
    public partial class ProcessListSelect : BasePage
    {
        public processListDAL dal = new processListDAL();




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

                GetDataListSearch(typeId, keys);
                Response.End();
            }

        }


        public void Bind()
        {
           


        }

        void GetDataList()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString()


                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要




            Response.Write(s);
        }


        void GetDataListSearch(int typeId, string keys)
        {

            string isWhere = " shopId='" + LoginUser.ShopId + "' ";

            if (typeId != 0)
            {
                isWhere += " and typeId='" + typeId + "' ";
            }

            if (keys != "")
            {
                isWhere += " and names like '%"+keys+"%' ";
            }

            DataSet ds = dal.GetList(isWhere);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

               

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    price = ds.Tables[0].Rows[i]["price"].ToString(),
                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),
                    seq = ds.Tables[0].Rows[i]["seq"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString()



                });

            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
