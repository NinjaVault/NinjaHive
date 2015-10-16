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
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<AddEquipmentItemCommand> addEquipmentItemCommandHandler;
        private readonly ICommandHandler<SaveEquipmentItemCommand> saveEquipmentItemCommandHandler;
        private readonly ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler;

        public EquipmentItemController(
            IQueryProcessor queryProcessor,
            ICommandHandler<AddEquipmentItemCommand> addEquipmentItemCommandHandler,
            ICommandHandler<SaveEquipmentItemCommand> saveEquipmentItemCommandHandler,
            ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler)
        {
            this.queryProcessor = queryProcessor;
            this.addEquipmentItemCommandHandler = addEquipmentItemCommandHandler;
            this.saveEquipmentItemCommandHandler = saveEquipmentItemCommandHandler;
            this.deleteEquipmentItemCommandHandler = deleteEquipmentItemCommandHandler;
        }

        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetAllEquipmentItemsQuery());

            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid itemId)
        {
            var item = this.queryProcessor.Execute(new GetEntityByIdQuery<EquipmentItem>(itemId));
        
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(EquipmentItem equipmentItem)
        {
            var command = new SaveEquipmentItemCommand
            {
                EquipmentItem = equipmentItem,
            };
            this.saveEquipmentItemCommandHandler.Handle(command);

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
            var command = new DeleteEquipmentItemCommand
            {
                EquipmentItem = equipmentItem
            };
            deleteEquipmentItemCommandHandler.Handle(command);

            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}