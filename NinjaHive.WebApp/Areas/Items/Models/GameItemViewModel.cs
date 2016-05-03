using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public class GameItemViewModel<DType> : IGameItemViewModel
        where DType : GameItemModel
    {

        public IEnumerable<CategoryModel> CategoriesList { private get; set; }

        public IEnumerable<SelectListItem> Categories
        {
            get
            {
                if (CategoriesList == null)
                {
                    throw new NullReferenceException(
                        $"GameItemViewModel CategoriesList was not defined for '{this}'.");
                }
                    
                return new SelectList(CategoriesList,
                    nameof(CategoryModel.Id), nameof(CategoryModel.Name), nameof(CategoryModel.MainCategoryName),
                    selectedValue: 1);
            }
        }

        public DType Item { get; set; }

        GameItemModel IGameItemViewModel.Item => this.Item;
    }
}