using System;
using System.ComponentModel.DataAnnotations;

namespace NinjaHive.Contract.DTOs
{
    public class Skill
    {
        [Required]
        public Guid Id                  { get; set; }
        [Required]
        public string Name              { get; set; }
        [Required]
        public int Range                { get; set; }
        [Required]
        public int Radius               { get; set; }
        [Required]
        public int target               { get; set; }
        [Required]
        public int targetCount          { get; set; }
        [Required]
        public bool friendly            { get; set; }
    }
}
