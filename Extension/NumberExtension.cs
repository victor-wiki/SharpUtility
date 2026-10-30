using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtility.Extension
{
    public static class NumberExtension
    {
        public static string ToFixed(this double value, int number)
        {
            return value.ToString(GetStringFormat(number));
        }

        public static string ToFixed(this float value, int number)
        {
            return value.ToString(GetStringFormat(number));
        }

        public static string ToFixed(this decimal value, int number)
        {
            return value.ToString(GetStringFormat(number));
        }

        private static string GetStringFormat(int number)
        {
            return "0." + string.Join("", Enumerable.Repeat("0", number));
        }
    }
}
