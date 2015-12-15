using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.DTOs
{
    public class Skill
    {
        [NonEmptyGuid]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int Range { get; set; }
        public int Radius { get; set; }
        public int Target { get; set; }
        public int TargetCount { get; set; }
        public bool Friendly { get; set; }

        public Guid StatInfoId { get; set; }
    }
}