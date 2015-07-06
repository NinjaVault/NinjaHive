using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Entities;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class SaveItemCommandHandler : ICommandHandler<SaveItemCommand>
    {
        private readonly NinjaHiveEntities db;

        public SaveItemCommandHandler(NinjaHiveEntities db)
        {
            this.db = db;
        }

        public void Handle(SaveItemCommand command)
        {
            ItemEntity item = this.db.Items.Find(command.Item.ItemId);

            if (item == null) {
                return;
            }

            item.Name = command.Item.Name;
        }
    }
}