using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    public class ErrorsController : BaseController
    {
        // GET: Errors
        public ActionResult DefaultError()
        {
            return View();
        }
    }
}