using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class DeleteEquipmentItemCommandHandler 
        : ICommandHandler<DeleteEquipmentItemCommand>
    {
        private readonly IRepository<EquipmentItemEntity> equipmentItemRepository;

        public DeleteEquipmentItemCommandHandler(IRepository<EquipmentItemEntity> equipmentItemRepository)
        {
            this.equipmentItemRepository = equipmentItemRepository;
        }

        public void Handle(DeleteEquipmentItemCommand command)
        {
            var equipmentItem = this.equipmentItemRepository.GetById(command.EquipmentItem.Id);
            this.equipmentItemRepository.Remove(equipmentItem);
        }
    }
}
