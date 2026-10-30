namespace SharpUtility.Extension
{
    public static class DateTimeExtension
    {
        public static float GetTotalSeconds(this DateTime dt)
        {
            return (float)dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
