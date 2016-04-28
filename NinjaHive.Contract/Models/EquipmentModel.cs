using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaHive.Contract.Models
{
    public enum EquipmentSlot
    {
        Head, Neck, Torso, Arms, LeftHand, RightHand, Legs, Feet
    }

    public class EquipmentModel: GameItemModel, IModel
    {
        public int Durability { get; set; }
        public EquipmentSlot Slot { get; set; }

        public EquipmentModel() { Durability = 0; Slot = 0; }
    }
}
