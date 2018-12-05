using DataMatrixPrinter.DomainClasses.Entities.Business;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace DataMatrixPrinter.DomainClasses.Entities.Identites
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }

        public virtual ICollection<ControllerUsers> Controllers { get; set; }
        public virtual ICollection<ActionUser> Actions { get; set; }
    }
}