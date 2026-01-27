using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.UI.src;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class ProcessTypeListAdd : BasePage
    {
        public ProcessTypeDAL dal = new ProcessTypeDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDetail();
            }
        }

        private void BindDetail()
        {
            txtNames.Focus();

            if (Request.QueryString.Count > 0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"]);
                hfId.Value = id.ToString();
                txtNames.Text = Request.QueryString["names"];
                txtSeq.Text = Request.QueryString["seq"];
            }
            else
            {
                hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNames.Text))
            {
                MessageBox.Show(this, "Please fill in the name！");
                txtNames.Focus();
                return;
            }

            int id = ConvertTo.ConvertInt(hfId.Value);
            dal.Id = id;
            dal.ShopId = LoginUser.ShopId;
            dal.Names = txtNames.Text;
            dal.Seq = ConvertTo.ConvertInt(txtSeq.Text);

            if (id == 0)
            {
                // Add new entry
                if (dal.IsExistsNamesAdd(LoginUser.ShopId, txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                if (dal.Add() > 0)
                {
                    AddLog("Create Process Category：" + txtNames.Text);
                    MessageBox.Show(this, "Operation Successful!");
                }
            }
            else
            {
                // Update existing entry
                if (dal.IsExistsNamesEdit(id, LoginUser.ShopId, txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {
                    AddLog("Edit Process Category：" + txtNames.Text);
                    MessageBox.Show(this, "Operation Successful!");
                }
            }
        }

        private void AddLog(string eventDescription)
        {
            LogsDAL logs = new LogsDAL
            {
                ShopId = LoginUser.ShopId,
                Users = $"{LoginUser.Phone}-{LoginUser.Names}",
                Events = eventDescription,
                Ip = System.Web.HttpContext.Current.Request.UserHostAddress
            };
            logs.Add();
        }
    }
}
