using BlueWhale.Common;
using BlueWhale.UI.src;
using BlueWhale.DAL;
using System;

namespace BlueWhale.UI.baseSet
{
    public partial class PayGetListAdd : BasePage
    {
        public PayGetDAL dal = new PayGetDAL();

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

            if (Request.QueryString.Count>0)
            {
                int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
                this.hfId.Value = id.ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
                this.ddlTypesList.SelectedValue = Request.QueryString["types"].ToString();
                

            }
            else
            {
               
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckPower("PayGetListAdd"))
            {
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

            if (this.txtNames.Text == "")
            {
                MessageBox.Show(this, "Please fill in the name！");
                this.txtNames.Focus();
                return;

            }

          
            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
            dal.ShopId = LoginUser.ShopId;
            dal.Names = this.txtNames.Text;
            dal.Types = this.ddlTypesList.SelectedValue.ToString();

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(LoginUser.ShopId,this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增收支类别：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id,LoginUser.ShopId,this.txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();

                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改收支类别：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful!");
                }
            }



            
        }
    }
}
