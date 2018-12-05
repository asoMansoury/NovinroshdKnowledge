using DataMatrixPrinter.ViewModels.Business.City.Add;
using System.Collections.Generic;

namespace DataMatrixPrinter.ViewModels.Business.Country.Add
{
    public class CountryCreateVm: BaseEntityVm
    {
        public virtual string CountryGS1Code { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string NationalCode { get; set; }
        public virtual bool isEnable { get; set; }
        public virtual IEnumerable<CityCreateVm> Cities { get; set; }
    }
}
