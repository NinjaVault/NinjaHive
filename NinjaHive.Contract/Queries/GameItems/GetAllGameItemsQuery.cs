using NinjaHive.Core;
using NinjaHive.Contract.Models;

namespace NinjaHive.Contract.Queries.GameItems
{
    public class GetAllGameItemsQuery<TItem> : IQuery<TItem[]>
        where TItem : GameItemModel
    {
    }
}
