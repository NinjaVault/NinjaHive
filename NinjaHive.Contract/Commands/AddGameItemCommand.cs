using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.Models;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class AddGameItemCommand
    {
        public AddGameItemCommand(GameItemModel item)
        {
            this.Item = item;
        }

        [Required]
        [ValidateObject]
        public GameItemModel Item { get; set; }
    }
}
