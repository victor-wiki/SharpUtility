using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SharpUtility.Helper
{
    public class ObjectHelper
    {
        public static T CloneObject<T>(object obj)
        {
            return (T)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj), typeof(T));
        }

        public static object Clone(object obj)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj),obj.GetType());
        }

        public static Dictionary<string, dynamic> ToJson(string value)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(value);
        }

        public static object GetPropertyValue(object obj, string propertyName, object defautValue = null)
        {
            if (obj is null || propertyName is null)
            {
                return defautValue;
            }

            var property = obj.GetType().GetProperties().FirstOrDefault(item => item.Name == propertyName);

            if (property is not null)
            {
                return property.GetValue(obj);
            }

            return defautValue;
        }

        public static bool HasProperty(object obj, string propertyName)
        {
            if(obj is null || propertyName is null)
            {
                return false;
            }

            return obj.GetType().GetProperties().FirstOrDefault(item => item.Name == propertyName) is not null;
        }

        public static List<string> GetPropertyNames(object obj)
        {
            return obj.GetType().GetProperties().Select(item => item.Name).ToList();
        }

        public static List<PropertyInfo> GetProperties(object obj)
        {
            return obj.GetType().GetProperties().ToList();
        }

        public static Dictionary<string, dynamic> GetPropertyProperties(object obj)
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();

            foreach(var property in obj.GetType().GetProperties())
            {
                dict.Add(property.Name, property.GetType());
            }

            return dict;
        }

        public static List<string> GetPropertyObjectPropertyNames(object obj, string propertyName)
        {
            return obj.GetType().GetProperties().FirstOrDefault(item=>item.Name == propertyName)?.PropertyType.GetProperties()?.Select(item => item.Name).ToList();
        }

        public static Dictionary<string, dynamic> GetPropertyObjectProperties(object obj, string propertyName)
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>(); 

            var properties = obj.GetType().GetProperties().FirstOrDefault(item => item.Name == propertyName)?.PropertyType.GetProperties();

            if(properties!=null)
            {
                foreach(var property in properties)
                {
                    dict.Add(property.Name, property.GetType());
                }
            }

            return dict;
        }

        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj is null || propertyName is null)
            {
                return;
            }

            var property = obj.GetType().GetProperties().FirstOrDefault(item => item.Name == propertyName);       
            
            if (property is not null && property.CanWrite)
            {
                property.SetValue(obj, value, null);
            }
        }

        public static void CopyProperties(object source, object target, bool excluedNullValue = false, List<string> excludePropertyNames = null)
        {
            var sourceProps = source.GetType().GetProperties().Where(x => x.CanRead).ToList();
            var targetProps = target.GetType().GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (excludePropertyNames is not null && excludePropertyNames.Contains(sourceProp.Name))
                {
                    continue;
                }

                if (targetProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = targetProps.FirstOrDefault(x => x.Name == sourceProp.Name);

                    if (p is not null && p.CanWrite)
                    {
                        var value = sourceProp.GetValue(source, null);

                        if (excluedNullValue && value is null)
                        {
                            continue;
                        }

                        p.SetValue(target, value, null);
                    }
                }
            }
        }

        public static bool HasField(object obj, string fieldName)
        {
            if(obj is null || fieldName is null)
            {
                return false;
            }

            return obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                   BindingFlags.Instance | BindingFlags.Static).Any(item => item.Name == fieldName);
        }

        public static object GetFieldValue(object obj, string fieldName, object defautValue = null)
        {
            if (obj is null || fieldName is null)
            {
                return defautValue;
            }

            var field = obj.GetType().GetFields().FirstOrDefault(item => item.Name == fieldName);

            if (field is not null)
            {
                return field.GetValue(obj);
            }

            return defautValue;
        }

        public static void SetFieldValue(object obj, string fieldName, object value)
        {
            if (obj is null || fieldName is null)
            {
                return;
            }

            var field = obj.GetType().GetFields().FirstOrDefault(item => item.Name == fieldName);

            if (field is not null)
            {
                field.SetValue(obj, value);
            }
        }
    
        public static void CopyDictionaryToObject<T>(Dictionary<string,dynamic> source, T target, bool excluedNullValue = false, List<string> excludePropertyNames = null)
        {
            T restult = (T)JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));

            ObjectHelper.CopyProperties(restult, target, excluedNullValue, excludePropertyNames);
        }

        public static bool IsTuple(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            
            Type type = obj.GetType();

            return type.IsGenericType && type.GetGenericTypeDefinition().Name.StartsWith("ValueTuple`");
        }

        public static bool IsList(object obj)
        {
            if (obj is null)
            {
                return false;
            }
                
            Type type = obj.GetType();

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static bool IsDictionary(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            Type type = obj.GetType();

            return TypeHelper.IsDictionary(type);
        }     

        public static bool IsListOrTuple(object obj)
        {
            return IsList(obj) || IsTuple(obj);
        }

        public static object GetObjectByType(object obj, Type targetType)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj), targetType);
        }        
    }
}
