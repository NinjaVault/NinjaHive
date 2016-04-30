using NinjaHive.Components.Enums;

namespace NinjaHive.Contract.Models
{
    public class EquipmentItemModel: GameItemModel
    {
        public EquipmentitemModel()
        {
            this.Durability = 0;
            this.Slot = BodySlot.Head;
        }

        public int Durability { get; set; }
        public BodySlot Slot { get; set; }
    }
}
