using System;
using System.ComponentModel.DataAnnotations;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.DTOs
{
    public class EquipmentItem
    {
        [NonEmptyGuid]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Craftable { get; set; }
        public bool UpgradeElement { get; set; }
        public bool CraftingElement { get; set; }
        public bool QuestItem { get; set; }
        public int Value { get; set; }
        public int Durability { get; set; }
    }
}