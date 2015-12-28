using System.Web.Mvc;
using NinjaHive.Core;
using NinjaHive.WebApp.Filters;
using SimpleInjector;

namespace NinjaHive.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, Container container)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogExceptionFilterAttribute(container.GetInstance<ILogger>()));
        }
    }
}
