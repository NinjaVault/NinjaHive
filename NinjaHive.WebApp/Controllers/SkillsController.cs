using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Controllers
{
    public class SkillsController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<AddSkillCommand> addSkillCommandHandler;

        public SkillsController(
            IQueryProcessor queryProcessor,
            ICommandHandler<AddSkillCommand> addSkillCommandHandler)
        {
            this.queryProcessor = queryProcessor;
            this.addSkillCommandHandler = addSkillCommandHandler;
        }

        // GET: Skills
        public ActionResult Index()
        {
            var skills = this.queryProcessor.Execute(new GetAllSkillsQuery());
            return View(skills);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid skillId)
        {
            var skill = this.queryProcessor.Execute(new GetEntityByIdQuery<Skill>(skillId));

            return View(skill);
        }

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            skill.Id = Guid.NewGuid();
            var command = new AddSkillCommand
            {
                Skill = skill,
            };
            this.addSkillCommandHandler.Handle(command);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}