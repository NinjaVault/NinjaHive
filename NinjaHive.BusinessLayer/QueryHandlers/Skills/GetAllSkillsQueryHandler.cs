using System;
using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Skills;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers.Skills
{
    public class GetAllSkillsQueryHandler : IQueryHandler<GetAllSkillsQuery, SkillModel[]>
    {
        private readonly IRepository<SkillEntity> skillRepository;
        private readonly IEntityMapper<SkillEntity, SkillModel> skillMapper;

        public GetAllSkillsQueryHandler(
            IRepository<SkillEntity> skillRepository,
            IEntityMapper<SkillEntity, SkillModel> skillMapper)
        {
            this.skillRepository = skillRepository;
            this.skillMapper = skillMapper;
        }

        public SkillModel[] Handle(GetAllSkillsQuery query)
        {
            var skills =
                from skill in this.skillRepository.Entities.ToArray()
                orderby skill.Name
                select this.skillMapper.Map(skill);

            return skills.ToArray();
        }
    }
}
