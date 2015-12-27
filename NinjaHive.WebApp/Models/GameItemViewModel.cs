using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    public class GameItemViewModel
    {
        public GameItemModel GameItem { get; set; }

        public IReadOnlyCollection<CategoryModel> categories;

        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(this.categories ?? new List<CategoryModel>(), "Id", "Name"); }
        }
    }
}