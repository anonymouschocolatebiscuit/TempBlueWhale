using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Data;

namespace BlueWhale.UI.baseSet
{
    public partial class PrintSet : BasePage
    {
        private readonly SystemSetDAL dal = new SystemSetDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRemarksPurOrder.Text = ds.Tables[0].Rows[0]["RemarksPurOrder"].ToString();
                txtRemarksSalesOrder.Text = ds.Tables[0].Rows[0]["RemarksSalesOrder"].ToString();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string remarks = txtRemarksPurOrder.Text;
            int update = dal.UpdatePrintSetPurOrderRemarks(LoginUser.ShopId, remarks);

            if (update > 0)
            {
                MessageBox.Show(this, "Save success!");
            }
        }

        protected void BtnSaveSalesOrderRemarks_Click(object sender, EventArgs e)
        {
            string remarks = txtRemarksSalesOrder.Text;
            int update = dal.UpdatePrintSetSalesOrderRemarks(LoginUser.ShopId, remarks);

            if (update > 0)
            {
                MessageBox.Show(this, "Save success!");
            }
        }
    }
}
