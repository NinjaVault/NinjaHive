using System;
using System.Web.Mvc;
using System.Collections.Generic;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Services;
using NinjaHive.WebApp.Models.Skills;

namespace NinjaHive.WebApp.Controllers
{
    public class SkillsController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<EditSkillCommand> editSkillCommandHandler;
        private readonly ICommandHandler<DeleteSkillCommand> deleteSkillCommandHandler;
        private readonly ICommandHandler<EditStatCommand> editStatCommandHandler;
        private readonly ICommandHandler<DeleteStatCommand> deleteStatCommandHandler;

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
            this.deleteStatCommandHandler = deleteStatCommandHandler;
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
            var stat = this.queryProcessor.Execute(new GetEntityByIdQuery<StatInfo>(skill.StatInfoId));

            var skillViewModel = new SkillViewModel();
            UpdateSkillViewModel(skillViewModel, skill);
            UpdateSkillViewModel(skillViewModel, stat);
            
            return View(skillViewModel);
        }

        [HttpPost]
        public ActionResult Edit(SkillViewModel viewModel)
        {
            var skill = new Skill();
            UpdateSkill(skill, viewModel);

            var stat = new StatInfo();
            UpdateStat(stat, viewModel);

            var editSkillcommand = new EditSkillCommand(skill, createNew: false);
            this.editSkillCommandHandler.Handle(editSkillcommand);

            var editStatCommand = new EditStatCommand(stat, createNew: false);
            this.editStatCommandHandler.Handle(editStatCommand);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }


        public ActionResult Create()
        {
            var view = new SkillViewModel();
            return View(view);
        }

        [HttpPost]
        public ActionResult Create(SkillViewModel viewModel)
        {
            var skill = new Skill();
            UpdateSkill(skill, viewModel);

            skill.Id = Guid.NewGuid();
            var editSkillcommand = new EditSkillCommand(skill, createNew: true);
            this.editSkillCommandHandler.Handle(editSkillcommand);

            var stat = new StatInfo();
            UpdateStat(stat, viewModel);

            stat.Id = Guid.NewGuid();
            var editStatCommand = new EditStatCommand(stat, createNew: true);
            this.editStatCommandHandler.Handle(editStatCommand);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }  

        public ActionResult Delete(Guid skill)
        {
            var deleteSkillCommand = new DeleteSkillCommand()
            {
                Skill = this.queryProcessor.Execute(new GetEntityByIdQuery<Skill>(skill))
            };

            var deleteStatCommand = new DeleteStatCommand()
            {
                Stat = this.queryProcessor.Execute(new GetEntityByIdQuery<StatInfo>(deleteSkillCommand.Skill.StatInfoId))
            };

            deleteSkillCommandHandler.Handle(deleteSkillCommand);
            deleteStatCommandHandler.Handle(deleteStatCommand);

            var redirectUri = UrlProvider<SkillsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        private void UpdateSkill(Skill skill, SkillViewModel viewModel)
        {
            skill.Id = viewModel.SkillId;
            skill.Name = viewModel.SkillName;
            skill.Radius = viewModel.SkillRadius;
            skill.Range = viewModel.SkillRange;
            skill.StatInfoId = viewModel.StatId;
            skill.Target = viewModel.SkillTarget;
            skill.TargetCount = viewModel.SkillTargetCount;
        }

        private void UpdateSkillViewModel(SkillViewModel viewModel, Skill skill)
        {
            viewModel.SkillId = skill.Id;
            viewModel.SkillName = skill.Name;
            viewModel.SkillRadius = skill.Radius;
            viewModel.SkillRange = skill.Range;
            viewModel.SkillTarget = skill.Target;
            viewModel.SkillTargetCount = skill.TargetCount;
        }

        private void UpdateSkillViewModel(SkillViewModel viewModel, StatInfo stat)
        {
            viewModel.StatAgility = stat.Agility;
            viewModel.StatAttack = stat.Attack;
            viewModel.StatDefense = stat.Defense;
            viewModel.StatHealth = stat.Health;
            viewModel.StatHunger = stat.Hunger;
            viewModel.StatId = stat.Id;
            viewModel.StatIntelligence = stat.Intelligence;
            viewModel.StatMagic = stat.Magic;
            viewModel.StatResistance = stat.Resistance;
            viewModel.StatStamina = stat.Stamina;
        }

        private void UpdateStat(StatInfo stat, SkillViewModel viewModel)
        {
            stat.Agility = viewModel.StatAgility;
            stat.Attack = viewModel.StatAttack;
            stat.Defense = viewModel.StatDefense;
            stat.Health = viewModel.StatDefense;
            stat.Hunger = viewModel.StatHunger;
            stat.Id = viewModel.StatId;
            stat.Intelligence = viewModel.StatIntelligence;
            stat.Magic = viewModel.StatMagic;
            stat.Resistance = viewModel.StatResistance;
            stat.Stamina = viewModel.StatStamina;
        }
    }
}
