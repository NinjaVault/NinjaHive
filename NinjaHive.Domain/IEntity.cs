using System;

namespace NinjaHive.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
        EditInfo EditInfo { get; set; }
    }
}
