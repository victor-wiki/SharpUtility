namespace SharpUtility.Helper
{
    public class ValueHelper
    {
        public static int NumberToBoolNumber(int number)
        {
            return Convert.ToInt32(Convert.ToBoolean(number));
        }

        public static object GetValue(object value)
        {
            if(value == DBNull.Value)
            {
                return null;
            }

            return value;
        }
    }
}
