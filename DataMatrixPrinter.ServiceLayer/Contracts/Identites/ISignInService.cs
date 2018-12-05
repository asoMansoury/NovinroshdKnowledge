using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DataMatrixPrinter.ViewModels.Business.UserVM;
using DataMatrixPrinter.DomainClasses.Entities.Identites;

namespace DataMatrixPrinter.ServiceLayer.Contracts.Identites
{
    public interface ISignInService: IDisposable
    {
        //Task<bool> SignOutAsync();
        //Task<LoginRespone> LoginAsync(LoginRequest request);
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        Task<UserVM> LoginAsync(UserVM request);
        


    }
}