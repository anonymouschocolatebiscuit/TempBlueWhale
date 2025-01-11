using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Common
{
    public class ConvertTo
    {
        public static int ConvertInt(string orinString)
        {
            int ret = 0;
            if (!string.IsNullOrEmpty(orinString.Trim()))
            {
                int.TryParse(orinString, out ret);
            }
            return ret;
        }
        public static decimal ConvertDec(string orinString)
        {
            decimal ret = 0;
            if (!string.IsNullOrEmpty(orinString.Trim()))
            {
                decimal.TryParse(orinString, out ret);
            }
            return ret;
        }

        public static double ConvertDou(string orinString)
        {
            double ret = 0;
            if (!string.IsNullOrEmpty(orinString.Trim()))
            {
                double.TryParse(orinString, out ret);
            }
            return ret;
        }


        public static float ConvertFloat(string orinString)
        {
            float ret = 0;
            if (!string.IsNullOrEmpty(orinString.Trim()))
            {
                float.TryParse(orinString, out ret);
            }
            return ret;
        }


    }
}
