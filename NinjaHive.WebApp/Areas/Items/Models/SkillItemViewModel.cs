using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class SkillItemViewModel : GameItemViewModel<SkillItemModel>
    {
        public IEnumerable<SkillModel> SkillsList { private get; set; }

        public IEnumerable<SelectListItem> Skills =>
            new SelectList(this.SkillsList, nameof(SkillModel.Id), nameof(SkillModel.Name), selectedValue: 1);
    }
}