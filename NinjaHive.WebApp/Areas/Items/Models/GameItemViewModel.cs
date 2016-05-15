using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class GameItemViewModel<TItem> : IGameItemViewModel
        where TItem : GameItemModel
    {
        public IEnumerable<CategoryModel> CategoriesList { private get; set; }

        public IEnumerable<SelectListItem> Categories => 
            new SelectList(this.CategoriesList, nameof(CategoryModel.Id), nameof(CategoryModel.Name),
                nameof(CategoryModel.MainCategoryName), selectedValue: 1);

        public TItem Item { get; set; }

        GameItemModel IGameItemViewModel.Item => this.Item;
    }
}