using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Controllers
{
    public class EquipmentItemController : Controller
    {
        private readonly IQueryHandler<GetAllEquipmentItemsQuery, EquipmentItem[]> equipmentItemsQueryHandler;
        private readonly ICommandHandler<AddEquipmentItemCommand> addEquipmentItemCommandHandler;

        // keep it while IQueryHandler implementation is finished
        private readonly EquipmentItem demoEquipmentItem;

        public EquipmentItemController(IQueryHandler<GetAllEquipmentItemsQuery, EquipmentItem[]> equipmentItemsQueryHandler,
            ICommandHandler<AddEquipmentItemCommand> addEquipmentItemCommandHandler)
        {
            this.demoEquipmentItem = new EquipmentItem
            {
                Id = Guid.NewGuid(),
                Name = "Demo item"
            };

            this.addEquipmentItemCommandHandler = addEquipmentItemCommandHandler;
            this.equipmentItemsQueryHandler = equipmentItemsQueryHandler;
        }

        public ActionResult Index()
        {
            var items = this.equipmentItemsQueryHandler.Handle(new GetAllEquipmentItemsQuery());

            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid itemId)
        {            
            var item = this.demoEquipmentItem;           
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(EquipmentItem equipmentItem)
        {
            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        [HttpPost]
        public ActionResult Create(EquipmentItem equipmentItem)
        {
            equipmentItem.Id = Guid.NewGuid();
            var command = new AddEquipmentItemCommand
            {
                EquipmentItem = equipmentItem,
            };

            this.addEquipmentItemCommandHandler.Handle(command);

            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        public ActionResult Delete(EquipmentItem equipmentItem)
        {
            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}