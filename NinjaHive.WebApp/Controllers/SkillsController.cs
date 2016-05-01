using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.Skills;

namespace NinjaHive.WebApp.Controllers
{
    public class SkillsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<SkillModel> skillRepository;

        List<SkillModel> tempList = new List<SkillModel>
        {
            new SkillModel { Id = Guid.Parse("26851558-4568-7895-5568-123645215468"), Name="First Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Second Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Third Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Fourth Skill"},
            new SkillModel { Id = Guid.NewGuid(), Name="Fifth Skill"},
        };

        public SkillsController(IQueryProcessor queryProcessor,
            IUnitOfWork<SkillModel> skillRepository)
        {
            this.queryProcessor = queryProcessor;
            this.skillRepository = skillRepository;
        }


        public ActionResult Index()
        {
            var skills = queryProcessor.Execute(new GetAllSkillsQuery());
            return View(skills);
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
                skillRepository.Create(viewModel.Skill);
                return this.Home();
            }
            return View(viewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var model = skillRepository.GetById(id);

            var viewModel = PrepareViewModel(model);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillViewModel model)
        {
            if (ModelState.IsValid)
            {
                skillRepository.Update(model.Skill);
                return this.Home();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            skillRepository.Delete(id);
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