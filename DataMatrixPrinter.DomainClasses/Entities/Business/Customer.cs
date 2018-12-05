using DataMatrixPrinter.DomainClasses.Entities.Base;

namespace DataMatrixPrinter.DomainClasses.Entities.Business
{
    public class Customer : BaseEntity
    {
        public virtual string FullName { get; set; }
    }
}