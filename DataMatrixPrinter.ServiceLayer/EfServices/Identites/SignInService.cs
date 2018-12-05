using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.ServiceHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DataMatrixPrinter.ViewModels.Business.UserVM;
using System;
using DataMatrixPrinter.DataLayer.Context;
using System.Data.Entity;
using System.Security.Principal;
using Microsoft.Owin.Security;
using DataMatrixPrinter.ViewModels.Business;
using DataMatrixPrinter.Common.CommonUtils;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class SignInService :
        SignInManager<ApplicationUser, int>,
        ISignInService
    {
        private readonly IUserService _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<ApplicationUser> _users;

        public SignInService(
            IUserService userManager,
            IUnitOfWork uow,
            IAuthenticationManager authenticationManager,
            IMappingEngine mappingEngine) :
            base((UserService)userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _mappingEngine = mappingEngine;
            this._uow = uow;
            _users = _uow.Set<ApplicationUser>();

        }

        public async Task<bool> SignOutAsync()
        {
            var currentUser = _userManager.GetCurrentUser();
            if (currentUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(currentUser.Id);
            }
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return currentUser != null;
        }

        public async Task<UserVM> LoginAsync(UserVM request)
        {
            var response = new UserVM();
            try
            {
                var user = await _userManager.FindAsync(request.nationalCode, request.password);

                //var user =  _userManager.FindUser(request.name, request.isRemember);
                if (user != null)
                {
                    await _userManager.UpdateSecurityStampAsync(user.Id);
                    var ident = await _userManager.GenerateUserIdentityAsync(user);
                    AuthenticationManager.SignIn(
                        new AuthenticationProperties() { IsPersistent = request.isRemember },
                        ident);
                }
                else
                {
                    response.isError = true;

                    response.Errors = ErrorVM.errorCustom(response.Errors, "نام کاربری یا پسورد وارد شده صحیح نمی باشد.");
                }
            }
            catch (Exception ex)
            {
                response.Errors = new Errors();
                response.Errors.EnumElements = CommonUtils.GetEnumDescription(EnumElements.ErrorAlert);
                response.Errors.Message = "خطا در ارتباط با دیتابیس، لطفا با مدیر سایت تماس برقرار نمایید.";
                response.Errors.pEnumElements = CommonUtils.GetEnumDescription(EnumElements.pErrorAlert);
                response.isError = true;
            }



            return response;
        }
    }
}