using System.Collections.Specialized;
using System.Linq;

namespace DataMatrixPrinter.Common.Extensions
{
    public static class MultipartExtension
    {
        public static string ToStringValue(this NameValueCollection collection, string key)
        {
            var foundKey = collection.AllKeys.FirstOrDefault(p => p.Contains(key));

            if (foundKey == null) return null;

            var value = collection[foundKey];

            switch (value)
            {
                case null:
                    return null;
                case "null":
                    return null;
                default:
                    return value;
            }
        }
    }
}
