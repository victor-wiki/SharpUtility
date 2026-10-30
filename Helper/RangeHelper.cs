namespace SharpUtility.Helper
{
    public class RangeHelper
    {
        public static int[] GetRange(int startNumber, int endNumber, int step = 1)
        {
            List<int> numbers = new List<int>();

            for (var i = startNumber; i < endNumber;)
            {
                numbers.Add(i);

                i += step;
            }

            return numbers.ToArray();
        }

        public static int[] GetRange(int number)
        {
            return GetRange(0, number, 1);
        }
    }
}
