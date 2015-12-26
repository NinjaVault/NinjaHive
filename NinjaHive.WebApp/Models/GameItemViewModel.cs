using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    public class GameItemViewModel
    {
        public GameItemViewModel(
            GameItemModel gameItem,
            CategoryViewModel category)
        {
            this.GameItem = gameItem;
            this.Category = category;
        }

        public GameItemModel GameItem { get; private set; }
        public CategoryViewModel Category { get; private set; }

        public void UpdateCategory()
        {
            this.GameItem.CategoryId = this.Category.SelectedCategoryId;
        }
    }
}