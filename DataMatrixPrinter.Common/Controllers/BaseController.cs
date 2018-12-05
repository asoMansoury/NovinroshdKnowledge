using DataMatrixPrinter.Common.Extensions;
using System.Configuration;
using System.Web.Mvc;

namespace MobinYarCrm.Common.Controllers
{
    public class BaseController : Controller
    {

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    //var userServicce= DependencyResolver.Current.GetService<IUserService>();
        //    var _UserID = System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.GetUserId() : (int?)null;
        //    //var Role = _userService.GetRolesAsync(_UserID.Value).Result; var _UserID = HttpContext.Current != null ? HttpContext.Current.GetUserId() : (int?)null;
        //    //var Role = _userService.GetRolesAsync(_UserID.Value).Result;
        //    //var currentLanguage = ConfigurationManager.AppSettings["CurrentLanguage"];
        //    //var cultureInfo = new System.Globalization.CultureInfo(currentLanguage);
        //    //System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //    //System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
        //    string actionName = filterContext.ActionDescriptor.ActionName;
        //    string controllerNae = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

        //    base.OnActionExecuting(filterContext);
        //}
    }
}
