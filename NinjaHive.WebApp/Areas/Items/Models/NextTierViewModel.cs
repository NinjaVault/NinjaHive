using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class NextTierViewModel
    {
        public Guid ParentTierId { get; set; }

        public int Tier { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Value { get; set; }
    }
}
