using NinjaHive.BusinessLayer.Extensions;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.GameItems
{
    public class OtherItemCommandHandlers :
        GameItemCommandHandlers<OtherItemModel, OtherItemEntity>
    {
        public OtherItemCommandHandlers(
            IRepository<OtherItemEntity> itemRepository,
            IRepository<SubCategoryEntity> categoryRepository)
            : base(itemRepository, categoryRepository)
        {
        }

        public override void Handle(CreateEntityCommand<OtherItemModel> command)
        {
            var entity = new OtherItemEntity
            {
                StatInfo = new StatInfoEntity(),
            };
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public override void Handle(UpdateEntityCommand<OtherItemModel> command)
        {
            var entity = this.itemRepository.FindById(command.Id);
            this.UpdateItem(entity, command.Model);
        }

        public override void Handle(DeleteEntityCommand<OtherItemModel> command)
        {
            this.itemRepository.RemoveById(command.Id);
        }

        protected override void UpdateItem(OtherItemEntity entity, OtherItemModel model)
        {
            base.UpdateItem(entity, model);

            entity.IsEnhancer = model.IsEnhancer;

            entity.StatInfo.UpdateStatsEntityFromModel(model.StatInfo);
        }
    }
}
