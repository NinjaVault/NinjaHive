using NinjaHive.Contract.DTOs;

namespace NinjaHive.Contract.Commands
{
    public class DeleteItemCommand
    {
        public DeleteItemCommand(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
