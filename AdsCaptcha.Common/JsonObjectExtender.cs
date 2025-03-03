using System;
using System.Collections.Generic;
using System.Linq;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common
{
    public static class JsonObjectExtender
    {
        public static int? OptInt(this JsonObject o, string key, int? defaultValue = null)
        {
            var obj = Opt(o, key, defaultValue);
            return null == obj ? defaultValue : Convert.ToInt32(obj) as int?;
        }

        public static object Opt(this JsonObject o, string key, object defaultValue)
        {
            if (o.Contains(key))
            {
                return o[key];
            }

            return defaultValue;
        }

        public static string OptString(this JsonObject o, string key, string defaultValue = null)
        {
            var obj = Opt(o, key, defaultValue);
            return null == obj ? defaultValue : obj.ToString();
        }

        public static bool? OptBool(this JsonObject o, string key, bool? defaultValue = null)
        {
            var obj = Opt(o, key, defaultValue);
            return null == obj ? defaultValue : Convert.ToBoolean(obj);
        }

        public static DateTime? OptDate(this JsonObject o, string key, DateTime? defaultValue = null)
        {
            var obj = Opt(o, key, defaultValue);
            if (null == obj)
            {
                return defaultValue;
            }
            else
            {
                DateTime dt;
                if (!DateTime.TryParse(obj.ToString(), out dt) &&
                    !DateTime.TryParseExact(obj.ToString(), "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    dt = DateTime.ParseExact(obj.ToString(), "yyyy-MM-dd", null);
                }

                return dt;
            }
        }

        public static object Get(this JsonObject o, string key)
        {
            if (!o.Contains(key))
            {
                throw new IndexOutOfRangeException(string.Format("Key '{0}' not found", key));
            }
            return o[key];
        }

        public static IEnumerable<long> GetMenyLong(this JsonObject o, string key)
        {
            var arr = (JsonArray)Get(o, key);
            return arr.Select(Convert.ToInt64);
        }

        public static int GetInt(this JsonObject o, string key)
        {
            return Convert.ToInt32(Get(o, key));
        }

        public static IEnumerable<int> GetMenyInt(this JsonObject o, string key)
        {
            var arr = (JsonArray)Get(o, key);
            return arr.Select(Convert.ToInt32);
        }

        public static string GetString(this JsonObject o, string key)
        {
            return Convert.ToString(Get(o, key));
        }

        public static long GetLong(this JsonObject o, string key)
        {
            return Convert.ToInt64(Get(o, key));
        }

        public static bool GetBool(this JsonObject o, string key)
        {
            return Convert.ToBoolean(Get(o, key));
        }

        public static double GetDouble(this JsonObject o, string key)
        {
            return Convert.ToDouble(Get(o, key));
        }

        public static decimal GetDecimal(this JsonObject o, string key)
        {
            return Convert.ToDecimal(Get(o, key));
        }

        public static void PutDate(this JsonObject o, string name, DateTime? date)
        {
            if (null == date)
            {
                o.Put(name, null);
            }
            else
            {
                o.Put(name, date.Value.ToString("yyyy-MM-dd HH:mm"));
            }
        }

        public static JsonObject GetJObject(this JsonObject o, string key)
        {
            return (JsonObject)Get(o, key);
        }

        public static JsonArray GetJArray(this JsonObject o, string key)
        {
            return (JsonArray)Get(o, key);
        }
    }
}