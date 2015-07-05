using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IQueryHandler<GetAllItemsQuery, ItemDto[]> getItemsQuery;
        private readonly ICommandHandler<CreateItemCommand> createItemCommand;

        public TestController(
            IQueryHandler<GetAllItemsQuery, ItemDto[]> getItemsQuery,
            ICommandHandler<CreateItemCommand> createItemCommand)
        {
            this.getItemsQuery = getItemsQuery;
            this.createItemCommand = createItemCommand;
        }

        public ActionResult Index()
        {
            var query = new GetAllItemsQuery();
            var items = this.getItemsQuery.Handle(query);

            return View(items);
        }

        public ActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateItem(ItemDto item)
        {
            item.ItemId = Guid.NewGuid();
            var command = new CreateItemCommand(item);
            createItemCommand.Handle(command);

            return RedirectToAction("Index");
        }
    }
}