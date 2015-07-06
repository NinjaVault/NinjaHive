using NinjaHive.Contract.DTOs;

namespace NinjaHive.Contract.Commands
{
    public class SaveItemCommand
    {
       public SaveItemCommand(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
