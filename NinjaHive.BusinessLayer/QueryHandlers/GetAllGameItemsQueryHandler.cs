using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllGameItemsQueryHandler
        : IQueryHandler<GetAllGameItemsQuery, GameItem[]>
    {
        private readonly IRepository<GameItemEntity> itemRepository;
        private readonly IEntityMapper<GameItemEntity, GameItem> itemMapper;

        public GetAllGameItemsQueryHandler(
            IRepository<GameItemEntity> itemRepository,
            IEntityMapper<GameItemEntity, GameItem> itemMapper)
        {
            this.itemMapper = itemMapper;
            this.itemRepository = itemRepository;
        }

        public GameItem[] Handle(GetAllGameItemsQuery query)
        {
            var items = this.GetGameItems();

            return this.MapGameItems(items);
        }

        private IEnumerable<GameItemEntity> GetGameItems()
        {
            return this.itemRepository.Entities.ToArray(); //load into memory here
        }

        private GameItem[] MapGameItems(IEnumerable<GameItemEntity> equipmentItems)
        {
            var items =
                from item in equipmentItems
                select this.itemMapper.Map(item);

            return items.ToArray();
        }
    }
}
