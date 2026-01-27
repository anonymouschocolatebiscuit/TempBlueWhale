using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace BlueWhale.Common
{
    public class ShortImages
    {
        /// <summary>
        /// Generate thumbnails when uploading pictures
        /// </summary>
        public static async Task ThumbnailsCreate(Stream originalStream, int thumbWidth, int thumbHeight, string originalPath,
            string thumbPath, bool sizeFlag)
        {
            Image oImage = Image.FromStream(originalStream);

            int oWidth = oImage.Width;
            int oHeight = oImage.Height;

            if (sizeFlag)
            {
                if (oWidth >= oHeight)
                    thumbHeight = (int)Math.Floor(oHeight * (thumbWidth / (double)oWidth));
                else
                    thumbWidth = (int)Math.Floor(oWidth * (thumbHeight / (double)oHeight));
            }

            Bitmap tImage = new Bitmap(thumbWidth, thumbHeight);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(oImage, new Rectangle(0, 0, thumbWidth, thumbHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);

            try
            {
                await CheckImageContainerExist(originalPath, thumbPath);

                oImage.Save(originalPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage?.Dispose();
                g?.Dispose();
                tImage?.Dispose();
            }
        }

        /// <summary>
        /// Generate thumbnails when uploading pictures
        /// </summary>
        public static void ThumbnailsCreate(Image oImage, int thumbWidth, int thumbHeight, string thumbPath, bool sizeFlag)
        {
            int oWidth = oImage.Width;
            int oHeight = oImage.Height;

            if (sizeFlag)
            {
                if (oWidth >= oHeight)
                    thumbHeight = (int)Math.Floor(oHeight * (thumbWidth / (double)oWidth));
                else
                    thumbWidth = (int)Math.Floor(oWidth * (thumbHeight / (double)oHeight));
            }

            Bitmap tImage = new Bitmap(thumbWidth, thumbHeight);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(oImage, new Rectangle(0, 0, thumbWidth, thumbHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);

            try
            {
                tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage?.Dispose();
                g?.Dispose();
                tImage?.Dispose();
            }
        }

        /// <summary>
        /// Get image container, if empty create the container
        /// </summary>
        private static async Task<bool> CheckImageContainerExist(string originalPath, string thumbPath)
        {
            string originalPathFolder = Path.GetDirectoryName(originalPath);
            string thumbPathFolder = Path.GetDirectoryName(thumbPath);

            if (!Directory.Exists(originalPathFolder))
                Directory.CreateDirectory(originalPathFolder);

            if (!Directory.Exists(thumbPathFolder))
                Directory.CreateDirectory(thumbPathFolder);

            return await Task.FromResult(true);
        }
    }
}
