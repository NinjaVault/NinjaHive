using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.GameItems
{
    public class EquipmentItemCommandHandlers :
        GameItemCommandHandlers<EquipmentModel, EquipmentItemEntity>
    {
        public EquipmentItemCommandHandlers(
            IRepository<EquipmentItemEntity> itemRepository,
            IRepository<SubCategoryEntity> categoryRepository)
            : base(itemRepository, categoryRepository)
        {
        }

        public override void Handle(CreateEntityCommand<EquipmentModel> command)
        {
            var entity = new EquipmentItemEntity();
            this.UpdateItem(entity, command.Model);
            this.itemRepository.Add(entity);
        }

        public override void Handle(UpdateEntityCommand<EquipmentModel> command)
        {
            var entity = this.itemRepository.FindById(command.Id);
            base.UpdateItem(entity, command.Model);
        }

        public override void Handle(DeleteEntityCommand<EquipmentModel> command)
        {
            this.itemRepository.RemoveById(command.Id);
        }

        protected override void UpdateItem(EquipmentItemEntity entity, EquipmentModel model)
        {
            base.UpdateItem(entity, model);

            entity.BodySlot = model.BodySlot;
            entity.Durability = model.Durability;
        }
    }
}
