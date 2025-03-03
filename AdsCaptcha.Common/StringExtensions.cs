using System;
using System.Collections.Generic;
using System.Text;

namespace Inqwise.AdsCaptcha.Common
{
    public static class StringExtensions
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string UnCamelCase(this string s, string separator = " ", bool lowerCase = true)
        {
            var output = new StringBuilder();
            foreach (char letter in s)
            {
                if (Char.IsUpper(letter) && output.Length > 0)
                {
                    output.Append(separator);
                }
                
                output.Append(letter);
            }

            return lowerCase ? output.ToString().ToLower() : output.ToString();
        }
    }
}