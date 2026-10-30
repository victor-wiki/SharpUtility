using System.Net.Http.Headers;

namespace SharpUtility.Extension
{
    public static class HttpHeadersExtension
    {
        public static string get(this HttpHeaders headers, string key, string defaultValue = null)
        {
            if (headers.TryGetValues(key, out var values))
            {
                if(values !=null)
                {
                    return string.Join(",", values);
                }                
            }

            return defaultValue;
        }
    }
}
