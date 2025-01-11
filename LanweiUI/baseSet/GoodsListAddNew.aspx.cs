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

namespace Lanwei.Weixin.UI.BaseSet
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
                    MessageBox.Show(this, "无此操作权限！");
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
            

            CangkuDAL ckDal = new CangkuDAL();
            this.ddlCangkuList.DataSource = ckDal.GetALLModelList(LoginUser.ShopId);
            this.ddlCangkuList.DataTextField = "names";
            this.ddlCangkuList.DataValueField = "id";
            this.ddlCangkuList.DataBind();

            this.ddlCangkuList.Items.Insert(0, new ListItem("请选择", "0"));
            

            SpecDAL specDal = new SpecDAL();
            this.ddlUnitList.DataSource = specDal.GetALLModelList();
            this.ddlUnitList.DataTextField = "names";
            this.ddlUnitList.DataValueField = "id";
            this.ddlUnitList.DataBind();

            this.ddlUnitList.Items.Insert(0, new ListItem("请选择", "0"));


            GoodsBrandDAL brandDal = new GoodsBrandDAL();
            this.ddlBrandList.DataSource = brandDal.GetALLModelList();
            this.ddlBrandList.DataTextField = "names";
            this.ddlBrandList.DataValueField = "id";
            this.ddlBrandList.DataBind();
            this.ddlBrandList.Items.Insert(0, new ListItem("请选择", "0"));


          


            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            if (id == 0)//新增
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
                MessageBox.Show(this, "无此操作权限！");
                return;
            }

            string fileName = "";
            string extName = "";
            string imageName = "";


            #region 图片部分

            //if (this.fload.HasFile)
            //{
            //    fileName = this.fload.PostedFile.FileName;
            //    extName = fileName.Substring(fileName.LastIndexOf(".") + 1);

            //    //以时间命名文件
            //    imageName = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + extName;


            //    if (extName != "" && extName.ToLower() != "jpg" && extName.ToLower() != "gif")
            //    {
            //        MessageBox.Show(this,"上传图片格式不正确，请上传JPG，GIF格式！");
            //        return;
            //    }

            //    int size = this.fload.PostedFile.ContentLength / 0x100000;
            //    if (size > 1)
            //    {
            //        MessageBox.Show(this, "上传图片文件过大【2M以内】，请重试！");
            //        return;

            //    }
            //    else
            //    {
            //        string oPath = Server.MapPath("../goodsPic/") + imageName;//源文件路径
            //        string tPath = Server.MapPath("../goodsPicSmall/") + imageName;//略微图路径


            //        //同时生成略微图------上传
            //        ShortImages.ThumbnailsCreate(this.fload.PostedFile.InputStream, 150, 150, oPath, tPath, true);

            //    }

            //}
            //else//如果没有拍照
            //{
            //    if (id > 0)
            //    {
            //        imageName = this.hfImagePath.Value.ToString();
            //    }
            //    else
            //    {
            //        imageName = "noPic.jpg";
            //    }
            //}

            #endregion

            dal.Id = ConvertTo.ConvertInt(this.hf.Value.ToString());
            dal.ShopId = LoginUser.ShopId;
            dal.Code = this.txtCode.Text;
            dal.Barcode = this.txtBarcode.Text;
            dal.Names = this.txtNames.Text;
            dal.TypeId =ConvertTo.ConvertInt(this.ddlVenderTypeList.SelectedValue.ToString());
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
            dal.Flag = "保存";
            dal.MakeDate = DateTime.Now;

            
            dal.FieldA = this.txtFieldA.Text;
            dal.FieldB = this.txtFieldB.Text;
            dal.FieldC = this.txtFieldC.Text;
            dal.FieldD = this.txtFieldD.Text;

            //if (this.cbIsWeight.Checked)
            //{
            //    dal.IsWeight = 1;

            //    if (this.txtBarcode.Text.ToString().Trim().Length != 5)
            //    {
            //        MessageBox.Show(this, "称重商品条码长度必须为5位！");
            //        return;
            //    }

            //}
            //else
            //{
            //    dal.IsWeight = 0;
            //}


            dal.Remarks = "";
            dal.ImagePath = imageName;


            if (id.ToString() == "0")//Add
            {
                if (dal.isExistsAdd(LoginUser.ShopId,this.txtCode.Text,this.txtBarcode.Text,this.txtNames.Text,this.txtSpec.Text))
                {
                    MessageBox.Show(this, "新增失败，存在相同编码、条码、名称、规格的商品！");
                    return;
                }
               

                if (dal.Add() > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "新增商品：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "操作成功！", "GoodsListAdd.aspx?id=" + id.ToString());
                }


            }
            else //Edit
            {
                if (dal.isExistsEdit(ConvertTo.ConvertInt(this.hf.Value.ToString()),LoginUser.ShopId,this.txtCode.Text,this.txtBarcode.Text,this.txtNames.Text,this.txtSpec.Text))
                {
                    MessageBox.Show(this, "修改失败，存在相同编码、条码、名称、规格的商品！");
                    return;
                }
              

                if (dal.Update() > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "修改商品：" + this.txtCode.Text + " 名称：" + this.txtNames.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    MessageBox.ShowAndRedirect(this, "操作成功！", "GoodsListAdd.aspx?id=" + id.ToString());
                }
 
            }



        }



        #region 绑定树形类别

        /// <summary>
        /// 递归绑定
        /// </summary>
        /// <param name="Pading">连接字符</param>
        /// <param name="DirId">数据库的id字段</param>
        /// <param name="datatable">传入的table</param>
        /// <param name="deep">第几级</param>
        /// <param name="list1">传入的dropList控件的名字</param>
        public void DropDownListBoind(string Pading, int DirId, DataTable datatable, int deep, DropDownList list1)
        {
            DataRow[] rowlist = datatable.Select("parentID='" + DirId + "'");//用datarow查询父级id为0的
            foreach (DataRow row in rowlist) //遍历元素
            {
                string strPading = "";
                for (int j = 0; j < deep; j++)
                {
                    strPading += "　";         //用全角的空格
                }
                //添加节点
                ListItem li = new ListItem(strPading + "├ " + row["names"].ToString(), row["id"].ToString());  //将要显示在DropDownList里面的文本和值(name,value)添加到DropDownList里面
                list1.Items.Add(li);
                DropDownListBoind(strPading, Convert.ToInt32(row["id"]), datatable, deep + 1, list1); //递归调用
            }
        }

        public void Bound(DropDownList list1)
        {

            list1.Items.Clear();

            int i = 0;
            DataTable datatable = GetDataTable();
            DataRow[] row = datatable.Select("parentID='0'");
            //添加根目录
            for (int j = 0; j < row.Length; j++)
            {
                i = 1;
                ListItem li = new ListItem(row[j]["names"].ToString(), row[j]["id"].ToString());
                list1.DataTextField = row[j]["names"].ToString();
                list1.Items.Add(li);
                DropDownListBoind("", Convert.ToInt32(row[j]["id"]), datatable, 1, list1);
            }
            ListItem items = new ListItem("(请选择)", "0");
            list1.Items.Insert(0,items);
            list1.SelectedValue = "0";
        }

        ///查询数据库并返回table
        public DataTable GetDataTable()
        {
            GoodsTypeDAL typesDal = new GoodsTypeDAL();

            DataSet ds = typesDal.GetALLModelList();
            DataTable tb = ds.Tables[0];
            return tb;
        }

        #endregion
    }
}
