
using DataMatrixPrinter.ViewModels.Business.Country;
using MobinYarCrm.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.Controllers.Api;

namespace DataMatrixPrinter.Controllers
{
    public class CountryController : BaseController
    {
        public CountryController(IUserService userService, IActionUserService actionUserService, IActionService actionService) : base(userService, actionUserService, actionService)
        {
        }

        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult CountryManager()
        {
            var CountryModel = new CountryVm();
            return View(CountryModel);
        }
    }
}