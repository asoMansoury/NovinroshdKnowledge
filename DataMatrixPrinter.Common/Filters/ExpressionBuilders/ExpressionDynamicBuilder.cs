using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataMatrixPrinter.Common.Filters
{
    public class ExpressionDynamicBuilder<TClass> : IFilter<TClass>
            where TClass : class
    {
        private Dictionary<Operation, Func<Expression, Expression, Expression>> Expressions;
        static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        static MethodInfo startsWith = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        static MethodInfo trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
        static MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

        public List<FilterStatement> Statements
        {
            get;set;    
        }

        public ExpressionDynamicBuilder()
        {
            Expressions = new Dictionary<Operation, Func<Expression, Expression, Expression>>();

            Expressions.Add(Operation.EqualTo, (member, constant) => Expression.Equal(member, constant));
            Expressions.Add(Operation.NotEqualTo, (member, constant) => Expression.NotEqual(member, constant));
            Expressions.Add(Operation.GreaterThan, (member, constant) => Expression.GreaterThan(member, constant));
            Expressions.Add(Operation.GreaterThanOrEqualTo, (member, constant) => Expression.GreaterThanOrEqual(member, constant));
            Expressions.Add(Operation.LessThan, (member, constant) => Expression.LessThan(member, constant));
            Expressions.Add(Operation.LessThanOrEqualTo, (member, constant) => Expression.LessThanOrEqual(member, constant));
            Expressions.Add(Operation.Contains, (member, constant) => Expression.Call(member, containsMethod, constant));
            Expressions.Add(Operation.StartsWith, (member, constant) => Expression.Call(member, startsWith, constant));
            Expressions.Add(Operation.EndsWith, (member, constant) => Expression.Call(member, endsWithMethod, constant));
        }


        public Expression<Func<TClass, bool>> BuildExpression()
        {
            Expression finalExpression = Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(TClass), "X");
            var connector = FilterStatementConnector.And;
            foreach (var statement in Statements)
            {
                var member = GetMemberExpression(parameter, statement.PropertyName);
                if (statement.Value is string)
                    statement.Value = statement.Value.ToString().ToLower();


                if (statement.Value.GetType().FullName == "System.Int32")
                {
                    var propertyType = ((PropertyInfo)member.Member).PropertyType;
                    var converter = TypeDescriptor.GetConverter(propertyType);
                    var propertyValue = converter.ConvertFromInvariantString(statement.Value.ToString());
                    var constant = Expression.Constant(propertyValue);
                    var valueExpression = Expression.Convert(constant, propertyType);
                    var expression = Expressions[statement.operatio].Invoke(member, valueExpression);

                    connector = statement.filterStatementConnector;
                    finalExpression = CombineExpressions(finalExpression, expression, connector);
                }
                else
                {
                    var constant = Expression.Constant(statement.Value);
                    //if (statement.Value is string)
                    //{
                    //    var trimConstantCall = Expression.Call(constant, toLowerMethod);
                    //    constant = Expression.Call(trimConstantCall, toLowerMethod);
                    //}
                    var expression = Expressions[statement.operatio].Invoke(member, constant);

                    connector = statement.filterStatementConnector;
                    finalExpression = CombineExpressions(finalExpression, expression, connector);
                }
            }

            return Expression.Lambda<Func<TClass, bool>>(finalExpression,parameter);
        }


        private Expression CombineExpressions(Expression expr1, Expression expr2, FilterStatementConnector connector)
        {
            if (connector == FilterStatementConnector.And)
                return Expression.AndAlso(expr1, expr2);
            else
                return Expression.OrElse(expr1, expr2);
        }
        private MemberExpression GetMemberExpression(Expression param, string propertyName)
        {
            if (propertyName.Contains("."))
            {
                int index = propertyName.IndexOf(".");
                var subParam = Expression.Property(param, propertyName.Substring(0, index));
                return GetMemberExpression(subParam, propertyName.Substring(index + 1));
            }

            return Expression.Property(param, propertyName);
        }
    }
}
