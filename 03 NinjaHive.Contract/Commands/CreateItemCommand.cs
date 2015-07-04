using NinjaHive.Contract.DTOs;

namespace NinjaHive.Contract.Commands
{
    public class CreateItemCommand
    {
        public CreateItemCommand(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
