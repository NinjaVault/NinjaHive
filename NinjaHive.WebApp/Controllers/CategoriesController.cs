﻿using System;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;
using NinjaHive.Contract.Queries.Categories;
using System.Linq;
using NinjaHive.Core.Extensions;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<MainCategoryModel> mainCategoryRepository;
        private readonly IUnitOfWork<SubCategoryModel> subCategoryRepository;

        public CategoriesController(
            IQueryProcessor queryProcessor,
            IUnitOfWork<MainCategoryModel> mainCategoryRepository,
            IUnitOfWork<SubCategoryModel> subCategoryRepository)
        {
            this.queryProcessor = queryProcessor;
            this.mainCategoryRepository = mainCategoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public ActionResult Index()
        {
            var categories = this.queryProcessor.Execute(new GetAllCategoriesQuery());
            return View(categories);
        }

        public ActionResult CreateMainCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMainCategory(MainCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.mainCategoryRepository.Create(model);
                return this.RedirectToIndex();
            }
            return View(model);
        }

        public ActionResult CreateSubCategory(Guid mainCategoryId)
        {
            //TODO: validate if ID exists
            var subCategoryModel = new SubCategoryModel {MainCategoryId = mainCategoryId};
            return View(subCategoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubCategory(SubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.subCategoryRepository.Create(model);
                return this.RedirectToIndex();

            }
            return View(model);
        }

        public ActionResult EditMainCategory(Guid id)
        {
            var model = this.mainCategoryRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMainCategory(MainCategoryModel model)
        {
            if(ModelState.IsValid)
            {
                this.mainCategoryRepository.Update(model);
                return this.RedirectToIndex();
            }
            return View(model);
        }

        public ActionResult EditSubCategory(Guid id)
        {
            var model = this.subCategoryRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(SubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.subCategoryRepository.Update(model);
                return this.RedirectToIndex();
            }
            return View(model);
        }

        public ActionResult DeleteMainCategory(Guid id)
        {
            var model = this.mainCategoryRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMainCategory(MainCategoryModel model)
        {
            var result = this.mainCategoryRepository.Delete(model.Id);
            if (result.IsValid)
            {
                return this.RedirectToIndex();
            }
            return View(model);
        }

        public ActionResult DeleteSubCategory(Guid id)
        {
            var model = this.subCategoryRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategory(SubCategoryModel model)
        {
            var result = this.subCategoryRepository.Delete(model.Id);
            if (result.IsValid)
            {
                return this.RedirectToIndex();
            }
            model.ValidationResults.AddRange(result.ValidationResults);
            return View(model);
        }

        private ActionResult RedirectToIndex()
        {
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }
    }
}