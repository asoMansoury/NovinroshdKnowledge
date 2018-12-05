using DataMatrixPrinter.Common.Filters;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;

using DataMatrixPrinter.ViewModels.Business.UserVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIES.IJP.RX;

namespace DataMatrixPrinter.Controllers
{

    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly IRoleService _roleService;

        public UserController(IRoleService roleService,ISignInService signInService,IUserService userService, IActionUserService actionUserService, IActionService actionService) : base(userService, actionUserService, actionService)
        {
            this._userService = userService;
            this._signInService = signInService;
            this._roleService = roleService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Registry()
        {
            var result = new UserRegistryVM();
            //result.Roles = _roleService.GetAllCustomRolesAsync().Result;
            return View(result);
        }

        //[AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //[CustomAuthorize(Roles = "Administrator")]
        public ActionResult UsersManager()
        {
            var data = new UserRegistryVM();
            data.Roles = _roleService.GetAllCustomRolesAsync().Result;
            return View(data);
        }

        public ActionResult UserProfile()
        {
            var UserID = System.Web.HttpContext.Current.User.Identity.Name;
            var UserProfile = _userService.GetAllUsersInfoAsync(new DomainClasses.Entities.Identites.ApplicationUser { UserName=UserID}).Result.FirstOrDefault();
            return View(UserProfile);
        }
    }
}