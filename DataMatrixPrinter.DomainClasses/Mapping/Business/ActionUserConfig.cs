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
    public  class ActionUserConfig:EntityTypeConfiguration<ActionUser>
    {
        public ActionUserConfig()
        {
            ToTable("ActionUser");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.Action).WithMany(d => d.ActionUsers).HasForeignKey(t => t.ActionID).WillCascadeOnDelete(false);
            HasRequired(t => t.Roles).WithMany(r => r.Actions).HasForeignKey(t => t.RoleID).WillCascadeOnDelete(false);

            Property(r => r.CreatedOn).IsRequired();
            Property(r => r.CreatedIp).IsRequired().HasMaxLength(45);
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
