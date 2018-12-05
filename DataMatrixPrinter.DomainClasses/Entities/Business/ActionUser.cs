using DataMatrixPrinter.DomainClasses.Entities.Base;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public  class ActionUser:BaseEntity
    {
        public virtual int? ActionID { get; set; }
        public virtual int? RoleID { get; set; }
        public virtual CustomRole Roles { get; set; }
        public virtual ActionMethods Action { get; set; }
    }
}
