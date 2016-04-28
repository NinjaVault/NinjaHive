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
    public class TiersController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IWriteOnlyRepository<TierModel> repository;

        public TiersController(IQueryProcessor queryProcessor,
            IWriteOnlyRepository<TierModel> repository)
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
            return null;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.repository.Create(viewModel.TierItem);
                return base.Home();
            }

            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var query = new GetEntityByIdQuery<TierModel>(id);
            var model = this.queryProcessor.Execute(query);
            var viewModel = this.PrepareViewModel(model);
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.repository.Update(viewModel.TierItem);
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

        public ActionResult GetTiers()
        {
            var query = new GetAllTiersQuery();
            var tiers = this.queryProcessor.Execute(query);

            if (tiers.Any())
            {
                // what should I do here?

                return base.NoResults();
            }

            return base.NoResults();
        }

        private TierViewModel PrepareViewModel(TierModel model)
        {
            return new TierViewModel
            {
                TierItem = model
            };
        }
    }
}
