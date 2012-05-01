using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Burr
{
    /// <summary>
    /// References:
    /// http://msdn.microsoft.com/en-us/library/bb546158.aspx
    /// http://nuget.codeplex.com/SourceControl/changeset/view/575589339714#src%2fCore%2fRepositories%2fAggregateQuery.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AggregateQuery<T> : IOrderedQueryable<T>, IQueryProvider
    {
        public AggregateQuery()
        {
            Expression = Expression.Constant(this);
        }

        private AggregateQuery(Expression expression)
        {
            Expression = expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get { return typeof(T); } }
        public Expression Expression { get; private set; }
        public IQueryProvider Provider { get { return this; } }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AggregateQuery<TElement>(expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            // Copied logic from EnumerableQuery
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            Type elementType = QueryableUtility.FindGenericType(typeof(IQueryable<>), expression.Type);

            if (elementType == null)
            {
                throw new ArgumentException(String.Empty, "expression");
            }

            var queryType = typeof(AggregateQuery<>).MakeGenericType(elementType);
            var ctor = queryType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Single();

            return (IQueryable)ctor.Invoke(new object[] { expression });
        }

        public TResult Execute<TResult>(Expression expression)
        {
            // this is where we need to do real work against the server
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            return Execute<object>(expression);
        }
    }

    internal static class QueryableUtility
    {
        private static readonly string[] _orderMethods = new[] { "OrderBy", "ThenBy", "OrderByDescending", "ThenByDescending" };
        private static readonly MethodInfo[] _methods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);

        private static MethodInfo GetQueryableMethod(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Call)
            {
                var call = (MethodCallExpression)expression;
                if (call.Method.IsStatic && call.Method.DeclaringType == typeof(Queryable))
                {
                    return call.Method.GetGenericMethodDefinition();
                }
            }
            return null;
        }

        public static bool IsQueryableMethod(Expression expression, string method)
        {
            return _methods.Where(m => m.Name == method).Contains(GetQueryableMethod(expression));
        }

        public static bool IsOrderingMethod(Expression expression)
        {
            return _orderMethods.Any(method => IsQueryableMethod(expression, method));
        }

        public static Expression ReplaceQueryableExpression(IQueryable query, Expression expression)
        {
            return new ExpressionRewriter(query).Visit(expression);
        }

        public static Type FindGenericType(Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type interfaceType in type.GetInterfaces())
                    {
                        Type genericType = FindGenericType(definition, interfaceType);
                        if (genericType != null)
                        {
                            return genericType;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        private class ExpressionRewriter : ExpressionVisitor
        {
            private readonly IQueryable _query;

            public ExpressionRewriter(IQueryable query)
            {
                _query = query;
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                // Replace the query at the root of the expression
                if (typeof(IQueryable).IsAssignableFrom(node.Type))
                {
                    return _query.Expression;
                }
                return base.VisitConstant(node);
            }
        }
    }
}
