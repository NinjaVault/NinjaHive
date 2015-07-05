using System;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly NinjaHiveEntities dbContext;

        public GetItemByIdQueryHandler(NinjaHiveEntities dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public ItemDto Handle(GetItemByIdQuery query)
        {
            var item = dbContext.Items.Find(query.ItemId);

            if (item != null)
            {
                return new ItemDto
                {
                    ItemId = item.Id,
                    Name = item.Name,                   
                };
            }

            return null;
        }
    }
}
