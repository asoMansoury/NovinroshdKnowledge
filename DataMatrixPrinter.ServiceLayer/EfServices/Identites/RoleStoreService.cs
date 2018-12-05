using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataMatrixPrinter.DataLayer.Context;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.ServiceLayer.Contracts.Identites;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Identites
{
    public class RoleStoreService :
        RoleStore<CustomRole, int, CustomUserRole>,
        IRoleStoreService
    {
        private readonly IUnitOfWork _context;

        public RoleStoreService(IUnitOfWork context)
            : base((DbContext)context)
        {
            _context = context;
        }

        public IList<CustomRole> FindUserRoles(int userId)
        {
            throw new NotImplementedException();
        }
    }
}