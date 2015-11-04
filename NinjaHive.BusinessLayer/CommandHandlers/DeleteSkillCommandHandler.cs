using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class DeleteSkillCommandHandler
        : ICommandHandler<DeleteSkillCommand>
    {
        private readonly IRepository<SkillEntity> skillEntityRepository;

        public DeleteSkillCommandHandler(IRepository<SkillEntity> skillEntityRepository)
        {
            this.skillEntityRepository = skillEntityRepository;
        }

        public void Handle(DeleteSkillCommand command)
        {
            var skillItem = this.skillEntityRepository.GetById(command.Skill.Id);
            this.skillEntityRepository.Remove(skillItem);
        }
    }
}
