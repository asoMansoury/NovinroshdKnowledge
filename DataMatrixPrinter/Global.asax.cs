using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
using StructureMap.Web.Pipeline;

namespace DataMatrixPrinter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    //AreaRegistration.RegisterAllAreas();
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    //BundleConfig.RegisterBundles(BundleTable.Bundles);
        //    ApplicationStart.Config();
        //}


        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                ApplicationStart.Config();

            }
            catch
            {
                HttpRuntime.UnloadAppDomain();
                throw;
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}
