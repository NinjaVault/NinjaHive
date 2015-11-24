using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class EditStatCommand
    {
        public EditStatCommand(StatInfo stat, bool createNew)
        {
            this.Stat = stat;
            this.CreateNew = createNew;
        }

        [Required]
        [ValidateObject]
        public StatInfo Stat { get; set; }

        public bool CreateNew { get; set; }
    }
}
