using System;
using System.Linq;
using AutoMapper;
using NinjaHive.Core;

namespace NinjaHive.BusinessLayer.Services
{
    public class EntitiesAutoMapper<TDestination> : IEntityMapper<TDestination>
        where TDestination : class
    {
        public EntitiesAutoMapper()
        {
            var sourceType =
                from typeMap in Mapper.GetAllTypeMaps()
                where typeMap.DestinationType == typeof (TDestination)
                select typeMap.SourceType;

            this.SourceType = sourceType.Single();
        }

        public Type SourceType { get; private set; }

        public TDestination Map(object source)
        {
            return (TDestination) Mapper.Map(source, this.SourceType, typeof (TDestination));
        }
    }

    public class EntitiesAutoMapper<TSource, TDestination> : IEntityMapper<TSource, TDestination>
        where TSource : class 
        where TDestination : class
    {
        public EntitiesAutoMapper()
        {
            if (typeof(TSource).IsArray || typeof(TDestination).IsArray)
            {
                throw new InvalidOperationException("AutoMapper is incompatible with arrays as either Source or Destination.");
            }
        }

        public TDestination Map(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
