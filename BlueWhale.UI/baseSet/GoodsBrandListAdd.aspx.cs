using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class GoodsBrandListAdd : BasePage
    {
        public GoodsBrandDAL dal = new GoodsBrandDAL();

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

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.txtFlag.Text = Request.QueryString["flag"].ToString();
            }
            else
            {
                this.txtFlag.Text = "0";
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please fill in the name！");
                this.txtNames.Focus();
                return;
            }

            if (this.txtFlag.Text == "")
            {
                MessageBox.Show(this, "Please fill in the display order！");
                this.txtFlag.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
            dal.Flag = ConvertTo.ConvertInt(this.txtFlag.Text);
            dal.Names = this.txtNames.Text;
            dal.ShopId = LoginUser.ShopId;

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Add new product brands：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful！");
                }
            }
            else
            {
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Modify product brand：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful！");
                }
            }
        }
    }
}