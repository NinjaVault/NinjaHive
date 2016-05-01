using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class SkillItemViewModel : IGameItemViewModel<SkillItemModel>
    {

        public SkillItemModel DerivedItem { get; set; }

        public GameItemModel Item => this.DerivedItem;

        public IEnumerable<SelectListItem> Categories =>
            new SelectList(this.CategoryList, nameof(CategoryModel.Id), nameof(CategoryModel.Name), nameof(CategoryModel.MainCategoryName), 1);

        public IEnumerable<CategoryModel> CategoryList { get; set; }
    }
}