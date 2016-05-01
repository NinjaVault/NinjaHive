using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public class SubCategoryModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumUnderscoreSpace, ErrorMessage = "a-z 0-9 spaces underscores")]
        public string Name { get; set; }

        [NonEmptyGuid]
        public Guid MainCategoryId { get; set; }
    }
}
