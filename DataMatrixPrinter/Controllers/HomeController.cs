using DataMatrixPrinter.ViewModels.Business.City.Add;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataMatrixPrinter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}