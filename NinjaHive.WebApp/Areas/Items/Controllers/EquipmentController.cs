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
            return View(items);
        }

        public ActionResult Create()
        {
            return View(PrepareViewModel(new EquipmentModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.equipmentItemsRepository.Create(viewModel.Item);
                return base.Home();
            }
            return View(PrepareViewModel(viewModel.Item));
        }

        public ActionResult Edit(Guid id)
        {
            var item = this.equipmentItemsRepository.GetById(id);
            return View(PrepareViewModel(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.equipmentItemsRepository.Update(viewModel.Item);
                return base.Home();
            }
            return View(PrepareViewModel(viewModel.Item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            this.equipmentItemsRepository.Delete(id);
            return base.Home();
        }

        private EquipmentViewModel PrepareViewModel(EquipmentModel model)
        {
            var categories = this.queryProcessor.Execute(new GetGroupedCategoriesQuery());
            return new EquipmentViewModel { Item = model, CategoriesList = categories };
        }
    }
}