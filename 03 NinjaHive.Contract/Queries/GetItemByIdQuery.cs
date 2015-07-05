using System;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetItemByIdQuery : IQuery<ItemDto>
    {
        public GetItemByIdQuery(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; set; }
    }
}
