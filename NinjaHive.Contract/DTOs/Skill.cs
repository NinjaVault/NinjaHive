using System;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract.DTOs
{
    public class Skill
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
