using NinjaHive.Core.Validation.Attributes;

namespace NinjaHive.Contract.Models
{
    public class OtherItemModel : GameItemModel
    {
        public OtherItemModel()
        {
            StatInfo = new StatInfoModel();
        }
        public bool IsEnhancer { get; set; }

        [ValidateObject]
        public StatInfoModel StatInfo { get; set; }
    }
}
