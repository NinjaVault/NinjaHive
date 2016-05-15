using NinjaHive.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public class TierModel: IModel, IValidatable
    {
        public TierModel()
        {
            this.ValidationResults = new Collection<ValidationResult>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Display(Name = "Tier Rank")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Tier { get; set; }

        [RequiredForValidation]
        public Guid EquipmentItemId { get; set; }
        [RequiredForValidation]
        public string EquipmentItemName { get; set; }

        public ICollection<ValidationResult> ValidationResults { get; }
    }
}
