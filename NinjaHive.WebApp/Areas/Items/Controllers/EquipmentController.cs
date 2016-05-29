using NinjaHive.Core;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.Categories;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class EquipmentController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<EquipmentModel> equipmentItemsRepository;

        public EquipmentController(IQueryProcessor queryProcessor,
            IUnitOfWork<EquipmentModel> equipmentItemsRepository)
        {
            this.queryProcessor = queryProcessor;
            this.equipmentItemsRepository = equipmentItemsRepository;
        }

        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetAllGameItemsQuery<EquipmentModel>());
            return this.View(items);
        }

        public ActionResult Create()
        {
            return this.View(this.PrepareViewModel(new EquipmentModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.equipmentItemsRepository.Create(viewModel.Item);
                return this.Home();
            }
            return this.View(this.PrepareViewModel(viewModel.Item));
        }

        public ActionResult Edit(Guid id)
        {
            var item = this.equipmentItemsRepository.GetById(id);
            return this.View(this.PrepareViewModel(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipmentViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.equipmentItemsRepository.Update(viewModel.Item);
                return this.Home();
            }
            return this.View(this.PrepareViewModel(viewModel.Item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: server side validation
            this.equipmentItemsRepository.Delete(id);
            return this.Home();
        }

        public ActionResult NextTier(Guid parentTierId)
        {
            var model = this.equipmentItemsRepository.GetById(parentTierId);
            var viewModel = new NextTierViewModel
            {
                ParentTierId = parentTierId,
                Tier = ++model.Tier,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NextTier(NextTierViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var model = this.equipmentItemsRepository.GetById(viewModel.ParentTierId);
                this.SetNextTier(model, viewModel);

                this.equipmentItemsRepository.Create(model);

                return this.Home();
            }
            return this.View(viewModel);
        }

        private void SetNextTier(EquipmentModel model, NextTierViewModel viewModel)
        {
            model.Id = Guid.Empty;
            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.ParentTierId = viewModel.ParentTierId;
            model.Tier += 1;
        }

        private EquipmentViewModel PrepareViewModel(EquipmentModel model)
        {
            var categories = this.queryProcessor.Execute(new GetGroupedCategoriesQuery());
            return new EquipmentViewModel { Item = model, CategoriesList = categories };
        }
    }
}