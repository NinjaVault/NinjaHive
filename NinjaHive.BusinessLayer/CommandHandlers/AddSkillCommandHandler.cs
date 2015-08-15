using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class AddSkillCommandHandler : ICommandHandler<AddSkillCommand>
    {
        private readonly NinjaHiveContext db;

        public AddSkillCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public void Handle(AddSkillCommand command)
        {
            var skill = new SkillEntity
            {
                Id = command.Skill.Id,
                Name = command.Skill.Name,
                StatInfo = new StatInfoEntity { Id = Guid.NewGuid() }
            };

            this.db.SkillEntities.Add(skill);
        }
    }
}
