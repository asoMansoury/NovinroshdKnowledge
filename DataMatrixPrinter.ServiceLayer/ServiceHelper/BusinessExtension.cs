using DataMatrixPrinter.ViewModels.Helper;

namespace DataMatrixPrinter.ServiceLayer.ServiceHelper
{
    public static class BusinessExtension
    {
        public static T ReturnWithCode<T>(this T response, ServiceResponseKey code) where T : Response
        {
            response.ResultCode = (int)code;
            response.Message = Message.Get(code);
            return response;
        }
        public static void SetResultCode<T>(this T response, ServiceResponseKey code) where T : Response
        {
            response.ResultCode = (int)code;
            response.Message = Message.Get(code);
        }
    }
}