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
    public partial class welcomeGoodsList : BasePage
    {

        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {

         

            if (Request.Params["Action"] == "GetDataListSearch")
            {
                string keys = Request.Params["keys"].ToString();

                GetDataListSearch(keys);
                Response.End();
            }


           

        }

    

        void GetDataListSearch(string keys)
        {
            DataSet ds = dal.GetAllModelView(keys);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    names = ds.Tables[0].Rows[i]["names"].ToString(),
                    typeId = ds.Tables[0].Rows[i]["typeId"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),

                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),

                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),

                    unitId = ds.Tables[0].Rows[i]["unitId"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                    priceCost = ds.Tables[0].Rows[i]["priceCost"].ToString(),
                    priceSales = ds.Tables[0].Rows[i]["priceSales"].ToString(),

                    remarks = ds.Tables[0].Rows[i]["remarks"].ToString(),

                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    imagePath = ds.Tables[0].Rows[i]["imagePath"].ToString(),

                    sumNumStart = ds.Tables[0].Rows[i]["sumNumStart"].ToString(),

                    costUnit = ds.Tables[0].Rows[i]["costUnit"].ToString(),

                    sumPriceStart = ds.Tables[0].Rows[i]["sumPriceStart"].ToString()


                });
            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
