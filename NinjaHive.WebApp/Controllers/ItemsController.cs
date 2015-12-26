using System;
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
                viewModel.UpdateCategory();
                this.repository.Create(viewModel.GameItem);
                return base.Home();
            }

            return View();
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
                viewModel.UpdateCategory();
                this.repository.Update(viewModel.GameItem);
                return base.Home();
            }

            return View();
        }

        [HttpPost] //TODO: anti forgery
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
            var categoryViewModel = this.InitializeAndGetCategories();
            return new GameItemViewModel(model, categoryViewModel);
        }

        private CategoryViewModel InitializeAndGetCategories()
        {
            var categories = this.queryProcessor.Execute(new GetAllCategoriesQuery());
            return new CategoryViewModel(categories);
        }
    }
}