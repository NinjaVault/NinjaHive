using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Models
{
    public class TierModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Tier { get; set; }

        [NonEmptyGuid]
        [Display(Name = "Equipment")]
        public Guid EquipmentId { get; set; }
    }
}
