using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.Common.Filters
{
    public interface IFilter<TClass> where TClass:class
    {
        List<FilterStatement> Statements { get; set; }
        Expression<Func<TClass,bool>> BuildExpression();
        
    }

    public interface IFilterStatement
    {
        string PropertyName { get; set; }
        object Value { get; set; }
        Operation operatio { get; set; }
        FilterStatementConnector filterStatementConnector { get; set; }
    }

    public enum Operation
    {
        EqualTo,
        Contains,
        StartsWith,
        EndsWith,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo
    }

    public enum FilterStatementConnector
    {
        And,
        Or
    }

    public class FilterStatement : IFilterStatement
    {
        public FilterStatementConnector filterStatementConnector
        {
            get;set;    
        }

        public Operation operatio
        {
            get;set;    
        }

        public string PropertyName
        {
            get;set;    
        }

        public object Value
        {
            get;set;    
        }
    }
}
