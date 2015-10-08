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
                  .ForMember(destination => destination.friendly,
                      options => options.MapFrom(source => source.Friendly))
                  .ForMember(destination => destination.Name,
                      options => options.MapFrom(source => source.Name))
                  .ForMember(destination => destination.Radius,
                      options => options.MapFrom(source => source.Radius))
                  .ForMember(destination => destination.Radius,
                      options => options.MapFrom(source => source.Radius))
                  .ForMember(destination => destination.Range,
                      options => options.MapFrom(source => source.Range))
                  .ForMember(destination => destination.target,
                      options => options.MapFrom(source => source.Target))
                  .ForMember(destination => destination.targetCount,
                      options => options.MapFrom(source => source.Targets));
        }
    };
}
