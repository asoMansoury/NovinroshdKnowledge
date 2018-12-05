using DataMatrixPrinter.DomainClasses.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public class ActionMethods:BaseEntity
    {
        public virtual string ActionName { get; set; }
        public virtual int? ControllerID { get; set; }
        public virtual Controllers Controller { get; set; }
        public virtual ICollection<ActionUser> ActionUsers { get; set; }
    }
}
