using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Models;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class ItemsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IWriteOnlyRepository<GameItemModel> repository; 

        public ItemsController(IQueryProcessor queryProcessor,
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
                this.repository.Create(viewModel.GameItem);
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
                this.repository.Update(viewModel.GameItem);
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
            var firstCategory = mainCategories.Count() > 0 ? mainCategories.First().SubCategories : null;
            
            return new GameItemViewModel
            {
                mainCategories = mainCategories,
                categories = firstCategory,
                GameItem = model,
            };
        }

        private IEnumerable<MainCategoryModel> GetMainCategories()
        {
            return this.queryProcessor.Execute(new GetAllCategoriesQuery())
                .Where(c => c.SubCategories.Count() > 0);
        }

        private IEnumerable<SubCategoryModel> GetCategories()
        {
            return this.queryProcessor.Execute(new GetAllCategoriesQuery())
                                      .SelectMany(c => c.SubCategories);
        }

        [HttpGet]
        public JsonResult GetMainCategory(string main)
        {
            var category = this.queryProcessor.Execute(new GetAllCategoriesQuery())
                                .First(c => c.Name.Equals(main));
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubCategories(Guid parent)
        {
            var subCategories = this.queryProcessor.Execute(new GetAllCategoriesQuery())
                                      .First(c => c.Id == parent)
                                      .SubCategories;
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsNameAvailable(string itemName)
        {
            bool exists = this.queryProcessor.Execute(new GetAllGameItemsQuery())
                .Where(c => c.Name == itemName )
                .Any();

            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}