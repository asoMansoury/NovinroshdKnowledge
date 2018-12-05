using System;
using DataMatrixPrinter.DomainClasses.Entities.Identites;

namespace DataMatrixPrinter.DomainClasses.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual string CreatedIp { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual int CreatedById { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        
    }
}