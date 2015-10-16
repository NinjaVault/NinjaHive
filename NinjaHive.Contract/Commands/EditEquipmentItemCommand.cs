using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class EditEquipmentItemCommand
    {
        public EditEquipmentItemCommand(EquipmentItem equipmentItem, bool createNew)
        {
            this.EquipmentItem = equipmentItem;
            this.CreateNew = createNew;
        }

        [Required]
        [ValidateObject]
        public EquipmentItem EquipmentItem { get; set; }

        public bool CreateNew { get; set; }
    }
}
