using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Expression
{
    public static class DynamicExpressions
    {

        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }


        /// <summary>
        /// orElse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return expr1.Compose(expr2, System.Linq.Expressions.Expression.Or);
        }


        /// <summary>
        /// AndAlso
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return expr1.Compose(expr2, BinaryExpression.And);
        }
        public static Expression<Func<T, bool>> LikeField<T>(Expression<Func<T, bool>> expr, string str)
        {
            return null;
        }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return System.Linq.Expressions.Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static System.Linq.Expressions.Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, System.Linq.Expressions.Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }


        protected override System.Linq.Expressions.Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }      
    }
}
