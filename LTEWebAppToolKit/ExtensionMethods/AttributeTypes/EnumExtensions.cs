using System;

namespace Erwine.Leonard.T.Toolkit.WebApp.ExtensionMethods.AttributeTypes
{
    public static class EnumExtensions
    {
        public static TAttribute[] GetEnumAttributes<TEnum, TAttribute>(this TEnum value)
            where TEnum : struct, IComparable, IFormattable, IConvertible
            where TAttribute : Attribute
        {
            Type t = value.GetType();

            if (!t.IsEnum)
                throw new ArgumentException(String.Format("{0} is not an enumerated type.", t.FullName), "value");

            return t.GetField(Enum.GetName(t, value)).GetAttributesOfType<TAttribute>(false);
        }
    }
}
