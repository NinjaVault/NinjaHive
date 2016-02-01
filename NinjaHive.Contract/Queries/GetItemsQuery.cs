using NinjaHive.Contract.Models;
using NinjaHive.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaHive.Contract.Queries
{
    public class GetItemsQuery: IQuery<GameItemModel[]>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string SubCategory { get; set; }
        public string MainCategory { get; set; }
    }
}
