using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class EquipmentViewModel: IGameItemViewModel
    {
        public EquipmentModel equipment;

        public IEnumerable<MainCategoryModel> mainCategories;
        public IEnumerable<SubCategoryModel> categories;

        // Implement the equipment property solely with a getter for consistency's sake
        public EquipmentModel Equipment { get { return equipment; } }

        public GameItemModel BaseGameItem { get { return equipment; } }

        // Get around EditorFor anti-recursion
        public IGameItemViewModel GameItemViewModel { get { return this; } }

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