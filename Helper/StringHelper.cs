using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpUtility.Helper
{
    public class StringHelper
    {
        public static bool FnmatchCase(string filename, string pattern)
        {
            string regexPattern = TranslatePattern(pattern);

            return Regex.IsMatch(filename, regexPattern, RegexOptions.CultureInvariant);
        }

        private static string TranslatePattern(string pattern)
        {
            var regex = new System.Text.StringBuilder();
            regex.Append("^");

            for (int i = 0; i < pattern.Length; i++)
            {
                char c = pattern[i];

                switch (c)
                {
                    case '*':
                        regex.Append(".*");
                        break;
                    case '?':
                        regex.Append(".");
                        break;
                    case '[':
                        int j = i + 1;
                        bool negate = false;

                        if (j < pattern.Length && pattern[j] == '!')
                        {
                            negate = true;
                            j++;
                        }
                        if (j < pattern.Length && pattern[j] == ']')
                        {
                            regex.Append("\\[");
                            break;
                        }

                        int end = pattern.IndexOf(']', j);

                        if (end == -1)
                        {
                            regex.Append("\\[");
                            break;
                        }

                        string charClass = pattern.Substring(j, end - j);

                        charClass = charClass.Replace("\\", "\\\\").Replace("^", "\\^").Replace("-", "\\-");

                        regex.Append(negate ? "[^" + charClass + "]" : "[" + charClass + "]");
                        i = end;

                        break;
                    default:
                        regex.Append(Regex.Escape(c.ToString()));
                        break;
                }
            }

            regex.Append("$");

            return regex.ToString();
        }

        public static int CountSubstring(string source, string sub)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(sub))
            {
                return 0;
            }

            int count = 0;
            int index = 0;

            while ((index = source.IndexOf(sub, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index += sub.Length;
            }

            return count;
        }

        public static string Sha1Hex(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha1.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
