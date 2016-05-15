using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Core;
using NinjaHive.Contract.Queries;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class TiersController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        public TiersController(
            IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public ActionResult Index(Guid equipmentId)
        {
            var equipment = this.queryProcessor.Execute(new GetEntityByIdQuery<EquipmentModel>(equipmentId));
            return View(equipment);
        }

        public ActionResult Create(Guid equipmentId)
        {
            var model = new TierModel ();
            var viewModel = new TierViewModel(
                model,
                GetEquipmentById(equipmentId)
            );
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return this.Home(viewModel.EquipmentId);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return this.Home(GetParentEquipment(id).Id);
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
                return this.Home(viewModel.EquipmentId);
            }
            return View(viewModel);
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
        private EquipmentModel GetEquipmentById(Guid id)
        {
            //TODO: query database
            return new EquipmentModel { Name = "Unimplemented", Id = id };
        }

        private TierViewModel PrepareViewModel(TierModel model)
        {
            var viewModel = new TierViewModel(
                    model,
                    GetParentEquipment(model)
                );

            return viewModel;
        }

        protected virtual RedirectResult Home(Guid id)
        {
            return Redirect<TiersController>(c => c.Index(id));
        }
    }
}