using System;

namespace NinjaHive.Contract.DTOs
{
    public class ItemDto
    {
        public ItemDto(Guid ItemId)
        {
            this.ItemId = ItemId;
        }

        public Guid ItemId { get; set; }
    }
}
