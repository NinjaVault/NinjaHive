using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Entities;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
    {
        public readonly NinjaHiveEntities db;

        public CreateItemCommandHandler(NinjaHiveEntities db)
        {
            this.db = db;
        }

        public void Handle(CreateItemCommand command)
        {
            this.db.Items.Add(new ItemEntity
            {
                Id = command.Item.ItemId,
                Name = command.Item.Name,
            });
        }
    }
}
