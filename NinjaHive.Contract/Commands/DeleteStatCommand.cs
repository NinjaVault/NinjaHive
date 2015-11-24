using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class DeleteStatCommand
    {
        [Required]
        [ValidateObject]
        public StatInfo Stat { get; set; }
    }
}