using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace DataMatrixPrinter.Common.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //if (filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    filterContext.HttpContext.Response.End();
            //}


            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
            //else
            //{
                filterContext.Result = new RedirectResult("~/Access/Index");
            //}

            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}