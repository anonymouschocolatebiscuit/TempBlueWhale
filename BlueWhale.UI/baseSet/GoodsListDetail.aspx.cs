using BlueWhale.Common;
using BlueWhale.DAL;
using System;
using System.Data;


namespace BlueWhale.UI.baseSet
{
    public partial class GoodsListDetail : System.Web.UI.Page
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Bind();
            }
        }
        public void Bind()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dal.GetModelById(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtNeirong.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
            }
            else
            {
                this.txtNeirong.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            string remarks = this.txtNeirong.Text;
            int update = dal.UpdateRemarks(id, remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "Save Successful!");

                this.Bind();
            }
        }
    }
}
