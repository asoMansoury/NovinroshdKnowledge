using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
namespace DataMatrixPrinter.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        private IUserService _userService;
        private IActionUserService _actionUserService;
        private IActionService _actionService;
        public BaseApiController(IUserService userService, IActionUserService actionUserService, IActionService actionService)
        {
            _userService = userService;
            _actionUserService = actionUserService;
            _actionService = actionService;
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            
            string controllerName = controllerContext.ControllerDescriptor.ControllerName;
            var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values.ToList()[1].Value.ToString();
            if (actionName != "Login")
            {
                var _UserID = System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.User.Identity.Name : /*(int?)*/null;
                //if (string.IsNullOrEmpty(_UserID))
                //    Response.Redirect("/user/Login");
                var userRole = _userService.FindByUserName(_UserID);
                var actionDB = _actionService.FindActionIDByName(actionName.Trim().ToLower());
                if (actionDB.ActionUsers.Any(r => userRole.Roles.Any(y => y.RoleId == r.RoleID)))
                {
                    
                }
                else
                {
                    string resultMsg = String.Format("کاربر گرامی دسترسی شما به متد {0} بسته شده است، برای دسترسی به این متد لطفا با مدیریت تماس برقرار نمایید.",actionName);
                    throw new Exception(resultMsg);
                }
            }
            base.Initialize(controllerContext);
        }

        protected async Task<IHttpActionResult> ExecuteServiceAsync<T>(Func<Task<T>> serviceDelegate)
        {


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Aggregate(string.Empty, (current1, modelState) =>
                             modelState.Errors.Aggregate(current1, (current, error) => current + (error.ErrorMessage + ",")));
                //ErrorSignal.FromCurrentContext().Raise(new Exception(errors));
                return BadRequest(errors);
            }
            try
            {
                var res = await serviceDelegate.Invoke();
                return Ok(res);
            }
            catch (Exception ex)
            {
                //ErrorSignal.FromCurrentContext().Raise(ex);
                return InternalServerError(ex);
            }
        }
    }
}