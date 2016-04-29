using System;

namespace NinjaHive.Contract.Models
{
    public class CategoryModel : IModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string MainCategoryName { get; set; }
    }
}
