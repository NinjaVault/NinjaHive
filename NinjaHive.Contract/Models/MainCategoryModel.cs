using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;

namespace NinjaHive.Contract.Models
{
    public class MainCategoryModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumUnderscoreSpace, ErrorMessage = "a-z 0-9 spaces underscores")]
        [Display(Name = "Main Category")]
        public string Name { get; set; }
        
        public IEnumerable<SubCategoryModel> SubCategories { get; set; } 
    }
}
