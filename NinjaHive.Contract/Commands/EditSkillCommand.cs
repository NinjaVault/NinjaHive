using System.ComponentModel.DataAnnotations;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core.Validations;

namespace NinjaHive.Contract.Commands
{
    public class EditSkillCommand
    {
        public EditSkillCommand(Skill skill, bool createNew)
        {
            this.Skill = skill;
            this.CreateNew = createNew;
        }

        [Required]
        [ValidateObject]
        public Skill Skill { get; set; }

        public bool CreateNew { get; set; }
    }
}
