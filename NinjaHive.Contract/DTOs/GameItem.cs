using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.DTOs
{
    public class GameItem
    {
        [NonEmptyGuid]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }
    }
}