using DataMatrixPrinter.Controllers.Api;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ViewModels.Business.City.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataMatrixPrinter.Controllers
{
    public class CityApiController : BaseApiController
    {
        private readonly ICityService _cityService;
        public CityApiController(ICityService cityService,IUserService userService, IActionUserService actionUserService, IActionService actionService) : base(userService, actionUserService, actionService)
        {
            _cityService = cityService;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public List<CityCreateVm> Cities(string CountryID)
        {
            var restul = new List<CityCreateVm>();
            restul.Add(new CityCreateVm { CityCode="1" , CityName= "tehran", CountryId=1, Id=1});
            return restul;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public CityCreateVm GetCityPrefix(string Code)
        {
            return null;
        }


    }
}