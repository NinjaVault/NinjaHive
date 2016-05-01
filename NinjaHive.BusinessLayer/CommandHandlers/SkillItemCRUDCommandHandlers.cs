using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class SkillItemCRUDCommandHandlers :
        ICommandHandler<CreateEntityCommand<SkillItemModel>>,
        ICommandHandler<UpdateEntityCommand<SkillItemModel>>,
        ICommandHandler<DeleteEntityCommand<SkillItemModel>>
    {
        private readonly IRepository<SkillItemEntity> skillItemRepository;
        private readonly IRepository<SkillEntity> skillRepository;

        public SkillItemCRUDCommandHandlers(IRepository<SkillItemEntity> skillItemRepository,
            IRepository<SkillEntity> skillRepository)
        {
            this.skillItemRepository = skillItemRepository;
            this.skillRepository = skillRepository;
        }

        public void Handle(CreateEntityCommand<SkillItemModel> command)
        {
            var skillItemEntity = new SkillItemEntity();
            var skillEntity = new SkillEntity();

            this.UpdateSkillItem(skillItemEntity, command.Model);
            this.skillItemRepository.Add(skillItemEntity);
            this.skillRepository.Add(skillEntity);
        }

        public void Handle(UpdateEntityCommand<SkillItemModel> command)
        {
            var skillItemEntity = skillItemRepository.FindById(command.Id);
            this.UpdateSkillItem(skillItemEntity, command.Model);
        }

        public void Handle(DeleteEntityCommand<SkillItemModel> command)
        {
            this.skillItemRepository.RemoveById(command.Id);
        }

        public void UpdateSkillItem(SkillItemEntity entity, SkillItemModel model)
        {
            entity.Craftable = model.Craftable;
            entity.Description = model.Description;
            entity.IsCrafter = model.IsCrafter;
            entity.IsQuestItem = model.IsQuestItem;
            entity.IsUpgrader = model.IsUpgrader;
            entity.Name = model.Name;
            entity.Value = model.Value;

            // skillUpdate
            entity.Skill.Id = model.Skill.Id;
            entity.Skill.Name = model.Skill.Name;
            entity.Skill.Radius = model.Skill.Radius;
            entity.Skill.Range = model.Skill.Range;

            // statInfoUpdate
            entity.Skill.StatInfo.Id = model.StatInfo.Id;
            entity.Skill.StatInfo.Health = model.StatInfo.Health;
            entity.Skill.StatInfo.Agility = model.StatInfo.Agility;
            entity.Skill.StatInfo.Attack = model.StatInfo.Attack;
            entity.Skill.StatInfo.Defense = model.StatInfo.Defense;
            entity.Skill.StatInfo.Hunger = model.StatInfo.Hunger;
            entity.Skill.StatInfo.Intelligence = model.StatInfo.Intelligence;
            entity.Skill.StatInfo.Magic = model.StatInfo.Magic;
            entity.Skill.StatInfo.Resistance = model.StatInfo.Resistance;
            entity.Skill.StatInfo.Stamina = model.StatInfo.Stamina;

        }
    }
}
