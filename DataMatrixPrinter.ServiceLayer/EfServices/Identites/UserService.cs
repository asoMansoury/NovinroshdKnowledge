using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.ServiceHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using System.Security.Cryptography;
using System.Net;
using Microsoft.Owin.Security;
using DataMatrixPrinter.ViewModels.Business.UserVM;
using System.Linq.Expressions;
using DataMatrixPrinter.AutoMapper.ManualMapping;
using DataMatrixPrinter.ViewModels.Business;
using DataMatrixPrinter.Common.CommonUtils;
using DataMatrixPrinter.Common.Filters;
using System.Web;
using DataMatrixPrinter.Common.Extensions;
using System.IO;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class UserService
        : UserManager<ApplicationUser, int>,
        IUserService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IMappingEngine _mappingEngine;
        private readonly IRoleService _roleManager;
        private readonly IUserStoreService _store;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<ApplicationUser> _users;
        private readonly Lazy<Func<IIdentity>> _identity;
        private ApplicationUser _user;
        private IUserStoreService _userRole;
        private IRoleService _roleService;
        public UserService(IUserStoreService userRole,
            IAuthenticationManager authenticationManager,
            IUserStoreService store,
            IUnitOfWork uow,
            Lazy<Func<IIdentity>> identity, // For lazy loading -> Controller gets constructed before the HttpContext has been set by ASP.NET.
            IRoleService roleManager,
            IDataProtectionProvider dataProtectionProvider,
            IIdentityMessageService smsService,
            IIdentityMessageService emailService,
            IMappingEngine mappingEngine,
            IRoleService roleService)
            : base((IUserStore<ApplicationUser, int>)store)
        {
            this._authenticationManager = authenticationManager;
            _userRole = userRole;
            this._roleService = roleService;
            _store = store;
            _uow = uow;
            _identity = identity;
            _users = _uow.Set<ApplicationUser>();
            _roleManager = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            _mappingEngine = mappingEngine;
            this.SmsService = smsService;
            this.EmailService = emailService;
            createApplicationUserManager();
        }



        public ApplicationUser FindById(int userId)
        {
            return _users.Find(userId);
        }

        public ApplicationUser FindByUserName(string name)
        {
            return _users.Where(r => r.UserName == name).Include(r => r.Roles).FirstOrDefault();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);

            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("FullName", applicationUser.FullName));
            //userIdentity.AddClaim(new Claim("OrgLevel", applicationUser.OrgLevel));
            //userIdentity.AddClaim(applicationUser.UserPicture == null
            //    ? new Claim("UserPicture", "")
            //    : new Claim("UserPicture", applicationUser.UserPicture));

            return userIdentity;
        }

        public Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return this.Users.ToListAsync();
        }

        public ApplicationUser FindUser(string userNae, bool isremember)
        {
            var user = this.Users.Where(r => r.UserName == userNae).FirstOrDefault();
            return user;
        }

        public ApplicationUser GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()).ConfigureAwait(false));
        }

        public int GetCurrentUserId()
        {
            return _identity.Value().GetUserId<int>();
        }

        //public async Task<AddUserResponse> RegisterAsync(AddUserRequest entityViewModel)
        //{
        //    var user = _mappingEngine.Map<ApplicationUser>(entityViewModel);
        //    var result = await CreateAsync(user, entityViewModel.Password).ConfigureAwait(false);
        //    var response = _mappingEngine.Map<AddUserResponse>(user);
        //    if (result.Succeeded)
        //    {
        //        response.SetResultCode(ServiceResponseKey.User_RegisterSuccess);
        //    }

        //    var error = result.Errors.FirstOrDefault();
        //    if (error != null)
        //    {
        //        response.SetResultCode(error.StartsWith("Name")
        //            ? ServiceResponseKey.User_RegisterDuplicateUserName
        //            : ServiceResponseKey.User_RegisterDuplicateEmail);
        //    }

        //    return
        //        response;
        //}

        //public async Task<List<SelectUserViewModel>> GetAllUserAsync()
        //{
        //    return
        //        await _users
        //        .Select(z => new SelectUserViewModel
        //        {
        //            Id = z.Id,
        //            FullName = z.FullName,
        //            UserName = z.UserName,
        //            Email = z.Email,
        //            Gender = (int)z.Gender,
        //            PhoneNumber = z.PhoneNumber,
        //            OrgLevel = z.OrgLevel,
        //            TwoFactorEnabled = z.TwoFactorEnabled
        //        }).AsNoTracking().ToListAsync();
        //}

        //public async Task<List<SelectUserDropDown>> GetAllUserForDropDownAsync()
        //{
        //    return
        //        await _users
        //            .Select(z => new SelectUserDropDown
        //            {
        //                Id = z.Id,
        //                FullName = z.FullName,
        //                UserName = z.UserName,
        //            }).AsNoTracking().ToListAsync();
        //}

        //public async Task<AddUserInRoleRespone> AddUserInRoleAsync(AddUserInRoleRequest request)
        //{
        //    var result = await this.AddToRolesAsync(request.UserId, request.RoleNames.ToArray()).ConfigureAwait(false);
        //    var response = _mappingEngine.Map<AddUserInRoleRespone>(request);
        //    if (result.Succeeded)
        //    {
        //        response.SetResultCode(ServiceResponseKey.User_UserInRoleSuccess);
        //    }

        //    var error = result.Errors.FirstOrDefault();
        //    if (error != null)
        //    {
        //        response.SetResultCode(ServiceResponseKey.User_UserInRoleDuplicate);
        //    }

        //    return
        //        response;
        //}

        //public async Task<List<SelectUserForListItemViewModel>> GetAvailableInstallerForListItemAsync()
        //{
        //    var role = await _roleManager.GetRoleWithUserAsync("Installer");

        //    if (role == null)
        //        return new List<SelectUserForListItemViewModel>();

        //    return role.Users.Select(user => FindById(user.UserId))
        //        .OrderBy(z => z.FullName)
        //        .Select(selectedUser => new SelectUserForListItemViewModel
        //        {
        //            Id = selectedUser.Id,
        //            FullName = selectedUser.FullName
        //        }).ToList();
        //}

        //public async Task<bool> SendSmsForCustomer(SendSmsCustomerViewModel entityViewModel)
        //{
        //    var sendedText = $"{entityViewModel.GenderTypeText} " +
        //                     $"{entityViewModel.CusomerFullName} کارشناس نصب ما " +
        //                     $"{entityViewModel.InstallerName} مورخ " +
        //                     $"{"15/5/5"} به منزل شما اعزام خواهد شد";

        //    await SmsService.SendAsync(new IdentityMessage
        //    {
        //        Body = sendedText,
        //        Destination = entityViewModel.Number
        //    });

        //    return
        //        true;
        //}

        //public async Task<SelectCurrentUserViewModel> GetCurrentUserViewModelAsync()
        //{
        //    var userId = GetCurrentUserId();
        //    return await _users.Select(z => new SelectCurrentUserViewModel
        //    {
        //        Id = z.Id,
        //        FullName = z.FullName,
        //        UserName = z.UserName,
        //        Gender = z.Gender,
        //        PhoneNumber = z.PhoneNumber,
        //        Email = z.Email
        //    }).FirstOrDefaultAsync(z => z.Id == userId);
        //}

        //public async Task<UpdateUserRespone> UpdatePersonalUserInfoAsync(UpdateUserRequest request)
        //{
        //    var user = await GetCurrentUserAsync();
        //    user.FullName = request.FullName;
        //    user.Email = request.Email;
        //    user.PhoneNumber = request.PhoneNumber;
        //    if(request.UserPicture != null)
        //        user.UserPicture = request.UserPicture;
        //    await UpdateAsync(user);

        //    var response = _mappingEngine.Map<UpdateUserRespone>(user);
        //    response.SetResultCode(ServiceResponseKey.User_UpdatePersonalInfoSuccess);

        //    return response;
        //}

        //public async Task<UpdateUserRespone> UpdatePasswordUserAsync(UpdateUserRequest request)
        //{
        //    var userId = GetCurrentUserId();
        //    var response = new UpdateUserRespone();

        //    var result = await ChangePasswordAsync(userId, request.PrePassword, request.Password);
        //    response.SetResultCode(result.Succeeded
        //        ? ServiceResponseKey.User_UpdatePasswordSuccess
        //        : ServiceResponseKey.User_PrePasswordIsWrong);

        //    return response;
        //}

        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PhoneNumber != null;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<UserService, ApplicationUser, int>(
                         validateInterval: TimeSpan.FromSeconds(0),
                         regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
                         getUserIdCallback: claimsIdentity => claimsIdentity.GetUserId<int>());
        }

        private void createApplicationUserManager()
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            //this.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(dataProtector);
            }
        }




        public async Task<UserVM> RegisterUser(ApplicationUser Model, List<CustomRole> roles, HttpPostedFile File)
        {
            var Result = new UserVM();
            Model.EmailConfirmed = true;
            Model.PhoneNumberConfirmed = true;
            try
            {
                Model.ImageName = "Images\\Profile\\" + Model.UserName + File.FileName;
                File.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Images\\Profile\\" + Model.UserName + File.FileName);
                var result = this.Create(Model, Model.PasswordHash);
                if (result.Succeeded)
                {
                    roles.ForEach(rl =>
                    {
                        //rl.Id = Model.Id;
                        var customRole = new CustomUserRole();
                        customRole.RoleId = rl.Id;
                        customRole.UserId = Model.Id;
                        _userRole.AddToUserRole(customRole);
                    });
                    _userRole.AddToUserRole(new CustomUserRole { UserId = Model.Id, RoleId = 5 });

                    var waitdb = await _uow.SaveAllChangesAsync();
                    Result.isError = false;
                    Result = AutoMapper.ManualMapping.UserMapping.Map(Model);
                }
                else
                {
                    Result.isError = true;
                    result.Errors.ToList().ForEach(er =>
                    {
                        Result.isError = true;
                        Result.Errors = ErrorVM.errorCustom(Result.Errors, er);
                    });
                }
            }
            catch (Exception ex)
            {
                Result.isError = true;
                Result.Errors = ErrorVM.errorCustom(Result.Errors, ex.Message);
            }
            return Result;

        }

        public async Task<List<UserVM>> GetAllUsersInfoAsync(ApplicationUser predicate)
        {
            var result = new List<UserVM>();
            var _UserID = HttpContext.Current != null ? HttpContext.Current.GetUserId() : (int?)null;
            //var Role = _userService.GetRolesAsync(_UserID.Value).Result;
            var Roles = _roleService.FindUserRoles(_UserID.Value).ToList().Select(r => r.Id).ToList();
            try
            {
                var query = queryGenerate(predicate);
                if (Roles.Any(r => r == 1))
                {
                    var lstUsers = predicate != null ? _users.Where(query)
                           .ToList() : _users.Select(r => r).ToList();

                    result = UserMapping.Map(lstUsers);
                }else
                {
                    
                    var lstUsersResult = new List<ApplicationUser>();

                    HashSet<ApplicationUser> resultUsers = new HashSet<ApplicationUser>(lstUsersResult);
                    result = UserMapping.Map(resultUsers.ToList());
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public async Task<UserVM> UpdateUser(UserVM input, HttpPostedFile imageFile=null)
        {
            var result = new UserVM();
            try
            {
                var user = _users.Where(r => r.UserName == input.nationalCode).FirstOrDefault();
                var userRoles = _roleManager.FindByIdAsync(user.Id).Result.ToList();
                user.Name = string.IsNullOrEmpty(input.name) ? user.Name : input.name;
                user.Family = string.IsNullOrEmpty(input.family) ? user.Family : input.family;
                user.Email = string.IsNullOrEmpty(input.email) ? user.Email : input.email;
                if (!string.IsNullOrEmpty(input.password))
                    if (!string.IsNullOrWhiteSpace(input.password))
                        user.PasswordHash = input.password;

                if (imageFile != null) {
                    try
                    {
                        string mainPath = AppDomain.CurrentDomain.BaseDirectory;
                        var FileName = mainPath + user.ImageName;
                        File.Delete(FileName);
                    }
                    catch (Exception ex)
                    {

                       
                    }

                    user.ImageName = "Images\\Profile\\" + user.UserName + imageFile.FileName;
                    imageFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Images\\Profile\\" + user.UserName + imageFile.FileName);
                }

                _uow.MarkAsChanged(user);

                userRoles.SelectMany(r=>r.Users).Where(r=>r.UserId==user.Id).ToList().ForEach(r =>
                  {
                      if (!input.UserRoles.Any(d => d.Id == r.RoleId))
                      {
                          _userRole.DeleteRole(r);
                      }
                  });

                input.UserRoles.ToList().ForEach(fr =>
                {
                    if (!userRoles.SelectMany(t => t.Users).Any(r => r.RoleId == fr.Id))
                        _userRole.AddToUserRole(new CustomUserRole { RoleId = fr.Id, UserId = user.Id });
                });
                result.Errors = new Errors();
                result.Errors.EnumElements = CommonUtils.GetEnumDescription(EnumElements.SuccessAlert);
                result.Errors.Message = "عملیات با موفقیت انجام شد.";
                result.Errors.pEnumElements = CommonUtils.GetEnumDescription(EnumElements.pSuccessAlert);

                await _uow.SaveAllChangesAsync();
            }
            catch (Exception ex)
            {
                result.isError = true;
                result.Errors = ErrorVM.errorCustom(result.Errors, ex.Message);
            }
            return result;
        }


        private Expression<Func<ApplicationUser, bool>> queryGenerate(ApplicationUser model)
        {
            var queryBuilder = new ExpressionDynamicBuilder<ApplicationUser>();
            queryBuilder.Statements = new List<FilterStatement>();
            if (!string.IsNullOrEmpty(model.Name))
            {
                queryBuilder.Statements.Add(new FilterStatement
                {
                    filterStatementConnector = FilterStatementConnector.And,
                    operatio = Operation.Contains,
                    PropertyName = "Name",
                    Value = model.Name
                });
            }

            if (!string.IsNullOrEmpty(model.Family))
            {
                queryBuilder.Statements.Add(new FilterStatement
                {
                    filterStatementConnector = FilterStatementConnector.And,
                    operatio = Operation.Contains,
                    PropertyName = "Family",
                    Value = model.Family
                });
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                queryBuilder.Statements.Add(new FilterStatement
                {
                    filterStatementConnector = FilterStatementConnector.And,
                    operatio = Operation.Contains,
                    PropertyName = "Email",
                    Value = model.Email
                });
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                queryBuilder.Statements.Add(new FilterStatement
                {
                    filterStatementConnector = FilterStatementConnector.And,
                    operatio = Operation.Contains,
                    PropertyName = "UserName",
                    Value = model.UserName
                });
            }

            if (model.Id != 0)
            {
                queryBuilder.Statements.Add(new FilterStatement
                {
                    filterStatementConnector = FilterStatementConnector.And,
                    operatio = Operation.EqualTo,
                    PropertyName = "Id",
                    Value = model.Id
                });
            }
            var result = queryBuilder.BuildExpression();
            return result;
        }
    }
}