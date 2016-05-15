using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.GameItems;
using NinjaHive.Contract.Queries.Skills;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    public class SkillController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<SkillItemModel> skillItemsRepository;

        public SkillController(
            IQueryProcessor queryProcessor,
            IUnitOfWork<SkillItemModel> skillItemsRepository)
        {
            this.queryProcessor = queryProcessor;
            this.skillItemsRepository = skillItemsRepository;
        }

        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetAllGameItemsQuery<SkillItemModel>());
            return View(items);
        }

        public ActionResult Create()
        {
            return View(this.PrepareViewModel(new SkillItemModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.skillItemsRepository.Create(viewModel.Item);
                return this.RedirectToIndex();
            }
            return View(this.PrepareViewModel(viewModel.Item));
        }

        public ActionResult Edit(Guid id)
        {
            var item = this.skillItemsRepository.GetById(id);
            return View(this.PrepareViewModel(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.skillItemsRepository.Update(viewModel.Item);
                return this.RedirectToIndex();
            }
            return View(this.PrepareViewModel(viewModel.Item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            this.skillItemsRepository.Delete(id);
            return this.RedirectToIndex();
        }

        private SkillItemViewModel PrepareViewModel(SkillItemModel item)
        {
            var categories = this.queryProcessor.Execute(new GetGroupedCategoriesQuery());
            var skills = this.queryProcessor.Execute(new GetAllSkillsQuery());
            return new SkillItemViewModel { Item = item, CategoriesList = categories, SkillsList = skills };
        }

        private ActionResult RedirectToIndex()
        {
            return base.Redirect<SkillController>(c => c.Index());
        }
    }
}