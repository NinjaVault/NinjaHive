using System;
using NinjaHive.Contract.Models;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetSubCategoriesQuery : IQuery<SubCategoryModel[]>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public Guid ParentId { get; set; }
    }
}