using DataMatrixPrinter.Common.CommonUtils;
using DataMatrixPrinter.Common.Filters;
using DataMatrixPrinter.Controllers.Api;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ViewModels.Business;
using DataMatrixPrinter.ViewModels.Business.Country;
using DataMatrixPrinter.ViewModels.Business.Country.Add;
using DataMatrixPrinter.ViewModels.Business.Country.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;

namespace DataMatrixPrinter.Controllers
{
    public class CountryApiController : BaseApiController
    {
        private readonly ICountryService _countryService;

        public CountryApiController(ICountryService countryService,IUserService userService, IActionUserService actionUserService, IActionService actionService) : base(userService, actionUserService, actionService)
        {
            _countryService = countryService;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public async Task<List<CountryCreateVm>> Countries(bool getAll)
        {
            
            var model = new List<CountryCreateVm>();
            var data =await _countryService.GetAll(getAll);
            model = data.Countries;
            return model;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public async Task<CountryUpdateVM> GetCountryPrefix(string Code)
        {
            var result =await _countryService.FindByID(int.Parse(Code));
            return result;
        }

        [System.Web.Http.AcceptVerbs("Post")]
        [System.Web.Http.HttpGet]
        public async Task<CountryCreateVm> CreateCountry(CountryVm model)
        {
            var result = await _countryService.Add(model);
            return result;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        public async Task<CountryUpdateVM> UpdateCountry(CountryVm model)
        {
            var result = new CountryUpdateVM();
            result =await _countryService.Update(model);
            return result;
        }
    }
}