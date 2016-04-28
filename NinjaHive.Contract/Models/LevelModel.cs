using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Helpers;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Models
{
    public class LevelModel : IModel
    {
        public Guid Id { get; set; }
    }
}
