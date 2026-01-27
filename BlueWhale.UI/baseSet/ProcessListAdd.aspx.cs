using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.baseSet
{
    public partial class ProcessListAdd : BasePage
    {

        public ProcessTypeDAL typeDAL = new ProcessTypeDAL();
        public ProcessListDAL dal = new ProcessListDAL();
        public UnitDAL unitDAL = new UnitDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindCaiPingType();

                this.BindDetail();
            }
        }

        protected void BindCaiPingType()
        {
            DataSet cateList = typeDAL.GetList("shopid=" + LoginUser.ShopId);

            this.ddlTypeList.DataValueField = "id";
            this.ddlTypeList.DataTextField = "names";
            this.ddlTypeList.DataSource = cateList;
            this.ddlTypeList.DataBind();

            this.ddlTypeList.Items.Insert(0, new ListItem("Please select", "0"));

            DataSet unitList = unitDAL.GetList("shopid=" + LoginUser.ShopId);

            this.ddlUnitList.DataValueField = "id";
            this.ddlUnitList.DataTextField = "names";
            this.ddlUnitList.DataSource = unitList;
            this.ddlUnitList.DataBind();

            this.ddlUnitList.Items.Insert(0, new ListItem("Please select", "0"));
        }

        public void BindDetail()
        {
            this.txtNames.Focus();

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.txtPrice.Text = Request.QueryString["price"].ToString();
                this.txtSortId.Text = Request.QueryString["seq"].ToString();
                this.ddlTypeList.SelectedValue = Request.QueryString["typeId"].ToString();
                this.ddlUnitList.SelectedValue = Request.QueryString["unitId"].ToString();
            }
            else
            {
                this.hfId.Value = "0";
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            if (!CheckPower("PayGetListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            if (this.txtNames.Text.Trim() == "")
            {
                MessageBox.Show(this, "Enter the process name!");
                return;
            }

            dal.Seq = ConvertTo.ConvertInt(this.txtSortId.Text);
            dal.ShopId = LoginUser.ShopId;
            dal.TypeId = ConvertTo.ConvertInt(this.ddlTypeList.SelectedValue.ToString());
            dal.Names = this.txtNames.Text;
            dal.Price = Convert.ToDecimal(this.txtPrice.Text);
            dal.UnitId = ConvertTo.ConvertInt(this.ddlUnitList.SelectedValue.ToString());
            int shopId = LoginUser.ShopId;

            if (id == 0)
            {
                if (dal.IsExistsNamesAdd(shopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                int iddd = dal.Add();

                if (iddd > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Add new process-:" + this.txtNames.Text,
                        Ip = Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.ShowAndRedirect(this.Page, "Added successfully!", "processListAdd.aspx");
                }
            }
            else
            {
                if (dal.IsExistsNamesEdit(id, shopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                dal.Id = id;

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = LoginUser.ShopId,
                        Users = LoginUser.Phone + "-" + LoginUser.Names,
                        Events = "Edit Process-ID:" + id.ToString() + " To:" + this.txtNames.Text,
                        Ip = Request.UserHostAddress.ToString()
                    };
                    logs.Add();

                    MessageBox.Show(this.Page, "Modification successful!");
                }
            }
        }
    }
}
