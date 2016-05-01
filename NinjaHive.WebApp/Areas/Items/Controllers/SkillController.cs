using NinjaHive.Core;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    public class SkillController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<SkillItemModel> skillItemRepository;

        List<SkillItemModel> tempList = new List<SkillItemModel>
        {
            new SkillItemModel { Id = Guid.Parse("26851558-4568-7895-5568-123645215468"), Name="First Skill Item", Description = "The first item of the Skill Items section", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Ally" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Second Skill Item", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Ally" },
            new SkillItemModel { Id = Guid.NewGuid(), Name="Third Skill Item", SubCategoryMainCategoryName="Enhancing", SubCategoryName="Enemy"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Foruth Skill Item", SubCategoryMainCategoryName="Attacks", SubCategoryName="Blasts"},
            new SkillItemModel { Id = Guid.NewGuid(), Name="Fifth Skill Item", SubCategoryMainCategoryName="Defenses", SubCategoryName="Shields"},
        };

        public SkillController(IQueryProcessor queryProcessor,
            IUnitOfWork<SkillItemModel> skillItemRepository)
        {
            this.queryProcessor = queryProcessor;
            this.skillItemRepository = skillItemRepository;
        }


        public ActionResult Index()
        {
            var skillItems = this.queryProcessor.Execute(new GetAllItemsQuery<SkillItemModel>());
            return View(skillItems);
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
                this.skillItemRepository.Create(viewModel.DerivedItem);
                return this.Home();
            }

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var model = this.skillItemRepository.GetById(id);

            var viewModel = PrepareViewModel(model);
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
            this.skillItemRepository.Delete(id);
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