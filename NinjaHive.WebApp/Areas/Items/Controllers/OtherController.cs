using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.GameItems;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class OtherController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<OtherItemModel> otherItemsRepository;

        public OtherController(IQueryProcessor queryProcessor,
            IUnitOfWork<OtherItemModel> otherItemsRepository)
        {
            this.queryProcessor = queryProcessor;
            this.otherItemsRepository = otherItemsRepository;
        }


        public ActionResult Index()
        {
            var items = this.queryProcessor.Execute(new GetAllGameItemsQuery<OtherItemModel>());
            return View(items);
        }

        public ActionResult Create()
        {
            var viewModel = PrepareViewModel(new OtherItemModel());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtherItemViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                this.otherItemsRepository.Create(viewModel.Item);
                return this.RedirectToIndex();
            }
            return View(PrepareViewModel(viewModel.Item));
        }

        public ActionResult Edit(Guid id)
        {
            var item = this.otherItemsRepository.GetById(id);
            return View(PrepareViewModel(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OtherItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.otherItemsRepository.Update(viewModel.Item);
                return this.RedirectToIndex();
            }
            return View(PrepareViewModel(viewModel.Item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: server side validation
            this.otherItemsRepository.Delete(id);
            return this.RedirectToIndex();
        }

        private OtherItemViewModel PrepareViewModel(OtherItemModel item)
        {
            var categories = this.queryProcessor.Execute(new GetGroupedCategoriesQuery());
            return new OtherItemViewModel { Item = item, CategoriesList = categories };
        }

        private ActionResult RedirectToIndex()
        {
            return base.Redirect<OtherController>(c => c.Index());
        }
    }
}