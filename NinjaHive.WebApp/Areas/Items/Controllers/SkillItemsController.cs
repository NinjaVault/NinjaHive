using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    public class SkillItemsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        List<SkillItemModel> tempList = new List<SkillItemModel>
        {
            new SkillItemModel { Id = Guid.Parse("26851558-4568-7895-5568-123645215468"), Name="First Other Item", Description = "The first item of the Skill Items section", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Second Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Third Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Defense"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Foruth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Consumables"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Fifth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Attack Items"},
        };

        public SkillItemsController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }


        public ActionResult Index()
        {
            //TODO: query database
            return View(tempList);
        }

        public ActionResult Create()
        {
            var viewModel = PrepareViewModel(new SkillItemModel());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return this.Home();
            }
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            //TODO: query database
            var viewModel = PrepareViewModel(tempList[0]);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return this.Home();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return this.Home();
        }

        private SkillItemViewModel PrepareViewModel(SkillItemModel item)
        {
            var categories = this.queryProcessor.Execute(new GetGroupedCategoriesQuery());

            return new SkillItemViewModel { DerivedItem = item, CategoryList = categories };
        }

        protected override RedirectResult Home()
        {
            return base.Redirect<OtherController>(c => c.Index());
        }
    }
}