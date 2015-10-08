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
        private readonly IQueryHandler<GetAllSkillsQuery, Skill[]> skillsQueryHandler;
        private readonly ICommandHandler<AddSkillCommand> addSkillCommandHandler;
        private readonly IQueryHandler<GetSkillByIdQuery, Skill> getSkillByIdCommandHandler;

        public SkillsController(IQueryHandler<GetAllSkillsQuery, Skill[]> skillsQueryHandler,
            ICommandHandler<AddSkillCommand> addSkillCommandHandler,
            IQueryHandler<GetSkillByIdQuery, Skill> getSkillByIdCommandHandler)
        {
            this.skillsQueryHandler = skillsQueryHandler;
            this.addSkillCommandHandler = addSkillCommandHandler;
            this.getSkillByIdCommandHandler = getSkillByIdCommandHandler;
        }

        // GET: Skills
        public ActionResult Index()
        {
            var skills = this.skillsQueryHandler.Handle(new GetAllSkillsQuery());
            return View(skills);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid skillId)
        {
            var skill = this.getSkillByIdCommandHandler.Handle(new GetSkillByIdQuery
            {
                SkillId = skillId
            });

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