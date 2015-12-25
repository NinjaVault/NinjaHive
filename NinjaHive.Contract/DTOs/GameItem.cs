using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract.DTOs
{
    public class GameItem
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}