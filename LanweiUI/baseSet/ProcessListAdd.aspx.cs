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

using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class ProcessListAdd : BasePage
    {
       
        public Lanwei.Weixin.DAL.processTypeDAL typeDAL = new Lanwei.Weixin.DAL.processTypeDAL();
        public Lanwei.Weixin.DAL.processListDAL dal = new Lanwei.Weixin.DAL.processListDAL();
        public Lanwei.Weixin.DAL.UnitDAL unitDAL = new Lanwei.Weixin.DAL.UnitDAL();

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

            this.ddlTypeList.Items.Insert(0, new ListItem("请选择", "0"));



            DataSet unitList = unitDAL.GetList("shopid=" + LoginUser.ShopId);

            this.ddlUnitList.DataValueField = "id";
            this.ddlUnitList.DataTextField = "names";
            this.ddlUnitList.DataSource = unitList;
            this.ddlUnitList.DataBind();

            this.ddlUnitList.Items.Insert(0, new ListItem("请选择", "0"));

        }


        public void BindDetail()
        {

            this.txtNames.Focus();

            if (Request.QueryString.Count>0)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int id = ConvertTo.ConvertInt(this.hfId.Value.ToString());

            //if (!CheckPower("PayGetListAdd"))
            //{
            //    MessageBox.Show(this, "无此操作权限！");
            //    return;
            //}

            if (this.txtNames.Text.Trim() == "")
            {
                MessageBox.Show(this, "请输入工序名称！");
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

                if (dal.isExistsNamesAdd(shopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "新增失败，名称重复！");
                    return;
                }

                int iddd = dal.Add();

                if (iddd > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增工序-：" + this.txtNames.Text;
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this.Page, "添加成功！", "processListAdd.aspx");
                }
            }
            else
            {



                if (dal.isExistsNamesEdit(id, shopId, this.txtNames.Text))
                {
                    MessageBox.Show(this, "修改失败，名称重复！");
                    return;
                }

                dal.Id = id;

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改工序-ID："+id.ToString()+" 为：" + this.txtNames.Text;
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.Show(this.Page, "修改成功！");
                }

            }



            
        }
    }
}
