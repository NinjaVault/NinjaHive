using System.Collections.Generic;
using System.Linq;
using NinjaHive.Contract.Models;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetAllGameItemsQueryHandler
        : IQueryHandler<GetAllGameItemsQuery, GameItemModel[]>
    {
        private readonly IRepository<GameItemEntity> itemRepository;
        private readonly IEntityMapper<GameItemEntity, GameItemModel> itemMapper;

        public GetAllGameItemsQueryHandler(
            IRepository<GameItemEntity> itemRepository,
            IEntityMapper<GameItemEntity, GameItemModel> itemMapper)
        {
            this.itemMapper = itemMapper;
            this.itemRepository = itemRepository;
        }

        public GameItemModel[] Handle(GetAllGameItemsQuery query)
        {
            var items = this.GetGameItems();

            return this.MapGameItems(items);
        }

        private IEnumerable<GameItemEntity> GetGameItems()
        {
            return this.itemRepository.Entities.ToArray(); //load into memory here
        }

        private GameItemModel[] MapGameItems(IEnumerable<GameItemEntity> equipmentItems)
        {
            var items =
                from item in equipmentItems
                orderby item.SubCategory.MainCategory.Name, item.SubCategory.Name, item.Name
                select this.itemMapper.Map(item);

            return items.ToArray();
        }
    }
}
