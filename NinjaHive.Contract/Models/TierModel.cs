using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract.Models
{
    public class TierModel: IModel
    {
        public TierModel()
        {
            StatInfo = new StatInfoModel();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int TierRank { get; set; }

        [Display(Name="Stats")]
        [ValidateObject]
        public StatInfoModel StatInfo { get; set; }
    }
}
