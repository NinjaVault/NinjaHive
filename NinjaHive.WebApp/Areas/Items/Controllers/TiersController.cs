using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Core;
using NinjaHive.Contract.Queries;
using NinjaHive.Core.Extensions;
using NinjaHive.Core.Models;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class TiersController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<TierModel> tiersRepository;

        public TiersController(
            IQueryProcessor queryProcessor,
            IUnitOfWork<TierModel> tiersRepository)
        {
            this.queryProcessor = queryProcessor;
            this.tiersRepository = tiersRepository;
        }

        public ActionResult Index(Guid equipmentId)
        {
            var equipment = this.GetEquipmentById(equipmentId);
            return View(equipment);
        }

        public ActionResult Create(Guid equipmentId)
        {
            var equipment = this.GetEquipmentById(equipmentId);
            var model = new TierModel
            {
                EquipmentItemId = equipment.Id,
                EquipmentItemName = equipment.Name,
            };

            return View(model);
        }

        public ActionResult Edit(Guid tierId)
        {
            var model = this.queryProcessor.Execute(new GetEntityByIdQuery<TierModel>(tierId));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TierModel model)
        {
            return this.UpdateModelForPostResult(model, this.tiersRepository.Create);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TierModel model)
        {
            return this.UpdateModelForPostResult(model, this.tiersRepository.Update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TierModel model)
        {
            //TODO: server side validation
            this.tiersRepository.Delete(model.Id);
            return this.RederictToIndex(model.EquipmentItemId);
        }

        private ActionResult UpdateModelForPostResult(TierModel model, Func<TierModel, WorkResult> unitOfWork)
        {
            if (ModelState.IsValid)
            {
                var result = unitOfWork.Invoke(model);
                if (result.IsValid)
                {
                    return this.RederictToIndex(model.EquipmentItemId);
                }
                model.ValidationResults.AddRange(result.ValidationResults);
            }
            return View(model);
        }

        private EquipmentModel GetEquipmentById(Guid id)
        {
            return this.queryProcessor.Execute(new GetEntityByIdQuery<EquipmentModel>(id));
        }

        private ActionResult RederictToIndex(Guid id)
        {
            return base.Redirect<TiersController>(c => c.Index(id));
        }
    }
}