using System.Text.RegularExpressions;

namespace Inqwise.AdsCaptcha.Common
{
    public static class StringExtender
    {
        public static string StripNonAsciiChars(this string s)
        {
            if (null == s) return null;
            return Regex.Replace(s, @"[^\u0000-\u007F]", string.Empty);
        }
    }
}