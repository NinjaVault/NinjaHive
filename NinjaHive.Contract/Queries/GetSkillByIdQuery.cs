using System;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core;


namespace NinjaHive.Contract.Queries
{
    public class GetSkillByIdQuery : IQuery<Skill>
    {
        public Guid SkillId { get; set; }
    }
}
