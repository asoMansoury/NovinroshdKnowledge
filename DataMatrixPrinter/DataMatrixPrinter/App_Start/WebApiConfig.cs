using System.Web.Http;
using System.Web.Http.Dispatcher;
using DataMatrixPrinter.IocConfig;

namespace DataMatrixPrinter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = ProjectObjectFactory.Container;
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new StructureMapHttpControllerActivator(container));

            //config.Filters.Add(new DefualtLangugeAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
