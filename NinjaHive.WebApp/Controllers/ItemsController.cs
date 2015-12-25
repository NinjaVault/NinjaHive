using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpes;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class ItemsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<AddGameItemCommand> commandHandler;

        public ItemsController(IQueryProcessor queryProcessor, ICommandHandler<AddGameItemCommand> commandHandler)
        {
            this.queryProcessor = queryProcessor;
            this.commandHandler = commandHandler;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GameItemModel model)
        {
            //if (ModelState.IsValid)
            {
                var item = new GameItemModel
                {
                    Name = model.Name,
                    Description = model.Description,
                };
                this.commandHandler.Handle(new AddGameItemCommand(item));

                return RedirectToRoute(UrlProvider<ItemsController>.GetRouteValues(c => c.Index()));
            }

            return View();
        }

        public ActionResult GetGameItems()
        {
            var query = new GetAllGameItemsQuery();
            var gameItems = this.queryProcessor.Execute(query);

            if (gameItems.Any())
            {
                return this.PartialView(Partials.GameItems, gameItems);
            }

            return base.NoResults();
        }
    }
}