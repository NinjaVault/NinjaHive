using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllItemsQueryHandler : IQueryHandler<GetAllItemsQuery, ItemDto[]>
    {
        private readonly NinjaHiveEntities dbContext;

        public GetAllItemsQueryHandler(NinjaHiveEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public ItemDto[] Handle(GetAllItemsQuery query)
        {
            var items =
                from item in this.dbContext.Items
                select new ItemDto
                {
                    ItemId = item.Id,
                    Name = item.Name,
                };

            return items.ToArray();
        }
    }
}
