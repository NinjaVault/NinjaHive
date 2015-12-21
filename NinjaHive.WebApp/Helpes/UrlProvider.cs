using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NinjaHive.WebApp.Helpes
{
    public static class UrlProvider<TController> where TController : Controller
    {
        private static string controllerAreaName;

        public static string GetParameterlessUrl(Expression<Action<TController>> actionSelector)
        {
            return GetVirtualPathData(actionSelector, false).VirtualPath;
        }

        public static string GetUrl(Expression<Action<TController>> action)
        {
            return GetVirtualPathData(action, true).VirtualPath;
        }

        public static VirtualPathData GetVirtualPathData(Expression<Action<TController>> actionSelector,
            bool includeParams = false)
        {
            if (actionSelector.Body is MethodCallExpression)
            {
                return GetVirtualPathData((MethodCallExpression)actionSelector.Body, includeParams);
            }

            throw new ArgumentException(string.Format(
                "Expression {0} has body of type {1}, which is not supported.", actionSelector, actionSelector.Body.GetType().Name));
        }

        private static VirtualPathData GetVirtualPathData(MethodCallExpression expression, bool includeParams)
        {
            var method = expression.Method;

            if (!method.IsConstructor && method.IsPublic)
            {
                var routes = new RouteValueDictionary(new
                {
                    Controller = typeof(TController).Name.Replace(typeof(Controller).Name, ""),
                    Action = method.Name,
                    Area = GetAreaName()
                });

                if (includeParams)
                {
                    IncludeParameters(expression, routes);
                }

                return RouteTable.Routes.GetVirtualPathForArea(HttpContext.Current.Request.RequestContext, routes);
            }

            throw new ArgumentException(string.Format("The expression '{0}' is not supported. ", expression));
        }

        public static RouteValueDictionary GetRouteValues(Expression<Action<TController>> actionSelector)
        {
            var expression = (MethodCallExpression)actionSelector.Body;
            var method = expression.Method;

            if (!method.IsConstructor && method.IsPublic)
            {
                var routes = new RouteValueDictionary(new
                {
                    Controller = typeof(TController).Name.Replace(typeof(Controller).Name, ""),
                    Action = method.Name,
                    Area = GetAreaName()
                });

                IncludeParameters(expression, routes);

                return routes;
            }

            throw new ArgumentException(string.Format("The expression '{0}' is not supported. ", expression));
        }

        private static void IncludeParameters(MethodCallExpression expression, RouteValueDictionary routes)
        {
            var methodParameters = expression.Method.GetParameters();

            for (var argumentIndex = 0; argumentIndex < expression.Arguments.Count; argumentIndex++)
            {
                var argumentExpression = expression.Arguments[argumentIndex];

                var methodParameter = methodParameters[argumentIndex];

                IncludeParameter(argumentExpression, methodParameter, routes);
            }
        }

        private static void IncludeParameter(Expression argumentExpression,
            ParameterInfo methodParameter, RouteValueDictionary routes)
        {
            if (methodParameter.ParameterType == typeof(string) || methodParameter.ParameterType.IsValueType)
            {
                var value = GetValue(argumentExpression);

                routes.Add(methodParameter.Name, value);
            }
        }

        private static object GetValue(Expression argumentExpression)
        {
            // Optimization: in some cases we don't need to compile the expression and invoke it.
            var constantExpression = argumentExpression as ConstantExpression;

            if (constantExpression != null)
            {
                return constantExpression.Value;
            }

            // This is the fallback case: now we need to compile it.
            var valueRetriever = Expression.Lambda(argumentExpression).Compile();

            return valueRetriever.DynamicInvoke();
        }

        private static string GetAreaName()
        {
            return controllerAreaName ?? (controllerAreaName = FindAreaName());
        }

        private static string FindAreaName()
        {
            var areaNames =
                from areaRegistrationType in typeof(TController).Assembly.ExportedTypes
                where areaRegistrationType.IsSubclassOf(typeof(AreaRegistration))
                let controllerBelongsToArea = typeof(TController).Namespace.StartsWith(areaRegistrationType.Namespace)
                where controllerBelongsToArea
                let areaRegistration = (AreaRegistration)Activator.CreateInstance(areaRegistrationType)
                select areaRegistration.AreaName;

            return areaNames.SingleOrDefault() ?? string.Empty;
        }
    }
}