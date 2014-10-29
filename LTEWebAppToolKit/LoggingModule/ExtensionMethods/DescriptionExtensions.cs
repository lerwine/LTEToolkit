using Erwine.Leonard.T.Toolkit.WebApp.ExtensionMethods.AttributeTypes;
using System;
using System.ComponentModel;
using System.Linq;

namespace Erwine.Leonard.T.Toolkit.WebApp.LoggingModule.ExtensionMethods
{
    public static class DescriptionExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum value)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            DescriptionAttribute da = value.GetEnumAttributes<TEnum, DescriptionAttribute>().FirstOrDefault(a => !String.IsNullOrWhiteSpace(a.Description));

            if (da != null)
                return da.Description;

            return Enum.GetName(value.GetType(), value).Replace("_", " ");
        }

    }
}
