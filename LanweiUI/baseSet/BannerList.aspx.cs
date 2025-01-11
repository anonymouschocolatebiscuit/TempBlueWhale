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

using Lanwei.Weixin.UI.src;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class BannerList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Image1.ImageUrl = "../weixinQY/images/ad1" + LoginUser.ShopId+".jpg";
                this.Image2.ImageUrl = "../weixinQY/images/ad2" + LoginUser.ShopId + ".jpg";
                this.Image3.ImageUrl = "../weixinQY/images/ad3" + LoginUser.ShopId + ".jpg";

            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            string fullFileName = this.ProductImg.PostedFile.FileName;
            string imageName = string.Empty;
            string type = string.Empty;
            type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1);
            if (RB1.Checked)
            {
                imageName = "ad1" + LoginUser.ShopId + ".jpg";

            }
            if (RB2.Checked)
            {
                imageName = "ad2" + LoginUser.ShopId + ".jpg";

            }
            if (RB3.Checked)
            {
                imageName = "ad3" + LoginUser.ShopId + ".jpg";

            }
           


            if (type.ToLower() == "jpg")
            {

                string ProductImgUrl = "weixinQY/images/" + imageName;

                string oPath = "../weixinQY/images/" + imageName;
                string tPath = "../weixinQY/img/" + imageName;

                //this.ProductImg.SaveAs(Server.MapPath("../" + ProductImgUrl));

                ShortImages.ThumbnailsCreate(this.ProductImg.PostedFile.InputStream, 700, 245, Server.MapPath(oPath), Server.MapPath(tPath), true);

            
                MessageBox.ShowAndRedirect(this, "修改成功","BannerList.aspx");

               



            }
            else
            {
                MessageBox.Show(this,"请选择正确的图片格式");
                return;
            }
        }
    }
}
