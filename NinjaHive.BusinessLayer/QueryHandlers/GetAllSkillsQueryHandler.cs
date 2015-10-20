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

        public GetAllSkillsQueryHandler(IRepository<SkillEntity> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public Skill[] Handle(GetAllSkillsQuery query)
        {
            var skills =
                from skill in this.skillsRepository.Entities
                select new Skill
                {
                    Id = skill.Id,
                    Name = skill.Name,
                };

            return skills.ToArray();
        }
    }
}
