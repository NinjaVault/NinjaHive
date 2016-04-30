using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Categories;
using NinjaHive.Core;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class OtherController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        List<OtherItemModel> tempList = new List<OtherItemModel>
        {
            new OtherItemModel { Id = Guid.Parse("12356458-4568-7895-5568-123645215468"), Name="First Other Item", Description = "The first item of the Other section", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            new OtherItemModel { Id = Guid.NewGuid(), Name="Second Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Attack" },
            new OtherItemModel { Id = Guid.NewGuid(), Name="Third Other Item", SubCategoryMainCategoryName="Enhancers", SubCategoryName="Defense"},
            new OtherItemModel { Id = Guid.NewGuid(), Name="Foruth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Consumables"},
            new OtherItemModel { Id = Guid.NewGuid(), Name="Fifth Other Item", SubCategoryMainCategoryName="Usables", SubCategoryName="Attack Items"},
        };

        public OtherController(IQueryProcessor queryProcessor)
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
            var viewModel = PrepareViewModel(new OtherItemModel());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtherItemViewModel viewModel)
        {
            if(ModelState.IsValid)
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
            return View( viewModel );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OtherItemViewModel model)
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

        private OtherItemViewModel PrepareViewModel(OtherItemModel item)
        {
            var categories = this.queryProcessor.Execute( new GetGroupedCategoriesQuery() );

            return new OtherItemViewModel(item, categories);
        }

        protected override RedirectResult Home()
        {
            return base.Redirect<OtherController>(c => c.Index());
        }
    }
}