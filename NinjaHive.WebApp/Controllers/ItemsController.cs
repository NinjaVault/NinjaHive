using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}