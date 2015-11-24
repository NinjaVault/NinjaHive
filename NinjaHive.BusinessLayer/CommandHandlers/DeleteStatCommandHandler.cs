using NinjaHive.Contract.Commands;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.CommandHandlers
{
    public class DeleteStatCommandHandler
        : ICommandHandler<DeleteStatCommand>
    {
        private readonly IRepository<StatInfoEntity> statRepository;

        public DeleteStatCommandHandler(IRepository<StatInfoEntity> statRepository)
        {
            this.statRepository = statRepository;
        }

        public void Handle(DeleteStatCommand command)
        {
            var stat = this.statRepository.GetById(command.Stat.Id);
            this.statRepository.Remove(stat);
        }
    }
}