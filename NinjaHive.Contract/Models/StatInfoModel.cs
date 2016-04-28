using System;

namespace NinjaHive.Contract.Models
{
    public class StatInfoModel: IModel
    {
        public Guid Id { get; set; }
        public int Health { get; set; }
        public int Magic { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Hunger { get; set; }
        public int Stamina { get; set; }
        public float Resistance { get; set; }
    }
}
