using NinjaHive.Core;
using NinjaHive.Contract.Models;

namespace NinjaHive.Contract.Queries.GameItems
{
    public class GetAllItemsQuery<TItem> : IQuery<TItem[]>
        where TItem : GameItemModel
    {
    }
}
