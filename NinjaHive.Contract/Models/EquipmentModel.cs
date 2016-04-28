using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Models
{
    public class EquipmentModel : IModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Durability { get; set; }


        // Slot: enum? 
    }
}