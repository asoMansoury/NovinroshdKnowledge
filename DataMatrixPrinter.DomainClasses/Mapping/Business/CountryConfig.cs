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
    public  class CountryConfig: EntityTypeConfiguration<Country>
    {
        public CountryConfig()
        {
            ToTable("Countries");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasRequired(t => t.Country).WithMany(d => d.Cities).HasForeignKey(t => t.CountryID).WillCascadeOnDelete(false);
            HasRequired(t => t.CreatedBy).WithMany(d => d.CreatedCountry).HasForeignKey(t => t.CreatedById).WillCascadeOnDelete(false);
            //Property(r => r.FullName).IsRequired().HasMaxLength(100);
            

            Property(r => r.CreatedOn).IsRequired();
            Property(r => r.CreatedIp).IsRequired().HasMaxLength(45);
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
