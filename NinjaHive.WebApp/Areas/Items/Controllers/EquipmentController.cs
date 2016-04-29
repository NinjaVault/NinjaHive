using NinjaHive.Core;
using NinjaHive.Contract.Queries;
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

        public EquipmentController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetEquipmentItemsQuery());
            return View(items);
        }

        public ActionResult Create()
        {
            return View(PrepareViewModel(new EquipmentModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return base.Home();
            }
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var item = this.queryProcessor.Execute(new GetEntityByIdQuery<EquipmentModel>(id));
            return View(PrepareViewModel(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return base.Home();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return base.Home();
        }

        private EquipmentViewModel PrepareViewModel(EquipmentModel model)
        {
            var categories = this.queryProcessor.Execute(new GetCategoriesQuery());
            return new EquipmentViewModel(model, categories);
        }
    }
}