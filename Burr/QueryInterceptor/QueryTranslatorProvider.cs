using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Burr.QueryInterceptor
{
    internal abstract class QueryTranslatorProvider : ExpressionVisitor
    {
        private readonly IQueryable _source;

        protected QueryTranslatorProvider(IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            _source = source;
        }

        internal IQueryable Source
        {
            get { return _source; }
        }
    }

    internal class QueryTranslatorProvider<T> : QueryTranslatorProvider, IQueryProvider
    {
        private readonly IEnumerable<ExpressionVisitor> _visitors;

        public QueryTranslatorProvider(IQueryable source, IEnumerable<ExpressionVisitor> visitors)
            : base(source)
        {
            if (visitors == null)
            {
                throw new ArgumentNullException("visitors");
            }
            _visitors = visitors;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return new QueryTranslator<TElement>(Source, expression, _visitors) as IQueryable<TElement>;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            Type elementType = expression.Type.GetGenericArguments().First();
            IQueryable result = (IQueryable)Activator.CreateInstance(typeof(QueryTranslator<>).MakeGenericType(elementType),
                    new object[] { Source, expression, _visitors });
            return result;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            object result = (this as IQueryProvider).Execute(expression);
            return (TResult)result;
        }

        public object Execute(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            Expression translated = VisitAll(expression);
            return Source.Provider.Execute(translated);
        }

        internal IEnumerable ExecuteEnumerable(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            Expression translated = VisitAll(expression);
            return Source.Provider.CreateQuery(translated);
        }

        private Expression VisitAll(Expression expression)
        {
            // Run all visitors in order
            var visitors = new ExpressionVisitor[] { this }.Concat(_visitors);

            return visitors.Aggregate<ExpressionVisitor, Expression>(expression, (expr, visitor) => visitor.Visit(expr));
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            // Fix up the Expression tree to work with the underlying LINQ provider
            if (node.Type.IsGenericType &&
                node.Type.GetGenericTypeDefinition() == typeof(QueryTranslator<>))
            {

                var provider = ((IQueryable)node.Value).Provider as QueryTranslatorProvider;

                if (provider != null)
                {
                    return provider.Source.Expression;
                }

                return Source.Expression;
            }

            return base.VisitConstant(node);
        }
    }
}
