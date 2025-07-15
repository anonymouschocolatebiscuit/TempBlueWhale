using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.BaseSet
{
    public partial class GoodsListNumStart : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("GoodsListAdd"))
                {
                    Response.Redirect("../OverPower.htm");
                }

                this.Bind();
            }
        }
        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            InventoryDAL ckDal = new InventoryDAL();
            this.ddlInventoryList.DataSource = ckDal.GetList(isWhere);
            this.ddlInventoryList.DataTextField = "names";
            this.ddlInventoryList.DataValueField = "id";
            this.ddlInventoryList.DataBind();

            DataSet ds = dal.GetModelById(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.lbCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                this.lbNames.Text = ds.Tables[0].Rows[0]["names"].ToString();
            }

            this.gvLevel.DataSource = dal.GetGoodsNumStartById(id);
            this.gvLevel.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckPower("GoodsListAdd"))
            {
                MessageBox.Show(this, "No permission for this action!");
                return;
            }

            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            int ckId = ConvertTo.ConvertInt(this.ddlInventoryList.SelectedValue.ToString());
            decimal num = ConvertTo.ConvertDec(this.txtNum.Text);
            decimal priceCost = ConvertTo.ConvertDec(this.txtPriceCost.Text);
            decimal sumPrice = num * priceCost;
            int add = dal.AddGoodsNumStart(id, ckId, num, priceCost, sumPrice);

            if (add > 0)
            {
                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "New goods number start: " + id.ToString() + " Number: " + this.txtNum.Text + " Inventory: " + this.ddlInventoryList.SelectedItem.Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                this.Bind();
            }
        }

        decimal sumNum = 0;
        decimal sumPrice = 0;

        protected void gvLevel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[5].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you confirm to delete?')");

                    if (e.Row.RowIndex != -1)
                    {
                        e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                        sumNum += ConvertTo.ConvertDec(e.Row.Cells[2].Text);
                        sumPrice += ConvertTo.ConvertDec(e.Row.Cells[4].Text);
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total: ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = sumNum.ToString();

                if (sumNum != 0)
                {
                    e.Row.Cells[3].Text = (sumPrice / sumNum).ToString("0.000");
                }
                else
                {
                    e.Row.Cells[3].Text = "0";
                }
                e.Row.Cells[4].Text = sumPrice.ToString();
            }
        }

        protected void gvLevel_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = int.Parse(this.gvLevel.DataKeys[e.RowIndex].Values[0].ToString());
            int t = dal.DeleteGoodsNumStart(Id);
            if (t > 0)
            {
                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "Delete goods number start: " + this.lbNames.Text + "Inventory: " + this.gvLevel.Rows[e.RowIndex].Cells[1].Text + "Number: " + this.gvLevel.Rows[e.RowIndex].Cells[2].Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                this.Bind();
            }
        }
    }
}