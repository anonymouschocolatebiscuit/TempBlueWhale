using System;
using System.IO;
using System.Text;
using System.Web;

namespace BlueWhale.Common
{
    public class LogUtil
    {
        private static readonly object writeFile = new object();

        /// <summary>
        /// Create local log
        /// </summary>
        /// <param name="exception"></param>
        public static void WriteLog(string debugstr)
        {
            lock (writeFile)
            {
                FileStream fs = null;
                StreamWriter sw = null;

                try
                {
                    string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    // Server log directory
                    string folder = HttpContext.Current.Server.MapPath("~/Log");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    fs = new FileStream(Path.Combine(folder, filename), FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}     {debugstr}\r\n");
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Dispose();
                        sw = null;
                    }
                    if (fs != null)
                    {
                        fs.Dispose();
                        fs = null;
                    }
                }
            }
        }
    }
}
