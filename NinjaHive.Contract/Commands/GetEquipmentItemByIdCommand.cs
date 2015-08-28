using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class GetEquipmentItemByIdCommand
    {
        [Required]
        [ValidateObject]
        public Guid EquipmentId;
    }
}
