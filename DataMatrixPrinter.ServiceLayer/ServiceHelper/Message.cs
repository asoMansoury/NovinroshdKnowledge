using System.Collections.Generic;

namespace DataMatrixPrinter.ServiceLayer.ServiceHelper
{
    public static class Message
    {
        public static string Get(ServiceResponseKey code)
        {
            string value;
            Messages.TryGetValue(code, out value);
            return value;
        }

        private static readonly Dictionary<ServiceResponseKey, string> Messages = new Dictionary
            <ServiceResponseKey, string>()
            {
                //Account
                {ServiceResponseKey.User_LoginSuccess, "ورود با موفقیت انجام شد لطفا منتظر بمانید ..."},
                {ServiceResponseKey.User_LoginLockedOut, "اکانت شما قفل می باشد"},
                {ServiceResponseKey.User_LoginRequiresVerification, "ورود دو مرحله ای"},
                {ServiceResponseKey.User_LoginFailure, "نام کاربری یا کلمه عبور معتبر نمی باشد"},
                {ServiceResponseKey.User_RegisterSuccess, "ثبت کاربر با موفقیت انجام شد"},
                {ServiceResponseKey.User_RegisterDuplicateUserName, "نام کاربری شما قبلا در سیستم ثبت شده است"},
                {ServiceResponseKey.User_RegisterDuplicateEmail, "ایمیل شما قبلا در سیستم ثبت شده است"},
                {ServiceResponseKey.User_RoleSuccess, "ثبت نقش با موفقیت انجام شد"},
                {ServiceResponseKey.User_RoleDuplicateName, "این نقش قبلا در سیستم ثبت شده است"},
                {ServiceResponseKey.User_UserInRoleSuccess, "ثبت کاربر در نقش انتخابی با موفقیت انجام شد"},
                {ServiceResponseKey.User_UserInRoleDuplicate, "کاربر قبلا در این نقش اضافه شده است"},
                {ServiceResponseKey.User_UpdatePersonalInfoSuccess, "ویرایش اطلاعات شما با موفقیت انجام شد"},
                {ServiceResponseKey.User_UpdatePasswordSuccess, "ویرایش کلمه عبور با موفقیت انجام شد"},
                {ServiceResponseKey.User_PrePasswordIsWrong, "کلمه عبور اشتباه است"},

                //Product
                {ServiceResponseKey.Product_Modem_DuplicateModem, "در این اکسل مودم تکراری یافت شد"},
                {ServiceResponseKey.Product_Modem_ExcelNotEqualWithCount, "تعداد مودم وارد شده با تعداد مودم های اکسل برابر نیست"},
                {ServiceResponseKey.Product_Modem_AddModemSuccess, "مودم ها با موفقیت ثبت گردید"},
                {ServiceResponseKey.Product_SimCart_DuplicateName, "اپراتور سیم کارت قبلا در سیستم ثبت گردیده است"},
                {ServiceResponseKey.Product_SimCart_AddOperatorSuccess, "اپراتور سیم کارت با موفقیت ثبت گردید"},
                {ServiceResponseKey.Product_SimCart_ExcelNotEqualWithCount, "تعداد سیم کارت وارد شده با تعداد سیم کارت های اکسل برابر نیست"},
                {ServiceResponseKey.Product_SimCart_DuplicateSimCart, "در این اکسل سیم کارت تکراری یافت شد"},
                {ServiceResponseKey.Product_SimCart_AddSimCartSuccess, "سیم کارت ها با موفقیت ثبت گردید"},
                {ServiceResponseKey.Product_Modem_MacNotFound, "Mac ادرس وارد شده در سیستم یافت نشد"},
                {ServiceResponseKey.Product_Modem_SerialNotFound, "سریال وارد شده در سیستم یافت نشد"},
                {ServiceResponseKey.Product_SimCart_IccIdNotFound, "ICCID وارد شده در سیستم یافت نشد"},
                {ServiceResponseKey.Product_Modem_ModemNotFound, "مودم در سیستم یافت نشد"},
                {ServiceResponseKey.Product_Modem_InstallerNotSet, "این مودم به کارشناس نصبی تحویل داده نشده است"},
                {ServiceResponseKey.Product_Modem_TokenModemSuccess, "مودم با موفقیت از کارشناس نصب دریافت شد"},
                {ServiceResponseKey.Product_SimCart_SimCartNotFound, "سیم کارت در سیستم یافت نشد"},
                {ServiceResponseKey.Product_SimCart_InstallerNotSet, "این سیم کارت به کارشناس نصبی تحویل داده نشده است"},
                {ServiceResponseKey.Product_SimCart_TokenSimCartSuccess, "سیم کارت با موفقیت از کارشناس نصب دریافت شد"},
                {ServiceResponseKey.Product_SimCart_IccIdScanNotFound, "ICCIDScan وارد شده در سیستم یافت نشد"},
                {ServiceResponseKey.Product_Modem_InstallerReturnedModemSuccess, "مودم با موفقیت به عنوان مرجوعی اعلام شد"},
                {ServiceResponseKey.Product_Product_SetInstallerSuccess, "تخصیص به کارشناس نصب با موفقیت انجام شد"},
                {ServiceResponseKey.Product_Modem_DuplicateModemDelivery, "مودم تکراری یافت شد"},
                {ServiceResponseKey.Product_SimCart_DuplicateSimCartDelivery, "سیم کارت تکراری یافت شد"},
                {ServiceResponseKey.Product_SimCart_InstallerReturnedSimCartSuccess, "سیم کارت با موفقیت به عنوان مرجوعی اعلام شد"},
                {ServiceResponseKey.Product_Modem_SwapInstallerSuccess, "جابجایی مودم با موفقیت انجام شد"},

                //Plan
                {ServiceResponseKey.Plan_AddPlanSuccess, "طرح جدید با موفقیت ثبت گردید"},
                {ServiceResponseKey.Plan_DuplicateName, "این نام قبلا در سیستم ثبت شده است"},

                //Customer
                {ServiceResponseKey.Customer_AddCustomerSuccess, "مشتری جدید با موفقیت ثبت گردید"},
                {ServiceResponseKey.Customer_DuplicateCustomer, "این مشتری قبلا توسط همکار شما در سیستم ثبت شده است"},
                {ServiceResponseKey.Customer_NotFound, "مشتری مورد نظر پیدا نشد"},
                {ServiceResponseKey.Customer_CustomerRejectBeforeInstall, "مشتری ثبت گردید و به لیست کنسل شده ها اضافه گردید"},
                {ServiceResponseKey.Customer_NoAccessForChangeCustomerStatus, "در حال حاضر شما نمیتوانید وضعیت مشتری را تغییر بدهید"},
                {ServiceResponseKey.Customer_ChangeCustomerStatusSuccess, "تغییر وضعیت مشتری با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_FactorNotFound, "فاکتور مشتری پیدا نشد"},
                {ServiceResponseKey.Customer_InstallerDoneSuccess, "نصب با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_InstallerCancelSuccess, "نصب با موفقیت کنسل شد"},
                {ServiceResponseKey.Customer_ActivationSuccess, "فعال سازی مشتری با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_InstallerLowSignalSuccess, "ضعف سیگنال برای مشتری ثبت گردید"},
                {ServiceResponseKey.Customer_OperatorTakeWorkSuccess, "دریافت کار با موفقیت برای شما ثبت گردید"},
                {ServiceResponseKey.Customer_ZeroCountForCustomerAdd, "موجودی اجناس شما برای ثبت این درخواست کافی نیست"},
                {ServiceResponseKey.Customer_InstallerSendDocumentSuccess, "فرستادن مدارک با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_EndCustomerJobsSuccess, "نصب مشتری با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_UpdateCustomerSuccess, "مشتری با موفقیت ویرایش گردید"},
                {ServiceResponseKey.Customer_NoAccessToUpdate, "شما دسترسی ویرایش این مشتری رو ندارید"},
                {ServiceResponseKey.Customer_SwapInstallerSuccess, "جابجایی نصب با موفقیت انجام شد"},
                {ServiceResponseKey.Customer_CancelInstallerJobsSuccess, "نصب انتخابی کنسل شد"},
                {ServiceResponseKey.Customer_InstallationSendDataDoneSuccess, "ارسال اطلاعات با موفقیت انجام شد"},

                //Financial
                {ServiceResponseKey.Financial_ConfirmationCustomerFactorSuccess, "تایید صورت حساب با موفقیت انجام شد"},
                {ServiceResponseKey.Financial_FactorNotFound, "فاکتور مشتری پیدا نشد"},
                {ServiceResponseKey.Financial_AddedPriceSuccess, "ثبت اطلاعات موفقیت انجام شد"},
                {ServiceResponseKey.Financial_UpdateAddedPriceSuccess, "ویرایش اطلاعات موفقیت انجام شد"},
                {ServiceResponseKey.Financial_CustomerModemMacNotFound, "Mac وارد شده با Mac ثبت شده در سیستم یکی نیست"},
                {ServiceResponseKey.Financial_DuplicatePosNumber, "شماره تراکنش وارد شده تکراری می باشد"},

                //System
                {ServiceResponseKey.System_AddPeriodTimeSuccess, "ثبت بازه با موفقیت انجام شد"},

                //Common
                {ServiceResponseKey.Common_OperationSuccess, "عملیات با موفقیت انجام شد"},
            };
    }
}