using NinjaHive.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class OtherItemViewModel : IGameItemViewModel<OtherItemModel>
    {
        private readonly IEnumerable<CategoryModel> categories;

        public OtherItemViewModel(
            OtherItemModel item,
            IEnumerable<CategoryModel> categories)
        {
            this.DerivedItem = item;
            this.categories = categories;
        }

        public OtherItemModel DerivedItem { get; }

        public GameItemModel Item => this.DerivedItem;

        public IEnumerable<SelectListItem> Categories =>
            new SelectList(this.categories, nameof(CategoryModel.Id), nameof(CategoryModel.Name), nameof(CategoryModel.MainCategoryName), 1);
    }
}