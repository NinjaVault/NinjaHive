using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.CommandHandlers.GameItems
{
    public class OtherItemCRUDCommandHandlers :
        ICommandHandler<CreateEntityCommand<OtherItemModel>>,
        ICommandHandler<UpdateEntityCommand<OtherItemModel>>,
        ICommandHandler<DeleteEntityCommand<OtherItemModel>>
    {
        public void Handle(CreateEntityCommand<OtherItemModel> command)
        {
            throw new NotImplementedException();
        }

        public void Handle(UpdateEntityCommand<OtherItemModel> command)
        {
            throw new NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<OtherItemModel> command)
        {
            throw new NotImplementedException();
        }
    }
}
