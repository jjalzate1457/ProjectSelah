using System.Linq;
using System.Reflection;

namespace ProjectSelah.API
{
    public static class ClassAttributeExtensions
    {
        public static CustomAttributeData GetAttribute(this PropertyInfo prop, string attributeName)
        {
            return prop.CustomAttributes.FirstOrDefault(i => i.AttributeType.Name == attributeName);
        }

        public static T GetValue<T>(this CustomAttributeData attribute, string name)
        {
            if (attribute != null)
            {
                var value = attribute.NamedArguments.FirstOrDefault(i => i.MemberName == name);
                if (value != null)
                    return (T)value.TypedValue.Value;
            }

            return default(T);
        }
    }
}
