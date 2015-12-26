using System;
using System.Linq;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;

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
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var query = new GetEntityByIdQuery<GameItemModel>(id);
            var model = this.queryProcessor.Execute(query);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(GameItemModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.Create(model);
                return base.Home();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(GameItemModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.Update(model);
                return base.Home();
            }

            return View();
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
    }
}