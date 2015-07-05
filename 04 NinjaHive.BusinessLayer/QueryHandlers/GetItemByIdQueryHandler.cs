using System;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
    {
        public ItemDto Handle(GetItemByIdQuery query)
        {
            throw new NotImplementedException(); //TODO: implement
        }
    }
}
