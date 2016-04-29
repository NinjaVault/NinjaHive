using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.WebApp.Tests.Mocks.Contract
{
    public class AddGameItemCommand
    {
        public AddGameItemCommand(GameItemModel item)
        {
            this.Item = new CreateEntityCommand<GameItemModel>(item);
        }

        [ValidateObjectDeep]
        public CreateEntityCommand<GameItemModel> Item { get; private set; }
    }

    public class AddGameItemCommandHandler : ICommandHandler<AddGameItemCommand>
    {
        public void Handle(AddGameItemCommand command)
        {
        }
    }
}
