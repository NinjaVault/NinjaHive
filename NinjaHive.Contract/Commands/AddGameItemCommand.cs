using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class AddGameItemCommand
    {
        public AddGameItemCommand(GameItem item)
        {
            this.Item = item;
        }

        [Required]
        [ValidateObject]
        public GameItem Item { get; set; }
    }
}
