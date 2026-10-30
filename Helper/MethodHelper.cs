using System.Reflection;

namespace SharpUtility.Helper
{
    public class MethodHelper
    {
        public static MethodInfo GetMethod(Type type, string  methodName)
        {
            if(type is null)
            {
                return null;
            }

            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance| BindingFlags.Static);
        }

        public static dynamic Invoke(Delegate @delegate, Dictionary<dynamic, dynamic> args)
        {
            if (@delegate == null)
            {
                return null;
            }

            var parameters = @delegate.Method.GetParameters() as ParameterInfo[];

            if (parameters.Length == 0)
            {
                return @delegate.DynamicInvoke();
            }

            var callingParameters = new List<object>();

            var integerKeys = args.Keys?.Where(item => item is int)?.Select(item => (int)item)?.ToList() ?? new List<int>();
            var stringKeys = args.Keys?.Where(item => item is string)?.Select(item => item as string)?.ToList() ?? new List<string>();

            int i = 0;

            foreach (var parameter in parameters)
            {
                string name = parameter.Name;
                object value = null;

                if (integerKeys.Contains(i))
                {
                    value = args[i];
                }
                else if (stringKeys.Contains(name))
                {
                    value = args[name];
                }
                else
                {
                    value = ValueHelper.GetValue(parameter.DefaultValue) ?? null;
                }

                callingParameters.Add(value);

                i++;
            }

            return @delegate.DynamicInvoke(callingParameters.ToArray());
        }
    }
}
