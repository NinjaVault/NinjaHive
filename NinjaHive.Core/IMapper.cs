using System;

namespace NinjaHive.Core
{
    public interface IMapper<TDestination>
    {
        Type SourceType { get; }

        TDestination Map(object source);
    }

    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}