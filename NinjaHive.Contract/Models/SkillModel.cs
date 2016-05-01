using System;
using NinjaHive.Components.Enums;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;

namespace NinjaHive.Contract.Models
{
    public class SkillModel : IModel
    {
        public SkillModel()
        {
            StatInfo = new StatInfoModel();           
            this.Target = Target.Ground;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Range { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Radius { get; set; }

        [Display(Name="Number of Targets")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Targets { get; set; }

        [Display(Name="Target Type")]
        public Target Target { get; set; }

        public bool Friendly { get; set; }

        public StatInfoModel StatInfo { get; set; }
        public SpecialModel[] Specials { get; set; }
    }
}