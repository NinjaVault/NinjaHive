using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class TierCRUDCommandHandlers :
        ICommandHandler<CreateEntityCommand<TierModel>>,
        ICommandHandler<UpdateEntityCommand<TierModel>>,
        ICommandHandler<DeleteEntityCommand<TierModel>>
    {
        private readonly IRepository<TierEntity> tierRepository;

        public TierCRUDCommandHandlers(IRepository<TierEntity> tierRepository,
            IRepository<SkillEntity> skillRepository)
        {
            this.tierRepository = tierRepository;
        }

        public void Handle(CreateEntityCommand<TierModel> command)
        {
            var tierEntity = new TierEntity();
            this.UpdateTier(tierEntity, command.Model);

            tierRepository.Add(tierEntity);
        }

        public void Handle(UpdateEntityCommand<TierModel> command)
        {
            var tierEntity = this.tierRepository.FindById(command.Id);
            UpdateTier(tierEntity, command.Model);
        }

        public void Handle(DeleteEntityCommand<TierModel> command)
        {
            tierRepository.RemoveById(command.Id);
        }

        private void UpdateTier(TierEntity entity, TierModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
