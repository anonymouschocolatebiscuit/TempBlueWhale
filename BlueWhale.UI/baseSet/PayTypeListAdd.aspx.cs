using System;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BlueWhale.UI.baseSet
{
    public partial class PayTypeListAdd : BasePage
    {
        public PayTypeDAL dal = new PayTypeDAL();

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



            }
            else
            {

                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckPower("PayTypeListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please enter name!");
                this.txtNames.Focus();
                return;

            }


            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
            dal.ShopId = LoginUser.ShopId;
            dal.Names = this.txtNames.Text;

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Add failed, name is duplicated!");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Add pay type:" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "successful!");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Edit failed, name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edit pay type: " + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "successful!");
                }
            }




        }
    }
}
