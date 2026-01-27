using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using BlueWhale.DAL;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class SpecListAdd : BasePage
    {
        public SpecDAL dal = new SpecDAL();

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
            }
            else
            {
                this.hfId.Value = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckPower("SpecListAdd"))
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

          
            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            dal.Id = id;
        
            dal.Names = this.txtNames.Text;

            if (id == 0)
            {
                if (dal.isExistsNamesAdd(this.txtNames.Text))
                {
                    MessageBox.Show(this, "Failed to add new name, duplicate name!");
                    return;
                }

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "New Measure Unit：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful！");
                }

            }
            else
            {
                if (dal.isExistsNamesEdit(id,this.txtNames.Text))
                {
                    MessageBox.Show(this, "Modification failed, the name is duplicated!");
                    return;
                }

                if (dal.Update() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edit Measure Unit：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this, "Operation Successful！");
                }
            }



            
        }
    }
}
