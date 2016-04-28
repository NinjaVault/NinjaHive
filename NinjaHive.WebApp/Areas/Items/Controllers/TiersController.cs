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
    public class TiersController : BaseController
    {
        IEnumerable<TierModel> tempList = new List<TierModel> {
                new TierModel{Id = Guid.Parse("43213232-4555-6899-1245-789435365457"), StatInfo = new StatInfoModel{Health = 200, Attack = 20, Defense = 15, Agility = 15, Resistance = 0.5f}, Name="First Tier", TierRank = 0},
                new TierModel{Id = Guid.NewGuid(), StatInfo = new StatInfoModel(), Name="Second Tier", TierRank = 1},
                new TierModel{Id = Guid.NewGuid(), StatInfo = new StatInfoModel(), Name="Third Tier", TierRank = 2},
                new TierModel{Id = Guid.NewGuid(), StatInfo = new StatInfoModel(), Name="Fourth Tier", TierRank = 3}
            };

        public TiersController()
        {
        }


        public ActionResult Index(Guid equipmentId)
        {
            //TODO: query database
            var viewModel = PrepareListViewModel(equipmentId);
            return View(viewModel);
        }

        public ActionResult Create(Guid equipmentId)
        {
            var model = new TierModel { StatInfo = new StatInfoModel() };
            var viewModel = new TierViewModel {
                Tier = model,
                Equipment = GetEquipmentById(equipmentId)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return this.Home(viewModel.Equipment.Id);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: query database
            return this.Home(GetParentEquipment(id).Id);
        }

        public ActionResult Edit(Guid id)
        {
            var viewModel = PrepareViewModel(tempList.First(c => c.Id == id));
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: query database
                return this.Home(viewModel.Equipment.Id);
            }
            return View(viewModel);
        }




        private IEnumerable<TierModel> GetTiersForEquipment(Guid id)
        {
            //TODO: Implement actual listing
            return tempList;
        }

        private EquipmentModel GetParentEquipment(Guid id)
        {
            //TODO: query database
            return new EquipmentModel { Name = "Unimplemented", Id = id };
        }
        private EquipmentModel GetParentEquipment(TierModel tier)
        {
            //TODO: query database
            return GetParentEquipment( tier.Id );
        }
        private EquipmentModel GetEquipmentById(Guid id)
        {
            //TODO: query database
            return new EquipmentModel { Name = "Unimplemented", Id = id };
        }

        private TiersListViewModel PrepareListViewModel(Guid equipmentId)
        {
            var viewModel = new TiersListViewModel();
            viewModel.Equipment = GetParentEquipment(equipmentId);
            viewModel.Tiers = GetTiersForEquipment(equipmentId);

            return viewModel;
        }

        private TierViewModel PrepareViewModel(TierModel model)
        {
            var viewModel = new TierViewModel();
            viewModel.Equipment = GetParentEquipment(model);
            viewModel.Tier = model;

            return viewModel;
        }

        protected virtual RedirectResult Home(Guid id)
        {
            return Redirect<TiersController>(c => c.Index(id));
        }
    }
}