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
        private readonly IWriteOnlyRepository<CategoryModel> repository; 

        public CategoriesController(IQueryProcessor queryProcessor, IWriteOnlyRepository<CategoryModel> repository)
        {
            this.queryProcessor = queryProcessor;
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var categories = this.queryProcessor.Execute(new GetAllCategoriesQuery());
            return View(categories);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.Create(model);
            }
            return Redirect(UrlProvider<CategoriesController>.GetUrl(c => c.Index()));
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            this.repository.Delete(id);
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