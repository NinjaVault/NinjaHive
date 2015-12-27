using System.Linq;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.Domain;
using NinjaHive.Domain.Extensions;

namespace NinjaHive.BusinessLayer.QueryHandlers
{
    public class GetLinkedGameItemNamesQueryHandler
        : IQueryHandler<GetLinkedGameItemNamesQuery, string[]>
    {
        private readonly IRepository<CategoryEntity> repository;

        public GetLinkedGameItemNamesQueryHandler(IRepository<CategoryEntity> repository)
        {
            this.repository = repository;
        }

        public string[] Handle(GetLinkedGameItemNamesQuery query)
        {
            return this.repository
                       .FindById(query.CategoryId)
                       .GameItems
                       .Select(i => i.Name)
                       .ToArray();
        }
    }
}
