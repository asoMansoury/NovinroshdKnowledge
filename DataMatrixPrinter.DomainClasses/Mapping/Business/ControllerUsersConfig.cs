using DataMatrixPrinter.DomainClasses.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Mapping.Business
{
    public class ControllerUsersConfig:EntityTypeConfiguration<ControllerUsers>
    {
        public ControllerUsersConfig()
        {
            ToTable("ControllerUsers");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasRequired(t => t.CreatedBy).WithMany(d => d.CreatedCity).HasForeignKey(t => t.CreatedById).WillCascadeOnDelete(false);
            HasRequired(t => t.Controller).WithMany(d => d.ControllerUsers).HasForeignKey(t => t.ControllerID).WillCascadeOnDelete(false);
            HasRequired(t => t.Roles).WithMany(r => r.Controllers).HasForeignKey(t => t.RoleID).WillCascadeOnDelete(false);

            Property(r => r.CreatedOn).IsRequired();
            Property(r => r.CreatedIp).IsRequired().HasMaxLength(45);
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
