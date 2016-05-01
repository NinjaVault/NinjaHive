using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Controllers
{
    public class SkillsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;

        List<SkillModel> tempList = new List<SkillModel>
        {
            new SkillModel { Id = Guid.Parse("26851558-4568-7895-5568-123645215468"), Name="First Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Second Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Third Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Fourth Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Fifth Skill"},
        };

        public SkillsController(IQueryProcessor queryProcessor)
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
            var viewModel = PrepareViewModel(new SkillModel());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillViewModel viewModel)
        {
            if (ModelState.IsValid)
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
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillViewModel model)
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

        private SkillViewModel PrepareViewModel(SkillModel model)
        {
            return new SkillViewModel { Skill = model };
        }

        protected override RedirectResult Home()
        {
            return base.Redirect<SkillsController>(c => c.Index());
        }
    }
}