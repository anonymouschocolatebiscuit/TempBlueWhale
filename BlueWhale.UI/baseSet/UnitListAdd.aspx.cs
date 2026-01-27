using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class UnitListAdd : BasePage
    {
        public UnitDAL dal = new UnitDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDetail();
            }
        }

        public void BindDetail()
        {
            txtNames.Focus();

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                hfId.Value = id.ToString();
                txtNames.Text = Request.QueryString["names"].ToString();
            }
            else
            {
                hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckPower("SpecListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            if (string.IsNullOrEmpty(txtNames.Text))
            {
                MessageBox.Show(this, "Please fill in the name!");
                txtNames.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(hfId.Value.ToString());

            dal.Id = id;
            dal.Names = txtNames.Text;
            dal.ShopId = LoginUser.ShopId;

            if (id == 0)
            {
                bool exists = dal.isExistsNamesAdd(LoginUser.ShopId, txtNames.Text);
                if (exists)
                {
                    MessageBox.Show(this, "Failed to add, name already exists!");
                    return;
                }

                int addResult = dal.Add();
                if (addResult > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Add New Unit Measurement：" + txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }
            }
            else
            {
                bool duplicate = dal.isExistsNamesEdit(id, LoginUser.ShopId, txtNames.Text);
                if (duplicate)
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                int updateResult = dal.Update();
                if (updateResult > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Edit Unit Measurement：" + txtNames.Text,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }
            }
        }
    }
}
