using DataMatrixPrinter.ViewModels.Business.Country.Add;
using DataMatrixPrinter.ViewModels.Business.Country.Update;
using System.Collections.Generic;

namespace DataMatrixPrinter.ViewModels.Business.Country
{
    public  class CountryVm: BaseEntityVm
    {
        public CountryCreateVm CreateCountry { get; set; }
        public CountryUpdateVM UpdateCountry { get; set; }
        public List<CountryCreateVm> Countries { get; set; }
    }
}
