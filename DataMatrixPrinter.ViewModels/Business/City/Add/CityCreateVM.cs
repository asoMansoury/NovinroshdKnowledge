using DataMatrixPrinter.ViewModels.Business.Country.Add;

namespace DataMatrixPrinter.ViewModels.Business.City.Add
{
    public  class CityCreateVm: BaseEntityVm
    {
        
        public virtual string CityCode { get; set; }
        public virtual string CityName { get; set; }
        public virtual int CountryId { get; set; }
        public virtual CountryCreateVm Country { get; set; }
    }
}
