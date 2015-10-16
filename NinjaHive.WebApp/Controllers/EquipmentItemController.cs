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
        private readonly ICommandHandler<SaveEquipmentItemCommand> saveEquipmentItemCommandHandler;
        private readonly ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler;
        private readonly IQueryHandler<GetEntityByIdQuery<EquipmentItem>, EquipmentItem> getEquipmentItemByIdQueryHandler;

        // keep it while IQueryHandler implementation is finished
        private readonly EquipmentItem demoEquipmentItem;

        public EquipmentItemController(
            IQueryHandler<GetAllEquipmentItemsQuery, EquipmentItem[]> equipmentItemsQueryHandler,
            ICommandHandler<AddEquipmentItemCommand> addEquipmentItemCommandHandler,
            ICommandHandler<SaveEquipmentItemCommand> saveEquipmentItemCommandHandler,
            ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler,
            IQueryHandler<GetEntityByIdQuery<EquipmentItem>, EquipmentItem> getEquipmentItemByIdQueryHandler)
        {
            this.demoEquipmentItem = new EquipmentItem
            {
                Id = Guid.NewGuid(),
                Name = "Demo item"
            };

            this.addEquipmentItemCommandHandler = addEquipmentItemCommandHandler;
            this.equipmentItemsQueryHandler = equipmentItemsQueryHandler;
            this.saveEquipmentItemCommandHandler = saveEquipmentItemCommandHandler;
            this.deleteEquipmentItemCommandHandler = deleteEquipmentItemCommandHandler;
            this.getEquipmentItemByIdQueryHandler = getEquipmentItemByIdQueryHandler;
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
            var item = this.getEquipmentItemByIdQueryHandler.Handle(new GetEntityByIdQuery<EquipmentItem>(itemId));
        
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(EquipmentItem equipmentItem)
        {
            saveEquipmentItemCommandHandler.Handle(new SaveEquipmentItemCommand
            {
                EquipmentItem = equipmentItem,
            });

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
            deleteEquipmentItemCommandHandler.Handle(new DeleteEquipmentItemCommand
            {
                EquipmentItem = equipmentItem
            });

            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}