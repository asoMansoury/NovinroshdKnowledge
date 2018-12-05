using System.IO;
using System.Web;

namespace DataMatrixPrinter.Common.Helpers
{
    public static class DataMatrixPrinter
    {
        static readonly string MediaPath = HttpContext.Current.Server.MapPath(ImageSetting.MediaPath);
        static readonly string ProductsImagePathWithoutCdn = HttpContext.Current.Server.MapPath(ImageSetting.Product.ProductsImagePathWithoutCdn);
        static readonly string CategoriesImagePathWithoutCdn = HttpContext.Current.Server.MapPath(ImageSetting.Category.CategoriesImagePathWithoutCdn);
        static readonly string CustomersImagePathWithoutCdn = HttpContext.Current.Server.MapPath(ImageSetting.Customer.CustomersImagePathWithoutCdn);

        public static void CreateMediaFolder()
        {
            CreatePath(MediaPath);
        }
        public static void CreateProductsFolder()
        {
            CreateMediaFolder();
            CreatePath(ProductsImagePathWithoutCdn);
            foreach (var size in ImageSetting.Product.GetAllSize())
            {
                var imageSizePath = Path.Combine(ProductsImagePathWithoutCdn, size.Key);
                CreatePath(imageSizePath);
            }
        }
        public static void CreateCategoryFolder()
        {
            CreateMediaFolder();
            CreatePath(CategoriesImagePathWithoutCdn);
            foreach (var size in ImageSetting.Category.GetAllSize())
            {
                var imageSizePath = Path.Combine(CategoriesImagePathWithoutCdn, size.Key);
                CreatePath(imageSizePath);
            }
        }

        public static void CreateFirstLevelCustomerFolder(int id)
        {
            CreatePath(CustomersImagePathWithoutCdn + id);
            var imageSizePath = Path.Combine(CategoriesImagePathWithoutCdn + id + "/", ImageSetting.Customer.FirstLevel.Key);
            CreatePath(imageSizePath);
        }

        public static void CreateLowSignalCustomerFolder(int id)
        {
            CreatePath(CustomersImagePathWithoutCdn + id);
            var imageSizePath = Path.Combine(CategoriesImagePathWithoutCdn + id + "/", ImageSetting.Customer.LowSignal.Key);
            CreatePath(imageSizePath);
        }

        public static void CreateFinalLevelCustomerFolder(int id)
        {
            CreatePath(CustomersImagePathWithoutCdn + id);
            var imageSizePath = Path.Combine(CategoriesImagePathWithoutCdn + id + "/", ImageSetting.Customer.FinalLevel.Key);
            CreatePath(imageSizePath);
        }

        public static void RemoveProductsImage(string imageName)
        {
            foreach (var size in ImageSetting.Product.GetAllSize())
            {
                var imagePath = Path.Combine(ProductsImagePathWithoutCdn,
                    size.Key,
                    imageName);
                RemoveFile(imagePath);
            }
        }
        public static void RemoveCategoryImage(string imageName)
        {
            foreach (var size in ImageSetting.Category.GetAllSize())
            {
                var imagePath = Path.Combine(CategoriesImagePathWithoutCdn,
                    size.Key,
                    imageName);
                RemoveFile(imagePath);
            }
        }

        public static void RemoveCustomerDirectory(int id)
        {
            var path = CustomersImagePathWithoutCdn + id;
            if (!ExistPath(path)) return;
            var di = new DirectoryInfo(path);
            foreach (var dir in di.GetDirectories())
            {
                foreach (var file in dir.GetFiles())
                {
                    file.Delete();
                }
                dir.Delete(true);
            }
            di.Delete(true);
        }

        private static bool ExistPath(string path)
        {
            return Directory.Exists(path);
        }
        private static bool ExistFile(string path)
        {
            return File.Exists(path);
        }
        private static void RemoveFile(string path)
        {
            if (!ExistFile(path))
            {
                return;
            }
            File.Delete(path);
        }
        private static void CreatePath(string path)
        {
            if (ExistPath(path))
            {
                return;
            }
            Directory.CreateDirectory(path);
        }
    }

}
