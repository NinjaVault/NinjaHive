namespace NinjaHive.Contract.Models
{
    public class EquipmentModel: GameItemModel
    {
        public EquipmentModel()
        {
            this.Durability = 0;
            this.Slot = EquipmentSlot.Head;
        }

        public int Durability { get; set; }
        public EquipmentSlot Slot { get; set; }
    }
}
