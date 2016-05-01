using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class SkillCURDCommandHandlers :
        ICommandHandler<CreateEntityCommand<SkillModel>>,
        ICommandHandler<UpdateEntityCommand<SkillModel>>,
        ICommandHandler<DeleteEntityCommand<SkillModel>>
    {
        private readonly IRepository<SkillEntity> skillRepository;
        private readonly IRepository<StatInfoEntity> statInfoRepository;
        private readonly IRepository<SpecialEntity> specialRepository;
        
        public SkillCURDCommandHandlers(IRepository<SkillEntity> skillRepository,
            IRepository<StatInfoEntity> statInfoRepository,
            IRepository<SpecialEntity> specialRepository)
        {
            this.skillRepository = skillRepository;
            this.statInfoRepository = statInfoRepository;
            this.specialRepository = specialRepository;
        }

        public void Handle(CreateEntityCommand<SkillModel> command)
        {
            var entity = new SkillEntity();
            var statInfo = new StatInfoEntity();
            entity.StatInfo = statInfo;

            this.UpdateSkill(entity, command.Model);
            this.skillRepository.Add(entity);
            this.statInfoRepository.Add(statInfo);
        }

        public void Handle(UpdateEntityCommand<SkillModel> command)
        {
            var entity = this.skillRepository.FindById(command.Id);
            this.UpdateSkill(entity, command.Model);
        }

        public void Handle(DeleteEntityCommand<SkillModel> command)
        {
            this.skillRepository.RemoveById(command.Id);
        }

        private void UpdateSkill(SkillEntity entity, SkillModel model)
        {
            // skill update
            entity.Name = model.Name;
            entity.Radius = model.Radius;
            entity.Range = model.Range;
            entity.Target = model.Target;
            entity.Targets = model.Targets;

            // StatInfo udpate
            entity.StatInfo.Id = model.StatInfo.Id;
            entity.StatInfo.Agility = model.StatInfo.Agility;
            entity.StatInfo.Attack = model.StatInfo.Attack;
            entity.StatInfo.Defense = model.StatInfo.Defense;
            entity.StatInfo.Health = model.StatInfo.Health;
            entity.StatInfo.Hunger = model.StatInfo.Hunger;
            entity.StatInfo.Intelligence = model.StatInfo.Intelligence;
            entity.StatInfo.Magic = model.StatInfo.Magic;
            entity.StatInfo.Resistance = model.StatInfo.Resistance;
            entity.StatInfo.Stamina = model.StatInfo.Stamina;

            // TODO: Special update
            
        }
    }
}
