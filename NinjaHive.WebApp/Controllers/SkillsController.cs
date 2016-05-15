using NinjaHive.Contract.Models;
using NinjaHive.Core;
using System;
using System.Web.Mvc;
using NinjaHive.Contract.Queries.Skills;
using NinjaHive.Core.Models;

namespace NinjaHive.WebApp.Controllers
{
    public class SkillsController : BaseController
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IUnitOfWork<SkillModel> skillRepository;

        public SkillsController(
            IQueryProcessor queryProcessor,
            IUnitOfWork<SkillModel> skillRepository)
        {
            this.queryProcessor = queryProcessor;
            this.skillRepository = skillRepository;
        }


        public ActionResult Index()
        {
            var skills = this.queryProcessor.Execute(new GetAllSkillsQuery());
            return View(skills);
        }

        public ActionResult Create()
        {
            return View(new SkillModel());
        }
        public ActionResult Edit(Guid id)
        {
            var model = this.skillRepository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillModel model)
        {
            return this.UpdateModelForPostResult(model, this.skillRepository.Create);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillModel model)
        {
            return this.UpdateModelForPostResult(model, this.skillRepository.Update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            //TODO: server side validation
            this.skillRepository.Delete(id);
            return this.RedirectToIndex();
        }

        private ActionResult UpdateModelForPostResult(SkillModel model, Func<SkillModel, WorkResult> unitOfWork)
        {
            if (ModelState.IsValid)
            {
                var result = unitOfWork.Invoke(model);
                if (result.IsValid)
                {
                    return this.RedirectToIndex();
                }
                //TODO: server side validation
            }
            return View(model);
        }

        private ActionResult RedirectToIndex()
        {
            return base.Redirect<SkillsController>(c => c.Index());
        }
    }
}