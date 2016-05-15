using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Components.Enums;

namespace NinjaHive.Contract.Models
{
    public class EquipmentModel : GameItemModel
    {
        public EquipmentModel()
        {
            this.BodySlot = BodySlot.Hair; //default value for dropdownlist
        }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Durability { get; set; }

        public BodySlot BodySlot { get; set; }

        public IEnumerable<TierModel> Tiers { get; set; }
    }
}
