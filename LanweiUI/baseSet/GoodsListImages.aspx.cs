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
    public partial class GoodsListImages : System.Web.UI.Page
    {
        public GoodsDAL dal = new GoodsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {



                this.Bind();
            }
        }
        public void Bind()
        {


            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            DataSet ds = dal.GetGoodsImagesById(id);

            this.DataList1.DataSource = ds;
            this.DataList1.DataBind();

          
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string command = e.CommandName.ToString();

            int Id = ConvertTo.ConvertInt(e.CommandArgument.ToString());

            int goodsId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            if (command == "moren")
            {
                int d = dal.SetMorenImages(Id,goodsId);
            }
          

            if (command == "del")
            {
                int d = dal.DeleteImages(Id);
            }

            this.Bind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string fileName = "";
            string extName = "";
            string imageName = "";

            #region 图片部分

            if (this.fload.HasFile)
            {
                fileName = this.fload.PostedFile.FileName;
                extName = fileName.Substring(fileName.LastIndexOf(".") + 1);

                //以时间命名文件
                imageName = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + extName;


                if (extName != "" && extName.ToLower() != "jpg" && extName.ToLower() != "gif" && extName.ToLower() != "png")
                {
                    MessageBox.Show(this, "上传图片格式不正确，请上传JPG，GIF，PNG格式！");
                    return;
                }

                int size = this.fload.PostedFile.ContentLength / 0x100000;
                if (size > 1)
                {
                    MessageBox.Show(this, "上传图片文件过大【限1M以内】，请重试！");
                    return;

                }
                else
                {
                    string oPath = Server.MapPath("../goodsPic/") + imageName;//源文件路径
                    string tPath = Server.MapPath("../goodsPicSmall/") + imageName;//略微图路径


                    //同时生成略微图------上传
                    ShortImages.ThumbnailsCreate(this.fload.PostedFile.InputStream, 500, 500, oPath, tPath, true);

                }

            }
            else//如果没有拍照
            {
                MessageBox.Show(this, "请选择图片！");
                return;
            }

            #endregion

            int moren = ConvertTo.ConvertInt(this.ddlMoren.SelectedValue.ToString());

            int goodsId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            int add = dal.AddGoodsImages(goodsId, imageName,moren);

            if (add > 0)
            {
               // MessageBox.Show(this, "上传成功！");
                this.Bind();

            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label lbMoren = (Label)e.Item.FindControl("lbMoren");

            Button btnMoren = (Button)e.Item.FindControl("btnMoren");


            if (lbMoren.Text == "1")
            {
                btnMoren.Visible = false;

                lbMoren.Text = "默认显示";
                lbMoren.ForeColor = System.Drawing.Color.Red;


            }
            else
            {
                btnMoren.Visible = true;

                lbMoren.Visible = false;

            }


        }
    }
}
