using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries.Skills;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers.Skills
{
    public class GetAllSkillsQueryHandler
        : IQueryHandler<GetAllSkillsQuery, SkillModel[]>
    {
        private readonly IRepository<SkillEntity> repository;
        private readonly IEntityMapper<SkillEntity, SkillModel> skillMapper;

        public GetAllSkillsQueryHandler(
            IRepository<SkillEntity> repository,
            IEntityMapper<SkillEntity, SkillModel> skillMapper)
        {
            this.repository = repository;
            this.skillMapper = skillMapper;
        }

        public SkillModel[] Handle(GetAllSkillsQuery query)
        {
            return this.GetSkills();
        }

        private SkillModel[] GetSkills()
        {
            // mapper trhows exceptions so I assigned the values manually
            var skills =
                from skill in this.repository.Entities.ToArray() //load into memory
                orderby skill.Name
                select this.skillMapper.Map(skill);

            return skills.ToArray();
        }
    }
}