using NinjaHive.Components.Enums;

namespace NinjaHive.Contract.Models
{
    public class EquipmentModel: GameItemModel
    {
        public EquipmentModel()
        {
            this.Durability = 0;
            this.Slot = BodySlot.Head;
        }

        public int Durability { get; set; }
        public BodySlot Slot { get; set; }
    }
}
