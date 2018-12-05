using AutoMapper;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMatrixPrinter.AutoMapper.ManualMapping;
using DataMatrixPrinter.ViewModels.Business.Country;
using DataMatrixPrinter.ViewModels.Business.Country.Add;
using DataMatrixPrinter.ViewModels.Business.Country.Update;
using DataMatrixPrinter.Common.CommonUtils;
using DataMatrixPrinter.ViewModels.Business;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Business
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Country> _country;
        private readonly IMappingEngine _mappingEngine;
        public CountryService(
    IUnitOfWork unitOfWork,
    IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _country = _unitOfWork.Set<Country>();
        }

        public async Task<CountryVm> GetAll(bool getAll)
        {
            var result = new CountryVm();
            try
            {

                var data = new List<Country>();
                if (getAll == true)
                    data= _country.Select(r => r).ToList();
                else
                    data = _country.Where(r => r.isEnable == true).Select(r => r).ToList();
                result.Countries = CountryMapping.lstCountryVmMap(data);
            }
            catch (Exception ex)
            {
                result.Errors = ErrorVM.errorCustom(result.Errors, ex.Message);
            }
            return  result;
        }
        public async Task<CountryUpdateVM> Update(CountryVm model)
        {
            var result = new CountryUpdateVM();
            try
            {
                var data = _country.Where(r => r.Id == model.UpdateCountry.Id).FirstOrDefault();
                data = CountryMapping.CountryMap(model.UpdateCountry,data);
                _unitOfWork.MarkAsChanged<Country>(data);
                await _unitOfWork.SaveAllChangesAsync();
                
            }
            catch (Exception ex)
            {
                result.Errors = ErrorVM.errorCustom(result.Errors, ex.Message);
            }
            return result;
        }
        public async Task<CountryCreateVm> Add(CountryVm model)
        {
            var result = new CountryCreateVm();
            try
            {
                if (_country.Any(r => r.GS1Prefix.Trim().ToLower() == model.CreateCountry.CountryGS1Code.Trim().ToLower()))
                {
                    throw new Exception("کد GS1 نباید تکراری باشد");
                }
                model.CreateCountry.isEnable = true;
                var eee = _country.Add(CountryMapping.CountryMap(model.CreateCountry));
                result = CountryMapping.CountryVmMap(eee);
                _unitOfWork.SaveAllChanges();
                
            }
            catch (Exception ex)
            {
                result.Errors = ErrorVM.errorCustom(result.Errors, ex.Message);
            }

            return result;
        }
        public async Task<CountryUpdateVM> FindByID(int ID)
        {
            var result = new CountryUpdateVM();
            try
            {
                var Data = _country.Where(r => r.Id == ID).FirstOrDefault();
                result = CountryMapping.CountryUPVmMap(Data);
            }
            catch (Exception ex)
            {

                result.Errors = ErrorVM.errorCustom(result.Errors, ex.Message);
            }
            return result;
        }

    }
}
