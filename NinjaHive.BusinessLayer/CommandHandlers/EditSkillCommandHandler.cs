using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Enums;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class EditSkillCommandHandler : ICommandHandler<EditSkillCommand>
    {
        private readonly NinjaHiveContext db;

        public EditSkillCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public void Handle(EditSkillCommand command)
        {
            var skill = command.CreateNew
                ? new SkillEntity()
                : this.db.SkillEntities.GetById(command.Skill.Id);

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
                this.db.SkillEntities.Add(skill);
            }
        }
    }
}
