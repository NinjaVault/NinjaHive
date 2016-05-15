using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.Models;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.CommandHandlers.Tiers
{
    public class TierCommandHandlers :
        ICommandHandler<CreateEntityCommand<TierModel>>,
        ICommandHandler<UpdateEntityCommand<TierModel>>,
        ICommandHandler<DeleteEntityCommand<TierModel>>
    {
        private readonly IRepository<EquipmentItemEntity> equipmentRepository;
        private readonly IRepository<TierEntity> tierRepository;

        public TierCommandHandlers(
            IRepository<EquipmentItemEntity> equipmentRepository,
            IRepository<TierEntity> tierRepository)
        {
            this.equipmentRepository = equipmentRepository;
            this.tierRepository = tierRepository;
        }

        public void Handle(CreateEntityCommand<TierModel> command)
        {
            var equipment = this.equipmentRepository.FindById(command.Model.EquipmentItemId);
            var tier = new TierEntity
            {
                Name = command.Model.Name,
                Tier = command.Model.Tier,
                EquipmentItem = equipment,
            };
            this.tierRepository.Add(tier);
        }

        public void Handle(UpdateEntityCommand<TierModel> command)
        {
            throw new NotImplementedException();
        }

        public void Handle(DeleteEntityCommand<TierModel> command)
        {
            throw new NotImplementedException();
        }
    }
}
