using System;

namespace NinjaHive.Contract.Models
{
    public class LevelModel : IModel
    {
        public Guid Id { get; set; }

        public Guid Parent { get; set; }
        public Guid Child { get; set; }
    }
}
