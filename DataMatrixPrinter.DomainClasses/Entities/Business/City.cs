using DataMatrixPrinter.DomainClasses.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public  class City: BaseEntity
    {
        public virtual string CityCode { get; set; }
        public virtual string CityName { get; set; }
        public virtual int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
