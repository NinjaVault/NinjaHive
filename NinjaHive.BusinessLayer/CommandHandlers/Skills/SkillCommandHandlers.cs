using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;
using NinjaHive.BusinessLayer.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.Skills
{
    public class SkillCommandHandlers :
        ICommandHandler<CreateEntityCommand<SkillModel>>,
        ICommandHandler<UpdateEntityCommand<SkillModel>>,
        ICommandHandler<DeleteEntityCommand<SkillModel>>
    {
        private readonly IRepository<SkillEntity> skillRepository;

        public SkillCommandHandlers(IRepository<SkillEntity> skillRepository)
        {
            this.skillRepository = skillRepository;
        }

        public void Handle(CreateEntityCommand<SkillModel> command)
        {
            var skill = new SkillEntity()
            {
                StatInfo = new StatInfoEntity(),
            };

            this.UpdateSkillEntityFromModel(skill, command.Model);
            this.skillRepository.Add(skill);
        }

        public void Handle(UpdateEntityCommand<SkillModel> command)
        {
            var skill = this.skillRepository.FindById(command.Id);
            this.UpdateSkillEntityFromModel(skill, command.Model);
        }

        public void Handle(DeleteEntityCommand<SkillModel> command)
        {
            this.skillRepository.RemoveById(command.Id);
        }

        private void UpdateSkillEntityFromModel(SkillEntity entity, SkillModel model)
        {
            entity.Name = model.Name;
            entity.Range = model.Range;
            entity.Radius = model.Radius;
            entity.Targets = model.Targets;
            entity.Target = model.Target;
            entity.Friendly = model.Friendly;

            entity.StatInfo.UpdateStatsEntityFromModel(model.StatInfo);
        }
    }
}
