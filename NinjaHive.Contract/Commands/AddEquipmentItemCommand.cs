using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class AddEquipmentItemCommand
    {
        [Required]
        [ValidateObject]
        public EquipmentItem EquipmentItem { get; set; }
    }
}
