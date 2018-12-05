using System;
using System.Web.Mvc;
using System.Web.Routing;
using DataMatrixPrinter.IocConfig;

namespace DataMatrixPrinter
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new InvalidOperationException($"Page not found: {requestContext.HttpContext.Request.RawUrl}");
            return ProjectObjectFactory.Container.GetInstance(controllerType) as Controller;
        }
    }
}