using System;
using System.Data;
using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.BaseSet
{
    public partial class AccountListAdd : BasePage
    {
        public AccountDAL dal = new AccountDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCode.Focus();
                txtYueDate.Text = DateTime.Now.ToShortDateString();
                Bind();
            }
        }

        public void Bind()
        {

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

                DataSet ds = dal.GetAllModel(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                    txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
                    txtYueDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["yueDate"].ToString()).ToShortDateString();
                    txtYuePrice.Text = ds.Tables[0].Rows[0]["yuePrice"].ToString();
                    ddlTypes.SelectedValue = ds.Tables[0].Rows[0]["types"].ToString();
                }
            }           
            else
            {
                // Nothing to bind for a new entry
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (Request.QueryString.Count > 0)
            {
                id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            }

            if (!CheckPower("AccountListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            if (txtCode.Text == "")
            {
                MessageBox.Show(this, "Please fill in the account number!");
                txtCode.Focus();
                return;
            }

            if (txtNames.Text == "")
            {
                MessageBox.Show(this, "Please fill in the account name!");
                txtNames.Focus();
                return;
            }

            dal.Code = txtCode.Text;
            dal.Names = txtNames.Text;
            dal.ShopId = LoginUser.ShopId;

            if (id == 0)
            {
                if (dal.isExistsCodeAdd(LoginUser.ShopId, txtCode.Text))
                {
                    MessageBox.Show(this, "Add failed, account number is duplicated!");
                    return;
                }
                if (dal.isExistsNamesAdd(LoginUser.ShopId, txtNames.Text))
                {
                    MessageBox.Show(this, "Add failed, account name is duplicated!");
                    return;
                }
            }
            else
            {
                if (dal.isExistsCodeEdit(id, LoginUser.ShopId, txtCode.Text))
                {
                    MessageBox.Show(this, "Edit failed, account number is duplicated!");
                    return;
                }
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, txtNames.Text))
                {
                    MessageBox.Show(this, "Edit failed, account name is duplicated!");
                    return;
                }
            }

            dal.YueDate = DateTime.Parse(txtYueDate.Text);
            dal.YuePrice = ConvertTo.ConvertDec(txtYuePrice.Text);
            dal.Types = ddlTypes.SelectedValue.ToString();
            dal.Id = id;

            if (id == 0)
            {
                if (dal.Add() > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Add account: " + txtCode.Text + " Name: " + txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Operation successful!");
                }
            }
            else
            {
                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Edit account: " + txtCode.Text + " Name: " + txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Operation successful!");
                }
            }
        }
    }
}
