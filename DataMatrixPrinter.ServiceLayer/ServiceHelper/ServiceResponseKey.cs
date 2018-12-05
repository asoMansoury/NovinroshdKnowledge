namespace DataMatrixPrinter.ServiceLayer.ServiceHelper
{
    public enum ServiceResponseKey
    {
        //Account => 1000
        User_LoginSuccess = 1000,
        User_LoginLockedOut = 1001,
        User_LoginRequiresVerification = 1002,
        User_LoginFailure = 1003,
        User_RegisterSuccess = 1004,
        User_RegisterDuplicateUserName = 1005,
        User_RegisterDuplicateEmail = 1006,
        User_RoleSuccess = 1007,
        User_RoleDuplicateName = 1008,
        User_UserInRoleSuccess = 1009,
        User_UserInRoleDuplicate = 1010,
        User_UpdatePersonalInfoSuccess = 1011,
        User_UpdatePasswordSuccess = 1012,
        User_PrePasswordIsWrong= 1013,

        //Product =>1500
        Product_Modem_DuplicateModem = 1500,
        Product_Modem_ExcelNotEqualWithCount = 1501,
        Product_Modem_AddModemSuccess = 1502,
        Product_SimCart_DuplicateName = 1503,
        Product_SimCart_AddOperatorSuccess = 1504,
        Product_SimCart_ExcelNotEqualWithCount = 1505,
        Product_SimCart_DuplicateSimCart = 1506,
        Product_SimCart_AddSimCartSuccess = 1507,
        Product_Modem_MacNotFound = 1508,
        Product_Modem_SerialNotFound = 1509,
        Product_SimCart_IccIdNotFound = 1510,
        Product_Modem_ModemNotFound = 1511,
        Product_Modem_InstallerNotSet = 1512,
        Product_Modem_TokenModemSuccess = 1513,
        Product_SimCart_SimCartNotFound = 1514,
        Product_SimCart_InstallerNotSet = 1515,
        Product_SimCart_TokenSimCartSuccess = 1516,
        Product_SimCart_IccIdScanNotFound = 1517,
        Product_Modem_InstallerReturnedModemSuccess = 1518,
        Product_Product_SetInstallerSuccess = 1519,
        Product_Modem_DuplicateModemDelivery = 1520,
        Product_SimCart_DuplicateSimCartDelivery = 1521,
        Product_SimCart_InstallerReturnedSimCartSuccess = 1522,
        Product_Modem_SwapInstallerSuccess = 1523,

        //Plan =>1700
        Plan_AddPlanSuccess = 1700,
        Plan_DuplicateName = 1701,

        //Customer =>1800
        Customer_AddCustomerSuccess = 1800,
        Customer_DuplicateCustomer = 1801,
        Customer_NotFound = 1802,
        Customer_NoAccessForChangeCustomerStatus = 1803,
        Customer_ChangeCustomerStatusSuccess = 1804,
        Customer_FactorNotFound = 1805,
        Customer_InstallerDoneSuccess = 1806,
        Customer_InstallerCancelSuccess = 1807,
        Customer_ActivationSuccess = 1808,
        Customer_CustomerRejectBeforeInstall = 1809,
        Customer_InstallerLowSignalSuccess = 1810,
        Customer_OperatorTakeWorkSuccess = 1811,
        Customer_ZeroCountForCustomerAdd = 1812,
        Customer_InstallerSendDocumentSuccess = 1813,
        Customer_EndCustomerJobsSuccess = 1814,
        Customer_UpdateCustomerSuccess = 1815,
        Customer_NoAccessToUpdate = 1816,
        Customer_SwapInstallerSuccess = 1817,
        Customer_CancelInstallerJobsSuccess = 1818,
        Customer_InstallationSendDataDoneSuccess = 1819,

        //Financial =>2000
        Financial_ConfirmationCustomerFactorSuccess = 2000,
        Financial_FactorNotFound = 2001,
        Financial_AddedPriceSuccess = 2002,
        Financial_UpdateAddedPriceSuccess = 2003,
        Financial_CustomerModemMacNotFound = 2004,
        Financial_DuplicatePosNumber = 2005,

        //System =>2500
        System_AddPeriodTimeSuccess = 2500,

        //Common =>3000
        Common_OperationSuccess = 3000,
    }
}
