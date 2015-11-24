using AutoMapper;
using NinjaHive.Contract.DTOs;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<EquipmentItemEntity, EquipmentItem>()
                  .ForMember(destination => destination.UpgradeElement,
                      options => options.MapFrom(source => source.IsUpgrader))
                  .ForMember(destination => destination.CraftingElement,
                      options => options.MapFrom(source => source.IsCrafter))
                  .ForMember(destination => destination.QuestItem,
                      options => options.MapFrom(source => source.IsQuestItem));

            Mapper.CreateMap<SkillEntity, Skill>()
                  .ForMember(destination => destination.TargetCount,
                      options => options.MapFrom(source => source.Targets));

            Mapper.CreateMap<StatInfoEntity, StatInfo>();                
        }
    };
}
