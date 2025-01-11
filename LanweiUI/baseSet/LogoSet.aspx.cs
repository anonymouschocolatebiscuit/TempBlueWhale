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

using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class LogoSet : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Image1.ImageUrl = "../sales/img/" + LoginUser.ShopId.ToString() + "logo.jpg";
                this.Image2.ImageUrl = "../sales/img/" + LoginUser.ShopId.ToString() + "zhang.jpg";
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
                imageName = LoginUser.ShopId.ToString() + "logo.jpg";

            }
            if (RB2.Checked)
            {
                imageName = LoginUser.ShopId.ToString() + "zhang.jpg";

            }
          
          


            if (type.ToLower() == "jpg")
            {

                string ProductImgUrl = "sales/pdf/" + imageName;

                string oPath = "../sales/pdf/" + imageName;
                string tPath = "../sales/img/" + imageName;

            

                if (RB1.Checked)
                {

                    ShortImages.ThumbnailsCreate(this.ProductImg.PostedFile.InputStream, 200, 120, Server.MapPath(oPath), Server.MapPath(tPath), false);
               
                }

                if (RB2.Checked)
                {

                    ShortImages.ThumbnailsCreate(this.ProductImg.PostedFile.InputStream, 200, 180, Server.MapPath(oPath), Server.MapPath(tPath), false);
                }

            
                MessageBox.ShowAndRedirect(this, "修改成功","LogoSet.aspx");

               



            }
            else
            {
                MessageBox.Show(this,"请选择正确的图片格式");
                return;
            }
        }
    }
}
