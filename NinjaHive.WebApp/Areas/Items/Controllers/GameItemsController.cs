using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Controllers;
using NinjaHive.WebApp.Areas.Items.Models;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class GameItemsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IWriteOnlyRepository<GameItemModel> repository; 

        public GameItemsController(IQueryProcessor queryProcessor,
            IWriteOnlyRepository<GameItemModel> repository)
        {
            this.queryProcessor = queryProcessor;
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new GameItemModel();
            var viewModel = this.PrepareViewModel(model);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.repository.Create(viewModel.BaseGameItem);
                return base.Home();
            }

            viewModel.mainCategories = this.GetMainCategories();
            viewModel.categories = this.GetCategories();
            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var query = new GetEntityByIdQuery<GameItemModel>(id);
            var model = this.queryProcessor.Execute(query);
            var viewModel = this.PrepareViewModel(model);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GameItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.repository.Update(viewModel.BaseGameItem);
                return base.Home();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            this.repository.Delete(id);
            return base.Home();
        }

        public ActionResult GetGameItems()
        {
            var query = new GetAllGameItemsQuery();
            var gameItems = this.queryProcessor.Execute(query);

            if (gameItems.Any())
            {
                return this.PartialView(Partials.GameItems, gameItems);
            }

            return base.NoResults();
        }

        private GameItemViewModel PrepareViewModel(GameItemModel model)
        {
            var mainCategories = this.GetMainCategories();
            var firstCategory = mainCategories.Any() ? mainCategories.First().SubCategories : null;
            
            return new GameItemViewModel
            {
                mainCategories = mainCategories,
                categories = firstCategory,
                gameItem = model,
            };
        }

        private IEnumerable<MainCategoryModel> GetMainCategories()
        {
            return this.queryProcessor.Execute(new GetMainCategoriesQuery { HasSubCategory = "" });
        }

        private IEnumerable<SubCategoryModel> GetCategories()
        {
            return this.queryProcessor.Execute(new GetSubCategoriesQuery());
        }


        [HttpGet]
        public JsonResult GetMainCategoriesJson(GetMainCategoriesQuery query)
        {
            var categories = this.queryProcessor.Execute(query);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubCategoriesJson(GetSubCategoriesQuery query)
        {
            var subCategories = this.queryProcessor.Execute(query);
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemsJson(GetItemsQuery query)
        {
            var items = this.queryProcessor.Execute(query);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}