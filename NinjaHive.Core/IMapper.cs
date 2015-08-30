namespace NinjaHive.Core
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}