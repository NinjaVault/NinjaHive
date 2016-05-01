using System;

namespace NinjaHive.Contract.Models
{
    public class SpecialModel: IModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
