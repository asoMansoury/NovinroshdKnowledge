using DataMatrixPrinter.DomainClasses.Entities.Identites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.ViewModels.Business.UserVM
{
    public class UserVM:BaseEntityVm
    {
        public UserVM()
        {
            Errors = new Errors();
        }
        public string name { get; set; }
        public string family { get; set; }
        public string nationalCode { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string companies { get; set; }
        public bool isRemember { get; set; }
        public bool isPersistant { get; set; }
        public string roleName { get; set; }
        public string imageName { get; set; }
        public List<CustomRole> UserRoles { get; set; }
        public string ReturnUrl { get; set; }
        public virtual bool isEnable { get; set; }

    }

    
}
