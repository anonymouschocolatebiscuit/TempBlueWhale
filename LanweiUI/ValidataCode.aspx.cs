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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Lanwei.Weixin.UI
{
    public partial class ValidataCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string tmp = RndNum(Convert.ToInt16(6));
                Session["verify"] = tmp;
                ValidateCode(tmp);
            }

        }
        private void ValidateCode(string VNum)
        {
            Bitmap Img = null;
            Graphics g = null;
            MemoryStream ms = null;
            int gheight = VNum.Length * 9;
            Img = new Bitmap(gheight, 18);
            g = Graphics.FromImage(Img);
            //背景颜色
            g.Clear(Color.WhiteSmoke);
            //文字字体
            Font f = new Font("Tahoma", 9);
            //文字颜色
            SolidBrush s = new SolidBrush(Color.Red);
            g.DrawString(VNum, f, s, 3, 3);
            ms = new MemoryStream();
            Img.Save(ms, ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            Img.Dispose();
            Response.End();
        }
        private string RndNum(int VcodeNum)
        {
            string MaxNum = "";
            string MinNum = "";
            for (int i = 0; i < 5; i++)//这里的4是验证码的位数
            {
                MaxNum = MaxNum + "5";
            }
            MinNum = MaxNum.Remove(0, 1);
            Random rd = new Random();
            string VNum = Convert.ToString(rd.Next(Convert.ToInt32(MinNum), Convert.ToInt32(MaxNum)));
            return VNum;
        }
    }
}
