using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

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
            db.GameItemEntities.Remove(db.GameItemEntities.Find(command.EquipmentItem.Id));
        }
    }
}
