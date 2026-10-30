namespace SharpUtility.Helper
{
    public class ArrayHelper
    {
        public static T[] GetArray<T>(params T[] values)
        {
            return values;
        }

        public static T[,] ToRectangular<T>(T[][] source)
        {
            int rows = source.Length;
            int cols = source.FirstOrDefault()?.Length ?? 0;

            T[,] result = new T[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = source[i][j];
                }
            }

            return result;
        }        
    }
}
