using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ViewModels.Business
{
    public enum EnumElements
    {
        [Description("#ErrorAlert")]
        ErrorAlert,
        [Description("#InfoAlert")]
        InfoAlert,
        [Description("#WarningAlert")]
        WarningAlert,
        [Description("#SuccessAlert")]
        SuccessAlert,

        [Description("#pErrorAlert")]
        pErrorAlert,
        [Description("#pInfoAlert")]
        pInfoAlert,
        [Description("#pWarningAlert")]
        pWarningAlert,
        [Description("#SuccessAlert")]
        pSuccessAlert
    }
}
