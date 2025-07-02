using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BlueWhale.UI
{
    public partial class Captcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tmp = GenerateCaptchaCode(6);
                Session["verify"] = tmp;
                ValidateCode(tmp);
            }
        }

        private void ValidateCode(string VNum)
        {
            int gheight = VNum.Length * 9;
            Bitmap Img = new Bitmap(gheight, 18);
            Graphics g = Graphics.FromImage(Img);
            // Background Color
            g.Clear(Color.WhiteSmoke);
            // Text Font Style
            Font f = new Font("Tahoma", 9);
            // Text Color
            SolidBrush s = new SolidBrush(Color.Red);
            g.DrawString(VNum, f, s, 3, 3);
            MemoryStream ms = new MemoryStream();
            Img.Save(ms, ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            Img.Dispose();
            Response.End();
        }

        private string GenerateCaptchaCode(int length)
        {
            int minValue = (int)Math.Pow(10, length - 1);  // e.g., 100000
            int maxValue = (int)Math.Pow(10, length) - 1;  // e.g., 999999

            Random rd = new Random();
            int number = rd.Next(minValue, maxValue + 1);

            return number.ToString();
        }
    }
}
