using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BlueWhale.UI.src;

namespace BlueWhale.UI.store
{
    public partial class InventoryTransferListAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("InventoryTransferListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }
                this.txtBizDate.Text = DateTime.Now.ToShortDateString();
            }

            if (Request.Params["Action"] == "GetData")
            {
                this.GetDataList();
                Response.End();
            }
        }

        void GetDataList()
        {
            IList<object> list = new List<object>();
            for (var i = 1; i < 9; i++)
            {
                list.Add(new
                {
                    id = i,
                    goodsId = "",
                    goodsName = "",
                    spec = "",
                    unitName = "",
                    num = "",
                    ckIdIn = "",
                    ckIdOut = "",
                    ckNameIn = "",
                    ckNameOut = "",

                    remarks = ""
                });
            }
            var griddata = new { Rows = list, Total = list.Count.ToString() };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }
    }
}
