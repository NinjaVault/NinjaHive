using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Controllers
{
    public class TestController : Controller
    {
        public readonly ICommandHandler<CreateItemCommand> createItem; 
        public TestController(
            ICommandHandler<CreateItemCommand> createItem)
        {
            this.createItem = createItem;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ItemDto item)
        {
            item.ItemId = Guid.NewGuid();
            var command = new CreateItemCommand(item);
            createItem.Handle(command);

            return Index();
        }
    }
}