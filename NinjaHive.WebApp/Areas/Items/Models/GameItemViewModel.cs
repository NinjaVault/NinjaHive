using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class GameItemViewModel : IGameItemViewModel
    {
        public GameItemModel gameItem;

        public IEnumerable<MainCategoryModel> mainCategories;
        public IEnumerable<SubCategoryModel> categories;

        public GameItemModel BaseGameItem { get { return gameItem; } }

        public IEnumerable<SelectListItem> MainCategories
        {
            get { return new SelectList(this.mainCategories ?? new List<MainCategoryModel>(), "Id", "Name"); }
        }

        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(this.categories ?? new List<SubCategoryModel>(), "Id", "Name"); }
        }
    }
}