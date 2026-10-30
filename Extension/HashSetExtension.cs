namespace SharpUtility.Extension
{
    public static class HashSetExtension
    {
        public static T pop<T>(this HashSet<T> set)
        {
            T first = set.FirstOrDefault();

            if (first != null)
            {
                set.Remove(first);
            }

            return first;
        }

        public static void update<T>(this HashSet<T> set, HashSet<T> value)
        {
            foreach(var item in value)
            {
                if(!set.Contains(item))
                {
                    set.Add(item);
                }
            }
        }
    }
}
