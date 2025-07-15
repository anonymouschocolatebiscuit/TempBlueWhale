using System;
using System.Data;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class PrintSet : BasePage
    {
        SystemSetDAL dal = new SystemSetDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string remarks = this.txtRemarksPurOrder.Text;

            int update = dal.UpdatePrintSetPurOrderRemarks(LoginUser.ShopId, remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "Save success!");
            }
        }

        protected void btnSaveSalesOrderRemarks_Click(object sender, EventArgs e)
        {
            string remarks = this.txtRemarksSalesOrder.Text;

            int update = dal.UpdatePrintSetSalesOrderRemarks(LoginUser.ShopId, remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "Save success!");
            }
        }
    }
}