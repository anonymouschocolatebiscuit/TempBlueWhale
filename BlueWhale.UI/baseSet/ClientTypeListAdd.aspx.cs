using System;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.BaseSet;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class ClientTypeListAdd : BasePage
    {
        public ClientTypeDAL dal = new ClientTypeDAL();

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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CheckPower("ClientTypeListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please fill in the name!");
                this.txtNames.Focus();
                return;
            }

            if (this.txtFlag.Text == "")
            {
                MessageBox.Show(this, "Please fill in the display order!");
                this.txtFlag.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.ShopId = LoginUser.ShopId;
            dal.Id = id;
            dal.Flag = ConvertTo.ConvertInt(this.txtFlag.Text);
            dal.Names = this.txtNames.Text;

            if (id == 0)
            {
                if (dal.IsExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Add new customer category:" + this.txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Execution successful!");
                }
            }
            else
            {
                if (dal.IsExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Edit customer category:" + this.txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }
            }
        }
    }
}