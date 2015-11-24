using System;
using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Enums;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    class EditStatCommandHandler : ICommandHandler<EditStatCommand>
    {
        private readonly IRepository<StatInfoEntity> statsRepository;

        public EditStatCommandHandler(IRepository<StatInfoEntity> statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        public void Handle(EditStatCommand command)
        {
            var stat = command.CreateNew
                ? new StatInfoEntity()
                : this.statsRepository.GetById(command.Stat.Id);

            stat.Id = command.Stat.Id;
            stat.Health = command.Stat.Health;
            stat.Magic = command.Stat.Magic;
            stat.Attack = command.Stat.Attack;
            stat.Defense = command.Stat.Defense;
            stat.Stamina = command.Stat.Stamina;
            stat.Hunger = command.Stat.Hunger;
            stat.Intelligence = command.Stat.Intelligence;
            stat.Resistance = command.Stat.Resistance;

            if (command.CreateNew)
            {
                this.statsRepository.Add(stat);
            }
        }
    }
}
