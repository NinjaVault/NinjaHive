using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    public class GameItemViewModel
    {
        public GameItemModel GameItem { get; set; }

        public IEnumerable<SubCategoryModel> categories;

        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(this.categories ?? new List<SubCategoryModel>(), "Id", "Name"); }
        }
    }
}