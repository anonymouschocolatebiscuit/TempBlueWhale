using System;
using BlueWhale.Common;
using BlueWhale.UI.src;

namespace BlueWhale.UI.baseSet
{
    public partial class LogoSet : BasePage
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Image1.ImageUrl = "../sales/img/" + LoginUser.ShopId.ToString() + "logo.jpg";
                Image2.ImageUrl = "../sales/img/" + LoginUser.ShopId.ToString() + "zhang.jpg";
            }
        }

        protected async void ButtonUpload_Click(object sender, EventArgs e)
        {

            string fullFileName = ProductImg.PostedFile.FileName;
            string imageName = string.Empty;
            string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1);

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
                    await ShortImages.ThumbnailsCreate(ProductImg.PostedFile.InputStream, 200, 120, Server.MapPath(oPath), Server.MapPath(tPath), false);
                }

                if (RB2.Checked)
                {
                    await ShortImages.ThumbnailsCreate(ProductImg.PostedFile.InputStream, 200, 180, Server.MapPath(oPath), Server.MapPath(tPath), false);
                }

                MessageBox.ShowAndRedirect(this, "successful", "LogoSet.aspx");
            }
            else
            {
                MessageBox.Show(this, "Please select the correct image format");
                return;
            }
        }
    }
}
