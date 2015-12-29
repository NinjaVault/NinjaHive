using System;
using System.Web.Mvc;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Helpers;

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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddMainCategory(MainCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.mainCategoryRepository.Create(model);
            }
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }

        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.subCategoryRepository.Create(model);
            }
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }

        [HttpPost]
        public ActionResult Delete(Guid id, bool isMainCategory)
        {
            if (isMainCategory)
            {
                this.mainCategoryRepository.Delete(id);
            }
            else
            {
                this.subCategoryRepository.Delete(id);
            }
            return base.Home();
        }

        [HttpPost]
        public JsonResult GetLinkedGameItems(Guid id)
        {
            var linkedGameItems = this.queryProcessor.Execute(new GetLinkedGameItemNamesQuery(id));
            return Json(linkedGameItems);
        }
    }
}