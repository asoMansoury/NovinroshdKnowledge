using DataMatrixPrinter.Common.Extensions;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.EfServices.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataMatrixPrinter.Controllers
{
    public class BaseController : Controller
    {
        private IUserService _userService;
        private IActionUserService _actionUserService;
        private IActionService _actionService;
        public BaseController(IUserService userService, IActionUserService actionUserService, IActionService actionService)
        {
            _userService = userService;
            _actionUserService = actionUserService;
            _actionService = actionService;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerNae = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (actionName != "Login"&&actionName!= "Registry")
            {
                var _UserID = System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.User.Identity.Name : /*(int?)*/null;
                if (string.IsNullOrEmpty(_UserID))
                    Response.Redirect("/user/Login");
                
                var userRole = _userService.FindByUserName(_UserID);
                var actionDB = _actionService.FindActionIDByName(actionName.Trim().ToLower());
                if (actionDB.ActionUsers.Any(r => userRole.Roles.Any(y=>y.RoleId==r.RoleID)))
                {
                    ViewBag.FullName = userRole.Name + " " + userRole.Family;
                }
                else
                {
                    Response.Redirect("/Access");
                }
            }


            //var RolerID = userServicce.GetRolesAsync(_UserID.Value);
            //var Role = _userService.GetRolesAsync(_UserID.Value).Result; var _UserID = HttpContext.Current != null ? HttpContext.Current.GetUserId() : (int?)null;
            //var Role = _userService.GetRolesAsync(_UserID.Value).Result;
            //var currentLanguage = ConfigurationManager.AppSettings["CurrentLanguage"];
            //var cultureInfo = new System.Globalization.CultureInfo(currentLanguage);
            //System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;


            base.OnActionExecuting(filterContext);
        }
    }
}