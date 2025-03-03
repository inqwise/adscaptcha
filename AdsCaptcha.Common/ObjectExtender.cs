using System;
using System.ComponentModel;

namespace Inqwise.AdsCaptcha.Common
{
    public static class ObjectExtender
    {
        public static T? ToNullable<T>(this object obj) where T : struct
        {
            T? result = null;
            try
            {
                if (null != obj)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof (T));
                    result = conv.ConvertFrom(obj) as T?;
                }
            }
            catch
            {
                
            }
            return result;
        }

        public static int? OptInt(this object obj)
        {
            int? result = null;
            
            if (null != obj)
            {
                result = Convert.ToInt32(obj);
            }
            
            return result;
        }
    }
}