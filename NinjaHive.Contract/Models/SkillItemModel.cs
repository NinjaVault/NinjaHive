using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public class SkillItemModel: GameItemModel
    {
        [NonEmptyGuid]
        [Display(Name = "Skill")]
        public Guid SkillId { get; set; }
    }
}
