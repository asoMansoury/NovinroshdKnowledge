﻿using DataMatrixPrinter.DomainClasses.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.DomainClasses.Mapping.Business
{
    public  class ActionsMethodConfig: EntityTypeConfiguration<ActionMethods>
    {
        public ActionsMethodConfig()
        {
            ToTable("ActionMethod");

            HasKey(d => d.Id);

            Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(t => t.Controller).WithMany(d => d.Methods).HasForeignKey(d => d.ControllerID).WillCascadeOnDelete(false);

            Property(r => r.CreatedById).IsOptional();
            Property(r => r.CreatedOn).IsOptional();
            Property(r => r.CreatedIp).IsOptional();
            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
