using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.GameItems;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    public class SkillController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        List<SkillItemModel> tempList = new List<SkillItemModel>
        {
            new SkillItemModel { Id = Guid.Parse("26851558-4568-7895-5568-123645215468"), Name="First Skill Item", Description = "The first item of the Skill Items section", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Ally" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Second Skill Item", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Ally" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Third Skill Item", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Enemy"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Foruth Skill Item", SubCategoryMainCategoryName="Attacks", SubCategoryName="Blasts"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Fifth Skill Item", SubCategoryMainCategoryName="Defenses", SubCategoryName="Shields"},
        };

        public SkillController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetAllGameItemsQuery<SkillItemModel>());
            return View(items);
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
            return View(PrepareViewModel(viewModel.Item));
        }

        public ActionResult Edit(Guid id)
        {
            //TODO: query database
            var viewModel = PrepareViewModel(tempList[0]);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return this.Home();
            }
            return View(PrepareViewModel(viewModel.Item));
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

            return new SkillItemViewModel { Item = item, CategoriesList = categories };
        }

        protected override RedirectResult Home()
        {
            return base.Redirect<SkillController>(c => c.Index());
        }
    }
}