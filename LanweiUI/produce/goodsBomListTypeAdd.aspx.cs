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

namespace Lanwei.Weixin.UI.produce
{
    public partial class goodsBomListTypeAdd : BasePage
    {
        public DAL.goodsBomListType dal = new DAL.goodsBomListType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               
                this.BindDetail();

            }
        }

        public void BindDetail()
        {

            this.txtNames.Focus();

            if (Request.QueryString.Count>0)
            {

                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.txtSortId.Text = Request.QueryString["sortId"].ToString();
                

            }
            else
            {
               
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //if (!CheckPower("goodsBomListTypeAdd"))
            //{
            //    MessageBox.Show(this, "无此操作权限！");
            //    return;
            //}

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "请填写名称！");
                this.txtNames.Focus();
                return;

            }

          
            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            Model.goodsBomListType model = new Model.goodsBomListType();
            model.id = id;

            model.names = this.txtNames.Text;
            model.shopId = LoginUser.ShopId;
            model.sortId = ConvertTo.ConvertInt(this.txtSortId.Text);

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId,this.txtNames.Text))
                {
                    MessageBox.Show(this, "新增失败，名称重复！");
                    return;
                }

                if (dal.Add(model) > 0)
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增BOM分组：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id,LoginUser.ShopId,this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，名称重复！");
                    return;
                }

                if (dal.Update(model))
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改BOM分组：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "操作成功！");
                }
            }



            
        }
    }
}
