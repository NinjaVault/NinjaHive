using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Models;
using NinjaHive.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Controllers
{
    [Authorize]
    public class EquipmentController : BaseController
    {
        List<EquipmentModel> tempList = new List<EquipmentModel>{
                new EquipmentModel { Id=new Guid("45216fda-6549-5532-432f-afd65a8a7899"), Name = "Sword 01", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons", Description="The first sword in the set.", Value=50 },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 02", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Sword 03", SubCategoryName="Swords", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 01", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Axe 02", SubCategoryName="Axe", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Hammer 01", SubCategoryName="Hammer", SubCategoryMainCategoryName="Weapons" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 01", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Helm 02", SubCategoryName="Helmets", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 01", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Leg 01", SubCategoryName="Trousers", SubCategoryMainCategoryName="Armor" },
                new EquipmentModel { Id=Guid.NewGuid(), Name = "Torso 02", SubCategoryName="Torsos", SubCategoryMainCategoryName="Armor" },
            };

        public EquipmentController()
        {
            //TODO: populate readonly fields for repository and query engine
        }

        public ActionResult Index()
        {
            //TODO: query database
            return View(tempList);
        }

        public ActionResult Create()
        {
            return View(PrepareViewModel(new EquipmentModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return base.Home();
            }
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            //TODO: query database
            return View(PrepareViewModel(tempList.First(m => m.Id == id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return base.Home();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return base.Home();
        }

        [HttpGet]
        public JsonResult GetEquipmentJson(string name)
        {
            //TODO: query database
            return Json(tempList.Where(d => d.Name == name), JsonRequestBehavior.AllowGet);
        }

        private EquipmentViewModel PrepareViewModel(EquipmentModel model)
        {
            var viewModel = new EquipmentViewModel();
            viewModel.equipment = model;

            //TODO: query categories database
            viewModel.mainCategories = new MainCategoryModel[1];
            viewModel.categories = new SubCategoryModel[1];

            return viewModel;
        }
    }
}