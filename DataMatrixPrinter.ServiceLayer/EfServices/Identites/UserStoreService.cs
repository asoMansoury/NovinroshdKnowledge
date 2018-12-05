using System;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class UserStoreService :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        IUserStoreService
    {
        private readonly IUnitOfWork _context;
        private readonly IDbSet<CustomUserRole> _userRole;

        public UserStoreService(IUnitOfWork context)
            : base((ApplicationDbContext)context)
        {
            _context = context;
            _userRole = _context.Set<CustomUserRole>();
        }

        public CustomUserRole AddToUserRole(CustomUserRole input)
        {
           return _userRole.Add(input);
        }

        public CustomUserRole DeleteRole(CustomUserRole input)
        {
            _userRole.Remove(input);
            return input;
        }
    }
}