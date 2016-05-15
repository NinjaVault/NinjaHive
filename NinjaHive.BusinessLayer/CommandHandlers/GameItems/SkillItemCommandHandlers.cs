using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.GameItems
{
    public class SkillItemCommandHandlers :
        GameItemCommandHandlers<SkillItemModel, SkillItemEntity>
    {
        private readonly IRepository<SkillEntity> skillRepository;

        public SkillItemCommandHandlers(
            IRepository<SkillItemEntity> itemRepository,
            IRepository<SubCategoryEntity> categoryRepository,
            IRepository<SkillEntity> skillRepository)
            : base(itemRepository, categoryRepository)
        {
            this.skillRepository = skillRepository;
        }

        public override void Handle(CreateEntityCommand<SkillItemModel> command)
        {
            var entity = new SkillItemEntity();
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public override void Handle(UpdateEntityCommand<SkillItemModel> command)
        {
            var entity = this.itemRepository.FindById(command.Id);
            this.UpdateItem(entity, command.Model);
        }

        public override void Handle(DeleteEntityCommand<SkillItemModel> command)
        {
            this.itemRepository.RemoveById(command.Id);
        }

        protected override void UpdateItem(SkillItemEntity entity, SkillItemModel model)
        {
            base.UpdateItem(entity, model);
            entity.Skill = this.skillRepository.FindById(model.SkillId);
        }
    }
}
