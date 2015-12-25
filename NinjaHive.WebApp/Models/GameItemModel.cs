using System.ComponentModel.DataAnnotations;
using NinjaHive.WebApp.Helpes;

namespace NinjaHive.WebApp.Models
{
    public class GameItemModel
    {
        [Required(ErrorMessage="{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        [RegularExpression(RegEx.AlphaNum, ErrorMessage = "Alphanumerics only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "{0}, minimum: {2}, maximum: {1}")]
        public string Description { get; set; }
    }
}