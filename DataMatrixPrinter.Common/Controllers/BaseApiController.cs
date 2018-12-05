using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DataMatrixPrinter.Common.Controllers
{
    public class BaseApiController : ApiController
    {
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

        //protected override void Initialize(HttpControllerContext controllerContext)
        //{
        //    var currentLanguage = ConfigurationManager.AppSettings["CurrentLanguage"];
        //    var cultureInfo = new System.Globalization.CultureInfo(currentLanguage);
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

        //    base.Initialize(controllerContext);
        //}
    }
}
