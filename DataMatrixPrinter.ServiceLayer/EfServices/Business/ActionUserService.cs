using DataMatrixPrinter.ServiceLayer.Contracts.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.DataLayer.Context;
using System.Data.Entity;
using AutoMapper;

namespace DataMatrixPrinter.ServiceLayer.EfServices.Business
{
    public class ActionUserService : IActionUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ActionUser> _actionUser;
        private readonly IMappingEngine _mappingEngine;

        public ActionUserService(IUnitOfWork unitOfWork,
                            IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _actionUser = _unitOfWork.Set<ActionUser>();
        }
        public async Task<ActionUser> FindByRoleActionID(int RoleID, int ActionID)
        {
            var result = new ActionUser();
            try
            {
                result = _actionUser.Where(r => r.RoleID == RoleID && r.ActionID == ActionID).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
