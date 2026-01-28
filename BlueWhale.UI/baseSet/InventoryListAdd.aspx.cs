using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class InventoryListAdd : BasePage
    {
        public InventoryDAL dal = new InventoryDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                this.BindDetail();

            }
        }

        public void BindDetail()
        {

            this.txtCode.Focus();

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.txtCode.Text = Request.QueryString["code"].ToString();


            }
            else
            {
                this.txtCode.Text = "";
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckPower("InventoryListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }



            if (this.txtCode.Text == "")
            {
                MessageBox.Show(this, "Please enter Warehouse number!");
                this.txtCode.Focus();
                return;

            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please enter Warehouse name!");
                this.txtNames.Focus();
                return;

            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
            dal.Flag = 1;
            dal.Names = this.txtNames.Text;
            dal.Code = this.txtCode.Text;

            dal.ShopId = LoginUser.ShopId;

            if (id == 0)
            {

                if (dal.isExistsCodeAdd(LoginUser.ShopId, this.txtCode.Text))
                {
                    MessageBox.Show(this, "Failed to add, code already exists!");
                    return;
                }

                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add, name already exists!");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "New Warehouse:" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }

            }
            else
            {

                if (dal.isExistsCodeEdit(id, LoginUser.ShopId, this.txtCode.Text))
                {
                    MessageBox.Show(this, "Failed to update, code already exists!");
                    return;
                }

                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to update, name already exists!");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edit Warehouse:" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }
            }




        }
    }
}
