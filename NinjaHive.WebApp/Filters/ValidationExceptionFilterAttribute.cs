using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NinjaHive.Core.Extensions;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Filters
{
    public class ValidationExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            if(exception is ValidationException &&
               exception.IsThrownFrom(typeof(PromptableCommandHandler<>)))
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);

                var validationMessage = exception.Message;

                //TODO: put validation message in HttpContext Response?
            }
        }
    }
}
