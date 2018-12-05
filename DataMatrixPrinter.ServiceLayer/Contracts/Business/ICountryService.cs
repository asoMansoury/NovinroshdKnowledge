using DataMatrixPrinter.ViewModels.Business.Country;
using DataMatrixPrinter.ViewModels.Business.Country.Add;
using DataMatrixPrinter.ViewModels.Business.Country.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ServiceLayer.Contracts.Business
{
    public interface ICountryService
    {
        Task<CountryVm> GetAll(bool getAll);
        Task<CountryCreateVm> Add(CountryVm model);
        Task<CountryUpdateVM> Update(CountryVm model);
        Task<CountryUpdateVM> FindByID(int ID);
    }
}
