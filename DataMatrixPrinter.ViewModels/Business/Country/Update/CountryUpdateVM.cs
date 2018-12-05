using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ViewModels.Business.Country.Update
{
    public class CountryUpdateVM:BaseEntityVm
    {
        public string CountryName { get; set; }
        public string CountryGS1 { get; set; }
        public virtual bool isEnable { get; set; }
        public string CountryNationalCode { get; set; }
    }
}
