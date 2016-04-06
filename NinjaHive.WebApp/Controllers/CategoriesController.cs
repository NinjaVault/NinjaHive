﻿using System;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;
using NinjaHive.WebApp.Extensions;

namespace NinjaHive.WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IWriteOnlyRepository<MainCategoryModel> mainCategoryRepository;
        private readonly IWriteOnlyRepository<SubCategoryModel> subCategoryRepository;

        public CategoriesController(
            IQueryProcessor queryProcessor,
            IWriteOnlyRepository<MainCategoryModel> mainCategoryRepository,
            IWriteOnlyRepository<SubCategoryModel> subCategoryRepository)
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
                return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));

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
                return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMainCategory(MainCategoryModel model)
        {
            if(ModelState.IsValid)
            {
                this.mainCategoryRepository.Update(model);

                var mainCategories = this.queryProcessor.Execute(new GetMainCategoriesQuery());
                return this.JsonSuccess(mainCategories);
            }
            return this.JsonFailure( ModelState.GetErrorsAsArray() );
            //return Json(errors, JsonRequestBehavior.DenyGet);
            //return base.Home();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(SubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.subCategoryRepository.Update(model);

                var subCategories = this.queryProcessor.Execute(new GetSubCategoriesQuery { ParentId = model.MainCategoryId });
                return this.JsonSuccess( subCategories );
            }
            return this.JsonFailure( ModelState.GetErrorsAsArray() );
            //return base.Home();
        }

        public ActionResult DeleteMainCategory(Guid id)
        {
            var model = this.queryProcessor.Execute(new GetEntityByIdQuery<MainCategoryModel>(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMainCategory(MainCategoryModel model)
        {
            //TODO: validate if we can delete by checking subcategories
            this.mainCategoryRepository.Delete(model.Id);
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }

        public ActionResult DeleteSubCategory(Guid id)
        {
            var model = this.queryProcessor.Execute(new GetEntityByIdQuery<SubCategoryModel>(id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategory(SubCategoryModel model)
        {
            //TODO: needs validation both server/client side to check if we can delete it by checking if gameitems are attached to it
            this.subCategoryRepository.Delete(model.Id);
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }

        [HttpPost]
        public JsonResult GetLinkedGameItems(Guid id)
        {
            var linkedGameItems = this.queryProcessor.Execute(new GetLinkedGameItemNamesQuery(id));
            return Json(linkedGameItems, JsonRequestBehavior.DenyGet);
        }
    }
}