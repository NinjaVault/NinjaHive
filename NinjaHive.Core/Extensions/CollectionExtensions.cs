using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NinjaHive.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> list)
        {
            var localList = new List<T>(list);
            return new ReadOnlyCollection<T>(localList);
        }
    }
}
