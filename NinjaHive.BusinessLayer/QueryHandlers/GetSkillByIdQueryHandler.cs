using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetSkillByIdQueryHandler
        : IQueryHandler<GetSkillByIdQuery, Skill>
    {
        private readonly NinjaHiveContext db;
        private readonly IMapper<SkillEntity, Skill> skillMapper;

        public GetSkillByIdQueryHandler(NinjaHiveContext db,
            IMapper<SkillEntity, Skill> skillMapper)
        {
            this.db = db;
            this.skillMapper = skillMapper;
        }

        public Skill Handle(GetSkillByIdQuery query)
        {
            var skill = (SkillEntity)db.SkillEntities.Find(query.SkillId);

            return this.skillMapper.Map(skill);
        }
    }
}
