namespace SharpUtility.Extension
{
    public static class ListExtension
    {
        public static void update<T>(this List<T> target, List<T> source)
        {
            foreach (var item in source)
            {
                if (!target.Contains(item))
                {
                    target.Add(item);
                }
            }
        }
    }
}
