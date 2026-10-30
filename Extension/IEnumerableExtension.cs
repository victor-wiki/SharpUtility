namespace SharpUtility.Extension
{
    public static class IEnumerableExtension
    {
        public static string ToFridenlyString<T>(this IEnumerable<T> array)
        {
            return $"[{string.Join(", ", array)}]";
        }

        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 0)
            {
                yield break;
            }

            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        public static bool HasValue<T>(this IEnumerable<T> source)
        {
            return source != null && source.Count() > 0;
        }
    }
}
