using DataMatrixPrinter.Common.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ViewModels.Business
{
    public class ErrorVM
    {
        public static Errors errorCustom(Errors result, string msg)
        {
            result.Message = msg;
            result.EnumElements = CommonUtils.GetEnumDescription(EnumElements.ErrorAlert);
            result.pEnumElements = CommonUtils.GetEnumDescription(EnumElements.pErrorAlert);
            return result;
        }
    }
}
