using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.DTOs
{
    public class StatInfo
    {
        [NonEmptyGuid]
        public Guid Id { get; set; }

        public int Health { get; set; }
        public int Magic { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Hunger { get; set; }
        public int Stamina { get; set; }
        public double Resistance { get; set; }
    }
}
