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
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;


namespace Lanwei.Weixin.UI.baseSet
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

            int update = dal.UpdateRemarks(id,remarks);
            if (update > 0)
            {
                MessageBox.Show(this, "保存成功！");

                this.Bind();
            }
        }
    }
}
