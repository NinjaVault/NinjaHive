using AutoMapper;
using NinjaHive.Contract.Models;
using NinjaHive.Domain;

namespace NinjaHive.BusinessLayer
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<MainCategoryEntity, MainCategoryModel>();
            Mapper.CreateMap<SubCategoryEntity, SubCategoryModel>();
            Mapper.CreateMap<SkillEntity, SkillModel>();
            Mapper.CreateMap<StatInfoEntity, StatInfoModel>();
            Mapper.CreateMap<SpecialEntity, SpecialModel>();
            Mapper.CreateMap<EquipmentItemEntity, EquipmentModel>();
            Mapper.CreateMap<OtherItemEntity, OtherItemModel>();
            Mapper.CreateMap<TierEntity, TierModel>().
                ForMember(dest => dest.TierRank, opts => opts.MapFrom(src => src.Tier));
        }
    };
}
