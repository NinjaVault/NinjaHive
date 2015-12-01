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
        private readonly ICommandHandler<EditSkillCommand> editSkillCommandHandler;
        private readonly ICommandHandler<DeleteSkillCommand> deleteSkillCommandHandler;
        private readonly ICommandHandler<EditStatCommand> editStatCommandHandler;
        private readonly ICommandHandler<DeleteStatCommand> deleteStatCommand;

        public SkillsController(
            IQueryProcessor queryProcessor,
            ICommandHandler<EditSkillCommand> editSkillCommandHandler,
            ICommandHandler<DeleteSkillCommand> deleteSkillCommandHandler,
            ICommandHandler<EditStatCommand> editStatCommandHandler,
            ICommandHandler<DeleteStatCommand> deleteStatCommandHandler)
        {
            this.queryProcessor = queryProcessor;
            this.editSkillCommandHandler = editSkillCommandHandler;
            this.deleteSkillCommandHandler = deleteSkillCommandHandler;
            this.editStatCommandHandler = editStatCommandHandler;
            this.deleteStatCommand = deleteStatCommand;
        }

        // GET: Skills
        public ActionResult Index()
        {
            var skills = this.queryProcessor.Execute(new GetAllSkillsQuery());
            return View(skills);
        }
      
        public ActionResult Edit(Guid skillId)
        {
            var skill = this.queryProcessor.Execute(new GetEntityByIdQuery<Skill>(skillId));

            return View(skill);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Skill skill)
        {
            skill.Id = Guid.NewGuid();
            var command = new EditSkillCommand(skill, createNew: true);
            this.editSkillCommandHandler.Handle(command);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }  

        public ActionResult Delete(Skill skill)
        {
            var command = new DeleteSkillCommand()
            {
                Skill = skill
            };

            deleteSkillCommandHandler.Handle(command);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}
