using Ganss.Xss;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FNT_CrossCutting
{
    public static class Util
    {
       
        public static string Serialize(this object obj)
        => JsonSerializer.Serialize(obj);

        public static T Deserialize<T>(this string value)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,

            };

            return JsonSerializer.Deserialize<T>(value, options);
        }           

        public static Int64 ObjectToInt64(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt64(obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static Int16 ObjectToInt16(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? Int16.MinValue : Convert.ToInt16(obj);
        }
        public static int ObjectToInt32(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static char ObjectToChar(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? ' ' : Convert.ToChar(obj);
            }
            catch (Exception ex)
            {
                return ' ';
            }
        }
        public static decimal ObjectToDecimal(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? 0.00M : Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static int ObjectToInt(object obj)
        {
            return ObjectToInt32(obj);
        }
        public static double ObjectToDouble(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? 0 : Convert.ToDouble(obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static string ObjectToString(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? string.Empty : Convert.ToString(obj);

            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public static string ObjectToStringTrim(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? string.Empty : Convert.ToString(obj).Trim();

            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public static DateTime ObjectToDateTime(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? DateTime.MinValue : Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

        }
        //public static DateTime ObjectToDateTimeSistema(object obj)
        //{
        //    return ((obj == null) || (obj == DBNull.Value)) ? ParametroDateUtil.DateTimeMinValueSQL : Convert.ToDateTime(obj);
        //}


        public static DateTime? ObjectToDateTimeNull(object obj)
        {
            if (obj == null || obj == DBNull.Value) return null; else return Convert.ToDateTime(obj);
        }
        public static Decimal? ObjectToDecimalNull(object obj)
        {
            if (obj == null || obj == DBNull.Value) return null; else return Convert.ToDecimal(obj);
        }
        public static Int32? ObjectToInt32Null(object obj)
        {
            if (obj == null || obj == DBNull.Value) return null; else return Convert.ToInt32(obj);
        }
        public static string ObjectToDateTimeStringNull(object obj)
        {
            if (obj == null || obj == DBNull.Value) return ""; else return Convert.ToDateTime(obj).ToString("yyyyMMdd");
        }
        public static DateTime StringToDateTime(string str)
        {
            return (string.IsNullOrEmpty(str)) ? DateTime.MinValue : Convert.ToDateTime(str);
        }
        public static byte ObjectToByte(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? byte.MinValue : Convert.ToByte(obj);
            }
            catch (Exception ex)
            {
                return byte.MinValue;
            }
        }
        public static byte[] ObjectToByteArray(object obj)
        {
            try
            {
                return ((obj == null) || (obj == DBNull.Value)) ? null : (byte[])obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int StringToInt(string str)
        {
            return (string.IsNullOrEmpty(str)) ? 0 : Convert.ToInt32(str);
        }
        public static string IntToString(int int1)
        {
            return ((int1 == 0)) ? "" : Convert.ToString(int1);
        }
        public static string ObjectDecimalToStringFormatMiles(object obj)
        {
            return ObjectToDecimal(obj).ToString("#,#0.00");
        }
        public static string BoolToString(bool flag)
        {
            return flag ? "1" : "0";
        }
        public static bool StringToBool(string flag)
        {
            return flag == "1";
        }
        public static string ObjectNullToString(object obj)
        {
            return (string.IsNullOrEmpty(ObjectToString(obj))) ? "1" : obj.ToString();
        }
        public static string ObjectToStringTime(object obj)
        {
            return ((obj == null) || (obj == DBNull.Value)) ? "" : Convert.ToString(obj);
        }
        public static string ObjectToDTStringNull(object obj)
        {
            if (obj == null || obj == DBNull.Value) return null; else return Convert.ToDateTime(obj).ToString("hh:MM:ss");
        }

        public static string ObjectToDTString(object obj)
        {
            if (obj == null || obj == DBNull.Value) return ""; else return Convert.ToDateTime(obj).ToString("hh:MM:ss");
        }

        public static string ObjectToDTimeString(object obj)
        {
            if (obj == null || obj == DBNull.Value) return ""; else return Convert.ToDateTime(obj).ToLongTimeString().Substring(0, 8);
        }

        public static string ObjectToDateTimeStringNullGeneral(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";
            else
            {
                if (Convert.ToDateTime(obj).ToShortDateString().Substring(0, 10) == "01/01/1900")
                    return "";
                else
                    return Convert.ToDateTime(obj).ToString("yyyyMMdd");
            }
        }
        #region TryParse
        public static DateTime StringObjectToDateTime(string obj)
        {
            DateTime Temp;
            if (obj == null)
                Temp = DateTime.MinValue;
            else
                try { Temp = Convert.ToDateTime(obj); }
                catch { Temp = DateTime.MinValue; }
            return Temp;
        }
        public static bool ObjectToBoolean(object obj)
        {
            bool Temp;
            if (obj == null)
                Temp = false;
            else
                try { Temp = Convert.ToBoolean(obj); }
                catch { Temp = false; }
            return Temp;
        }
        public static DateTime ObjectToDateTimeTryParse(object obj)
        {
            DateTime Temp;
            if (obj == null)
                Temp = DateTime.MinValue;
            else
                try { Temp = Convert.ToDateTime(obj); }
                catch { Temp = DateTime.MinValue; }
            return Temp;
        }
        //public static DateTime? ObjectToDateTimeSQLTryParse(object obj)
        //{
        //    DateTime? Temp;
        //    if (obj == null)
        //        Temp = null;
        //    else
        //        try
        //        {
        //            Temp = Convert.ToDateTime(obj);
        //            if (ParametroDateUtil.DateTimeMinValueSQL >= Temp && Temp >= ParametroDateUtil.DateTimeMaxValueSQL)
        //            {
        //            }
        //            else
        //            {
        //                Temp = null;
        //            }
        //        }
        //        catch { Temp = null; }
        //    return Temp;
        //}
        public static int ObjectToIntTryParse(object obj)
        {
            int Temp;
            if (obj == null)
                Temp = int.MinValue;
            else
                try { Temp = Convert.ToInt32(obj); }
                catch { Temp = int.MinValue; }
            return Temp;
        }
        public static Int64 ObjectToInt64TryParse(object obj)
        {
            Int64 Temp;
            if (obj == null)
                Temp = 0;
            else
                try { Temp = Convert.ToInt64(obj); }
                catch { Temp = 0; }
            return Temp;
        }
        public static decimal ObjectToDecimalTryParse(object obj)
        {
            decimal Temp;
            if (obj == null)
                Temp = decimal.MinValue;
            else
                try { Temp = Convert.ToDecimal(obj); }
                catch { Temp = decimal.MinValue; }
            return Temp;
        }
        public static string ObjectToStringTryParse(object obj)
        {
            string Temp;
            if (obj == null)
                Temp = string.Empty;
            else
                try { Temp = obj.ToString(); }
                catch { Temp = string.Empty; }
            return Temp;
        }
        //public static Nullable<DateTime> DateTimeToDateTimeSQL(DateTime obj)
        //{
        //    if (ParametroDateUtil.DateTimeMinValueSQL <= obj && obj <= ParametroDateUtil.DateTimeMaxValueSQL)
        //    {
        //        return obj;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        public static DateTime DateMorTime(DateTime obj, DateTime oTime)
        {
            try
            {
                TimeSpan oNewTime = new TimeSpan();
                if (TimeSpan.TryParse(oTime.ToShortTimeString(), out oNewTime))
                {
                    obj = (Convert.ToDateTime(obj.ToShortDateString())).Add(oNewTime);
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }
        #endregion TryParse

        public static string GetString8FromDateTime(DateTime dt)
        {
            return string.Concat(dt.Year, dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
        }


        public static string StringReturnNotNull(string Value)
        {
            return string.IsNullOrEmpty(Value) ? "" : Value;
        }
        public static string StringReturnNotCero(string Value)
        {
            return (Value != null && Value != "0") ? Value : "";
        }

        public static string[] StringArrayFromSplit(string Value, string split)
        {
            if (string.IsNullOrEmpty(Value) || Value.Trim() == "") return [];
            return Value.Split(split).Where(sp => sp != null && sp.Trim() != "").ToArray();
        }

        public static string GetStringNameMonthSpanish(int month)
        {
            var culture = new CultureInfo("es-PE");
            var dictionary = new Dictionary<int, string>();
            var i = 1;
            foreach (string name in culture.DateTimeFormat.MonthNames)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    dictionary.Add(i, culture.TextInfo.ToTitleCase(name));
                }
                i++;
            }
            return dictionary.First(x => x.Key == month).Value;
        }
        public static string GetStringDateHourWithNameMonth(DateTime date)
        {
            return $"{date.Day} {GetStringNameMonthSpanish(date.Month)} {date.Year} {date.ToString("HH:mm")}";
        }
        
        public static string ConvertProgressDecimalToStringTwoDecimal(decimal value)
        {
            return $"{((value > 0 && value < 100) ? string.Format("{0:0.0#}", value) : value.ToString())}%";
        }

        private static readonly
            ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum? value)
        {
            if (value == null) return "";
            var key = $"{value?.GetType().FullName}.{value}";

            var displayName = DisplayNameCache.GetOrAdd(key, x =>
            {
                var name = (DescriptionAttribute[])value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                return name.Length > 0 ? name[0].Description : value.ToString();
            });

            return displayName;
        }

        public static bool IsContentHtmlOrCharactersSpecials(string? inputText)
        {
            Regex regex = new Regex(@"[<>[\]{}()'""`´¡!¿?@#$%^&*=|;:\/\\~_+]");
            return regex.IsMatch(inputText ?? "");
        }
    }

    public class StringUpperComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            return x.ToUpper() == y.ToUpper();
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
