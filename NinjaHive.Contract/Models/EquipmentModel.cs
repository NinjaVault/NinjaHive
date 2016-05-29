using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Components.Enums;

namespace NinjaHive.Contract.Models
{
    public class EquipmentModel : GameItemModel
    {
        public EquipmentModel()
        {
            this.Tier = 1; //default value for new equipment items
            this.BodySlot = BodySlot.Hair; //default value for dropdownlist
        }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer for {0}. Minimum: {1}")]
        public int Durability { get; set; }

        [Display(Name = "Body slot")]
        public BodySlot BodySlot { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimum {0} rank is {1}.")]
        public int Tier { get; set; }

        public Guid ParentTierId { get; set; }
        public Guid NextTierId { get; set; }
        public bool HasNextTier => this.NextTierId != Guid.Empty;
    }
}
