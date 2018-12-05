using QRCoder;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OnBarcode.Barcode;

namespace DataMatrixPrinter.Common.CommonUtils
{
    public static  class CommonUtils
    {
        public static string HashingPassword(string Password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(Password);
            MemoryStream stream = new MemoryStream(byteArray);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(stream);
            string result = System.Text.Encoding.UTF8.GetString(sha1data);
            return result;
        }


        public static Int64 GenerateRandomNumber(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            string s;
            for (int i = 0; i < size; i++)
            {
                s = Convert.ToString(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(s);
            }

            return Convert.ToInt64((builder.ToString()));
        }

        public static string GetEnumDescription(Enum value)
        {

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GenerateQRCode(string Gtin,string expireDate,string UID,string LOT)
        {
            string strQRCode = Gtin + expireDate + UID + LOT;
            QRCodeGenerator qrGenerate = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerate.CreateQrCode(strQRCode, QRCodeGenerator.ECCLevel.H);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            
            Bitmap qrCodeImage = qrCode.GetGraphic(8, "#000000", "#ffffff");
            string FileNameQR = HttpContext.Current.Server.MapPath("~/sd.bmp");
            Byte[] imgByte = qrCodeData.GetRawData(QRCodeData.Compression.Uncompressed);
            qrCodeImage.Save(FileNameQR);
            //streamQR.CopyTo(streamQR);
            return FileNameQR;
        }

        public static StiReport generateImageReport(string GTIN,string UID,string LOT,string ExpireDate,string Price,string fileNamePath,string imageFilePath)
        {
            StiReport mainreport = new StiReport();
            try
            {

                var pdfServiceSti = new Stimulsoft.Report.Export.StiPdfExportService();
                var jpegServiceSti = new Stimulsoft.Report.Export.StiJpegExportService();
                mainreport.RegBusinessObject("Product", new { GTIN = GTIN, UID = UID, LOT = LOT, EXP = ExpireDate, Price = Price, ImgQrCode = fileNamePath });
                string pathFileMrt = HttpContext.Current.Server.MapPath("~/Report.mrt");
                mainreport.Load(pathFileMrt);
                mainreport.Render();
                var settings = new Stimulsoft.Report.Export.StiJpegExportSettings();
                // Create an PDF service instance.



                using (var stream = new MemoryStream())
                {
                    jpegServiceSti.ExportTo(mainreport, stream, settings);
                    var data = stream.ToArray();
                    var fileName = String.IsNullOrEmpty(mainreport.ReportAlias) ? mainreport.ReportName : mainreport.ReportAlias;
                    stream.CopyTo(stream);
                    System.IO.File.WriteAllBytes(imageFilePath, stream.ToArray());
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return mainreport;
        }
        public static string generatePDFReportForLabeledPrinters(string imageFilePath, List<productReport> data)
        {
            StiReport mainreport = new StiReport();
            try
            {

                var service = new Stimulsoft.Report.Export.StiPdfExportService();
                var settings = new Stimulsoft.Report.Export.StiPdfExportSettings();

                mainreport.RegBusinessObject("Product", data);
                string pathFileMrt = HttpContext.Current.Server.MapPath("~/ReportPDF.mrt");
                mainreport.Load(pathFileMrt);
                mainreport.Render();

                // Create an PDF service instance.
                using (var stream = new MemoryStream())
                {
                    service.ExportTo(mainreport, stream, settings);
                    var dataStream = stream.ToArray();
                    var fileName = String.IsNullOrEmpty(mainreport.ReportAlias) ? mainreport.ReportName : mainreport.ReportAlias;
                    stream.CopyTo(stream);
                    System.IO.File.WriteAllBytes(imageFilePath, stream.ToArray());
                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return imageFilePath;
        }

        public static string generate(string Gtin, string expireDate, string UID, string LOT)
        {
            return  Gtin +   UID  + expireDate  + LOT;
        }

        public static string GenerateDataMatrix(string Gtin, string expireDate, string UID, string LOT,string FilePath)
        {
            DataMatrix datamatrix = new DataMatrix();
            try
            {
                datamatrix.Data ="(01)"+ Gtin+"(21)"  + UID +"(17)"+ expireDate + "(10)"+LOT;
                datamatrix.DataMode = DataMatrixDataMode.ASCII;
                datamatrix.FormatMode = DataMatrixFormatMode.Format_24X24;
                datamatrix.UOM = UnitOfMeasure.PIXEL;
                datamatrix.X = 3;
                datamatrix.LeftMargin = 0;
                datamatrix.RightMargin = 0;
                datamatrix.TopMargin = 0;
                datamatrix.BottomMargin = 0;
                datamatrix.Resolution = 300;
                datamatrix.Rotate = Rotate.Rotate0;
                datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                datamatrix.drawBarcode(FilePath);
            }
            catch (Exception ex)
            {

                throw;
            }
            return FilePath;
        }
        public class productReport
        {
            public string GTIN { get; set; }
            public string UID { get; set; }
            public string LOT { get; set; }
            public string EXP { get; set; }
            public string Price { get; set; }
            public string ImgQrCode { get; set; }
        }
    }
}
