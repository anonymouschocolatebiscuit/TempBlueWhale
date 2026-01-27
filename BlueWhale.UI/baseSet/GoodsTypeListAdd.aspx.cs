using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class GoodsTypeListAdd : BasePage
    {
        public GoodsTypeDAL dal = new GoodsTypeDAL();

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
                this.hfId.Value = Request.QueryString["id"].ToString();
                this.hfParentId.Value = Request.QueryString["parentId"].ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.txtFlag.Text = Request.QueryString["seq"].ToString();
                this.txtParentName.Text = Request.QueryString["parentName"].ToString();

                if (this.txtParentName.Text == "")
                {
                    this.txtParentName.Text = "Parent Category";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckPower("GoodsTypeListAdd"))
            {
                MessageBox.Show(this, "No permission to perform this action！");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please enter the category name！");
                this.txtNames.Focus();

                return;
            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
            dal.Seq = ConvertTo.ConvertInt(this.txtFlag.Text);
            dal.Names = this.txtNames.Text;
            dal.ParentId = ConvertTo.ConvertInt(this.hfParentId.Value.ToString());
            dal.ShopId = LoginUser.ShopId;

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId, ConvertTo.ConvertInt(this.hfParentId.Value.ToString()), this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to create，duplicate name found！");
                    return;
                }

                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Create item category：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation success！");
                }
            }
            else
            {
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, ConvertTo.ConvertInt(this.hfParentId.Value.ToString()), this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to create，duplicate name found！");
                    return;
                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Modified item category：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation success！");
                }
            }
        }
    }
}