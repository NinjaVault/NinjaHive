using System;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetLinkedGameItemNamesQuery : IQuery<string[]>
    {
        public GetLinkedGameItemNamesQuery(Guid categoryId)
        {
            this.CategoryId = categoryId;
        }

        public Guid CategoryId { get; private set; }
    }
}
