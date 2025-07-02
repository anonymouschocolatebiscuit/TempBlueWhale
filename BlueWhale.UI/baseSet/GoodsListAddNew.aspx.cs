﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.BaseSet
{
    public partial class GoodsListAddNew : BasePage
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                if (!CheckPower("GoodsListAdd"))
                {
                    MessageBox.Show(this, "No permission for this operation!");
                    return;
                }

                this.txtCode.Focus();


                this.Bind();
            }
        }
        public void Bind()
        {

            this.lbFieldA.Text = SysInfo.FieldA.ToString();
            this.lbFieldB.Text = SysInfo.FieldB.ToString();
            this.lbFieldC.Text = SysInfo.FieldC.ToString();
            this.lbFieldD.Text = SysInfo.FieldD.ToString();



            this.ddlUnitList.Items.Clear();
            this.ddlCangkuList.Items.Clear();
            this.ddlVenderTypeList.Items.Clear();
            this.ddlBrandList.Items.Clear();


            this.Bound(this.ddlVenderTypeList);


            InventoryDAL inventoryDal = new InventoryDAL();
            this.ddlCangkuList.DataSource = inventoryDal.GetALLModelList(LoginUser.ShopId);
            this.ddlCangkuList.DataTextField = "names";
            this.ddlCangkuList.DataValueField = "id";
            this.ddlCangkuList.DataBind();

            this.ddlCangkuList.Items.Insert(0, new ListItem("Please select", "0"));


            SpecDAL specDal = new SpecDAL();
            this.ddlUnitList.DataSource = specDal.GetALLModelList();
            this.ddlUnitList.DataTextField = "names";
            this.ddlUnitList.DataValueField = "id";
            this.ddlUnitList.DataBind();

            this.ddlUnitList.Items.Insert(0, new ListItem("Please select", "0"));


            GoodsBrandDAL brandDal = new GoodsBrandDAL();
            this.ddlBrandList.DataSource = brandDal.GetALLModelList();
            this.ddlBrandList.DataTextField = "names";
            this.ddlBrandList.DataValueField = "id";
            this.ddlBrandList.DataBind();
            this.ddlBrandList.Items.Insert(0, new ListItem("Please select", "0"));





            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            if (id == 0)
            {
                this.Title = "新增商品";
            }
            else
            {
                this.Title = "修改商品";
            }


            DataSet ds = dal.GetModelById(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.hf.Value = ds.Tables[0].Rows[0]["id"].ToString();
                this.txtCode.Text = ds.Tables[0].Rows[0]["code"].ToString();
                this.txtBarcode.Text = ds.Tables[0].Rows[0]["barcode"].ToString();
                this.txtNames.Text = ds.Tables[0].Rows[0]["names"].ToString();

                this.ddlVenderTypeList.SelectedValue = ds.Tables[0].Rows[0]["typeId"].ToString();
                this.ddlBrandList.SelectedValue = ds.Tables[0].Rows[0]["brandId"].ToString();

                this.txtSpec.Text = ds.Tables[0].Rows[0]["spec"].ToString();
                this.ddlUnitList.SelectedValue = ds.Tables[0].Rows[0]["unitId"].ToString();

                this.ddlCangkuList.SelectedValue = ds.Tables[0].Rows[0]["ckId"].ToString();
                this.txtPlace.Text = ds.Tables[0].Rows[0]["place"].ToString();

                this.txtPriceCost.Text = ds.Tables[0].Rows[0]["PriceCost"].ToString();

                this.txtPriceSalesWhole.Text = ds.Tables[0].Rows[0]["PriceSalesWhole"].ToString();
                this.txtPriceSalesRetail.Text = ds.Tables[0].Rows[0]["PriceSalesRetail"].ToString();

                this.txtNumMax.Text = ds.Tables[0].Rows[0]["numMax"].ToString();
                this.txtNumMin.Text = ds.Tables[0].Rows[0]["numMin"].ToString();

                this.txtFieldA.Text = ds.Tables[0].Rows[0]["fieldA"].ToString();
                this.txtFieldB.Text = ds.Tables[0].Rows[0]["fieldB"].ToString();
                this.txtFieldC.Text = ds.Tables[0].Rows[0]["fieldC"].ToString();
                this.txtFieldD.Text = ds.Tables[0].Rows[0]["fieldD"].ToString();

                string isWeight = ds.Tables[0].Rows[0]["isWeight"].ToString();





                this.hfImagePath.Value = ds.Tables[0].Rows[0]["imagePath"].ToString();

            }
            else
            {

                this.ddlUnitList.SelectedValue = "0";
                this.ddlCangkuList.SelectedValue = "0";
                this.ddlVenderTypeList.SelectedValue = "0";
                this.ddlBrandList.SelectedValue = "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            if (!CheckPower("GoodsListAdd"))
            {
                MessageBox.Show(this, "No permission for this operation!");
                return;
            }

            string imageName = "";

            dal.Id = ConvertTo.ConvertInt(this.hf.Value.ToString());
            dal.ShopId = LoginUser.ShopId;
            dal.Code = this.txtCode.Text;
            dal.Barcode = this.txtBarcode.Text;
            dal.Names = this.txtNames.Text;
            dal.TypeId = ConvertTo.ConvertInt(this.ddlVenderTypeList.SelectedValue.ToString());
            dal.BrandId = ConvertTo.ConvertInt(this.ddlBrandList.SelectedValue.ToString());

            dal.Spec = this.txtSpec.Text;
            dal.UnitId = ConvertTo.ConvertInt(this.ddlUnitList.SelectedValue.ToString());

            dal.CkId = ConvertTo.ConvertInt(this.ddlCangkuList.SelectedValue.ToString());
            dal.Place = this.txtPlace.Text;

            dal.PriceCost = ConvertTo.ConvertDec(this.txtPriceCost.Text);
            dal.PriceSalesWhole = ConvertTo.ConvertDec(this.txtPriceSalesWhole.Text);
            dal.PriceSalesRetail = ConvertTo.ConvertDec(this.txtPriceSalesRetail.Text);

            dal.NumMin = ConvertTo.ConvertInt(this.txtNumMin.Text);
            dal.NumMax = ConvertTo.ConvertInt(this.txtNumMax.Text);

            dal.BzDays = 0;
            dal.Flag = "Save";
            dal.MakeDate = DateTime.Now;


            dal.FieldA = this.txtFieldA.Text;
            dal.FieldB = this.txtFieldB.Text;
            dal.FieldC = this.txtFieldC.Text;
            dal.FieldD = this.txtFieldD.Text;


            dal.Remarks = "";
            dal.ImagePath = imageName;


            if (id.ToString() == "0")
            {
                if (dal.isExistsAdd(LoginUser.ShopId, this.txtCode.Text, this.txtBarcode.Text, this.txtNames.Text, this.txtSpec.Text))
                {
                    MessageBox.Show(this, "Failed to add, same code, barcode, name, specificaiton exist!");
                    return;
                }


                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Add Good：" + this.txtCode.Text + " Name：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "Operation Successful!", "GoodsListAdd.aspx?id=" + id.ToString());
                }


            }
            else //Edit
            {
                if (dal.isExistsEdit(ConvertTo.ConvertInt(this.hf.Value.ToString()), LoginUser.ShopId, this.txtCode.Text, this.txtBarcode.Text, this.txtNames.Text, this.txtSpec.Text))
                {
                    MessageBox.Show(this, "Failed to edit, same code, barcode, name, specificaiton exist!");
                    return;
                }


                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "Edit Good：" + this.txtCode.Text + " Name:" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "Operation Successful!", "GoodsListAdd.aspx?id=" + id.ToString());
                }

            }



        }

        public void DropDownListBoind(string Pading, int DirId, DataTable datatable, int deep, DropDownList list1)
        {
            DataRow[] rowlist = datatable.Select("parentID='" + DirId + "'");
            foreach (DataRow row in rowlist)
            {
                string strPading = "";
                for (int j = 0; j < deep; j++)
                {
                    strPading += "　";
                }

                ListItem li = new ListItem(strPading + "├ " + row["names"].ToString(), row["id"].ToString());
                list1.Items.Add(li);
                DropDownListBoind(strPading, Convert.ToInt32(row["id"]), datatable, deep + 1, list1);
            }
        }

        public void Bound(DropDownList list1)
        {

            list1.Items.Clear();

            DataTable datatable = GetDataTable();
            DataRow[] row = datatable.Select("parentID='0'");

            for (int j = 0; j < row.Length; j++)
            {
                ListItem li = new ListItem(row[j]["names"].ToString(), row[j]["id"].ToString());
                list1.DataTextField = row[j]["names"].ToString();
                list1.Items.Add(li);
                DropDownListBoind("", Convert.ToInt32(row[j]["id"]), datatable, 1, list1);
            }
            ListItem items = new ListItem("(Please select)", "0");
            list1.Items.Insert(0, items);
            list1.SelectedValue = "0";
        }
        public DataTable GetDataTable()
        {
            GoodsTypeDAL typesDal = new GoodsTypeDAL();

            DataSet ds = typesDal.GetALLModelList();
            DataTable tb = ds.Tables[0];
            return tb;
        }

    }
}
