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
            var skills =
                from skill in this.db.SkillEntities
                select new Skill
                {
                    Id = skill.Id,
                    Name = skill.Name,
                };

            return skills.ToArray();
        }
    }
}
