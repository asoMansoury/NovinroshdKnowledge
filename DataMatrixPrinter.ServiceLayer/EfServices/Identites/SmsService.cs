using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string uri = "http://rest.payamak-panel.com/api/SendSMS/SendSMS";
            string postData = $"username=9123360024&password=Dornevis@88311080&to={message.Destination}&from=2188861018&text={message.Body}&isflash=false";

            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(uri); request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var readToEnd = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return Task.FromResult(0);

        }
    }
}
