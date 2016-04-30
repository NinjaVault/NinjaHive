using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.WebApp.Helpers
{
    public static class PropertyHelper
    {
        public static string DisplayNameFromExpression<TModel, TProp>(Expression<Func<TModel, TProp>> lambda)
        {
            var member = GetMemberExpression(lambda);

            var prop = member.Member as PropertyInfo;
            if (prop != null)
            {
                var propDisplayAttribute = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

                if(propDisplayAttribute != null &&
                    !String.IsNullOrEmpty(propDisplayAttribute.Name))
                {
                    return propDisplayAttribute.Name;
                }
                return prop.Name;
            }

            var field = member.Member as FieldInfo;
            var fieldDisplayAttribute = field.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            if (fieldDisplayAttribute != null && !String.IsNullOrEmpty(fieldDisplayAttribute.Name))
            {
                return fieldDisplayAttribute.Name;
            }

            return field.Name;
        }

        public static string NameFromExpression<TModel, TProp>(Expression<Func<TModel, TProp>> lambda)
        {
            var member = GetMemberExpression(lambda);

            var prop = member.Member as PropertyInfo;
            if(prop != null)
            {
                return prop.Name;
            }

            var field = member.Member as PropertyInfo;
            return field.Name;
        }

        private static MemberExpression GetMemberExpression<TModel, TProp>(Expression<Func<TModel, TProp>> lambda)
        {
            var member = lambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("Lambda body does not contain a field or property");
            return member;
        }
    }
}