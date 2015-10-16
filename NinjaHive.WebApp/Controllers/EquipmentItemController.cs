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
        private readonly ICommandHandler<EditEquipmentItemCommand> editEquipmentItemCommandHandler;
        private readonly ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler;

        public EquipmentItemController(
            IQueryProcessor queryProcessor,
            ICommandHandler<EditEquipmentItemCommand> editEquipmentItemCommandHandler,
            ICommandHandler<DeleteEquipmentItemCommand> deleteEquipmentItemCommandHandler)
        {
            this.queryProcessor = queryProcessor;
            this.editEquipmentItemCommandHandler = editEquipmentItemCommandHandler;
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
            this.EditEquipmentItem(equipmentItem, createNew: false);

            var redirectUri = UrlProvider<EquipmentItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        [HttpPost]
        public ActionResult Create(EquipmentItem equipmentItem)
        {
            equipmentItem.Id = Guid.NewGuid();
            this.EditEquipmentItem(equipmentItem, createNew: true);

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

        private void EditEquipmentItem(EquipmentItem equipmentItem, bool createNew)
        {
            var command = new EditEquipmentItemCommand(equipmentItem, createNew);
            this.editEquipmentItemCommandHandler.Handle(command);
        }
    }
}