using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class DeleteEquipmentItemCommandHandler 
        : ICommandHandler<DeleteEquipmentItemCommand>
    {
        private readonly NinjaHiveContext db;
        
        public DeleteEquipmentItemCommandHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public void Handle(DeleteEquipmentItemCommand command)
        {
            var equipmentItem = this.db.GameItemEntities.GetById(command.EquipmentItem.Id);
            db.GameItemEntities.Remove(equipmentItem);
        }
    }
}
