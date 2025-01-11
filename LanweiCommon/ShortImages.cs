using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Lanwei.Weixin.Common
{
    public class ShortImages
    {


        /// <summary>
        /// 上传图片同时生成缩略图
        /// </summary>
        /// <param name="imgStream">图像流</param>
        /// <param name="thumbWidth">缩略图的宽度</param>
        /// <param name="thumbHeight">缩略图的高度</param>
        /// <param name="originalPath">原图的保存路径</param>
        /// <param name="thumbPath">缩略图的保存路径</param>
        /// <param name="sizeFlag">按比例缩放与否默认为False</param>
        public static void ThumbnailsCreate(System.IO.Stream originalStream, int thumbWidth, int thumbHeight, string originalPath, 
            string thumbPath, bool sizeFlag)
        {
            System.IO.Stream oStream = originalStream; //读取图像数据流
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);//以读取的数据流来创建Image对象

            int oWidth = oImage.Width; //原图的宽度
            int oHeight = oImage.Height; //原图的高度

            //按比例计算出缩略图的宽度和高度 
            if (sizeFlag)
            {
                //				thumbWidth = 120; //设置缩略图初始宽度 
                //				thumbHeight = 120; //设置缩略图初始高度

                if (oWidth >= oHeight)
                {
                    thumbHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(thumbWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    thumbWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(thumbHeight) / Convert.ToDouble(oHeight)));
                }
            }
            Bitmap tImage = new Bitmap(thumbWidth, thumbHeight); //以缩略图的宽高创建位图对象
            Graphics g = Graphics.FromImage(tImage); //创建Graphics对象，用于缩略图的图像的绘制
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //获取或设置与此 Graphics 对象关联的插补模式。指定高质量插值法。
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//指定高质量、低速度呈现。
            g.Clear(Color.Transparent); //清除整个绘图面并以透明色填充。
            g.DrawImage(oImage, new Rectangle(0, 0, thumbWidth, thumbHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);//缩略图绘制 

            try
            {
                //以JPG格式保存图片 
                using (MemoryStream mem = new MemoryStream())
                {
                    oImage.Save(originalPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    oImage.Dispose(); //释放由此 Image 对象使用的所有资源。
                    g.Dispose(); //释放由此 Graphics 对象使用的所有资源。
                    tImage.Dispose();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage.Dispose(); //释放由此 Image 对象使用的所有资源。
                g.Dispose(); //释放由此 Graphics 对象使用的所有资源。
                tImage.Dispose(); //释放由此 Image 对象使用的所有资源。
            }
        }

        /// <summary>
        /// 上传图片同时生成缩略图
        /// </summary>
        /// <param name="imgStream">图像流</param>
        /// <param name="thumbWidth">缩略图的宽度</param>
        /// <param name="thumbHeight">缩略图的高度</param>
        /// <param name="originalPath">原图的保存路径</param>
        /// <param name="thumbPath">缩略图的保存路径</param>
        /// <param name="sizeFlag">按比例缩放与否默认为False</param>
        public static void ThumbnailsCreate(Image oImage, int thumbWidth, int thumbHeight, string originalPath, string thumbPath, bool sizeFlag)
        {
            
            int oWidth = oImage.Width; //原图的宽度
            int oHeight = oImage.Height; //原图的高度

            //按比例计算出缩略图的宽度和高度 
            if (sizeFlag)
            {
                //				thumbWidth = 120; //设置缩略图初始宽度 
                //				thumbHeight = 120; //设置缩略图初始高度

                if (oWidth >= oHeight)
                {
                    thumbHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(thumbWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    thumbWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(thumbHeight) / Convert.ToDouble(oHeight)));
                }
            }

            Bitmap tImage = new Bitmap(thumbWidth, thumbHeight); //以缩略图的宽高创建位图对象

            Graphics g = Graphics.FromImage(tImage); //创建Graphics对象，用于缩略图的图像的绘制
           
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //获取或设置与此 Graphics 对象关联的插补模式。指定高质量插值法。
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//指定高质量、低速度呈现。
            g.Clear(Color.Transparent); //清除整个绘图面并以透明色填充。
            g.DrawImage(oImage, new Rectangle(0, 0, thumbWidth, thumbHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);//缩略图绘制 

            try
            {

                //以JPG格式保存图片 
                using (MemoryStream mem = new MemoryStream())
                {
                    // oImage.Save(originalPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    oImage.Dispose(); //释放由此 Image 对象使用的所有资源。
                    g.Dispose(); //释放由此 Graphics 对象使用的所有资源。
                    tImage.Dispose();

                }
                


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage.Dispose(); //释放由此 Image 对象使用的所有资源。
                g.Dispose(); //释放由此 Graphics 对象使用的所有资源。
                tImage.Dispose(); //释放由此 Image 对象使用的所有资源。
            }
        }
   
    
    
    
    }
}
