using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IQueryHandler<GetAllItemsQuery, ItemDto[]> getItemsQuery;
        private readonly IQueryHandler<GetItemByIdQuery, ItemDto> getItemByIdQuery; 
        private readonly ICommandHandler<CreateItemCommand> createItemCommand;
        private readonly ICommandHandler<DeleteItemCommand> deleteItemCommand;

        public ItemController(
            IQueryHandler<GetAllItemsQuery, ItemDto[]> getItemsQuery,
            ICommandHandler<CreateItemCommand> createItemCommand,
            ICommandHandler<DeleteItemCommand> deleteItemCommand,
            IQueryHandler<GetItemByIdQuery, ItemDto> getItemByIdQuery)
        {
            this.getItemsQuery = getItemsQuery;
            this.createItemCommand = createItemCommand;
            this.deleteItemCommand = deleteItemCommand;
            this.getItemByIdQuery = getItemByIdQuery;
        }

        public ActionResult Index()
        {
            var query = new GetAllItemsQuery();
            var items = this.getItemsQuery.Handle(query);

            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(Guid itemId)
        {
            var query = new GetItemByIdQuery(itemId);
            var item = this.getItemByIdQuery.Handle(query);

            return View(item);
        }

        [HttpPost]
        public ActionResult Create(ItemDto item)
        {
            item.ItemId = Guid.NewGuid();
            var command = new CreateItemCommand(item);
            createItemCommand.Handle(command);

            var redirectUri = UrlProvider<ItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        public ActionResult Delete(ItemDto item)
        {
            var command = new DeleteItemCommand(item);
            deleteItemCommand.Handle(command);

            var redirectUri = UrlProvider<ItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}