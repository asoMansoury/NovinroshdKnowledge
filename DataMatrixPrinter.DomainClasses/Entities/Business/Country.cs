using DataMatrixPrinter.DomainClasses.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public class Country: BaseEntity
    {
        public virtual string CountryCode { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string GS1Prefix { get; set; }
        public virtual bool isEnable { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        

    }
}
