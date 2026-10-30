namespace SharpUtility.Helper
{
    public class EnumerableHelper
    {
        public static List<T> Concat<T>(params IEnumerable<IEnumerable<T>> items)
        {
            List<T> list = new List<T>();

            foreach (var item in items)
            {
                if (item != null)
                {
                    list.AddRange(item);
                }
            }

            return list;
        }
    }
}
