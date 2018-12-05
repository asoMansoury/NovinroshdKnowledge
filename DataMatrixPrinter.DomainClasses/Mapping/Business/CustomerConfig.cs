using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataMatrixPrinter.DomainClasses.Entities.Business;

namespace DataMatrixPrinter.DomainClasses.Mapping.Business
{
    public class CustomerConfig : EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            ToTable("Customers");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.CreatedBy).WithMany(d => d.CreatedCustomers).HasForeignKey(t => t.CreatedById).WillCascadeOnDelete(false);

            Property(r => r.FullName).IsRequired().HasMaxLength(100);

            Property(r => r.CreatedOn).IsRequired();
            Property(r => r.CreatedIp).IsRequired().HasMaxLength(45);
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
