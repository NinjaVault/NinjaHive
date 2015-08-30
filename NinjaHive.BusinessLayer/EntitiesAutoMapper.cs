using AutoMapper;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer
{
    public class EntitiesAutoMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public TDestination Map(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
