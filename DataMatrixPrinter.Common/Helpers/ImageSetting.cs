using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace DataMatrixPrinter.Common.Helpers
{
    public static class ImageSetting
    {
        public static string CdnUrl = HttpContext.Current.Server.MapPath("~");
        public static string MediaPath = "/Upload/";

        public static class Product
        {
            public static string ProductsImagePathWithoutCdn = MediaPath + "products/";
            public static string ProductsImagePath = CdnUrl + ProductsImagePathWithoutCdn;
            public static string ProductImageUrlFormat = ProductsImagePath + "{0}/{1}_{2}.{3}{4}";

            public static KeyValuePair<string, int> Org = new KeyValuePair<string, int>("org", 32);
            public static KeyValuePair<string, int> Tiny = new KeyValuePair<string, int>("tiny", 32);
            public static KeyValuePair<string, int> Thumb = new KeyValuePair<string, int>("thumb", 64);
            public static KeyValuePair<string, int> Small = new KeyValuePair<string, int>("small", 180);
            public static KeyValuePair<string, int> Meduim = new KeyValuePair<string, int>("meduim", 400);
            public static KeyValuePair<string, int> Large = new KeyValuePair<string, int>("large", 632);

            public static List<KeyValuePair<string, int>> GetAllSize()
            {
                
                return new List<KeyValuePair<string, int>>
                {
                    Org,
                    Tiny,
                    Thumb,
                    Small,
                    Meduim,
                    Large
                };
            }
        }
        public static class Category
        {
            public static string CategoriesImagePathWithoutCdn = MediaPath + "Customer/";
            public static string CategoriesImagePath = CdnUrl + CategoriesImagePathWithoutCdn;
            public static string CategoryImageUrlFormat = CategoriesImagePath + "{0}/{1}.jpg";// 1=>Directory 2=>CategoryId

            public static KeyValuePair<string, int> Org = new KeyValuePair<string, int>    ("org", 32);

            public static List<KeyValuePair<string, int>> GetAllSize()
            {
                return new List<KeyValuePair<string, int>>
                {
                    Org
                };
            }
        }

        public static class Customer
        {
            public static string CustomersImagePathWithoutCdn = MediaPath + "Customer/";
            public static string CustomersImagePath = CdnUrl + CustomersImagePathWithoutCdn;
            public static string CustomersImageUrlFormat = CustomersImagePath + "{0}/{1}.jpg";

            public static KeyValuePair<string, int> FirstLevel = new KeyValuePair<string, int>("FirstLevel", 1000);
            public static KeyValuePair<string, int> FinalLevel = new KeyValuePair<string, int>("FinalLevel", 1000);
            public static KeyValuePair<string, int> LowSignal = new KeyValuePair<string, int>("LowSignal", 1000);

            public static List<KeyValuePair<string, int>> GetAllSize()
            {
                return new List<KeyValuePair<string, int>>
                {
                    FirstLevel,
                    FinalLevel,
                    LowSignal
                };
            }
        }
    }
}
