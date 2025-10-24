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
            Stream oStream = originalStream;
            Image oImage = Image.FromStream(oStream);

            int oWidth = oImage.Width;
            int oHeight = oImage.Height;

            if (sizeFlag)
            {

                if (oWidth >= oHeight)
                {
                    thumbHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(thumbWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    thumbWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(thumbHeight) / Convert.ToDouble(oHeight)));
                }
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

                using (MemoryStream mem = new MemoryStream())
                {
                    oImage.Save(originalPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
        }

        /// <summary>
        /// Generate thumbnails when uploading pictures
        /// </summary>
        public static void ThumbnailsCreate(Image oImage, int thumbWidth, int thumbHeight, string originalPath, string thumbPath, bool sizeFlag)
        {

            int oWidth = oImage.Width;
            int oHeight = oImage.Height;

            if (sizeFlag)
            {
                if (oWidth >= oHeight)
                {
                    thumbHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(thumbWidth) / Convert.ToDouble(oWidth)));
                }
                else
                {
                    thumbWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(thumbHeight) / Convert.ToDouble(oHeight)));
                }
            }

            Bitmap tImage = new Bitmap(thumbWidth, thumbHeight);

            Graphics g = Graphics.FromImage(tImage);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            g.DrawImage(oImage, new Rectangle(0, 0, thumbWidth, thumbHeight), new Rectangle(0, 0, oWidth, oHeight), GraphicsUnit.Pixel);

            try
            {
                using (MemoryStream mem = new MemoryStream())
                {
                    tImage.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
        }

        /// <summary>
        /// Get image container, if empty create the container
        /// </summary>
        private static async Task<bool> CheckImageContainerExist(string originalPath, string thumbPath)
        {
            // Get the directory part of the file path
            string originalPathFolder = Path.GetDirectoryName(originalPath);
            string thumbPathFolder = Path.GetDirectoryName(thumbPath);

            // Check if the directory exists
            if (!Directory.Exists(originalPathFolder))
            {
                // Create the directory if it doesn't exist
                Directory.CreateDirectory(originalPathFolder);
            }

            if (!Directory.Exists(thumbPathFolder))
            {
                // Create the directory if it doesn't exist
                Directory.CreateDirectory(thumbPathFolder);
            }

            return await Task.FromResult(true);
        }
    }
}