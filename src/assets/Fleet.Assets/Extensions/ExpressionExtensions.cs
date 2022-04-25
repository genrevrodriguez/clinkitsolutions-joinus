using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Assets.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left, Expression<TDelegate> right)
        {
            return Expression.Lambda<TDelegate>(Expression.AndAlso(left, right), left.Parameters);
        }
    }
}
