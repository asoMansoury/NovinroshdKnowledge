using DataMatrixPrinter.DomainClasses.Entities.Base;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public class ControllerUsers:BaseEntity
    {
        public virtual int? ControllerID { get; set; }
        public virtual int? RoleID { get; set; }
        public virtual CustomRole Roles { get; set; }
        public virtual Controllers Controller { get; set; }
    }
}
