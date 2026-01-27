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

		//private void ValidateCode(string VNum)
		//{
		//    int gheight = VNum.Length * 9;
		//    Bitmap Img = new Bitmap(gheight, 18);
		//    Graphics g = Graphics.FromImage(Img);
		//    // Background Color
		//    g.Clear(Color.WhiteSmoke);
		//    // Text Font Style
		//    Font f = new Font("Tahoma", 9);
		//    // Text Color
		//    SolidBrush s = new SolidBrush(Color.Red);
		//    g.DrawString(VNum, f, s, 3, 3);
		//    MemoryStream ms = new MemoryStream();
		//    Img.Save(ms, ImageFormat.Jpeg);
		//    Response.ClearContent();
		//    Response.ContentType = "image/Jpeg";
		//    Response.BinaryWrite(ms.ToArray());
		//    g.Dispose();
		//    Img.Dispose();
		//    Response.End();
		//}
		private void ValidateCode(string VNum)
		{
			int width = VNum.Length * 18;
			int height = 40;

			Bitmap Img = new Bitmap(width, height);
			Graphics g = Graphics.FromImage(Img);

			// Background Color - Dark Tech Blue
			g.Clear(Color.FromArgb(30, 60, 90)); // You can adjust RGB here

			// Anti-Aliasing for smooth text
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// Text Font Style
			Font f = new Font("Segoe UI", 18, FontStyle.Bold);

			// Text Color - Aqua/White
			SolidBrush s = new SolidBrush(Color.FromArgb(180, 220, 255));

			// Draw Captcha Text
			g.DrawString(VNum, f, s, 5, 5);

			// Optional: Add Noise / Lines (Security)
			Pen linePen = new Pen(Color.FromArgb(100, 150, 200), 1);
			g.DrawLine(linePen, 0, new Random().Next(height), width, new Random().Next(height));

			// Output Image
			MemoryStream ms = new MemoryStream();
			Img.Save(ms, ImageFormat.Png);
			Response.ClearContent();
			Response.ContentType = "image/png";
			Response.BinaryWrite(ms.ToArray());

			// Cleanup
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
