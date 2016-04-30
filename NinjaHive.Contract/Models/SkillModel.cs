using System;
using NinjaHive.Components.Enums;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract.Models
{
    public class SkillModel : IModel
    {
        public SkillModel()
        {
            StatInfo = new StatInfoModel();
            Special = new SpecialModel();
            this.Target = Target.Ground;
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Range { get; set; }

        [Range(0, int.MaxValue)]
        public int Radius { get; set; }

        public int Targets { get; set; }

        public Target Target { get; set; }

        public bool Friendly { get; set; }

        public StatInfoModel StatInfo { get; set; }

        public SpecialModel Special { get; set; }
    }
}