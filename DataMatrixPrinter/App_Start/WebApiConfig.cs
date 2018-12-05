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
                name: "Default Api",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }


    }
}
