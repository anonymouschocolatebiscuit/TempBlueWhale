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
    public partial class GoodsTypeListPic : BasePage
    {
        public GoodsTypeDAL dal = new GoodsTypeDAL();

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

                this.hfId.Value = Request.QueryString["id"].ToString();
                this.hfImagePath.Value = Request.QueryString["picUrl"].ToString();
                this.txtNames.Text = Request.QueryString["names"].ToString();
               



            }
           
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
                imageName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + extName;


                if (extName != "" && extName.ToLower() != "jpg" && extName.ToLower() != "png")
                {
                    MessageBox.Show(this, "上传图片格式不正确，请上传JPG，PNG格式！");
                    return;
                }

                int size = this.fload.PostedFile.ContentLength / 0x100000;
                if (size > 1)
                {
                    MessageBox.Show(this, "上传图片文件过大【1M以内】，请重试！");
                    return;

                }
                else
                {

                    string oPath = Server.MapPath("../upload/") + imageName;//源文件路径
                   

                    this.fload.PostedFile.SaveAs(oPath);

                    //同时生成略微图------上传
                    //ShortImages.ThumbnailsCreate(this.fload.PostedFile.InputStream, 150, 150, oPath, tPath, true);

                }

            }
            else//如果没有拍照
            {
                imageName = this.hfImagePath.Value.ToString();
            }

            #endregion


            string picUrl = "/upload/" + imageName;//源文件路径

            int typeId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            int isShowXCX = 0;
            if (rbxcx1.Checked == true)
            {
                isShowXCX = 1;

            }
            int isShowGZH = 0;
            if (rbgzh1.Checked == true)
            {
                isShowGZH = 1;
            }



            if (dal.UpdatePic(typeId,isShowXCX,isShowGZH,picUrl) > 0)
            {

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "修改商品类别图片：" + this.txtNames.Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.Show(this, "操作成功！");
            }


        }
    }
}
