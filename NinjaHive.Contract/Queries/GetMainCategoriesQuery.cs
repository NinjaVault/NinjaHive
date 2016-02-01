using System;
using NinjaHive.Contract.Models;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetMainCategoriesQuery: IQuery<MainCategoryModel[]>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HasSubCategory { get; set; }
        public int Count { get; set; }
        public bool RequireSubCategories { get; set; }
    }
}
