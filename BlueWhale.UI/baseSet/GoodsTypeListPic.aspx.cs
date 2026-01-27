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


            #region Picture region

            if (this.fload.HasFile)
            {
                fileName = this.fload.PostedFile.FileName;
                extName = fileName.Substring(fileName.LastIndexOf(".") + 1);

                //naming file with time
                imageName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + extName;


                if (extName != "" && extName.ToLower() != "jpg" && extName.ToLower() != "png")
                {
                    MessageBox.Show(this, "Incorrect image format. Please upload JPG or PNG format!");
                    return;
                }

                int size = this.fload.PostedFile.ContentLength / 0x100000;
                if (size > 1)
                {
                    MessageBox.Show(this, "Image file is too large [must be under 1MB], please try again!");
                    return;

                }
                else
                {

                    string oPath = Server.MapPath("../upload/") + imageName;
                   

                    this.fload.PostedFile.SaveAs(oPath);

                    //ShortImages.ThumbnailsCreate(this.fload.PostedFile.InputStream, 150, 150, oPath, tPath, true);

                }

            }
            else
            {
                imageName = this.hfImagePath.Value.ToString();
            }

            #endregion


            string picUrl = "/upload/" + imageName;

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
                logs.Events = "Updated product category image: " + this.txtNames.Text;
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.Show(this, "Operation successful!");
            }


        }
    }
}
