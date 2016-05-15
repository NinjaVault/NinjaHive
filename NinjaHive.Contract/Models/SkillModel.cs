using System;
using NinjaHive.Components.Enums;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public class SkillModel : IModel
    {
        public SkillModel()
        {
            this.Target = Target.Ground; //default value for dropdownlist
            this.StatInfo = new StatInfoModel();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Range { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Radius { get; set; }

        [Display(Name="Number of Targets")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Targets { get; set; }

        [Display(Name="Target Type")]
        public Target Target { get; set; }

        public bool Friendly { get; set; }

        [ValidateObject]
        public StatInfoModel StatInfo { get; set; }
    }
}