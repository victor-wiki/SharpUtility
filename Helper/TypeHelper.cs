namespace SharpUtility.Helper
{
    public class TypeHelper
    {
        public static dynamic CreateInstance(Type type, Dictionary<string, dynamic> args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static bool IsDictionary(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }
    }
}
