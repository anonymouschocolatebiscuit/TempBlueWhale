using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;
using System.Data;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class PrintSet : BasePage
    {
        SystemSetDAL dal = new SystemSetDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //不存在权限、或者不是管理员
                //if (!CheckPower("SystemSet"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}



                this.Bind();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            if (ds.Tables[0].Rows.Count > 0)
            {



                this.txtRemarksPurOrder.Text = ds.Tables[0].Rows[0]["RemarksPurOrder"].ToString();
                this.txtRemarksSalesOrder.Text = ds.Tables[0].Rows[0]["RemarksSalesOrder"].ToString();
                //this.txtFieldC.Text = ds.Tables[0].Rows[0]["FieldC"].ToString();
                //this.txtFieldD.Text = ds.Tables[0].Rows[0]["FieldD"].ToString();


            }



        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            string remarks = this.txtRemarksPurOrder.Text;

            int update = dal.UpdatePrintSetPurOrderRemarks(LoginUser.ShopId, remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "保存成功！");             
            }
        }

        protected void btnSaveSalesOrderRemarks_Click(object sender, EventArgs e)
        {
            string remarks = this.txtRemarksSalesOrder.Text;

            int update = dal.UpdatePrintSetSalesOrderRemarks(LoginUser.ShopId, remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "保存成功！");
            }
        }
    }
}