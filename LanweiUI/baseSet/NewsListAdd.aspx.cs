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
using Lanwei.Weixin.Model;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class NewsListAdd : BasePage
    {
        public NewsDAL dal = new NewsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

                DataSet ds = dal.GetNewsList(id);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    this.txtTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                    this.ddlTypeName.SelectedValue = ds.Tables[0].Rows[0]["typeName"].ToString();
                    this.txtNeirong.Text = ds.Tables[0].Rows[0]["contents"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            string replay = this.txtNeirong.Text;// this.hfValue.Value.ToString();

            string title = this.txtTitle.Text;
            string typeName = this.ddlTypeName.SelectedValue.ToString();


            if (title == "")
            {
                MessageBox.Show(this, "请填写标题！");
                return;
            }

            if (replay == "")
            {
                MessageBox.Show(this, "请填写内容！");
                return;
            }

         



            dal.Id = id;
            dal.ShopId = LoginUser.ShopId;
            dal.Title = title;
            dal.TypeName = typeName;
            dal.Contents = replay;
            dal.ImagePath = "";

            if (id == 0)
            {

                int add = dal.Add();
                if (add > 0)
                {
                    MessageBox.Show(this, "新增成功！");

                    this.txtTitle.Text = "";
                    this.txtNeirong.Text = "";

                }
            }
            else
            {
                int update = dal.Update();

                if (update > 0)
                {
                    MessageBox.Show(this, "修改成功！");
                }

            }
            





        }
    }
}
