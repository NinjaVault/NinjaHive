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
            Mapper.CreateMap<SubCategoryEntity, CategoryModel>();

            Mapper.CreateMap<EquipmentItemEntity, EquipmentModel>();
            Mapper.CreateMap<OtherItemEntity, OtherItemModel>();
            Mapper.CreateMap<SkillItemEntity, SkillItemModel>();

            Mapper.CreateMap<TierEntity, TierModel>();
            Mapper.CreateMap<SkillEntity, SkillModel>();
            Mapper.CreateMap<StatInfoEntity, StatInfoModel>();
        }
    };
}
