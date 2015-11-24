using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllStatsQueryHandler
        : IQueryHandler<GetAllStatsQuery, StatInfo[]>
    {
        private readonly IRepository<StatInfoEntity> statsRepository;

        public GetAllStatsQueryHandler(IRepository<StatInfoEntity> statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        public StatInfo[] Handle(GetAllStatsQuery query)
        {
            var stats =
                from stat in this.statsRepository.Entities
            select new StatInfo
            {
                Id = stat.Id,
                Health = stat.Health,
                Magic = stat.Magic,
                Attack = stat.Attack,
                Defense = stat.Defense,
                Hunger = stat.Hunger,
                Stamina = stat.Stamina,
                Resistance = stat.Resistance
            };

            return stats.ToArray();
        }
    }
}
