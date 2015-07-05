using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Entities;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
    {
        public readonly NinjaHiveEntities db;

        public DeleteItemCommandHandler(NinjaHiveEntities db)
        {
            this.db = db;
        }

        public void Handle(DeleteItemCommand command)
        {
            var item = db.Items.Find(command.Item.ItemId);
            db.Items.Remove(item);
        }
    }
}
