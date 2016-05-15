using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public abstract class GameItemModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage="{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNumSpace, ErrorMessage = "Alphanumerics and spaces only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [NonEmptyGuid]
        [Display(Name = "Category")]
        public Guid SubCategoryId { get; set; }

        //conventions for automapper
        public string SubCategoryName { get; set; }
        public string SubCategoryMainCategoryName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Value { get; set; }

        [Display(Name = "Upgrade element")]
        public bool IsUpgrader { get; set; }

        [Display(Name = "Craft element")]
        public bool IsCrafter { get; set; }

        [Display(Name = "Craftable")]
        public bool Craftable { get; set; }

        [Display(Name = "Quest item")]
        public bool IsQuestItem { get; set; }
    }
}