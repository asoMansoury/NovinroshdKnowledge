using DataMatrixPrinter.Common.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ViewModels.Business
{
    public class BaseEntityVm
    {
        public BaseEntityVm()
        {
            Errors = new Errors();
            Errors.EnumElements = CommonUtils.GetEnumDescription(EnumElements.SuccessAlert);
            Errors.Message = "عملیات با موفقیت انجام شد.";
            Errors.pEnumElements = CommonUtils.GetEnumDescription(EnumElements.pSuccessAlert);
        }
        public virtual int Id { get; set; }
        public virtual Errors Errors { get; set; }
        public bool isError { get; set; }
    }

    public class Errors
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public string EnumElements {get;set;}

        public string pEnumElements{get;set;}

    }
}
