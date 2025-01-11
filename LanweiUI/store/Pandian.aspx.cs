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
    public partial class Pandian : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

       

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["Action"] == "GetDataList")
            {
               
                string ckId = Request.Params["ckId"].ToString();

                string typeId = Request.Params["typeId"].ToString();

                string code = Request.Params["goodsId"].ToString();

                this.GetDataList(typeId, ckId, code);
                Response.End();
            }
        }

        void GetDataList(string typeId,string ckId, string code)
        {



            DataSet ds = dal.GetGoodsStoreNumNow(LoginUser.ShopId,typeId, ckId, code);


            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                   
                    ckId = ds.Tables[0].Rows[i]["ckId"].ToString(),
                    ckName = ds.Tables[0].Rows[i]["ckName"].ToString(),
                    goodsId = ds.Tables[0].Rows[i]["goodsId"].ToString(),
                    code = ds.Tables[0].Rows[i]["code"].ToString(),
                    goodsName = ds.Tables[0].Rows[i]["goodsName"].ToString(),
                    spec = ds.Tables[0].Rows[i]["spec"].ToString(),
                    unitName = ds.Tables[0].Rows[i]["unitName"].ToString(),

                   
                    sumNum = ds.Tables[0].Rows[i]["sumNum"].ToString(),
                    sumNumPD = "",
                    sumNumPK = ""

                    
                    

                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

    }
}
