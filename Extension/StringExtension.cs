namespace SharpUtility.Extension
{
    public static class StringExtension
    {
        public static string RemovePrefix(this string str, string prefix)
        {
            return str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        }

        public static string RemoveSuffix(this string str, string suffix)
        {
            return str.EndsWith(suffix) ? str.Substring(0, str.Length - suffix.Length) : str;
        }
    }
}
