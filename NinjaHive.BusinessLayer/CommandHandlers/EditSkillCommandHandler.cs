using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Enums;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class EditSkillCommandHandler : ICommandHandler<EditSkillCommand>
    {
        private readonly IRepository<SkillEntity> skillsRepository;

        public EditSkillCommandHandler(IRepository<SkillEntity> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public void Handle(EditSkillCommand command)
        {
            var skill = command.CreateNew
                ? new SkillEntity()
                : this.skillsRepository.GetById(command.Skill.Id);

            skill.Id = command.Skill.Id;
            skill.Name = command.Skill.Name;
            skill.Radius = command.Skill.Radius;
            skill.Range = command.Skill.Range;
            skill.Target = (Target) command.Skill.Target;
            skill.Targets = command.Skill.TargetCount;
            skill.Friendly = command.Skill.Friendly;
            skill.StatInfo = new StatInfoEntity {Id = Guid.NewGuid()};

            if (command.CreateNew)
            {
                skill.Id = command.Skill.Id;
                this.skillsRepository.Add(skill);
            }
        }
    }
}
