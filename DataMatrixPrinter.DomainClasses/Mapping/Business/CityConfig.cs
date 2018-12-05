using DataMatrixPrinter.DomainClasses.Entities.Business;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataMatrixPrinter.DomainClasses.Mapping.Business
{
    public class CityConfig: EntityTypeConfiguration<City>
    {
        public CityConfig()
        {
            ToTable("Cities");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.CreatedBy).WithMany(d => d.CreatedCity).HasForeignKey(t => t.CreatedById).WillCascadeOnDelete(false);
            HasRequired(t => t.Country).WithMany(d => d.Cities).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);

            Property(r => r.CityCode).IsRequired().HasMaxLength(50);
            Property(r => r.CityName).IsRequired().HasMaxLength(100);

            Property(r => r.CreatedOn).IsRequired();
            Property(r => r.CreatedIp).IsRequired().HasMaxLength(45);
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
