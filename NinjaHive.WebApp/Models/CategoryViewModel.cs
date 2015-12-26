using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    public class CategoryViewModel
    {
        private readonly IReadOnlyCollection<CategoryModel> categories;

        public CategoryViewModel(IReadOnlyCollection<CategoryModel> categories)
        {
            this.categories = categories;
        }

        [Display(Name = "Category")]
        public Guid SelectedCategoryId { get; set; }
        
        public IEnumerable<SelectListItem> Categories
        {
            get { return new SelectList(this.categories, "Id", "Name");}
        }
    }
}