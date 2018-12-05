using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace DataMatrixPrinter.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualWithOrdinalIgnoreCase(this string source, string value)
        {
            return source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }


    }
}