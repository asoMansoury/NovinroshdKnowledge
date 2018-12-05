using DataMatrixPrinter.AutoMapper.ManualMapping;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ViewModels.Business.UserVM;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataMatrixPrinter.Controllers.Api
{
    public class UserApiController : BaseApiController
    {
        private IUserService _userService;
        private ISignInService _signInService;
        private IRoleService _roleService;

        public UserApiController(IRoleService roleService, IUserService userService, ISignInService signInService,  IActionUserService actionUserService, IActionService actionService) : base(userService, actionUserService, actionService)
        {
            this._roleService = roleService;
            this._signInService = signInService;
            this._userService = userService;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        public async Task<UserVM> RegisterUser()
        {

            var image = HttpContext.Current.Request.Files.Get(0);
            var formData = JsonConvert.DeserializeObject<UserVM>(HttpContext.Current.Request.Form["FormData"]);
            var Result = await _userService.RegisterUser(UserMapping.Map(formData), UserMapping.MapRoles(formData), image);
            return Result;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        public async Task<UserVM> Login(UserVM model)
        {
            var result = new UserVM();

            result = await _signInService.LoginAsync(model);
            return result;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public async Task<string> SignOut()
        {

            var owinContext = HttpContext.Current.GetOwinContext();
            owinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalBearer);
            return "/User/Login";
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [HttpPost]
        public DataSourceResult GetUsers([DataSourceRequest]DataSourceRequestUser request)
        {
            var result = new List<UserVM>();
            result = _userService.GetAllUsersInfoAsync(new DomainClasses.Entities.Identites.ApplicationUser { UserName = request.UserName, Email = request.Email, Name = request.Name, Family = request.Family }).Result;
            DataSourceResult result1 = result.ToDataSourceResult(request);
            return result1;
        }

        public async Task<UserVM> GetUserInformation(int Id)
        {
            var result = new UserVM();
            var data = await _userService.GetAllUsersInfoAsync(new DomainClasses.Entities.Identites.ApplicationUser { Id = Id });
            result = data.FirstOrDefault();
            result.UserRoles = _roleService.FindByIdAsync(Id).Result.ToList();
            return result;
        }

        public async Task<UserVM> Update()
        {

            var image = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files.Get(0) : null;
            var formData = JsonConvert.DeserializeObject<UserVM>(HttpContext.Current.Request.Form["FormData"]);

            var data = new UserVM();
            data = await _userService.UpdateUser(formData, image);
            return data;
        }

        
        public async Task<UserVM> UpdateProfile(UserVM model)
        {

            var data = new UserVM();
            ApplicationUser user = _userService.FindByIdAsync(model.Id).Result;
            user.Name = model.name;
            user.Family = model.family;
            user.Email = model.email;

            await _userService.UpdateAsync(user);
            return data;
        }

    }

    public class DataSourceRequestUser : DataSourceRequest
    {
        public string UserName
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Family
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }
    }
}