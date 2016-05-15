using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Core;
using NinjaHive.Contract.Queries;
using NinjaHive.Core.Extensions;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TierModel model)
        {
            if (ModelState.IsValid)
            {
                var result = this.tiersRepository.Create(model);
                if (result.IsValid)
                {
                    return this.RederictToIndex(model.EquipmentItemId);
                }
                model.ValidationResults.AddRange(result.ValidationResults);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return this.RederictToIndex(GetParentEquipment(id).Id);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = PrepareViewModel(new TierModel());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return this.RederictToIndex(viewModel.EquipmentId);
            }
            return View(viewModel);
        }

        private EquipmentModel GetEquipmentById(Guid id)
        {
            return this.queryProcessor.Execute(new GetEntityByIdQuery<EquipmentModel>(id));
        }


        private EquipmentModel GetParentEquipment(Guid id)
        {
            //TODO: query database
            return new EquipmentModel { Name = "Unimplemented", Id = id };
        }
        private EquipmentModel GetParentEquipment(TierModel tier)
        {
            //TODO: query database
            return GetParentEquipment( tier.Id );
        }
        

        private TierViewModel PrepareViewModel(TierModel model)
        {
            var viewModel = new TierViewModel(
                    model,
                    GetParentEquipment(model)
                );

            return viewModel;
        }

        private ActionResult RederictToIndex(Guid id)
        {
            return base.Redirect<TiersController>(c => c.Index(id));
        }
    }
}