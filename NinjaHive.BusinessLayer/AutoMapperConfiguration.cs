using AutoMapper;
using NinjaHive.Contract.DTOs;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<GameItemEntity, GameItem>();

            Mapper.CreateMap<SkillEntity, Skill>()
                  .ForMember(destination => destination.TargetCount,
                      options => options.MapFrom(source => source.Targets))
                  .ForMember(destination => destination.StatInfoId,
                      options => options.MapFrom(source => source.StatInfo.Id));

            Mapper.CreateMap<StatInfoEntity, StatInfo>();                
        }
    };
}
