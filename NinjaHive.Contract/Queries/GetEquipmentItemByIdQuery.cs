using System;
using NinjaHive.Contract.DTOs;
using NinjaHive.Core;

namespace NinjaHive.Contract.Queries
{
    public class GetEquipmentItemByIdQuery : IQuery<EquipmentItem>
    {
        public Guid EquipmentItemId { get; set; }
    }
}
