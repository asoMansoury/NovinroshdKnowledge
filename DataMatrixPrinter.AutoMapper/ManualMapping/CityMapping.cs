using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.ViewModels.Business.City.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.AutoMapper.ManualMapping
{
    public class CityMapping
    {
        #region DomainMapping
        public static City CityMap(CityCreateVm model)
        {
            return new City
            {
                CityCode = model.CityCode,
                CityName = model.CityName

            };
        }
        #endregion

        #region VMMapping
        public static CityCreateVm CityVmMap(City model)
        {
            return new CityCreateVm
            {
                CityCode = model.CityCode,
                CityName = model.CityName
            };
        }
        #endregion
    }
}
