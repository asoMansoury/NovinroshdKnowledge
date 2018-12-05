using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;

namespace DataMatrixPrinter.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetIp(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            return request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
        }

        public static int GetUserId(this HttpContext httpContext)
        {
            return
                int.Parse(httpContext.User.Identity.GetUserId());
        }
        
        public static string GetFullName(this HttpContext httpContext)
        {
            return
                ((ClaimsIdentity) httpContext.User.Identity).FindFirst("FullName").Value;
        }

        public static string GetUserPicture(this HttpContext httpContext)
        {
            return
                ((ClaimsIdentity)httpContext.User.Identity).FindFirst("UserPicture").Value;
        }

        public static string GetOrgLevel(this HttpContext httpContext)
        {
            return
                ((ClaimsIdentity)httpContext.User.Identity).FindFirst("OrgLevel").Value;
        }

        public static string GetMapPath(this HttpContext httpContext,string path)
        {
            return
                httpContext.Server.MapPath(path);
        }
    }
}