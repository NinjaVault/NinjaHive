using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    public class ErrorsController : BaseController
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