using NinjaHive.Components.Enums;

namespace NinjaHive.Contract.Models
{
    public class EquipmentItemModel: GameItemModel
    {
        public EquipmentItemModel()
        {
            this.Durability = 0;
            this.BodySlot = BodySlot.Head;
            this.NumberOfSlots = 1;
        }

        public int Durability { get; set; }
        public BodySlot BodySlot { get; set; }
        public int NumberOfSlots {get; set;}
    }
}
