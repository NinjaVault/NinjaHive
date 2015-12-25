using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class ItemsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        public ItemsController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetGameItems()
        {
            var query = new GetAllGameItemsQuery();
            var gameItems = this.queryProcessor.Execute(query);

            if (gameItems.Any())
            {
                return this.PartialView("_GameItemsPartial", gameItems);
            }

            return base.NoResults();
        }
    }
}