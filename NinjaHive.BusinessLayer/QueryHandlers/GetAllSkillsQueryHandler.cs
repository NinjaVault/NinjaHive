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
        private readonly IRepository<SkillEntity> skillsRepository;
        private readonly IEntityMapper<SkillEntity, Skill> skillMapper; 

        public GetAllSkillsQueryHandler(
            IRepository<SkillEntity> skillsRepository,
            IEntityMapper<SkillEntity, Skill> skillMapper)
        {
            this.skillsRepository = skillsRepository;
            this.skillMapper = skillMapper;
        }

        public Skill[] Handle(GetAllSkillsQuery query)
        {
            var skills =
                from skill in this.skillsRepository.Entities
                //select this.skillMapper.Map(skill);
                select new Skill ()
                {
                    StatInfoId = skill.StatInfo.Id,
                    Friendly = skill.Friendly,
                    Name = skill.Name,
                    Radius = skill.Radius,
                    Range = skill.Range,
                    Target = (int)skill.Target,
                    TargetCount = skill.Targets,
                    Id = skill.Id
                };
           

            return skills.ToArray();
        }
    }
}
