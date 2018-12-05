using DataMatrixPrinter.DomainClasses.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ServiceLayer.Contracts.Business
{
    public interface IActionUserService
    {
        Task<ActionUser> FindByRoleActionID(int RoleID, int ActionID);
    }
}
