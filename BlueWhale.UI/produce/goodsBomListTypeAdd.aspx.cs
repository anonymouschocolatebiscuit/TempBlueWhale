using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Text.RegularExpressions;

namespace BlueWhale.UI.produce
{
    public partial class goodsBomListTypeAdd : BasePage
    {
        public DAL.produce.goodsBomListType dal = new DAL.produce.goodsBomListType();

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
                this.txtSortId.Text = Request.QueryString["sortId"].ToString();
            }
            else
            {
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Empty check
            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please enter a name!");
                this.txtNames.Focus();
                return;
            }

            // 2. Name Pattern check (only letters and digits allowed)
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            if (!regex.IsMatch(this.txtNames.Text))
            {
                MessageBox.Show(this, "No symbol allowed!");
                this.txtNames.Focus();
                return;
            }

            // 3. SortId Patterb cgecj (only number allowed)
            if (string.IsNullOrWhiteSpace(this.txtSortId.Text) || !int.TryParse(this.txtSortId.Text, out _))
            {
                MessageBox.Show(this, "Sort must be a valid number!");
                this.txtSortId.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            Model.produce.goodsBomListType model = new Model.produce.goodsBomListType();
            model.id = id;

            model.names = this.txtNames.Text;
            model.shopId = LoginUser.ShopId;
            model.sortId = ConvertTo.ConvertInt(this.txtSortId.Text);

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Add BOM group failed, duplicate name!");
                    return;
                }

                if (dal.Add(model) > 0)
                {
                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Added BOM Group：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Add successfully");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id, LoginUser.ShopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Edit failed, duplicate name!");
                    return;
                }

                if (dal.Update(model))
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edited BOM Group：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Edit successfully！");
                }
            }
        }
    }
}