using System;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.WebApp.Models.Skills
{
    public class SkillViewModel
    {
        public Guid         SkillId { get; set; }
        public string       SkillName { get; set; }
        public int          SkillRange { get; set; }
        public int          SkillRadius { get; set; }
        public int          SkillTarget { get; set; }
        public int          SkillTargetCount { get; set; }
        public bool         SkillFriendly { get; set; }

        public Guid         StatId { get; set; }
        public int          StatHealth { get; set; }
        public int          StatMagic { get; set; }
        public int          StatAttack { get; set; }
        public int          StatDefense { get; set; }
        public int          StatAgility { get; set; }
        public int          StatIntelligence { get; set; }
        public int          StatHunger { get; set; }
        public int          StatStamina { get; set; }
        public double       StatResistance { get; set; }
    }
}