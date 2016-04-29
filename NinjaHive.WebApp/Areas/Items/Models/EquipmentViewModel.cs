using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class EquipmentViewModel : IGameItemViewModel<EquipmentModel>
    {
        private readonly IEnumerable<CategoryModel> categories;

        public EquipmentViewModel(
            EquipmentModel equipment,
            IEnumerable<CategoryModel> categories)
        {
            this.DerivedItem = equipment;
            this.categories = categories;
        }

        public GameItemModel Item => this.DerivedItem;

        public EquipmentModel DerivedItem { get; }

        public IEnumerable<SelectListItem> Categories =>
            new SelectList(this.categories, nameof(CategoryModel.Id), nameof(CategoryModel.Name), nameof(CategoryModel.MainCategoryName), 1);

        
    }
}