using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Common
{
    public class ShareUrl
    {



        static string Number = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";


        /// <summary> 
        /// 压缩ID标识 
        /// </summary> 
        /// <param name="n"></param> 
        /// <returns></returns> 
        public static string Short(long n)
        {
            string result = string.Empty;
            int l = Number.Length;
            while (n / l >= 1)
            {
                result = Number[(int)(n % l)] + result;
                n /= l;
            }
            result = Number[(int)n] + result;
            return result;
        }
        /// <summary> 
        /// 还原ID标识 
        /// </summary> 
        /// <param name="s"></param> 
        /// <returns></returns> 
        public static long UnShort(string s)
        {
            long result = 0;
            if (s != null)
            {
                s = s.Trim();
                int l = s.Length;
                int m = Number.Length;
                for (int x = 0; x < l; x++)
                {
                    result += Number.IndexOf(s[l - 1 - x]) * (long)Math.Pow(m, x);
                }
            }
            return result;
        }



    
    }
}
