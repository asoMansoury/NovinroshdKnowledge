using System.Collections.Generic;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace DataMatrixPrinter.DomainClasses.Entities.Identites
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationUser()
        {            
            CreatedCity = new List<City>();
        }
        public virtual string Family { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageName { get; set; }
        public virtual bool isEnable { get; set; }
        public virtual ICollection<City> CreatedCity { get; set; }
        public virtual ICollection<Country> CreatedCountry { get; set; }


    }

}