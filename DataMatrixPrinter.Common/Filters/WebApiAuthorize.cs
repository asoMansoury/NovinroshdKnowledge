using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DataMatrixPrinter.Common.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class WebApiAuthorize : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        private readonly string[] _roles;

        public WebApiAuthorize(string[] roles = null)
        {
            _roles = roles;
        }

        public override bool AllowMultiple => false;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }
            if (HttpContext.Current == null ||
                HttpContext.Current.User == null ||
                HttpContext.Current.User.Identity == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated ||
                !RoleIsValid())
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }

        private bool RoleIsValid()
        {
            var result = _roles == null || _roles.Any(role => HttpContext.Current.User.IsInRole(role));
            return result;
        }

        private bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}