using System.Web.Mvc;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Filters
{
    public class LogExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger logger;

        public LogExceptionFilterAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            this.logger.Log(filterContext.Exception);
        }
    }
}