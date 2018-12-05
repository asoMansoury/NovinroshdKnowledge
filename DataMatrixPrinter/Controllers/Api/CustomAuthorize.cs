using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace DataMatrixPrinter.Controllers.Api
{
    public class CustomAuthorize: ActionFilterAttribute
    {
        public IUserService _userService;
        public IActionUserService _actionUserService;
        public IActionService _actionService;


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}