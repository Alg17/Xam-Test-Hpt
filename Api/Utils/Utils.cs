using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDotolWebApi.Libraries.Util
{
    public static class Utils
    {
        public static int Toint(this object str)
        {
            if (!string.IsNullOrEmpty(str.ToString()))
                return Convert.ToInt32(str);
            return 0;
        }
        public static Int64 Toint64(this object str)
        {
            if (!string.IsNullOrEmpty(str.ToString()))
                return Convert.ToInt64(str);
            return 0;
        }

        public static TimeSpan ToTimeSpan(this object str)
        {
            TimeSpan.TryParse(str.ToString(), out TimeSpan valor);
            return valor;
        }

        public static string ToSql(this string str)
        {
            if (str != null)
                return "'" + str.Replace("'", "") + "'";
            else
                return "''";
        }
        public static char ToChar(this object str)
        {
            char.TryParse(str.ToString(), out char valor);
            return valor; ;
        }
        public static byte ToByte(this object str)
        {
            if (str.ToBool())
                str = 1;
            else if (str.ToBool())
                str = 0;
            byte.TryParse(str.ToString(), out byte valor);
            return valor;
        }
        public static sbyte ToSByte(this object str)
        {
            sbyte.TryParse(str.ToString(), out sbyte valor);
            return valor;
        }
        public static float Tofloat(this object str)
        {
            float.TryParse(str.ToString(), out float valor);
            return valor;
        }
        public static DateTime ToDatetime(this object str)
        {
            DateTime.TryParse(str.ToString(), out DateTime valor);
            return valor;
        }
        public static decimal ToDecimal(this object str)
        {
            decimal.TryParse(str.ToString(), out decimal valor);
            return valor;
        }
        public static bool ToBool(this object str)
        {
            bool.TryParse(str.ToString(), out bool valor);
            return valor;
        }

        public static string[] sample(this object arr)
        {
            string[] res = arr as string[];
            return res;

        }

        public static string ToSqlDate(this DateTime str)
        {
            return "'" + str.Date.Year + "" + (str.Date.Month.ToString().Length == 1 ? "0" : "") + str.Date.Month + "" + (str.Date.Day.ToString().Length == 1 ? "0" : "") + str.Date.Day + "'";
        }
        public static Double TODouble(this object str)
        {
            double.TryParse(str.ToString(), out double valor);
            return valor;
        }
    }
}