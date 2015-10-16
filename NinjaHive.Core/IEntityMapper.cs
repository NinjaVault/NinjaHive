namespace NinjaHive.Core
{
    public interface IEntityMapper<TDestination> : IMapper<TDestination>
        where TDestination : class
    {
    }

    public interface IEntityMapper<TSource, TDestination> : IMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
    }
}
