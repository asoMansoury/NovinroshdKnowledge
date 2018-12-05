using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.ViewModels.Business.Country.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMatrixPrinter.ViewModels.Business.City.Add;
using DataMatrixPrinter.ViewModels.Business.Country.Update;

namespace DataMatrixPrinter.AutoMapper.ManualMapping
{
    public class CountryMapping
    {
        #region DomainMap
        public static Country CountryMap(CountryCreateVm model)
        {
            return new Country
            {
                CountryCode = model.NationalCode,
                CountryName = model.CountryName,
                GS1Prefix=model.CountryGS1Code,
                isEnable = model.isEnable,
                Cities = model.Cities!=null? model.Cities.ToList().ConvertAll(mapCityMapping):null
            };
        }
        public static Country CountryMap(CountryUpdateVM model,Country result)
        {
            result.CountryCode = model.CountryNationalCode;
            result.CountryName = model.CountryName;
            result.GS1Prefix = model.CountryGS1;
            result.isEnable = model.isEnable;
            return result;
        }
        private static City mapCityMapping(CityCreateVm input)
        {
            return CityMapping.CityMap(input);
        }
        #endregion


        #region VmMapping
        public static CountryCreateVm CountryVmMap(Country model)
        {
            return new CountryCreateVm
            {
                Id=model.Id,
                NationalCode=model.CountryCode,
                CountryGS1Code = model.GS1Prefix,
                CountryName = model.CountryName,
                isEnable = model.isEnable,
                Cities = model.Cities!=null? model.Cities.ToList().ConvertAll(mapCityVmMapping):null
            };
        }

        public static CountryUpdateVM CountryUPVmMap(Country model)
        {
            return new CountryUpdateVM
            {
                Id = model.Id,
                CountryNationalCode = model.CountryCode,
                CountryGS1 = model.GS1Prefix,
                isEnable = model.isEnable,
                CountryName = model.CountryName,
            };
        }
        private static CityCreateVm mapCityVmMapping(City input)
        {
            return CityMapping.CityVmMap(input);
        }



        public static List<CountryCreateVm> lstCountryVmMap(List<Country> model)
        {
            var result =new List<CountryCreateVm>();
            result = model.ConvertAll(maplstCityVmMap);
            return result;
        }
        private static CountryCreateVm maplstCityVmMap(Country input)
        {
            return CountryVmMap(input);
        }
        #endregion
    }
}
