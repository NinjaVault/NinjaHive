using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllSkillsQueryHandler
        : IQueryHandler<GetAllSkillsQuery, Skill[]>
    {
        private readonly NinjaHiveContext db;

        public GetAllSkillsQueryHandler(NinjaHiveContext db)
        {
            this.db = db;
        }

        public Skill[] Handle(GetAllSkillsQuery query)
        {
            var skills = this.db.SkillEntities;
            return skills.Select(s => new Skill
            {
                Id = s.Id,
                Name = s.Name
            }).ToArray();
        }
    }
}
