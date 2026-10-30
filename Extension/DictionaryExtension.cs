using Newtonsoft.Json.Linq;
using SharpUtility.Helper;

namespace SharpUtility.Extension
{
    public static class DictionaryExtension
    {
        public static T get<T>(this Dictionary<string, dynamic> dict, string key, T defaultValue = default(T))
        {
            return dict?.GetValueOrDefault(key, defaultValue);
        }

        public static Dictionary<string, T> update<T>(this Dictionary<string, T> target, Dictionary<string, T> source)
        {
            foreach (var item in source)
            {
                target[item.Key] = item.Value;
            }

            return target;
        }

        public static OrderedDictionary<string, T> update<T>(this OrderedDictionary<string, T> target, OrderedDictionary<string, T> source)
        {
            foreach (var item in source)
            {
                target[item.Key] = item.Value;
            }

            return target;
        }

        public static void update<T>(this Dictionary<int, T> target, Dictionary<int, T> source)
        {
            foreach (var item in source)
            {
                target[item.Key] = item.Value;
            }
        }

        public static Dictionary<string, T> clone<T>(this Dictionary<string, T> source)
        {
            if(source == null)
            {
                return null;
            }

            var result = ObjectHelper.CloneObject<Dictionary<string, T>>(source);

            foreach (var (key,value) in result)
            {
                if (value is not null)
                {
                    var oldValue = source[key];

                    if ((oldValue is Type && value is string))
                    {
                        result[key] = source[key];
                    }
                    else if (oldValue is Int32 && value is Int64)
                    {
                        result[key] = oldValue;
                    }
                    if(oldValue is Dictionary<string, dynamic> && value is JObject)
                    {
                        result[key] = (T)ObjectHelper.Clone(oldValue);
                    }
                }
            }

            return result;
        }

        public static T pop<T>(this Dictionary<string, T> dict, string key, T defaultValue = default(T))
        {
            T value = dict.GetValueOrDefault(key, defaultValue);

            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }

            return value;
        }

        public static dynamic setdefault(this Dictionary<string, dynamic> dict, string key, object value)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = value;
            }

            return dict[key];
        }

        public static T setdefault<T>(this Dictionary<string, T> dict, string key, T value)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = value;
            }

            return dict[key];
        }
    }
}
