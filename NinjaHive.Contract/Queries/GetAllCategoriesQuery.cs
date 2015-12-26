using System.Collections.ObjectModel;
using NinjaHive.Contract.Models;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetAllCategoriesQuery
        : IQuery<ReadOnlyCollection<CategoryModel>>
    {
    }
}
