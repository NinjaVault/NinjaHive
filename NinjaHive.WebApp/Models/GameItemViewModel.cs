using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    [ValidateInput(false)]
    public class GameItemViewModel
    {
        public GameItemModel GameItem { get; set; }

        public IEnumerable<MainCategoryModel> mainCategories;
        public IEnumerable<SubCategoryModel> categories;

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