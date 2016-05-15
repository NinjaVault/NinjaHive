using NinjaHive.Contract.Models;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.Extensions
{
    public static class DomainExtensions
    {
        public static void UpdateStatsEntityFromModel(this StatInfoEntity entity, StatInfoModel model)
        {
            entity.Agility = model.Agility;
            entity.Attack = model.Attack;
            entity.Defense = model.Defense;
            entity.Health = model.Health;
            entity.Hunger = model.Hunger;
            entity.Intelligence = model.Intelligence;
            entity.Magic = model.Magic;
            entity.Stamina = model.Stamina;
            entity.Resistance = model.Resistance;
        }
    }
}
