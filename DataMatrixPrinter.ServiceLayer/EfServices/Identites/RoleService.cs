using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using DataMatrixPrinter.ServiceLayer.ServiceHelper;
using Microsoft.AspNet.Identity;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class RoleService :
        RoleManager<CustomRole, int>,
        IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRoleStoreService _roleStore;
        private readonly IMappingEngine _mappingEngine;
        private readonly IDbSet<ApplicationUser> _users;
        private readonly IDbSet<CustomRole> _roles;
        private readonly IUserStoreService _userRole;
        public RoleService(
            IUserStoreService userRole,
            IUnitOfWork uow,
            IRoleStoreService roleStore,
            IMappingEngine mappingEngine)
            : base((IRoleStore<CustomRole, int>)roleStore)
        {
            _uow = uow;
            this._userRole = userRole;
            _roleStore = roleStore;
            _users = _uow.Set<ApplicationUser>();
            _roles = _uow.Set<CustomRole>();
            _mappingEngine = mappingEngine;
        }

        public CustomRole FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }

        public IdentityResult CreateRole(CustomRole role)
        {
            return this.Create(role); // RoleManagerExtensions
        }

        public IList<CustomUserRole> GetCustomUsersInRole(string roleName)
        {
            return this.Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
            // = this.FindByName(roleName).Users
        }

        public IList<ApplicationUser> GetApplicationUsersInRole(string roleName)
        {
            var roleUserIdsQuery = from role in this.Roles
                                  where role.Name == roleName
                                  from user in role.Users
                                  select user.UserId;
            return _users.Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
                         .ToList();
        }

        public IList<CustomRole> FindUserRoles(int userId)
        {
            //var userRolesQuery = from role in this.Roles
            //            from user in role.Users
            //            where user.UserId == userId
            //            select role;
            var userRoleQueryies = _roles.Where(r => r.Users.Any(k => k.UserId == userId)).Include(e => e.Users).ToList();
            return /*userRolesQuery.OrderBy(x => x.Name)*/userRoleQueryies.ToList();
        }

        public string[] GetRolesForUser(int userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.Select(x => x.Name).ToArray();
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in this.Roles
                        where role.Name == roleName
                        from user in role.Users
                        where user.UserId == userId
                        select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        public Task<List<CustomRole>> GetAllCustomRolesAsync()
        {
            return this.Roles.ToListAsync();
        }

        //public async Task<AddRoleRespone> AddRoleAsync(AddRoleRequest request)
        //{
        //    var role = _mappingEngine.Map<CustomRole>(request);
        //    var result = await this.CreateAsync(role);
        //    var response = _mappingEngine.Map<AddRoleRespone>(role);
        //    if (result.Succeeded)
        //    {
        //        response.SetResultCode(ServiceResponseKey.User_RoleSuccess);
        //    }

        //    var error = result.Errors.FirstOrDefault();
        //    if (error != null)
        //    {
        //        response.SetResultCode(ServiceResponseKey.User_RoleDuplicateName);
        //    }

        //    return
        //        response;
        //}

        //public async Task<List<SelectRoleViewModel>> GetAllRoleAsync()
        //{
        //    return
        //        await _roles
        //            .Select(z => new SelectRoleViewModel()
        //            {
        //                Id = z.Id,
        //                Name = z.Name
        //            }).AsNoTracking().ToListAsync();
        //}

        //public async Task<List<SelectUserInRoleViewModel>> GetAllUserInRoleAsync()
        //{
        //    var roles = await this.Roles.Include(z => z.Users).ToListAsync();
        //    var result = new List<SelectUserInRoleViewModel>();
        //    foreach (var role in roles)
        //    {
        //        result.AddRange(role.Users.Select(user => _users.Find(user.UserId))
        //            .Select(selectUser => new SelectUserInRoleViewModel()
        //            {
        //                RoleName = role.Name,
        //                UserId = selectUser.Id,
        //                FullName = selectUser.FullName,
        //                UserName = selectUser.UserName,
        //            }));
        //    }
        //    return result;
        //}

        public async Task<CustomRole> GetRoleWithUserAsync(string roleName)
        {
            return await _roles
                .Include(z => z.Users)
                .FirstOrDefaultAsync(z => z.Name == roleName);
        }

       async  Task<List<CustomRole>> IRoleService.FindByIdAsync(int roleId)
        {

            var Roles = _roles.Where(z => z.Users.Any(t => t.UserId == roleId)).Include(tt=>tt.Users).ToList();
            return Roles;
        }
    }
}