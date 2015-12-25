using System;

namespace NinjaHive.Core
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
