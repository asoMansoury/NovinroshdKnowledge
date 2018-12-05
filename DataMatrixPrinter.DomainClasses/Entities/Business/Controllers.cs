using DataMatrixPrinter.DomainClasses.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public class Controllers: BaseEntity
    {
        public virtual string ControllerName { get; set; }
        public virtual ICollection<ActionMethods> Methods { get; set; }
        public virtual ICollection<ControllerUsers> ControllerUsers { get; set; }
    }
}
