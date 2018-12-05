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
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ActionMethods> _actionUser;
        private readonly IMappingEngine _mappingEngine;
        public ActionService(IUnitOfWork unitOfWork,
                            IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _actionUser = _unitOfWork.Set<ActionMethods>();
        }
        public ActionMethods FindActionIDByName(string actionName)
        {
            var result = new ActionMethods();
            try
            {
                result = _actionUser.Where(r => r.ActionName.Trim().ToLower() == actionName).Include(er=>er.ActionUsers).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
