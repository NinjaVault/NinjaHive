using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NinjaHive.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyNameFromExpression<TModel, TProp>(this Expression<Func<TModel, TProp>> expression)
        {
            var member = GetMemberExpression(expression);

            var prop = member.Member as PropertyInfo;

            if (prop == null)
            {
                throw new ArgumentException($"Member body '{member}' from expression '{expression}' is not a PropertyInfo");
            }

            return prop.Name;
        }

        private static MemberExpression GetMemberExpression<TModel, TProp>(Expression<Func<TModel, TProp>> expression)
        {
            var member = expression.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException($"Lambda body '{expression}' is not a MemberExpression");
            }

            return member;
        }
    }
}
