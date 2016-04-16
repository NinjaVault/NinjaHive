using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult DefaultError()
        {
            return View();
        }

        public ActionResult UnauthorizedError()
        {
            return View();
        }
    }
}