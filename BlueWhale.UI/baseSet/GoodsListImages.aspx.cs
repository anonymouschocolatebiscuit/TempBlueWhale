using BlueWhale.Common;
using BlueWhale.DAL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.baseSet
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
                int d = dal.SetDefaultImages(Id, goodsId);
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

            #region Image Part

            if (this.fload.HasFile)
            {
                fileName = this.fload.PostedFile.FileName;
                extName = fileName.Substring(fileName.LastIndexOf(".") + 1);
                imageName = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + extName;

                if (extName != "" && extName.ToLower() != "jpg" && extName.ToLower() != "gif" && extName.ToLower() != "png")
                {
                    MessageBox.Show(this, "Image uploaded wrong format ，Please upload format in JPG，GIF，PNG!");
                    return;
                }

                int size = this.fload.PostedFile.ContentLength / 0x100000;
                if (size > 1)
                {
                    MessageBox.Show(this, "Image file uploaded size too big [Limit in 1M]，Please retry!");
                    return;
                }
                else
                {
                    string oPath = Server.MapPath("../goodsPic/") + imageName;
                    string tPath = Server.MapPath("../goodsPicSmall/") + imageName;

                    ShortImages.ThumbnailsCreate(this.fload.PostedFile.InputStream, 500, 500, oPath, tPath, true);
                }
            }
            else
            {
                MessageBox.Show(this, "Please select an image");
                return;
            }

            #endregion

            int isDefault = ConvertTo.ConvertInt(this.ddlDefault.SelectedValue.ToString());

            int goodsId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            int add = dal.AddGoodsImages(goodsId, imageName, default);

            if (add > 0)
            {
                // MessageBox.Show(this, "Uploaded success!");
                this.Bind();
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label lbDefault = (Label)e.Item.FindControl("lbDefault");

            Button btnDefault = (Button)e.Item.FindControl("btnDefault");

            if (lbDefault.Text == "1")
            {
                btnDefault.Visible = false;
                lbDefault.Text = "View as default";
                lbDefault.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                btnDefault.Visible = true;
                lbDefault.Visible = false;
            }
        }
    }
}