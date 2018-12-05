using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DataMatrixPrinter.Common.Extensions
{
    public static class ImageExtensions
    {
        public static Image ToImage(this byte[] source)
        {
            var stream = new MemoryStream(source);
            return Image.FromStream(stream);
        }
        public static Image ImageResize(this Image sourceImage, int newWidth, int newHeight)
        {
            Bitmap bitmap = new Bitmap(newWidth, newHeight, sourceImage.PixelFormat);

            if (bitmap.PixelFormat == PixelFormat.Format1bppIndexed ||
                bitmap.PixelFormat == PixelFormat.Format4bppIndexed ||
                bitmap.PixelFormat == PixelFormat.Format8bppIndexed ||
                bitmap.PixelFormat == PixelFormat.Undefined ||
                bitmap.PixelFormat == PixelFormat.DontCare ||
                bitmap.PixelFormat == PixelFormat.Format16bppArgb1555 ||
                bitmap.PixelFormat == PixelFormat.Format16bppGrayScale)
            {
                throw new NotSupportedException("Pixel format of the image is not supported.");
            }

            System.Drawing.Graphics graphicsImage = System.Drawing.Graphics.FromImage(bitmap);
            graphicsImage.Clear(Color.White);
            graphicsImage.SmoothingMode = SmoothingMode.Default;
            graphicsImage.InterpolationMode = InterpolationMode.Default;
            graphicsImage.PixelOffsetMode = PixelOffsetMode.Default;
            graphicsImage.CompositingQuality = CompositingQuality.Default;
            graphicsImage.DrawImage(sourceImage, 0, 0, newWidth, newHeight);
            graphicsImage.Dispose();
            return bitmap;
        }
        public static void SaveImage(this Image image, int imageQuality, string savePath)
        {
            var imageQualitysParameter = new EncoderParameter(Encoder.Quality, imageQuality);

            var alleCodecs = ImageCodecInfo.GetImageEncoders();

            var codecParameter = new EncoderParameters(1) { Param = { [0] = imageQualitysParameter } };

            var imageCodecInfo = alleCodecs.FirstOrDefault(t => t.MimeType == "image/jpeg"); 

            if (imageCodecInfo == null)
            {
                throw new Exception(" jpegCodecInfo is null ");
            }

            image.Save(savePath, imageCodecInfo, codecParameter);
        }

        private static readonly Dictionary<Guid, string> KnownImageFormats =
            (from p in typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public)
             where p.PropertyType == typeof(ImageFormat)
             let value = (ImageFormat)p.GetValue(null, null)
             select new { Guid = value.Guid, Name = value.ToString() })
            .ToDictionary(p => p.Guid, p => p.Name);

        public static string GetImageFormatName(this ImageFormat format)
        {
            string name;
            if (KnownImageFormats.TryGetValue(format.Guid, out name))
                return name.ToLower();
            return null;
        }
    }
}
